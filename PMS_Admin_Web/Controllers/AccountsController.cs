using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models;
using Dapper;
using PMS_Admin_Web.Models.Accounts;
using Microsoft.AspNetCore.Http;
using PMS_Admin_Web.Repository;
using Microsoft.Office.Interop;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace PMS_Admin_Web.Controllers
{
    public class AccountsController : Controller
    {
        private Connection sqlConnectionString = new();

        public IActionResult Accounting()
        {
            return View();
        }

        public IActionResult TemporaryReceipt()
        {
            return View();
        }

        public IActionResult TemporaryReceiptCreation()
        {
            TemporaryReceiptCreationModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.tomailidlist = connection.Query<string>($"select mailid from emailusers where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and mailstatus='to' order by id").ToList();
                model.ccmailidlist = connection.Query<string>($"select mailid from emailusers where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and mailstatus='cc' order by id").ToList();

                foreach(var item in model.tomailidlist)
                {
                    if(model.tomailid == null)
                    {
                        model.tomailid = item;
                    }
                    else
                    {
                        model.tomailid = model.tomailid + ";" + item;
                    }
                }

                foreach (var item in model.ccmailidlist)
                {
                    if (model.ccmailid == null)
                    {
                        model.ccmailid = item;
                    }
                    else
                    {
                        model.ccmailid = model.ccmailid + ";" + item;
                    }
                }

                connection.Close();
            }
            return View(model);
        }

        public async Task<string> CreateTemporaryReceipt(string voucherno,string multiplepayment,int rowslength,string loino,string inqno,string loino1,string[] arrayVouchertype,string[] arrayTotalamt,string[] arrayCash,string[] arrayKnet,string[] arrayCheque,string []arrayBt,string other,string amounttype,string totalamount,string vouchermadeon,string remarks,string fromdate, string todate,string cashtype,string tenantname,string propertyname,string aptno,string cash,string knet,string cheque,string bt,string tomail,string ccmail)
        {
            string message="";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if(voucherno != "-")
                {
                    var recordcount = connection.Query<int>($"select count(*) from paymentvoucher where voucherno='{voucherno}'").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        message = "This voucher is already there";
                        goto gotoreturn;
                    }
                }

                string totalamountinwords = "";
                if (multiplepayment == "true")
                {
                    int grandtotal = 0;
                    if (rowslength == 0)
                    {
                        message = "Add the enteries";
                        goto gotoreturn;
                    }

                    for(int i = 0; i < rowslength; i++)
                    {
                        if(grandtotal == 0)
                        {
                            grandtotal = Convert.ToInt32(arrayTotalamt[i]);
                        }
                        else
                        {
                            grandtotal = grandtotal + Convert.ToInt32(arrayTotalamt[i]);
                        }
                    }

                    totalamountinwords = CommonRepository.ConvertAmount(grandtotal.ToString());

                    for(int i = 0; i < rowslength; i++)
                    {
                        var insert = await connection.ExecuteAsync($"insert into paymentvoucher(loino,inqno,loi_no,lc_no,voucherno,[amt-type],amt,payment_date,comments,doc,dept,[user],datefrom,dateto,ptype,TNAME,PNAME,APTNO,CASH,KNET,CHEQUE,BT,amtinwords)values('{loino}','{inqno}','{loino1}','{loino1}','{voucherno}','{arrayVouchertype[i]}','{arrayTotalamt[i]}','{vouchermadeon}','{remarks}',getdate(),'{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','{fromdate}','{todate}','{cashtype}','{tenantname}','{propertyname}','{aptno}','{arrayCash[i]}','{arrayKnet[i]}','{arrayCheque[i]}','{arrayBt[i]}','{totalamountinwords}')");
                    }
                    message = "Voucher created";
                }
                else
                {
                    totalamountinwords = CommonRepository.ConvertAmount(totalamount);
                    if(amounttype == "OTHER")
                    {
                        var insert = await connection.ExecuteAsync($"insert into paymentvoucher(loino,inqno,loi_no,lc_no,voucherno,[amt-type],amt,payment_date,comments,doc,dept,[user],datefrom,dateto,ptype,TNAME,PNAME,APTNO,CASH,KNET,CHEQUE,BT,amtinwords)values('{loino}','{inqno}','{loino1}','{loino1}','{voucherno}','{other}','{totalamount}','{vouchermadeon}','{remarks}',getdate(),'{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','{fromdate}','{todate}','{cashtype}','{tenantname}','{propertyname}','{aptno}','{cash}','{knet}','{cheque}','{bt}','{totalamountinwords}')");
                    }
                    else
                    {
                        var insert = await connection.ExecuteAsync($"insert into paymentvoucher(loino,inqno,loi_no,lc_no,voucherno,[amt-type],amt,payment_date,comments,doc,dept,[user],datefrom,dateto,ptype,TNAME,PNAME,APTNO,CASH,KNET,CHEQUE,BT,amtinwords)values('{loino}','{inqno}','{loino1}','{loino1}','{voucherno}','{amounttype}','{totalamount}','{vouchermadeon}','{remarks}',getdate(),'{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','{fromdate}','{todate}','{cashtype}','{tenantname}','{propertyname}','{aptno}','{cash}','{knet}','{cheque}','{bt}','{totalamountinwords}')");
                    }
                    message = "Voucher created";
                }
                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public JsonResult LoadDetFromLoiInfo(string loino1)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loiinfo = connection.Query<Loiinformation>($"select dept,PROPERTYSOURCE,LLRESF,ClientRESF,lename,LoiNo,inqno,loipath,CONVERT(VARCHAR(11),leasedate,106) as leasedate,CONVERT(VARCHAR(11),Leaseenddate,106) as Leaseenddate,RENT,DEPOSIT, loi_no,clientname,aptno,propertyname from loiinformation where loi_no='{loino1}' ").FirstOrDefault();
                connection.Close();
                return Json(loiinfo);
            }
        }

        public JsonResult LoadDetFromLcInfoByLoiNo(string loino1)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from lcinfo where loi_no='{loino1}'").FirstOrDefault();
                Lcinfo lcinfo = new();
                if (reccount > 0)
                {
                    lcinfo = connection.Query<Lcinfo>($"select TENNAME, lcpath,CONVERT(VARCHAR(11),Leasestart,106) as Leasestart,CONVERT(VARCHAR(11),Leaseend,106) as Leaseend,RENT,DEPOSIT,RESF, loi_no,tenname,aptno,pname,aptno from lcinfo where loi_no='{loino1}' ").FirstOrDefault();
                    connection.Close();
                    return Json(lcinfo);
                }
                else
                {
                    connection.Close();
                    return Json(reccount);
                }
                
            }
        }

        public JsonResult LoadPayVoucherByLoiNo(string loino1)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from PAYMENTVOUCHER where loi_no='{loino1}'").FirstOrDefault();
                LoadPayVoucherByLoiNoModel model = new();
                if (reccount > 0)
                {
                    model.rentpaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{loino1}' and [amt-type]='Rent'").FirstOrDefault();
                    model.deppaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{loino1}' and [amt-type]='DEPOSIT'").FirstOrDefault();
                    model.resfpaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{loino1}' and [amt-type]='RESF'").FirstOrDefault();
                    model.LLRESFPAID = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{loino1}' and [amt-type]='RESF-LL'").FirstOrDefault();
                    connection.Close();
                    return Json(model);
                }
                else
                {
                    connection.Close();
                    return Json(reccount);
                }

            }
        }

        public string LoadVoucherNo()
        {
            string voucherno = "";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) from paymentvoucher where year(payment_date)=year(getdate()) and voucherno not in('-')").FirstOrDefault();
                if(recordcount == 0)
                {
                    voucherno = "TR" + DateTime.Now.ToString("yy") + " - " + "0001";
                }
                else
                {
                    string no = "";
                    var newno = connection.Query<int>($"select right(voucherno,4)+1 as no from paymentvoucher where id=(select max(id) from paymentvoucher where voucherno not in('-'))").FirstOrDefault();
                    if(newno <= 9)
                    {
                        no = "000" + newno;
                    }
                    else if(newno > 9 && newno <= 99)
                    {
                        no = "00" + newno;
                    }
                    else if(newno > 99 && newno <= 999)
                    {
                        no = "0" + newno;
                    }
                    voucherno = "TR" + DateTime.Now.ToString("yy") + " - " + no;
                }
                connection.Close();
            }
            return voucherno;
        }

        public JsonResult AptChangeEventTempReceipt(string aptno,string propertyname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                AptChangeEventTempReceiptNoModel model = new();
                model = connection.Query<AptChangeEventTempReceiptNoModel>($"select cname, CONVERT(VARCHAR(11),[leasestart] ,106) as leasestart,CONVERT(VARCHAR(11),[leaseend] ,106) as leaseend from propertymaster where bldgname='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
                connection.Close();
                return Json(model);
            }
        }

        public IActionResult TemporaryReceiptList()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<Paymentvoucher> model = new();
                model = connection.Query<Paymentvoucher>($"select top 100 inqno,loi_no LoiNo1,flag,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,sum(cast(amt as decimal(34,3))) AS amt ,sum(cast(cash as decimal(34,3))) AS cash ,sum(cast(knet as decimal(34,3))) AS knet ,sum(cast(cheque as decimal(34,3))) AS cheque,sum(cast(bt as decimal(34,3))) AS bt,(Select top 1  substring( ( Select ','+ST1.[amt-type] AS [text()] From dbo.paymentvoucher ST1 Where ST1.VOUCHERNO =st2. voucherno  For XML PATH ('') ), 2, 1000) [address]) as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where year(payment_date)=year(getdate()) and len(voucherno)>2 group by voucherno,payment_date,tname,PNAME ,aptno,datefrom,flag ,inqno,loi_no order by voucherno desc ").ToList();
                connection.Close();
                return View(model);
            }
        }

        public JsonResult ViewAllTemporaryReceiptList(string year)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<Paymentvoucher> model = new();
                model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,CONVERT(VARCHAR(11),datefrom,106) as datefrom,TNAME,pname,aptno ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType from paymentvoucher where year(payment_date)='{year}' order by id desc ").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult ViewMarketingResfTemporaryReceiptList(string year, string month)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<Paymentvoucher> model = new();
                model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where year(datefrom)='{year}' and month(datefrom)='{month}' and inqno not in('0000') AND [amt-type] IN ('RESF-LL', 'RESF') order by id desc ").ToList();
                var amt = connection.Query<string>($"select sum(cast(amt as decimal(34,3))) AS amt from paymentvoucher where year(datefrom)='{year}' and month(datefrom)='{month}' and inqno not in('0000') AND [amt-type] IN ('RESF-LL', 'RESF') ").FirstOrDefault();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult ViewOtherReceiptsTemporaryReceiptList(string year)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<Paymentvoucher> model = new();
                model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where year(payment_date)='{year}' and voucherno='-' order by id desc ").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult SearchTemporaryReceiptList(string categories,string searchtxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<Paymentvoucher> model = new();
                if(categories == "loi")
                {
                    model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where loi_no like '{searchtxt}%' OR loi_no LIKE '%{searchtxt}' OR loi_no LIKE '%{searchtxt}%' order by id desc ").ToList();
                }
                if (categories == "cglpgl")
                {
                    model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where INQNO like '{searchtxt}%' OR INQNO LIKE '%{searchtxt}' OR INQNO LIKE '%{searchtxt}%' order by id desc ").ToList();
                }
                if (categories == "voucherno")
                {
                    model = connection.Query<Paymentvoucher>($"select id, inqno,loi_no LoiNo1,voucherno,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,TNAME ,cast(amt as decimal(34,3)) AS amt ,cast(cash as decimal(34,3)) AS cash ,cast(knet as decimal(34,3)) AS knet ,cast(cheque as decimal(34,3)) AS cheque,cast(bt as decimal(34,3)) AS bt,[amt-type] as AmtType ,PNAME,APTNO,CONVERT(VARCHAR(11),datefrom,106) as DATEFROM from paymentvoucher st2 where voucherno like '{searchtxt}%' OR voucherno LIKE '%{searchtxt}' OR voucherno LIKE '%{searchtxt}%' order by id desc ").ToList();
                }
                connection.Close();
                return Json(model);
            }
        }

        public string DeleteTemporaryReceipt(int id,string voucherno)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (voucherno == "" || voucherno == null)
                {
                    var insert = connection.Execute($"insert into deletedtvoucher select * from paymentvoucher where id={id}");
                    var delete = connection.Execute($"delete from paymentvoucher where id={id}");
                }
                else
                {
                    var insert = connection.Execute($"insert into deletedtvoucher select * from paymentvoucher where voucherno='{voucherno}'");
                    var delete = connection.Execute($"delete from paymentvoucher where voucherno='{voucherno}'");
                }
                message = "Temporary voucher is deleted successfully!";
                connection.Open();
            }
            return message;
        }

        public async Task<IActionResult> EditTemporaryReceipt(int id,string vno,string type, Paymentvoucher model)
        {
            //Paymentvoucher model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if(type != null)
                {
                    if (type == "yes")
                    {
                        model = connection.Query<Paymentvoucher>($"select id,voucherno,comments,pname,aptno,tname,[amt-type] as at, cast(amt as decimal(34,3)) AS amt,cast(cash as decimal(34,3)) as cash, cast(knet as decimal(34,3)) as knet, cast(cheque as decimal(34,3)) AS cheque,cast(BT as decimal(34,3)) AS bt,lc_no,CONVERT(VARCHAR(11),datefrom,106) as datefrom,CONVERT(VARCHAR(11),dateto,106) as dateto,comments from paymentvoucher where voucherno='{vno}' and id={id}").FirstOrDefault();
                    }
                    if (type == "no")
                    {
                        model = connection.Query<Paymentvoucher>($"select id,voucherno,comments,pname,aptno,tname,[amt-type] as at, cast(amt as decimal(34,3)) AS amt,cast(cash as decimal(34,3))   as cash, cast(knet as decimal(34,3))   as knet, cast(cheque as decimal(34,3))  AS  cheque,cast(BT as decimal(34,3))  AS bt,lc_no,CONVERT(VARCHAR(11),datefrom,106) as datefrom,CONVERT(VARCHAR(11),dateto,106) as dateto,comments from paymentvoucher where voucherno='{vno}' ").FirstOrDefault();
                    }
                    else
                    {
                        ViewBag.Message = "Refresh";
                    }    
                }
                else
                {
                    var update = await connection.ExecuteAsync($"update paymentvoucher set comments='{model.Comments}' where voucherno='{model.Voucherno}'");
                    ViewBag.Message = "Updated Successfully";
                }
                
                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> AccountsReceivable(AccountsReceivableModel model, string fromtemp, int? id,string voucherno, string bldngname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //AccountsReceivableModel model = new();
                connection.Open();
                if(voucherno != null)
                {
                    if (fromtemp == "yes")
                    {
                        var paymentvoucher = connection.Query<Paymentvoucher>($"select pname,aptno,tname,[amt-type] as AmtType,amt,cash,knet,cheque,bt,lc_no,CONVERT(VARCHAR(11),datefrom,106) as datefrom,CONVERT(VARCHAR(11),payment_date,106) as PaymentDate,CONVERT(VARCHAR(11),dateto,106) as dateto,comments from paymentvoucher where voucherno='{voucherno}' and id='{id}'").FirstOrDefault();
                        model.propertyname = paymentvoucher.Pname;
                        model.aptno = paymentvoucher.Aptno;
                        model.tname = paymentvoucher.Tname;
                        model.rentdatefrom = paymentvoucher.Datefrom;
                        model.rentdateto = paymentvoucher.Dateto;
                        model.vdate = paymentvoucher.PaymentDate;
                        model.amt = paymentvoucher.Amt.ToString();
                        model.desc = paymentvoucher.Comments;
                        model.type = paymentvoucher.AmtType;
                        model.bt = paymentvoucher.Bt.ToString();
                        model.cash = paymentvoucher.Cash.ToString();
                        model.knet = paymentvoucher.Knet.ToString();
                        model.cheque = paymentvoucher.Cheque.ToString();
                        model.fromtemp = fromtemp;

                        var accountsPM = connection.Query<AccountsPm>($"select * from accountsPM where pname='{model.propertyname}' and aptno='{model.aptno}'").FirstOrDefault();
                        if (accountsPM != null)
                        {
                            model.monthrent = accountsPM.Mrent.ToString();
                            model.pref = accountsPM.Pref.ToString();
                        }

                    }
                    else if (fromtemp == "PA")
                    {
                        var PAPAYMENTS = connection.Query<Papayment>($"select month ,year,collectionmonth,collectionyear,rno, pname,aptno,tname,PAYMENTTYPE,cast(totamt as decimal(34,3)) as totamt,cast(cash as decimal(34,3)) as cash,cast(knet as decimal(34,3)) as knet,cast(cheque as decimal(34,3)) as cheque,cast(bt as decimal(34,3)) as bt,CONVERT(VARCHAR(11),rentdatefrom,106) as datefrom,CONVERT(VARCHAR(11),rdate,106) as rdate,CONVERT(VARCHAR(11),rentdateto,106) as dateto,description  from PAPAYMENTS where ID='{id}' and PNAME='{bldngname}'").FirstOrDefault();

                    }
                    else if (fromtemp == "bulkprint")
                    {

                    }
                    else
                    {

                    }
                }
                else
                {
                    int tenantid = 0;
                    if (model.fromtemp != "bulkprint")
                    {
                        var reccount = connection.Query<int>($"select count(*) from bulkprinting where pname='{model.propertyname}' and rno='{model.vno}'").FirstOrDefault();
                        if(reccount > 0)
                        {
                            ViewBag.Message = "This Entry is already available in Bulk Print.Submit from the Bulk print";
                            goto gotoreturn;
                        }
                    }

                    if(model.rentmonth == null || model.rentmonth == "")
                    {
                        ViewBag.Message = "Select the Statement month";
                        goto gotoreturn;
                    }

                    if (model.rentyear == null || model.rentyear == "")

                    {
                        ViewBag.Message = "Select the Statement year";
                        goto gotoreturn;
                    }

                    var recordcount = connection.Query<int>($"select count(*) AS REC from PAYMENTSSTATUS where month='{model.collectionmonth}' and year='{model.collectionyear}'  and pname='{model.propertyname}' AND STATE='CLOSED'").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        ViewBag.Message("Collection month is closed");
                        goto gotoreturn;
                    }

                    string hl = "";
                    if(model.chkhighlight)
                    {
                        hl = "yes";
                    }
                    else
                    {
                        hl = "no";
                    }

                    if(!model.chkpending)
                    {
                        if(model.type == null || model.type == "")
                        {
                            ViewBag.Message("Select the the Type");
                            goto gotoreturn;
                        }

                        if (model.chkcash)
                        {
                            if(model.cash == null)
                            {
                                ViewBag.Message("Enter the Cash");
                                goto gotoreturn;
                            }
                        }

                        if (model.chkknet)
                        {
                            if (model.knet == null)
                            {
                                ViewBag.Message("Enter the Knet Amount");
                                goto gotoreturn;
                            }
                        }

                        if (model.chkcheque)
                        {
                            if (model.cheque == null)
                            {
                                ViewBag.Message("Enter the Cheque");
                                goto gotoreturn;
                            }
                        }

                        if(!model.chkpayingtocourt)
                        {
                            if (model.vno == null || model.vno == "")
                            {
                                ViewBag.Message("Enter the Receipt NO");
                                goto gotoreturn;
                            }
                            if (model.vdate == null)
                            {
                                ViewBag.Message("Enter the received date");
                                goto gotoreturn;
                            }
                        }

                        if(model.rentdatefrom == null)
                        {
                            ViewBag.Message("Select the rent period");
                            goto gotoreturn;
                        }

                        if (model.rentdateto == null)
                        {
                            ViewBag.Message("Select the rent period");
                            goto gotoreturn;
                        }

                        if(!model.chkcash && !model.chkknet && !model.chkcheque)
                        {
                            ViewBag.Message("Check the payment type");
                            goto gotoreturn;
                        }
                    }

                    if(model.monthrent == null)
                    {
                        ViewBag.Message("Monthly rent is required");
                        goto gotoreturn;
                    }
                    if (model.monthrent == "0")
                    {
                        ViewBag.Message("Monthly rent cannot be 0");
                        goto gotoreturn;
                    }
                    if (model.monthrent == "0.000")
                    {
                        ViewBag.Message("Monthly rent cannot be 0");
                        goto gotoreturn;
                    }

                    if(Convert.ToInt16(model.amt) != Convert.ToInt16(model.cash) + Convert.ToInt16(model.knet) + Convert.ToInt16(model.cheque) + Convert.ToInt16(model.bt))
                    {
                        ViewBag.Message("Total Not matching");
                        goto gotoreturn;
                    }

                    if(model.chkdiscountamount)
                    {
                        if(Convert.ToInt16(model.amt) + Convert.ToInt16(model.discountamount) != Convert.ToInt16(model.monthrent))
                        {
                            ViewBag.Message($"Total mismatching. Amount received must be {Convert.ToInt16(model.monthrent) - Convert.ToInt16(model.discountamount)}");
                            goto gotoreturn;
                        }
                    }

                    var reccount1 = connection.Query<int>($"SELECT COUNT(*) FROM PAYMENTS WHERE pname='{model.propertyname}' AND APTNO='{model.aptno}' AND paymenttype='{model.type}' and ID IN(select ID from payments where '{model.rentdatefrom}' BETWEEN RENTDATEFROM AND RENTDATETO OR '{model.rentdateto}' BETWEEN RENTDATEFROM AND RENTDATETO )").FirstOrDefault();
                    if(reccount1 > 0)
                    {
                        var recordcount1 = connection.Query<int>($"SELECT COUNT(*) FROM PAYMENTS WHERE pname='{model.propertyname}' AND APTNO='{model.aptno}' AND paymenttype='{model.type}' and TID=(SELECT TID FROM ACCOUNTSPM WHERE pname='{model.propertyname}' AND APTNO='{model.aptno}') AND ID IN(select ID from payments where '{model.rentdatefrom}' BETWEEN RENTDATEFROM AND RENTDATETO OR '{model.rentdateto}' BETWEEN RENTDATEFROM AND RENTDATETO )").FirstOrDefault();
                        if(recordcount1 > 0)
                        {
                            ViewBag.Message($"{model.type} IS ALREADY PAID  BY THIS TENANT FOR THIS PARTICULAR DATES");
                            goto gotoreturn;
                        }
                    }

                    string apt = "";
                    if(model.chkmultipleapartment)
                    {
                        apt = model.groupaptno;
                    }
                    else
                    {
                        apt = model.aptno;
                    }

                    if(model.fromtemp == "PA")
                    {
                        if(model.aptno.Contains(","))
                        {
                            var listaptno = model.aptno.Split(",").ToList();
                            var CHKNAME = connection.Query<string>($"select TNAME from accountsPM where PNAME='{model.propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                            if(CHKNAME != model.tname)
                            {
                                if(!model.rbshowvacatedtenant && !model.rbshowresevedtenant)
                                {
                                    ViewBag.Message = "This Name is Not matching with accounts Property Master";
                                    goto gotoreturn;
                                }
                            }
                            else
                            {
                                tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{model.propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                            }
                        }
                        else
                        {
                            var CHKNAME = connection.Query<string>($"select TNAME from accountsPM where PNAME='{model.propertyname}' and aptno='{model.aptno}'").FirstOrDefault();
                            if(CHKNAME != model.tname)
                            {
                                if (!model.rbshowvacatedtenant && !model.rbshowresevedtenant)
                                {
                                    ViewBag.Message = "This Name is Not matching with accounts Property Master";
                                    goto gotoreturn;
                                }
                            }
                            else
                            {
                                tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{model.propertyname}' and aptno='{model.aptno}'").FirstOrDefault();
                            }
                        }
                    }
                    else
                    {
                        if(model.rbReservedVacatedCurrent != "reservedtenant" && model.rbReservedVacatedCurrent != "vacatedtenant" && model.rbReservedVacatedCurrent != "currenttenant")
                        {
                            if(model.aptno.Contains(","))
                            {
                                var listaptno = model.aptno.Split(",").ToList();
                                tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{model.propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                            }
                            else
                            {
                                tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{model.propertyname}' and aptno='{model.aptno}'").FirstOrDefault();
                            }
                        }
                    }

                    if(model.chkpayingtocourt)
                    {
                        var inserttopayments = await connection.ExecuteAsync($"insert into payments(entrymode,rno,pname,aptno,tname,mrent,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,discamt,remarks,highlight,entryuser,tid) values('smode','{model.vno}','{model.propertyname}','{apt}','{model.tname}','{model.monthrent}','{model.vdate}','{model.rentdatefrom}','{model.rentdateto}','{model.type}','{model.cash}','{model.knet}','{model.cheque}','{model.amt}','{model.desc}',getdate(),'{model.rentmonth}','{model.rentyear}','{model.collectionmonth}','{model.collectionyear}','{model.discountamount}','Paying at Court','{hl}','{Environment.MachineName}','{tenantid}')");
                    }
                    else
                    {
                        var inserttopayments = await connection.ExecuteAsync($"insert into payments(entrymode,rno,pname,aptno,tname,mrent,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,discamt,remarks,highlight,entryuser,tid) values('smode','{model.vno}','{model.propertyname}','{apt}','{model.tname}','{model.monthrent}','{model.vdate}','{model.rentdatefrom}','{model.rentdateto}','{model.type}','{model.cash}','{model.knet}','{model.cheque}','{model.amt}','{model.desc}',getdate(),'{model.rentmonth}','{model.rentyear}','{model.collectionmonth}','{model.collectionyear}','{model.discountamount}','','{hl}','{Environment.MachineName}','{tenantid}')");
                    }

                    if (model.vno.Length < 1)
                    {
                        var update = await connection.ExecuteAsync($"UPDATE PAYMENTS SET RNO=NULL WHERE ID=(SELECT MAX(ID) FROM PAYMENTS WHERE PNAME='{model.propertyname}')");
                    }
                    if(!model.vdate.HasValue)
                    {
                        var update = await connection.ExecuteAsync($"UPDATE PAYMENTS SET RDATE=NULL WHERE ID=(SELECT MAX(ID) FROM PAYMENTS WHERE PNAME='{model.propertyname}')");
                    }

                    if(model.fromtemp == "PA")
                    {
                        var PAPAYMENTS = await connection.ExecuteAsync($"UPDATE PAPAYMENTS SET REMSTATUS='SEEN' WHERE PNAME='{model.propertyname}' AND ID='{model.id}'");
                        var PAYMENTS = await connection.ExecuteAsync($"UPDATE PAYMENTS SET entrymode='PA' WHERE ID=(SELECT MAX(ID) FROM PAYMENTS WHERE PNAME='{model.propertyname}')");

                        if(model.chkpamistake)
                        {
                            var updatePAPAYMENTS = await connection.ExecuteAsync($"UPDATE PAPAYMENTS SET RDATE='{model.vdate}',cash='{model.cash}',knet='{model.knet}',cheque='{model.cheque}',totamt='{model.amt}' WHERE RNO='{model.vno}' AND PNAME='{model.propertyname}')");
                        }
                    }

                    if(model.fromtemp == "bulkprint")
                    {
                        var amountinwords = CommonRepository.ConvertAmount(model.monthrent);
                        var papayments = await connection.ExecuteAsync($"insert into papayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,mrent,REMARKS) values('BulkPrint','{model.vno}','{model.propertyname}','{apt}','{model.tname}','{model.vdate}','{model.rentdatefrom}','{model.rentdateto}','{model.type}','{model.cash}','{model.knet}','{model.cheque}',0.000,'','','{model.amt}','{model.desc}',getdate(),'{model.rentmonth}','{model.rentyear}','{model.collectionmonth}','{model.collectionyear}','{amountinwords}','HO','{model.monthrent}','{HttpContext.Session.GetString("CurrentUsername")}')");
                        var updatePAYMENTS = await connection.ExecuteAsync($"UPDATE PAYMENTS SET entrymode='Bulk Print' WHERE ID=(SELECT MAX(ID) FROM PAYMENTS WHERE PNAME='{model.propertyname}')");
                        var deletebulkprinting = await connection.ExecuteAsync($"delete from bulkprinting WHERE PNAME='{model.propertyname}' AND ID='{model.id}'");
                    }

                    ViewBag.Message = "Entry is successful";
                }

                connection.Close();
                gotoreturn:
                return View(model);
            }
        }

        public JsonResult AptnoChangeEventAccountsReceivable(string propertyname, string aptno)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                AccountsPm model = new();
                connection.Open();
                model = connection.Query<AccountsPm>($"select PREF,tname,cast(mrent as decimal(34,3)) as mrent from accountsPM where PNAME='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
                if(aptno.Contains(","))
                {
                    List<string> listaptno = aptno.Split(",").ToList();
                    model.Tid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                }
                else
                {
                    model.Tid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
                }
                connection.Close();
                return Json(model);
            }
        }
        //public int AptChangeEventAccountsReceivable(string propertyname, string aptno, string gaptno)
        //{
        //    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
        //    {
        //        int tenantid = 0;
        //        connection.Open();
        //        if(gaptno != null)
        //        {
        //            List<string> listaptno = gaptno.Split(",").ToList();
        //            tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
        //        }
        //        else
        //        {
        //            tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
        //        }

        //        connection.Close();
        //        return tenantid;
        //    }
        //}

        public List<string> ReservedRbCheckedEvent()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var telcnos = connection.Query<string>("select top 10 te_lcno from tenantentry order by id desc").ToList();
                connection.Close();
                return telcnos;
            }
        }

        public JsonResult ReservedLcDetails(string reservedlc)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var tentry = connection.Query<TenantEntry>($"select te_tname TeTname,id from tenantentry where te_lcno='{reservedlc}'").FirstOrDefault();
                connection.Close();
                return Json(tentry);
            }
        }

        public JsonResult VacantLcDetails(string propertyname,string aptno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                int ptenant = 0;
                connection.Open();
                if (aptno.Contains(","))
                {
                    List<string> listaptno = aptno.Split(",").ToList();
                    ptenant = connection.Query<int>($"select case when ptid is null then 0 else ptid end as ptid from accountspm where pname='{propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                }
                else
                {
                    ptenant = connection.Query<int>($"select case when ptid is null then 0 else ptid end as ptid from accountspm where pname='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
                }

                if(ptenant == 0)
                {
                    connection.Close();
                    return Json(0);
                }
                else
                {
                    var tentry = connection.Query<TenantEntry>($"select te_tname TeTname,id from tenantentry where id={ptenant}").FirstOrDefault();
                    connection.Close();
                    return Json(tentry);
                }
            }
        }

        public JsonResult CurrentLcDetails(string propertyname, string aptno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                AccountsPm model = new();
                connection.Open();
                if (aptno.Contains(","))
                {
                    List<string> listaptno = aptno.Split(",").ToList();
                    model = connection.Query<AccountsPm>($"select case when tid is null then 0 else tid end as tid,PREF,Tname,cast(MRENT as decimal(34,3)) as crent from accountsPM where PNAME='{propertyname}' and aptno='{listaptno[0]}'").FirstOrDefault();
                }
                else
                {
                    model = connection.Query<AccountsPm>($"select case when tid is null then 0 else tid end as tid,PREF,Tname,cast(MRENT as decimal(34,3)) as crent from accountsPM where PNAME='{propertyname}' and aptno='{aptno}'").FirstOrDefault();
                }
                return Json(model);
            }
        }

        public JsonResult SearchVoucherAccountsReceivable(string searchvoucherno)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) from payments where rno='{searchvoucherno}'").FirstOrDefault();
                if(recordcount > 0)
                {
                    var payments = connection.Query<Payment>($"select paymenttype,cast(totamt as decimal(34,3)) as totamt,rno,id from payments where rno='{searchvoucherno}'").ToList();
                    connection.Close();
                    return Json(payments);
                }
                else
                {
                    connection.Close();
                    return Json("No records found");
                }
            }
        }

        public IActionResult PropertyList(string mode)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                PropertyListModel model = new();
                connection.Open();
                var propertynames = connection.Query<string>("select distinct pname from accountsPM ORDER BY PNAME").ToList();
                var otherpropertynames = connection.Query<string>("select distinct pname from otherproperty order by pname").ToList();
                propertynames.AddRange(otherpropertynames);
                model.propertylist = propertynames;
                model.mode = /*"RECEIVABLE"*/mode;
                connection.Close();
                return View(model);
            }
        }

        public IActionResult LLDetails(string mode)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                LLDetailsModel model = new();
                connection.Open();
                model.lldetails = connection.Query<Lldetail>("select * from lldetails").ToList();
                model.mode = /*"CASHFLOW"*/mode;
                connection.Close();
                return View(model);
            }
        }

        public async Task<IActionResult> LLUpdate(int id, LLUpdateModel model, string update)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if(id != 0)
                {
                    model.lldetail = connection.Query<Lldetail>($"select * from lldetails where id={id}").FirstOrDefault();
                    if (model.lldetail.Status == "YES")
                        model.chkvisibleinreport = true;
                    else
                        model.chkvisibleinreport = false;
                }
                else
                {
                    if(update == "update")
                    {
                        string VSTATUS = "";
                        if (model.chkvisibleinreport)
                            VSTATUS = "YES";
                        else
                            VSTATUS = "NO";

                        var updatelldetails = await connection.ExecuteAsync($"update lldetails set LLNAME='{model.lldetail.Llname}',reportname='{model.lldetail.Reportname}',location='{model.lldetail.Location}',attprop='{model.lldetail.Pname} Property',pname ='{model.lldetail.Pname}',code='{model.lldetail.Code}',arabicllname='{model.lldetail.Arabicllname}',address='{model.lldetail.Address}',arabicaddress='{model.lldetail.Arabicaddress}',AccName= '{model.lldetail.AccName}', AccNO='{model.lldetail.AccNo}', Accbank='{model.lldetail.Accbank}',Attname='{model.lldetail.Attname}',Attdesg='{model.lldetail.Attdesg}',Attph='{model.lldetail.Attph}',Attfax='{model.lldetail.Attfax}',STATUS='{VSTATUS}',r1='{model.lldetail.R1}',r2='{model.lldetail.R2}',r3='{model.lldetail.R3}' where id={model.lldetail.Id}");
                    }
                    else
                    {
                        var updatelldetailsarabic = await connection.ExecuteAsync($"update lldetails set arabicllname='{model.lldetail.Arabicllname}',arabicaddress='{model.lldetail.Arabicaddress}',pnameinarabic='{model.lldetail.Pnameinarabic}' where id={model.lldetail.Id}");
                    }
                    
                    ViewBag.Message = "Updated successfully";
                }
                connection.Close();
            }
            return View(model);
        }

        public IActionResult SubleaseList()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var subleases = connection.Query<Sublease>($"select nationality,lcrent,actualrent,paymode,ftype,ttype,bed,ID, pname,aptno,lno,slno,name,(select rent from lcinfo where lc_no=lno) as rent ,active from subleases where active=1 order by id desc").ToList();
                connection.Close();
                return View(subleases);
            }
        }

        public JsonResult LoadEditSublease(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var sublease = connection.Query<Sublease>($"select * from subleases where id={id}").FirstOrDefault();
                connection.Close();
                return Json(sublease);
            }
        }

        public async Task<string> UpdateSublease(int subleaseid, string TTYPE,string PAYMENTMODE,string nationality,string lcrent,string payablerent,string type,string bedrooms,string leasestart,string leaseend)
        {
            string message = "";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var updatesublease = await connection.ExecuteAsync($"UPDATE SUBLEASES SET lstart='{leasestart}',lend='{leaseend}',LCRENT='{lcrent}',ACTUALRENT='{payablerent}',NATIONALITY='{nationality}',PAYMODE='{PAYMENTMODE}',BED='{bedrooms}',TTYPE='{TTYPE}',FTYPE='{type}' WHERE ID={subleaseid}");
                if(leasestart == null || leasestart=="")
                {
                    var updatelstartnull = await connection.ExecuteAsync($"UPDATE SUBLEASES SET lstart=null WHERE ID={subleaseid}");
                }
                if (leaseend == null || leaseend == "")
                {
                    var updatelendnull = await connection.ExecuteAsync($"UPDATE SUBLEASES SET lend=null WHERE ID={subleaseid}");
                }
                message = "UPDATED";
                connection.Close();
            }
            return message;
        }

        public async Task<string> InactiveSublease(int subleaseid)
        {
            string message = "";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var inactive = await connection.ExecuteAsync($"update subleases set active='0' where id = {subleaseid}");
                message= "Sublease is disabled";
                connection.Close();
            }
            return message;
        }

        public JsonResult LoadShowClientsSublease(string pname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertymaster = connection.Query<PropertymasterModel>($"select BLDGNAME,APTNO,cleaseno,cname,crent,(select compaddress from lcinfo where lc_no=cleaseno) as compaddress from propertymaster where bldgname='{pname}' AND  propertyref IN (select pref from subleases where active=1 and PNAME='{pname}') order by orderid").ToList();
                connection.Close();
                return Json(propertymaster);
            }
        }

        public async Task<string> ImportSelectedSublease(string pname,string aptNo,string cleaseno,string cname,string crent,string compaddress)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var import = await connection.ExecuteAsync($"insert into accountsPM (pname,aptno,tname,mrent,leaseno,remarks,doc,PREF)values('Q8REALTOR - {pname}','{aptNo}','{cname}','{crent}','{cleaseno}','',getdate(),'-')");
                message = "Selected Sublease Apartment is uploaded in Accounts Master";
                connection.Close();
                return message;
            }
        }

        public int ExistInAccPm(string pname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) from accountsPM where pname='Q8REALTOR - {pname}' ").FirstOrDefault();
                connection.Close();
                return recordcount;
            }
        }

        public async Task<string> ImportAllSublease(int response,int rowslength,string pname, string[] arraybldgname, string[] arrayaptno, string[] arrayleaseno, string[] arrayname, string[] arrayrent, string[] arraycompaddress)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                
                if(response > 0)
                {
                    var delete = await connection.ExecuteAsync($"delete from accountsPM where pname='Q8REALTOR - {pname}'");
                    for(int i=0;i < rowslength;i++)
                    {
                        var import = await connection.ExecuteAsync($"insert into accountsPM (pname,aptno,tname,mrent,leaseno,remarks,doc,PREF)values('Q8REALTOR - {pname}','{arrayaptno[i]}','{arrayname[i]}','{arrayrent[i]}','{arrayleaseno[i]}','',getdate(),'-')");
                    }
                }
                else
                {
                    for (int i = 0; i < rowslength; i++)
                    {
                        var import = await connection.ExecuteAsync($"insert into accountsPM (pname,aptno,tname,mrent,leaseno,remarks,doc,PREF)values('Q8REALTOR - {pname}','{arrayaptno[i]}','{arrayname[i]}','{arrayrent[i]}','{arrayleaseno[i]}','',getdate(),'-')");
                    }
                }
                message = "Sublease Property is uploaded";
                connection.Close();
                return message;
            }
        }

        public IActionResult OtherTypes()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var OTHERPAYTYPE = connection.Query<Otherpaytype>($"SELECT PAYTYPE,id FROM OTHERPAYTYPE order by ID desc").ToList();
                connection.Close();
                return View(OTHERPAYTYPE);
            }
        }

        public async Task<string> AddPaymentType(string newpaymenttype)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();

                var insert = await connection.ExecuteAsync($"insert into OTHERPAYTYPE values('{newpaymenttype}')");
                if (insert == 1)
                    message = "Inserted";
                else
                    message = "Unknown Error";

                connection.Close();
                return message;
            }
        }

        public async Task<string> DeletePaymentType(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();

                var delete = await connection.ExecuteAsync($"delete from otherpaytype where id={id}");
                if (delete == 1)
                    message = "Deleted";
                else
                    message = "Unknown Error";

                connection.Close();
                return message;
            }
        }

        public IActionResult ExpensesAndReports()
        {
            return View();
        }

        public IActionResult ContractBasedExpenses()
        {
            return View();
        }

        public List<string> LoadStatements(DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<string> stcombo = new();
                connection.Open();
                var recordcount = connection.Query<int>($"select COUNT(*) from pmesST where datepart(mm,smonth)='{statementmonth.Month}' and datepart(yyyy,smonth)='{statementmonth.Year}' AND exptype='Period' ").FirstOrDefault();
                if(recordcount > 0)
                {
                    stcombo = connection.Query<string>($"select DISTINCT NO from pmesst where datepart(mm,smonth)='{statementmonth.Month}' and datepart(yyyy,smonth)='{statementmonth.Year}' AND exptype='Period'").ToList();
                }
                connection.Close();
                return stcombo;
            }
        }

        public JsonResult LoadPMEsreqGRID(DateTime statementmonth, string statement)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ContractBasedExpensesModel model = new();
                connection.Open();
                model.PMEsreqGRID = connection.Query<Pmesst>($"SELECT category,smonth,CONVERT(VARCHAR(11),DATE,106) as DATE,CAT,DESCRIPTION,MAPT,PNAME,APTNO,REFNO,UPRICE,PREF,QTY,cast(amt as decimal(34,3)) as AMT,NO,EXP,INVNO,id,accounts FROM PMESST WHERE month(smonth)='{statementmonth.Month}' and  year(smonth)='{statementmonth.Year}' and exptype='Period' and no='{statement}' order by id desc").ToList();
                var recordcount = connection.Query<int>($"SELECT count(*) FROM PMESST WHERE month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and no='{statement}' and exptype='Period'").FirstOrDefault();
                if(recordcount > 0)
                {
                    model.MAINTOTAMTXT = connection.Query<decimal>($"SELECT SUM(AMT) FROM PMESST WHERE month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period' and no='{statement}'").FirstOrDefault();
                }
                else
                {
                    model.MAINTOTAMTXT = 0;
                }

                var recordcount1 = connection.Query<int>($"select count(*) from periodexpensesentry where pmeid in(SELECT id FROM PMESST WHERE NO='{statement}' and month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period')").FirstOrDefault();
                if(recordcount1 > 0)
                {
                    model.periodexpensesentry = connection.Query<Periodexpensesentry>($"select * from periodexpensesentry where pmeid in(SELECT id FROM PMESST WHERE NO='{statement}' and month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period')").ToList();
                }
                connection.Close();
                return Json(model);
            }
        }

        public async Task<string> UpdateCategory(DateTime statementmonth, string statement,string category)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update PMESST set category='{category}' WHERE month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period' and no='{statement}'");
                if (update == 1)
                    message = "Category Updated";
                else
                    message = "Unknown Error";
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateAccToVerified(DateTime statementmonth, string statement)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update PMESST set accounts='VERIFIED' WHERE month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period' and no='{statement}'");
                if (update == 1)
                    message = "Verified";
                else
                    message = "Unknown Error";
                connection.Close();
                return message;
            }
        }

        public async Task<string> RemoveVerified(DateTime statementmonth, string statement)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update PMESST set accounts='' WHERE month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period' and no='{statement}'");
                if (update == 1)
                    message = "Verification Removed";
                else
                    message = "Unknown Error";
                connection.Close();
                return message;
            }
        }

        public IActionResult ContractBasedExpensesByProperty()
        {
            return View();
        }

        public JsonResult Loadpmepropgrid(DateTime statementmonth, string pname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ContractBasedExpensesByPropertyModel model = new();
                connection.Open();
                List<int> ids = connection.Query<int>($"Select distinct id from pmesst where month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period' and pname='{pname}'").ToList();
                if(ids.Count > 0)
                {
                    for(int i=0; i < ids.Count; i++)
                    {
                        model.pmepropgrid = connection.Query<Pmesst>($"select category,smonth,CONVERT(VARCHAR(11),DATE,106) as DATE,CAT,DESCRIPTION,MAPT,PNAME,APTNO,REFNO,UPRICE,PREF,QTY,cast(amt as decimal(34,3)) as AMT,NO,EXP,INVNO,id,accounts from pmesst where month(smonth)='{statementmonth.Month}' and  year(smonth)='{statementmonth.Year}' and exptype='Period' and pname='{pname}' and id={ids[i]}").ToList();
                        var perexpent = connection.Query<Pmesst>($"select month+'  -  '+year as date,cast(amt as decimal(34,3)) as amt,pmeid from periodexpensesentry where pmeid={ids[i]} order by id").ToList();
                        model.pmepropgrid.AddRange(perexpent);
                    }
                    model.MAINTOTAMTXT = connection.Query<decimal>($"select cast(sum(amt) as decimal(34,3))  as AMT from pmesst where month(smonth)='{statementmonth.Month}' and  year(smonth)='{statementmonth.Year}' and exptype='Period' and pname='{pname}'").FirstOrDefault();
                }
                

                connection.Close();
                return Json(model);
            }
        }

        public JsonResult Loadallpmepropgrid(DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ContractBasedExpensesByPropertyModel model = new();
                model.pmepropgrid = new();
                List<Pmesst> model1 = new();
                connection.Open();
                List<int> ids = connection.Query<int>($"Select distinct id from pmesst where month(smonth)='{statementmonth.Month}' and year(smonth)='{statementmonth.Year}' and exptype='Period'").ToList();
                if (ids.Count > 0)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {
                        model1 = connection.Query<Pmesst>($"select category,smonth,CONVERT(VARCHAR(11),DATE,106) as DATE,CAT,DESCRIPTION,MAPT,PNAME,APTNO,REFNO,UPRICE,PREF,QTY,cast(amt as decimal(34,3)) as AMT,NO,EXP,INVNO,id,accounts from pmesst where month(smonth)='{statementmonth.Month}' and  year(smonth)='{statementmonth.Year}' and exptype='Period' and id={ids[i]}").ToList();
                        var perexpent = connection.Query<Pmesst>($"select month+'  -  '+year as date,cast(amt as decimal(34,3)) as amt,pmeid from periodexpensesentry where pmeid={ids[i]} order by id").ToList();
                        model1.AddRange(perexpent);
                        model.pmepropgrid.AddRange(model1);
                    }
                    
                    //model.MAINTOTAMTXT = connection.Query<decimal>($"select cast(sum(amt) as decimal(34,3))  as AMT from pmesst where month(smonth)='{statementmonth.Month}' and  year(smonth)='{statementmonth.Year}' and exptype='Period'").FirstOrDefault();
                }


                connection.Close();
                return Json(model);
            }
        }

        public IActionResult Categories()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<AccountsCategoryExpense> model = new();
                connection.Open();
                model = connection.Query<AccountsCategoryExpense>("select category,ID from accounts_category_expenses order by id desc").ToList();
                connection.Close();
                return View(model);
            }
        }

        public async Task<string> AddCategory(string newcategory)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var insert = await connection.ExecuteAsync($"Insert into accounts_category_expenses values('{newcategory}')");
                if (insert == 1)
                    message = "Inserted";
                else
                    message = "Unknown Error";
                connection.Close();
                return message;
            }
        }

        public async Task<string> DeleteCategory(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();

                var delete = await connection.ExecuteAsync($"delete from accounts_category_expenses where id={id}");
                if (delete == 1)
                    message = "Deleted";
                else
                    message = "Unknown Error";

                connection.Close();
                return message;
            }
        }

        public IActionResult IncomeExpenses()
        {
            return View();
        }

        public JsonResult IncomeExpensesBtnOk(DateTime month,string pname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                IncomeExpensesModel model = new();
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) from payments where pname='{pname}' and month='{month.Month}' and year='{month.Year}' and paymenttype in('RENT','ADDITIONAL RENT PAYMENT')").FirstOrDefault();
                if(recordcount > 0)
                {
                    model.monrent = connection.Query<string>($"select sum(totamt) from payments where pname='{pname}' and month='{month.Month}' and year='{month.Year}' and paymenttype in('RENT','ADDITIONAL RENT PAYMENT')").FirstOrDefault();
                    var reccount = connection.Query<int>($"select count(*) from pmf where pname='{pname}' and month='{month.Month}' and year='{month.Year}' ").FirstOrDefault();
                    if(reccount > 0)
                    {
                        model.pmf = connection.Query<string>($"select pmfamt from pmf where pname='{pname}' and month='{month.Month}' and year='{month.Year}' ").FirstOrDefault();
                    }
                }

                var recordcount1 = connection.Query<int>($"select count(*) from pmesaccounts where pname='{pname}' and month='{month.Month}' and year='{month.Year}'").FirstOrDefault();
                if(recordcount1 > 0)
                {
                    model.monexp = connection.Query<string>($"select sum(totamt) from pmesaccounts where pname='{pname}' and month='{month.Month}' and year='{month.Year}'").FirstOrDefault();
                }
                else
                {
                    model.monexp = "0";
                }

                var recordcount2 = connection.Query<int>($"select count(*) from pmesst where pname='{pname}' and month(smonth)='{month.Month}' and year(smonth)='{month.Year}' and exptype='Period' ").FirstOrDefault();
                if(recordcount2 > 0)
                {
                    model.totperiodexp = connection.Query<string>($"select sum(amt) from periodexpensesentry where pmeid in(select id from pmesst where pname='{pname}' and month(smonth)='{month.Month}' and year(smonth)='{month.Year}' and exptype='Period') and month='{month.ToString("MMMM")}' and year='{month.Year}'").FirstOrDefault();
                    model.categorygrid = connection.Query<CategorygridModel>($"select cast(amt as decimal(34,3))  as amt,(select category from pmesst where id=pmeid ) as cat from periodexpensesentry where pmeid in(select id from pmesst where pname='{pname}' and month(smonth)='{month.Month}' and year(smonth)='{month.Year}' and exptype='Period') and month='{month.ToString("MMMM")}' and year='{month.Year}'").ToList();
                }
                connection.Close();
                return Json(model);
            }
        }

        public IActionResult RentNotPaid()
        {
            return View();
        }

        public JsonResult RentNotPaidList(DateTime month)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select COUNT(*) from payments where month='{month.Month}' and year='{month.Year}' and  totamt =0 AND tname NOT IN('Vacant')").FirstOrDefault();
                if(reccount > 0)
                {
                    var RENTGRID = connection.Query<Payment>($"select PNAME,APTNO from payments where month='{month.Month}' and year='{month.Year}' and totamt =0 AND tname NOT IN('Vacant')");
                    connection.Close();
                    return Json(RENTGRID);
                }
                else
                {
                    connection.Close();
                    return Json(0);
                }
                
            }
        }

        public IActionResult Datelistpme(string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                DatelistpmeModel model = new();
                connection.Open();
                model.pname = bldngname;
                model.yearcombo = connection.Query<string>($"select distinct year from pmesaccounts where pname='{bldngname}' AND accstname IS NOT NULL").ToList();
                model.dategrid = connection.Query<string>($"select distinct accstname from pmesaccounts where pname='{bldngname}' and year=year(getdate())").ToList();
                connection.Close();
                return View(model);
            }
        }

        public int BtnCreateDatelistpme(string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"SELECT COUNT(*) FROM PMESACCOUNTS WHERE STATUS='OPEN' and pname='{bldngname}'").FirstOrDefault();
                connection.Close();
                return recordcount;
            }
        }

        public IActionResult PMESaccounts(string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                PMESaccountsModel model = new();
                connection.Open();
                model.bldngname = bldngname;
                model.PMEGRID = connection.Query<Pmesaccount>($"SELECT PNAME,category,id,CONVERT(VARCHAR(11),INVDATE,106) as INVDATE,DESCRIPTION,APTNO,INVNO,cast(TOTAMT as decimal(34,3)) as TOTAMT,STNAME from PMESACCOUNTS where PNAME='{bldngname}' AND ACCSTNAME IS NULL ORDER BY ID desc").ToList();
                connection.Close();
                return View(model);
            }
        }

        public List<string> LoadOnYearChange(string year,string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var dategrid = connection.Query<string>($"select distinct accstname from pmesaccounts where pname='{bldngname}' and year='{year}'").ToList();
                connection.Close();
                return dategrid;
            }
        }

        public IActionResult Pmesstaccounts(string accpme, string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                PmesstaccountsModel model = new();
                connection.Open();
                model.bldngname = bldngname;
                model.accpme = accpme;
                model.PMEGRID = connection.Query<Pmesaccount>($"SELECT accstatus,STATUS,month,year,id,CONVERT(VARCHAR(11),INVDATE,106) as INVDATE,DESCRIPTION,APTNO,INVNO,cast(TOTAMT as decimal(34,3)) as TOTAMT,STNAME from PMESACCOUNTS where PNAME='{bldngname}' AND ACCSTNAME='{accpme}' ORDER BY INVDATE").ToList();
                foreach(var item in model.PMEGRID)
                {
                    model.pmemonth = item.Month;
                    model.pmeyear = item.Year;
                    model.PMEaccSTATUS = item.Status.Trim();
                }
                model.PMETOT = connection.Query<decimal>($"SELECT SUM(TOTAMT) From PMESACCOUNTS where PNAME='{bldngname}' AND ACCSTNAME='{accpme}'").FirstOrDefault();
                //if (model.PMEaccSTATUS == "OPEN")
                //{
                //      Panel9.Enabled = True
                //      Panel4.Enabled = True
                //}
                //if (model.PMEaccSTATUS == "CLOSED")
                //{
                //      Panel9.Enabled = False
                //      Panel4.Enabled = False
                //}
                connection.Close();
                return View(model);
            }
        }

        public async Task<string> BtnAddPmesstaccounts(string invoicedate,string amountkd,string category,string invoiceno,string itemworkdescription,string aptno,bool chkmultipleapartment,string bldngname,string greftxt,string accpme)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                int insert = 0;
                connection.Open();
                if(chkmultipleapartment)
                {
                    insert = await connection.ExecuteAsync($"insert into pmesaccounts (pname,invdate,description,aptno,invno,totamt,STNAME,ACCSTNAME,MONTH,YEAR,doc,category) values('{bldngname}','{invoicedate}','{itemworkdescription}','{greftxt}','{invoiceno}','{amountkd}','ACCOUNTS','{accpme}','{Convert.ToDateTime(invoicedate).Month}','{Convert.ToDateTime(invoicedate).Year}',getdate(),'{category}')");
                }
                else
                {
                    insert = await connection.ExecuteAsync($"insert into pmesaccounts (pname,invdate,description,aptno,invno,totamt,STNAME,ACCSTNAME,MONTH,YEAR,doc,category) values('{bldngname}','{invoicedate}','{itemworkdescription}','{aptno}','{invoiceno}','{amountkd}','ACCOUNTS','{accpme}','{Convert.ToDateTime(invoicedate).Month}','{Convert.ToDateTime(invoicedate).Year}',getdate(),'{category}')");
                }

                if(insert == 1)
                {
                    message = "Added";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnReplace (int selectedid,string invoicedate, string amountkd, string invoiceno, string itemworkdescription, string aptno, string accpme)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update pmesaccounts set invdate='{invoicedate}' ,description='{itemworkdescription}',aptno='{aptno}',invno='{invoiceno}',totamt='{amountkd}' where id={selectedid} AND ACCSTNAME='{accpme}'");
                if (update == 1)
                {
                    message = "Replaced";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public string DeletePmesstaccounts(int selectedid, string accpme)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = connection.Execute($"delete from pmesaccounts where id={selectedid} AND ACCSTNAME='{accpme}'");
                if (delete == 1)
                {
                    message = "Deleted";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateStatusPmesstaccounts(int[] selectedids,string status)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                int update = 0;
                connection.Open();
                for(int i=0;i<selectedids.Count();i++)
                {
                    update = await connection.ExecuteAsync($"UPDATE pmesaccounts SET ACCSTATUS='{status}' where id={selectedids[i]}");
                }
                
                if (update == 1)
                {
                    message = "Status Updated";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnOpenPmesstaccounts(string accpme, string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"UPDATE pmesaccounts SET ACCSTATUS=NULL,month=null,year=null,status=null where PNAME='{bldngname}' AND ACCSTNAME='{accpme}'");
                if (update == 1)
                {
                    message = "Opened";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult pmesALLPROPERTY(string pmesaccmode,string pmemonth,string pmeyear)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                pmesALLPROPERTYModel model = new();
                connection.Open();
                if(pmesaccmode == "FROMLOC")
                {
                    var recordcount = connection.Query<int>($"select count(*) from pmesaccounts where ACCSTNAME IS NULL OR LEN(ACCSTNAME)<1").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        model.PMEGRID = connection.Query<Pmesaccount>($"select PNAME,id,CONVERT(VARCHAR(11),INVDATE,106) as INVDATE,DESCRIPTION,APTNO,INVNO,cast(TOTAMT as decimal(34,3)) as TOTAMT,STNAME from pmesaccounts where ACCSTNAME IS NULL OR LEN(ACCSTNAME)<1 order by pname,INVDATE").ToList();
                        model.DG = connection.Query<Pmesaccount>("select PNAME,cast(SUM(TOTAMT) as decimal(34,3)) as TOTAMT from pmesaccounts where ACCSTNAME IS NULL OR LEN(ACCSTNAME)<1 GROUP by pname").ToList();
                        model.totamt = connection.Query<decimal>($"select sum(TOTAMT) as TOTAMT from pmesaccounts where ACCSTNAME IS NULL OR LEN(ACCSTNAME)<1").FirstOrDefault();
                    }
                }

                if (pmesaccmode == "FROMST")
                {
                    model.pmemonth = pmemonth;
                    model.pmeyear = pmeyear;
                    model.PMEGRID = connection.Query<Pmesaccount>($"select PNAME,id,CONVERT(VARCHAR(11),INVDATE,106) as INVDATE,DESCRIPTION,APTNO,INVNO,cast(TOTAMT as decimal(34,3)) as TOTAMT,ACCSTNAME from pmesaccounts where LEN(ACCSTNAME)>1 and month='{pmemonth}' and year='{pmeyear}' order by pname,INVDATE").ToList();
                    model.DG = connection.Query<Pmesaccount>($"select PNAME,cast(SUM(TOTAMT) as decimal(34,3)) as TOTAMT from pmesaccounts where LEN(ACCSTNAME)>1 and month='{pmemonth}' and year='{pmeyear}' GROUP by pname").ToList();
                    model.totamt = connection.Query<decimal>($"select sum(TOTAMT) as TOTAMT from pmesaccounts where LEN(ACCSTNAME)>1 ").FirstOrDefault();
                }
                connection.Close();
                return View(model);
            }
        }

        public JsonResult BtnSummaryGrpStatementpmesALLPROPERTY(string pmemonth,string pmeyear)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var pmegroupgrid = connection.Query<Pmesaccount>($"select PNAME,cast(SUM(TOTAMT) as decimal(34,3)) as TOTAMT,accstatus from pmesaccounts where LEN(ACCSTNAME)>1 and month='{pmemonth}' and year='{pmeyear}' GROUP by pname,accstatus").ToList();
                connection.Close();
                return Json(pmegroupgrid);
            }
            
        }

        public string BtnClosepmesALLPROPERTY(string pmemonth, string pmeyear)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var OPENST = connection.Query<int>("SELECT COUNT(*) FROM PMESACCOUNTS WHERE STNAME='OPEN'").FirstOrDefault();
                if(OPENST > 0)
                {
                    message = "PARTICULAR STATEMENT IS OPEN";
                    goto gotoreturn;
                }
                var update = connection.Execute($"UPDATE pmesaccounts SET STATUS='CLOSED' WHERE MONTH='{pmemonth}' AND YEAR='{pmeyear}'");
                if(update==1)
                {
                    message = "PME IS CLOSED";
                }
                else
                {
                    message = "Unknown Error";
                }

                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public List<string> LoadStatementsPMESaccounts(DateTime invoicedate,string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<string> statements = new();
                connection.Open();
                var recordcount = connection.Query<int>($"select COUNT(*) AS REC from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND PNAME='{bldngname}' AND EXP='LL'").FirstOrDefault();
                if(recordcount > 0)
                {
                    var statement = connection.Query<string>($"select DISTINCT st from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND PNAME='{bldngname}' AND EXP='LL'").ToList();
                    statements.AddRange(statement);
                }

                var recordcount1 = connection.Query<int>($"select COUNT(*) AS REC from pmesst where datepart(mm,SMONTH)='{invoicedate.Month}' and datepart(yyyy,SMONTH)='{invoicedate.Year}' AND EXP='LL'").FirstOrDefault();
                if(recordcount1 > 0)
                {
                    var statement = connection.Query<string>($"select DISTINCT no from pmesst where datepart(mm,SMONTH)='{invoicedate.Month}' and datepart(yyyy,SMONTH)='{invoicedate.Year}' AND EXP='LL'").ToList();
                    statements.AddRange(statement);
                }
                connection.Close();
                return statements;
            }
        }

        public JsonResult StatementsChangeEventPMESaccounts(DateTime invoicedate, string statements)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                StatementsChangeEventPMESaccountsModel model = new();
                connection.Open();
                var recordcount = connection.Query<int>($"select COUNT(*) AS REC from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}'").FirstOrDefault();
                if (recordcount > 0)
                {
                    model.pmemode = "PME";
                    model.tot = connection.Query<decimal>($"select SUM(AMT) AS REC from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}'").FirstOrDefault();
                }

                var recordcount1 = connection.Query<int>($"select COUNT(*) AS REC from pmesst where datepart(mm,SMONTH)='{invoicedate.Month}' and datepart(yyyy,SMONTH)='{invoicedate.Year}' AND no='{statements}'").FirstOrDefault();
                if (recordcount1 > 0)
                {
                    model.pmemode = "PMEST";
                    model.tot = connection.Query<decimal>($"select SUM(AMT) AS REC from pmesst where datepart(mm,SMONTH)='{invoicedate.Month}' and datepart(yyyy,SMONTH)='{invoicedate.Year}' AND no='{statements}'").FirstOrDefault();
                }
                connection.Close();
                return Json(model);
            }
        }

        public async Task<string> BtnLoadPMESaccounts(DateTime invoicedate, string statements,string bldngname,string pmemode)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM PMESACCOUNTS WHERE PNAME='{bldngname}' AND ACCSTNAME IS NULL AND STNAME='{statements}'").FirstOrDefault();
                if(reccount > 0)
                {
                    message = "This statement is already loaded. Check in the grid below";
                    goto gotoreturn;
                }
                else
                {
                    if(pmemode == "PME")
                    {
                        var recordcount = connection.Query<int>($"select COUNT(*) AS REC from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}' AND PNAME='{bldngname}'").FirstOrDefault();
                        if(recordcount > 0)
                        {
                            var insert = await connection.ExecuteAsync($"INSERT INTO PMESACCOUNTS(PNAME,INVDATE,DESCRIPTION,APTNO,INVNO,TOTAMT,STNAME,category) SELECT PNAME,INVDATE,WORKDESC,APTNO,INVNO,AMT,st,cat from pmes where datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}' AND PNAME='{bldngname}' ORDER BY INVDATE");
                            if(insert == 1)
                            {
                                message = "Loaded";
                            }
                            else
                            {
                                message = "Unknown Error";
                            }
                        }
                        else
                        {
                            message = "NO RECORDS FOUND";
                            goto gotoreturn;
                        }
                    }

                    if (pmemode == "PMEST")
                    {
                        var recordcount = connection.Query<int>($"select COUNT(*) AS REC  from pmesst where datepart(mm,smonth)='{invoicedate.Month}' and datepart(yyyy,smonth)='{invoicedate.Year}' AND no='{statements}' AND PNAME='{bldngname}'").FirstOrDefault();
                        if(recordcount > 0)
                        {
                            var insert = await connection.ExecuteAsync($"INSERT INTO PMESACCOUNTS(PNAME,INVDATE,DESCRIPTION,APTNO,INVNO,TOTAMT,STNAME,category) SELECT PNAME,DATE,description,APTNO,INVNO,AMT,no,category from pmesst where datepart(mm,smonth)='{invoicedate.Month}' and datepart(yyyy,smonth)='{invoicedate.Year}' AND no='{statements}' AND PNAME='{bldngname}' ORDER BY DATE");
                            if (insert == 1)
                            {
                                message = "Loaded";
                            }
                            else
                            {
                                message = "Unknown Error";
                            }
                        }
                        else
                        {
                            message = "NO RECORDS FOUND";
                            goto gotoreturn;
                        }
                    }
                }
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public int BtnUnload1PMESaccounts(string statements, string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM PMESACCOUNTS WHERE PNAME='{bldngname}' AND ACCSTNAME IS NULL AND STNAME='{statements}'").FirstOrDefault();
                connection.Close();
                return reccount;
            }
        }

        public string BtnUnload2PMESaccounts(string statements, string bldngname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = connection.Execute($"delete from pmesaccounts where accstname is null and STNAME='{statements}' and pname='{bldngname}'");
                if(delete==1)
                {
                    message = "Successfully unloaded";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        //public JsonResult BtnViewPMESaccounts(DateTime invoicedate,string statements)
        //{
        //    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
        //    {
        //        connection.Open();
        //        var pmes = connection.Query<Pme>($"select PNAME,INVDATE,WORKDESC,APTNO,INVNO,AMT from PMES WHERE datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}' order by PNAME").ToList();
        //        connection.Close();
        //        return Json(pmes);
        //    }
        //}

        public IActionResult BtnViewPMESaccounts(/*PMESaccountsModel model*/DateTime invoicedate,string statements)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //var pmes = connection.Query<Pme>($"select PNAME,INVDATE,WORKDESC,APTNO,INVNO,AMT from PMES WHERE datepart(mm,invdate)='{model.invoicedate.Month}' and datepart(yyyy,invdate)='{model.invoicedate.Year}' AND ST='{model.statements}' order by PNAME").ToList();
                var pmes = connection.Query<Pme>($"select PNAME,INVDATE,WORKDESC,APTNO,INVNO,AMT from PMES WHERE datepart(mm,invdate)='{invoicedate.Month}' and datepart(yyyy,invdate)='{invoicedate.Year}' AND ST='{statements}' order by PNAME").ToList();
                using (var workbook=new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PME");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "Property Name";
                    worksheet.Cell(currentRow, 2).Value = "Invoice Date";
                    worksheet.Cell(currentRow, 3).Value = "Description";
                    worksheet.Cell(currentRow, 4).Value = "Apt No";
                    worksheet.Cell(currentRow, 5).Value = "Invoice No";
                    worksheet.Cell(currentRow, 6).Value = "Amount";

                    foreach (var item in pmes)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.Pname;
                        worksheet.Cell(currentRow, 2).Value = item.Invdate;
                        worksheet.Cell(currentRow, 3).Value = item.Workdesc;
                        worksheet.Cell(currentRow, 4).Value = item.Aptno;
                        worksheet.Cell(currentRow, 5).Value = item.Invno;
                        worksheet.Cell(currentRow, 6).Value = item.Amt;
                    }

                    using(var stream=new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        connection.Close();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PMES FROM PMS.xlsx");
                    }
                }
            }
        }

        public IActionResult TaskModule(string bldngname)
        {
            TaskModuleModel model = new();
            model.viewmode = "off";
            model.createmode = "off";
            model.buildingname = bldngname;
            return View(model);
        }

        public List<string> FetchDistinctOtherPaytypeTaskModule()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var otherpaytypes = connection.Query<string>($"SELECT distinct PAYTYPE FROM OTHERPAYTYPE").ToList();
                connection.Close();
                return otherpaytypes;
            }
        }

        public List<string> FetchReservedLcReservedRb()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reservedlc = connection.Query<string>($"select top 10 te_lcno from tenantentry order by id desc").ToList();
                connection.Close();
                return reservedlc;
            }
        }

        public JsonResult ReservedLcSelectedIndex(string reservedlc)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reservedlcdet = connection.Query<TenantEntry>($"select te_tname TeTname,id from tenantentry where te_lcno='{reservedlc}'").FirstOrDefault();
                connection.Close();
                return Json(reservedlcdet);
            }
        }

        public JsonResult ShowVacatedRb(string bldngname,string aptcombo)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                showvacatedrbModel model = new();
                connection.Open();
                var ptenant = connection.Query<int>($"select case when ptid is null then 0 else ptid end as ptid from accountspm where pname='{bldngname}' and aptno='{aptcombo}'").FirstOrDefault();
                if(ptenant==0)
                {
                    model.vacatedrb = false;
                    model.message = "No previous Tenant Info";
                }
                else
                {
                    var vacatedlcdet = connection.Query<TenantEntry>($"select te_tname TeTname,id Id from tenantentry where id='{ptenant}'").FirstOrDefault();
                    model.te_tname = vacatedlcdet.TeTname;
                    model.id = vacatedlcdet.Id;
                }
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult ShowCurrentRb(string bldngname, string aptcombo)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                showcurrentrbModel model = new();
                connection.Open();
                
                var accountsPM = connection.Query<AccountsPm>($"select case when tid is null then 0 else tid end as tid,PREF,Tname,cast(MRENT as decimal(34,3)) as MRENT from accountsPM where PNAME='{bldngname}' and aptno='{aptcombo}'").FirstOrDefault();
                model.Tname = accountsPM.Tname;
                model.tid = Convert.ToInt16(accountsPM.Tid);
                model.crent = accountsPM.Mrent.ToString();
                
                connection.Close();
                return Json(model);
            }
        }

        public string ChkForceToGenerateVoucherNo(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var vnotxt = connection.Query<string>($"SELECT MAX(RNO)+1 as newrno FROM PAYMENTS WHERE PNAME='{buildingname}' and collectionmonth=month(getdate()) and collectionyear=year(getdate())").FirstOrDefault();
                connection.Close();
                return vnotxt;
            }
        }

        public JsonResult ChkAllApt(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                chkallaptModel model = new();
                connection.Open();
                model.totapt = connection.Query<int>($"select count(*) as rec from accountspm where pname='{buildingname}' and id not in(select id from accountspm where aptno like 'S%' or aptno like 'M%' or aptno like 'B%')").FirstOrDefault();
                var accountsPM = connection.Query<AccountsPm>($"select top 1 PREF,Tname,cast(MRENT as decimal(34,3)) as MRENT from accountsPM where PNAME='{buildingname}'").FirstOrDefault();
                model.Tname = accountsPM.Tname;
                model.crent = accountsPM.Mrent.ToString();
                connection.Close();
                return Json(model);
            }
        }

        public string AddMonths(DateTime rentdatefrom,int aggtime)
        {
            var leaseendingdate = Convert.ToDateTime(rentdatefrom).AddMonths(aggtime).ToString("yyyy-MM-dd");
            leaseendingdate = Convert.ToDateTime(leaseendingdate).AddDays(-1).ToString("yyyy-MM-dd");
            return leaseendingdate;
        }

        public string AddDays(DateTime rentdatefrom,int aggtime)
        {
            var leaseendingdate = Convert.ToDateTime(rentdatefrom).AddDays(aggtime).ToString("yyyy-MM-dd");
            leaseendingdate = Convert.ToDateTime(leaseendingdate).AddDays(-1).ToString("yyyy-MM-dd");
            return leaseendingdate;
        }

        public List<string> BtnLoadVoucherNo(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var oldsdcombo = connection.Query<string>($"select distinct rno from payments where pname='{buildingname}' and paymenttype='DEPOSIT' ").ToList();//order by id
                connection.Close();
                return oldsdcombo;
            }
        }

        public JsonResult LoadOldVoucherNoDetails(string buildingname,string oldvoucherno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                loadoldvouchernodetailsModel model = new();
                connection.Open();
                var payments = connection.Query<Payment>($"select rno,totamt,rdate from payments where rno='{oldvoucherno}' and pname='{buildingname}'").FirstOrDefault();
                model.rno = payments.Rno.ToString();
                model.rdate = Convert.ToDateTime(payments.Rdate).ToString("yyyy-MM-dd");
                model.totamt = payments.Totamt.ToString();
                connection.Close();
                return Json(model);
            }
        }

        public List<string> ChkNewDepositAgainstOldDepositLoadRno(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var oldsdcombo = connection.Query<string>($"select top 100 rno from payments where pname='{buildingname}' and paymenttype='DEPOSIT' order by id").ToList();
                connection.Close();
                return oldsdcombo;
            }
        }

        public int GetCountOfPayments(string vnotxt, string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM payments WHERE RNO ='{vnotxt}' and PNAME='{buildingname}'").FirstOrDefault();
                connection.Close();
                return reccount;
            }
        }

        public int GetCountOfPaPayments(string aptno, string buildingname, string paymentcategory, string rentdatefrom, string rentdateto, string tenantname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"SELECT COUNT(*) as rec FROM PAPAYMENTS WHERE pname='{buildingname}' AND APTNO='{aptno}' AND paymenttype='{paymentcategory}' and id in(select id from papayments where '{rentdatefrom}' BETWEEN RENTDATEFROM AND RENTDATETO OR '{rentdateto}' BETWEEN RENTDATEFROM AND RENTDATETO ) and remstatus not in('CANCELLED') and '{tenantname}' IN(SELECT TNAME FROM PAPAYMENTS WHERE pname='{buildingname}' AND APTNO='{aptno}' AND paymenttype='{paymentcategory}' and id in(select id from papayments where '{rentdatefrom}' BETWEEN RENTDATEFROM AND RENTDATETO OR '{rentdateto}' BETWEEN RENTDATEFROM AND RENTDATETO ) and remstatus not in('CANCELLED'))").FirstOrDefault();
                connection.Close();
                return reccount;
            }
        }

        public int BtnForceReservedVacatedCurrentFalseTenid(string buildingname, string aptno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var tenantid = connection.Query<int>($"select case when tid is null then 0 else tid end as tid from accountsPM where PNAME='{buildingname}' and aptno='{aptno}'").FirstOrDefault();
                connection.Close();
                return tenantid;
            }
        }

        public string BtnForceDeleteAndInsertTempPayments(string vnotxt, string buildingname, string aptnotxt, string tenantname, string voucherdate, string rentdatefrom, string rentdateto, string paymentcategory, string cashtxt, string knettxt, string chequetxt, string bttxt, string chequenotxt, string chequebankname, string receivedamt, string remarks, string receivedamttxt, string mrent)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var delete = connection.Execute($"delete from temppayments where pname='{buildingname}'");
                var insert = connection.Execute($"insert into temppayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,remarks,MRENT) values('Single','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chequetxt}','{bttxt}','{chequenotxt}','{chequebankname}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{receivedamttxt}','HO','Template','{mrent}')");
                connection.Close();
                if (delete == 1 && insert == 1)
                {
                    return "Success";
                }
                else if (delete == 0 && insert == 1)
                {
                    return "Only Insert success";
                }
                else if (delete == 1 && insert == 0)
                {
                    return "Only Delete success";
                }
                else
                {
                    return "Failed";
                }
            }
        }

        public string BtnForceInsertPaPaymentsAndPayments(string vnotxt, string buildingname, string aptnotxt, string tenantname, string voucherdate, string rentdatefrom, string rentdateto, string paymentcategory, string cashtxt, string knettxt, string chequetxt, string bttxt, string chequenotxt, string chequebankname, string receivedamt, string remarks, string receivedamttxt, string mrent,int tenantid,string monthlyrent,string chqbtamt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var insert1 = connection.Execute($"insert into papayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,mrent,REMARKS) values('Ontime','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chequetxt}','{bttxt}','{chequenotxt}','{chequebankname}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{receivedamttxt}','HO','{mrent}','{HttpContext.Session.GetString("CurrentUsername")}')");
                var insert2 = connection.Execute($"insert into payments(entrymode,rno,pname,aptno,tname,mrent,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,entryuser,tid) values('On Time','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{monthlyrent}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chqbtamt}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{Environment.MachineName}','{tenantid}')");
                connection.Close();
                if (insert1 == 1 && insert2 == 1)
                {
                    return "Inserted";
                }
                else if(insert1 == 1 && insert2 == 0)
                {
                    return "Inserted into papayments";
                }
                else if (insert1 == 0 && insert2 == 1)
                {
                    return "Inserted into payments";
                }
                else
                {
                    return "Failed";
                }
            }
        }

        public string Btn14PreviewDeleteAndInsertTempPayments(string vnotxt, string buildingname, string aptnotxt, string tenantname, string voucherdate, string rentdatefrom, string rentdateto, string paymentcategory, string cashtxt, string knettxt, string chequetxt, string bttxt, string chequenotxt, string chequebankname, string receivedamt, string remarks, string receivedamttxt, string mrent,bool chknewdepagainstolddep,string oldvouchernotxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var delete = connection.Execute($"delete from temppayments where pname='{buildingname}'");
                int insert = 0;
                if (chknewdepagainstolddep)
                {
                    insert = connection.Execute($"insert into temppayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,remarks,MRENT,oldsd) values('Single','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chequetxt}','{bttxt}','{chequenotxt}','{chequebankname}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{receivedamttxt}','HO','Template','{mrent}',{oldvouchernotxt})");
                }
                else
                {
                    insert = connection.Execute($"insert into temppayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,remarks,MRENT) values('Single','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chequetxt}','{bttxt}','{chequenotxt}','{chequebankname}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{receivedamttxt}','HO','Template','{mrent}')");
                }
                connection.Close();
                if (delete == 1 && insert == 1)
                {
                    return "Success";
                }
                else if (delete == 0 && insert == 1)
                {
                    return "Only Insert success";
                }
                else if (delete == 1 && insert == 0)
                {
                    return "Only Delete success";
                }
                else
                {
                    return "Failed";
                }
            }
        }

        public string Btn15PrintInsertPapaymentsPayments(string vnotxt, string buildingname, string aptnotxt, string tenantname, string voucherdate, string rentdatefrom, string rentdateto, string paymentcategory, string cashtxt, string knettxt, string chequetxt, string bttxt, string chequenotxt, string chequebankname, string receivedamt, string remarks, string receivedamttxt, string mrent, string monthlyrent, string chqbtamt, string Newrno, int tenantid, bool chknewdepagainstolddep, string oldvouchernotxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
                string message = "";
                try
                {
                    connection.Open();
                    var insert1 = connection.Execute($"insert into papayments(entrymode,rno,pname,aptno,tname,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,bt,chqno,bankname,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,amtinwords,remstatus,mrent,REMARKS) values('Ontime','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chequetxt}','{bttxt}','{chequenotxt}','{chequebankname}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{receivedamttxt}','HO','{mrent}','{HttpContext.Session.GetString("CurrentUsername")}')");
                    int insert2 = 0;
                    if (chknewdepagainstolddep)
                    {
                        insert2 = connection.Execute($"insert into payments(entrymode,rno,pname,aptno,tname,mrent,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,entryuser,tid,oldsd) values('On Time','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{monthlyrent}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chqbtamt}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{Environment.MachineName}','{tenantid}','{oldvouchernotxt}')");
                    }
                    else
                    {
                        insert2 = connection.Execute($"insert into payments(entrymode,rno,pname,aptno,tname,mrent,rdate,rentdatefrom,rentdateto,paymenttype,cash,knet,cheque,totamt,description,doc,month,year,COLLECTIONMONTH,COLLECTIONYEAR,entryuser,tid) values('On Time','{vnotxt}','{buildingname}','{aptnotxt}','{tenantname}','{monthlyrent}','{voucherdate}','{rentdatefrom}','{rentdateto}','{paymentcategory}','{cashtxt}','{knettxt}','{chqbtamt}','{receivedamt}','{remarks}',getdate(),'{Convert.ToDateTime(rentdatefrom).Month}','{Convert.ToDateTime(rentdatefrom).Year}','{Convert.ToDateTime(voucherdate).Month}','{Convert.ToDateTime(voucherdate).Year}','{Environment.MachineName}','{tenantid}')");
                    }
                    var delete1 = connection.Execute($"delete from maxvoucherno where pname='{buildingname}'");
                    var insert3 = connection.Execute($"insert into maxvoucherno(no,pname)values('{Newrno}','{buildingname}')");
                    var delete2 = connection.Execute("delete from temppayments");
                    connection.Close();
                    
                    if (insert1 == 1 && insert2 == 1 && insert3 == 1 && delete1 == 1 && delete2 == 1)
                    {
                        message= "Success";
                        logger.Info("Success");
                    }
                    else if (insert1 == 0 && insert2 == 1 && insert3 == 1 && delete1 == 1 && delete2 == 1)
                    {
                        logger.Info("insert1 Failed");
                        message= "insert1 Failed";
                    }
                    else if (insert1 == 1 && insert2 == 0 && insert3 == 1 && delete1 == 1 && delete2 == 1)
                    {
                        logger.Info("insert2 Failed");
                        message= "insert2 Failed";
                    }
                    else if (insert1 == 1 && insert2 == 1 && insert3 == 0 && delete1 == 1 && delete2 == 1)
                    {
                        logger.Info("insert3 Failed");
                        message= "insert3 Failed";
                    }
                    else if (insert1 == 1 && insert2 == 1 && insert3 == 1 && delete1 == 0 && delete2 == 1)
                    {
                        logger.Info("delete1 Failed");
                        message= "delete1 Failed";
                    }
                    else if (insert1 == 1 && insert2 == 1 && insert3 == 1 && delete1 == 1 && delete2 == 0)
                    {
                        logger.Info("delete2 Failed");
                        message= "delete2 Failed";
                    }
                    else
                    {
                        logger.Info("Failed");
                        message= "Failed";
                    }
                }
                catch(Exception ex)
                {
                    logger.Info(ex);
                }
                return message;
            }
        }

        public string Btn15PrintMaxst(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var maxst = connection.Query<string>($"SELECT case when MAX(RNO) is null then 0 else MAX(RNO) end AS MAXRNO FROM PAYMENTS WHERE PNAME='{buildingname}' AND year(doc)='{DateTime.Today.Year}'").FirstOrDefault();
                connection.Close();
                return maxst;
            }
        }

        public string Btn15PrintMaxbulk(string buildingname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var maxbulk = connection.Query<string>($"SELECT case when MAX(RNO) is null then 0 else MAX(RNO) end AS MAXRNO FROM Bulkprinting WHERE PNAME='{buildingname}'").FirstOrDefault();
                connection.Close();
                return maxbulk;
            }
        }

        public IActionResult CASHFLOW()
        {
            return View();
        }

        public JsonResult BtnOkCASHFLOW(string propertyname, DateTime date)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                CASHFLOWmodel model = new();
                int pmf = 0, monrent = 0;
                var recordcount = connection.Query<int>($"select count(*) as record from payments where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}' and paymenttype in('RENT','ADDITIONAL RENT PAYMENT')").FirstOrDefault();
                if(recordcount > 0)
                {
                    monrent = connection.Query<int>($"select sum(totamt) as rec from payments where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}' and paymenttype in('RENT','ADDITIONAL RENT PAYMENT')").FirstOrDefault();
                    var reccount = connection.Query<int>($"select count(*)  as recpmf from pmf where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}'").FirstOrDefault();
                    
                    if (reccount > 0)
                    {
                        pmf = connection.Query<int>($"select pmfamt from pmf where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}'").FirstOrDefault();
                    }
                }
                model.totrenttxt = monrent.ToString("0.000");
                model.totalrenttxt = monrent.ToString("0.000");
                model.pmftxt = pmf.ToString("0.000");

                var monexp = 0;
                var recordcount1 = connection.Query<int>($"select count(*) as rec from pmesaccounts where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}'").FirstOrDefault();
                if (recordcount1 > 0)
                {
                    monexp = connection.Query<int>($"select sum(totamt) as totpme from pmesaccounts where pname='{propertyname}' and month='{date.Month}' and year='{date.Year}'").FirstOrDefault();
                }
                else
                {
                    monexp = 0;
                }
                model.mexptxt= monexp.ToString("0.000");

                var totperiodexp = 0;
                var recordcount2 = connection.Query<int>($"select count(*) as rec from pmesst where pname='{propertyname}' and month(smonth)='{date.Month}' and year(smonth)='{date.Year}' and exptype='Period'").FirstOrDefault();
                if (recordcount2 > 0)
                {
                    totperiodexp = connection.Query<int>($"select sum(amt) as totperiod from periodexpensesentry where pmeid in(select id from pmesst where pname='{propertyname}' and month(smonth)='{date.Month}' and year(smonth)='{date.Year}' and exptype='Period') and month='{Convert.ToDateTime(date.Month).ToString("MMMM")}' and year='{date.Year}'").FirstOrDefault();
                    model.categorygrid = new();
                    model.categorygrid = connection.Query<categorygridmodel>($"select cast(amt as decimal(34,3)) as amt,(select category from pmesst where id=pmeid ) as cat from periodexpensesentry where pmeid in(select id from pmesst where pname='{propertyname}' and month(smonth)='{date.Month}' and year(smonth)='{date.Year}' and exptype='Period')  and month='{Convert.ToDateTime(date.Month).ToString("MMMM")}' and year='{date.Year}'").ToList();
                }
                model.cbexptxt = totperiodexp.ToString("0.000");

                connection.Close();

                var totepenses = pmf + monexp + totperiodexp;
                model.totalexptxt= totepenses.ToString("0.000");
                return Json(model);
            }
            
        }

    }

    public class loadoldvouchernodetailsModel
    {
        public string rno { get; set; }
        public string totamt { get; set; }
        public string rdate { get; set; }
    }

    public class chkallaptModel
    {
        public int totapt { get; set; }
        public string Tname { get; set; }
        public string crent { get; set; }
    }

    public class showcurrentrbModel
    {
        public string Tname { get; set; }
        public int tid { get; set; }
        public string crent { get; set; }
    }

    public class showvacatedrbModel
    {
        public string te_tname { get; set; }
        public int id { get; set; }
        public bool vacatedrb { get; set; }
        public string message { get; set; }
    }

    //public class reservedlcselectedindexModel
    //{
    //    public string te_tname { get; set; }
    //    public int id { get; set; }
    //}

    public class StatementsChangeEventPMESaccountsModel
    {
        public string pmemode { get; set; }
        public decimal tot { get; set; }
    }

    public class CategorygridModel
    {
        public string cat { get; set; }
        public string amt { get; set; }
    }

    public class IncomeExpensesModel
    {
        public string monrent { get; set; }
        public string pmf { get; set; }
        public string monexp { get; set; }
        public string totperiodexp { get; set; }
        public List<CategorygridModel> categorygrid { get; set; }
    }
    
    public class PropertymasterModel
    {
        public string bldgName { get; set; }
        public string aptNo { get; set; }
        public string cleaseno { get; set; }
        public string cname { get; set; }
        public string crent { get; set; }
        public string compaddress { get; set; }
    }

    public class LoadPayVoucherByLoiNoModel
    {
        public decimal rentpaid { get; set; }
        public decimal deppaid { get; set; }
        public decimal resfpaid { get; set; }
        public decimal LLRESFPAID { get; set; }
    }

    public class AptChangeEventTempReceiptNoModel
    {
        public string cname { get; set; }
        public string leasestart { get; set; }
        public string leaseend { get; set; }
    }
}
