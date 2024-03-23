using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_Admin_Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Controllers
{
    public class CommonController : Controller
    {
        private Connection sqlConnectionString = new();
        private RealtorContext context = new();

        public CommonController(RealtorContext _context)
        {
            context = _context;
        }

        private static String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public string ConvertAmount(string amount)
        {
            try
            {
                Int64 amount_int = (Int64)Convert.ToInt32(amount);
                Int64 amount_dec = (Int64)Math.Round((Convert.ToInt32(amount) - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertTo(amount_int) + " Only.";
                }
                else
                {
                    return ConvertTo(amount_int) + " Point " + ConvertTo(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertTo(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + ConvertTo(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + ConvertTo(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertTo(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + ConvertTo(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertTo(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertTo(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertTo(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertTo(i % 10000000) : "");
            }
            return ConvertTo(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertTo(i % 1000000000) : "");
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                if (oldPassword != null && newPassword != null)
                {
                    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                    {
                        connection.Open();
                        var exist = connection.Query<User>($"select * from users where Password='{oldPassword}' and Usrname='{HttpContext.Session.GetString("CurrentUsername")}'").FirstOrDefault();
                        if (exist != null)
                        {
                            connection.Execute($"update users set Password='{newPassword}' where Usrname='{HttpContext.Session.GetString("CurrentUsername")}'");
                            ViewBag.Message = "Updated";
                        }
                        else
                        {
                            ViewBag.Message = "Account does not exist";
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Fields are empty";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }

            return ViewBag.Message;
        }

        public JsonResult LoadRefNoForRentChange(string propertySource,string propertyName,string aptNo)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var refno = connection.Query<Propertymaster>($"select PropertyRef,RESF,location,cname,cast(crent as decimal(34,3)) as crent from propertymaster where PropertySource='{propertySource}' and BldgName='{propertyName}' and AptNo='{aptNo}'").FirstOrDefault();
                connection.Close();
                return Json(refno);
            }
        }

        public JsonResult PaymentView(string loiNo)
        {
            string LoiNo;
            if (loiNo == null)
                LoiNo = null;
            else
                LoiNo = loiNo;

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ViewPaymentModel viewPaymentModel = new();
                connection.Open();

                var loiinfo = connection.Query<Loiinformation>($"select payremarks,PROPERTYSOURCE,LLRESF,ClientRESF,lename,LoiNo,inqno,loipath,CONVERT(VARCHAR(11),leasedate,106) as Leasestart,CONVERT(VARCHAR(11),Leaseenddate,106) as Leaseend,RENT,DEPOSIT,loi_no,clientname,aptno,propertyname from loiinformation where loi_no='{LoiNo}'").FirstOrDefault();
                var rentamt = loiinfo.Rent;
                var depositamt = loiinfo.Deposit;
                var clresf = loiinfo.ClientResf;
                var LLRESF = loiinfo.Llresf;

                var recordcount = connection.Query<int>($"select count(*) from PAYMENTVOUCHER where loi_no='{LoiNo}'").FirstOrDefault();

                if (recordcount > 0)
                {
                    viewPaymentModel.RentPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RENT'").FirstOrDefault();
                    viewPaymentModel.DepositPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='DEPOSIT'").FirstOrDefault();
                    viewPaymentModel.ClResfPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RESF'").FirstOrDefault();
                    viewPaymentModel.LlResfPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RESF-LL'").FirstOrDefault();
                    var paymentvoucherList = connection.Query<ViewPaymentListModel>(@$"select pname,aptno,tname,lc_no lcno,loi_no,[amt-type] as VoucherType, 
                                                                                                cast(amt as decimal(34,3)) AS Amt,
                                                                                                cast(cash as decimal(34,3)) as Cash, 
                                                                                                cast(knet as decimal(34,3)) as Knet, 
                                                                                                cast(cheque as decimal(34,3)) as Cheque,
                                                                                                cast(BT as decimal(34,3)) as BT,
                                                                                                CONVERT(VARCHAR(11),datefrom,106) as DateFrom,
                                                                                                CONVERT(VARCHAR(11),dateto,106) as DateTo,comments Remarks
                                                                                                from paymentvoucher 
                                                                                                where loi_no='{LoiNo}'").ToList();
                    viewPaymentModel.viewPaymentListModels = new();
                    foreach (var item in paymentvoucherList)
                    {
                        DateTime fromdate = Convert.ToDateTime(item.DateFrom);
                        DateTime todate = Convert.ToDateTime(item.DateTo);

                        ViewPaymentListModel model = new();
                        model.pname = item.pname;
                        model.aptno = item.aptno;
                        model.tname = item.tname;
                        model.lcno = item.lcno;
                        model.VoucherType = item.VoucherType;
                        model.Amt = item.Amt;
                        model.Cash = item.Cash;
                        model.Knet = item.Knet;
                        model.Cheque = item.Cheque;
                        model.BT = item.BT;
                        model.DateFrom = fromdate.ToString("dd-MM-yyyy");
                        model.DateTo = todate.ToString("dd-MM-yyyy");
                        model.Remarks = item.Remarks;

                        viewPaymentModel.viewPaymentListModels.Add(model);
                    }

                    viewPaymentModel.RentDue = (rentamt - Convert.ToDecimal(viewPaymentModel.RentPaid)).ToString();
                    viewPaymentModel.DepositDue = (depositamt - Convert.ToDecimal(viewPaymentModel.DepositPaid)).ToString();
                    viewPaymentModel.ClResfDue = (clresf - Convert.ToDecimal(viewPaymentModel.ClResfPaid)).ToString();
                    viewPaymentModel.LlResfDue = (LLRESF - Convert.ToDecimal(viewPaymentModel.LlResfPaid)).ToString();
                }

                connection.Close();
                return Json(viewPaymentModel);
            }
        }

        public List<string> LoadLOINos()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loinocombo = connection.Query<string>("SELECT DISTINCT LOI_NO as rec FROM LOIINFORMATION where loi_no is not null").ToList();
                connection.Close();
                return loinocombo;
            }
        }

        public List<string> LoadAptNo()
        {
            List<string> aptno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                aptno = connection.Query<string>($"select AptNo from propertymaster where BldgName='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public List<string> LoadAptNumber(string propertyname)
        {
            List<string> aptno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                aptno = connection.Query<string>($"select AptNo from propertymaster where BldgName='{propertyname}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public List<string> LoadCategory()
        {
            List<string> categories;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                categories = connection.Query<string>($"select distinct ordertype from ot order by ordertype asc").ToList();
                connection.Close();
                return categories;
            }
        }

        public List<string> LoadStatus()
        {
            List<string> statuses;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                statuses = connection.Query<string>($"SELECT DISTINCT category FROM maintenacecat").ToList();
                connection.Close();
                return statuses;
            }
        }

        public List<string> LoadNationality()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var Nationalities = connection.Query<string>($"select * from nationality").ToList();
                connection.Close();
                return Nationalities;
            }
        }

        public List<string> LoadAreaName()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var areaNames = connection.Query<string>($"select AreaName from Area Order by AreaName").ToList();
                connection.Close();
                return areaNames;
            }
        }

        public List<string> LoadSource()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var sources = connection.Query<string>($"select source from source Order by Source").ToList();
                connection.Close();
                return sources;
            }
        }

        public List<string> LoadLeDetails()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var le = connection.Query<string>($"select LEname,lemail from LEInfo order by LEname").ToList();
                connection.Close();
                return le;
            }
        }

        public List<string> LoadOtherSource()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var othersource = connection.Query<string>($"select distinct name from othersource").ToList();
                connection.Close();
                return othersource;
            }
        }

        public List<string> SourceProgressLoadSource()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var sources = connection.Query<string>($"select distinct source from (select distinct source from cgl union select distinct source from pgl) src").ToList();
                connection.Close();
                return sources;
            }
        }

        public List<string> SourceProgressLoadOtherSource()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var sources = connection.Query<string>($"select distinct othersource from (select distinct othersource from cgl union select distinct othersource from pgl) src").ToList();
                connection.Close();
                return sources;
            }
        }

        public string LoadPglRefNo()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string refno = "";
                var reccount = connection.Query<int>($"select count(*) from pgl WHERE YEAR(DOC)=YEAR(GETDATE()) ").FirstOrDefault();
                if(reccount == 0)
                {
                    refno = "PGL" + DateTime.Today.Year + "-001";
                }
                else
                {
                    var nexid = connection.Query<int>("select MAX(RIGHT(PGLREFNO,LEN(PGLREFNO)-8))+1 from Pgl where LEFT(Pglrefno,7)='PGL'+CONVERT(varchar(10), YEAR(GETDATE()))").FirstOrDefault();
                    if(nexid <= 9)
                        refno = "PGL" + DateTime.Today.Year + "-00" + nexid;

                    else if(nexid <= 99 && nexid > 9)
                        refno = "PGL" + DateTime.Today.Year + "-0" + nexid;

                    else if(nexid > 99)
                        refno = "PGL" + DateTime.Today.Year + "-" + nexid;
                    
                }
                connection.Close();
                return refno;
            }
        }

        public string LoadCglRefNo()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string refno = "";
                var reccount = connection.Query<int>($"select count(*) from cgl WHERE YEAR(DOC)=YEAR(GETDATE()) ").FirstOrDefault();
                if (reccount == 0)
                {
                    refno = "CGL" + DateTime.Today.Year + "-001";
                }
                else
                {
                    var nexid = connection.Query<int>("select MAX(RIGHT(CGLREFNO,LEN(CGLREFNO)-8))+1 from cgl where LEFT(cglrefno,7)='CGL'+CONVERT(varchar(10), YEAR(GETDATE()))").FirstOrDefault();
                    if (nexid <= 9)
                        refno = "CGL" + DateTime.Today.Year + "-00" + nexid;

                    else if (nexid <= 99 && nexid > 9)
                        refno = "CGL" + DateTime.Today.Year + "-0" + nexid;

                    else if (nexid > 99)
                        refno = "CGL" + DateTime.Today.Year + "-" + nexid;

                }
                connection.Close();
                return refno;
            }
        }

        public List<string> LoadReportingRemarks()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<string> remarks = new();
                remarks = connection.Query<string>($"select remarks from reportingremarks").ToList();
                connection.Close();
                return remarks;
            }
        }

        public string LoadPropertyRefNoMarketing(string propertySource)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string refno = "";
                if (propertySource == "ManagedProperty")
                {
                    var MPRECCOUNT = connection.Query<int>("SELECT COUNT(*) FROM PROPERTYMASTER WHERE PropertySource='ManagedProperty'").FirstOrDefault();
                    MPRECCOUNT = MPRECCOUNT + 1;
                    refno = "MPL" + DateTime.Today.Year + "-00" + MPRECCOUNT;
                }
                else if(propertySource == "StandAloneProperty")
                {
                    var SPRECCOUNT = connection.Query<int>("SELECT right(propertyref,len(propertyref)-9)+1 AS NEWID from PROPERTYMASTER WHERE PropertySource='StandAloneProperty' and id=(select max(id) from PROPERTYMASTER WHERE PropertySource='StandAloneProperty')").FirstOrDefault();
                    refno = "PL" + DateTime.Today.Year + "-00" + SPRECCOUNT;
                }
                connection.Close();
                return refno;
            }
        }

        public List<string> LoadDriver()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<string> drivers = connection.Query<string>($"select DriverName from drivers").ToList();
                connection.Close();
                return drivers;
            }
        }

        public List<string> LoadLeName()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<string> lenames = connection.Query<string>($"select LEname from LEInfo order by LEname").ToList();
                connection.Close();
                return lenames;
            }
        }

        public List<string> LoadPropertyName()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<string> propertynames = connection.Query<string>($"select distinct BldgName from propertymaster where PropertySource='ManagedProperty'").ToList();
                connection.Close();
                return propertynames;
            }
        }

        public List<string> LoadUserNameMarketing()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //List<string> usernames = connection.Query<string>($"select DISTINCT USRNAME from users").ToList();
                List<string> usernames = connection.Query<string>($"select DISTINCT USRNAME from users where Department='marketing'").ToList();
                connection.Close();
                return usernames;
            }
        }

        public string LoadPasswordAccess()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var password = connection.Query<string>($"select apppwd from passwordaccess").FirstOrDefault();
                connection.Close();
                return password;
            }
        }

        public List<string> LoadCgls()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var cgls = connection.Query<string>($"select cglrefno from cgl where inquirystatus='Open' order by id desc").ToList();
                connection.Close();
                return cgls;
            }
        }

        public List<string> LoadPgls()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var pgls = connection.Query<string>($"select pglrefno from pgl where inquirystatus='Open' order by id desc").ToList();
                connection.Close();
                return pgls;
            }
        }

        public string AppPassword()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var apppwd = connection.Query<string>($"select apppwd from passwordaccess").FirstOrDefault();
                connection.Close();
                return apppwd;
            }
        }

        public List<string> LoiNotCancelled()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loi = connection.Query<string>($"select lOI_no from loiinformation where LOISTATUS NOT IN('Cancelled') AND DATEPART(yyyy,LOIDATE)=year(getdate()) order by lOI_no desc").ToList();
                connection.Close();
                return loi;
            }
        }

        public List<string> LoadAptNoAccReceivable(string propertyname)
        {
            List<string> aptno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                aptno = connection.Query<string>($"select distinct AptNo from accountspm where pname='{propertyname}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public List<string> LoadPnameFromSublease()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var pnames = connection.Query<string>($"select distinct PNAME from subleases order by PNAME").ToList();
                connection.Close();
                return pnames;
            }
        }

        public List<string> LoadAccCategoryExpenses()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var category = connection.Query<string>($"select distinct category from accounts_category_expenses").ToList();
                connection.Close();
                return category;
            }
        }

        public List<string> LoadCategories()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var category = connection.Query<string>($"select distinct categories from category").ToList();
                connection.Close();
                return category;
            }
        }

        public ActionResult ViewDocument(string path)
        {
            byte[] abc = System.IO.File.ReadAllBytes(path);
            System.IO.File.WriteAllBytes(path, abc);
            MemoryStream ms = new MemoryStream(abc);
            return new FileStreamResult(ms, "application/pdf");
        }

        //public string UploadFile(string section, IFormFile file, string IformfileFilename, string filename, string docNo)
        //{
        //    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Images/{section}");
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);
        //    FileInfo fileInfo = new FileInfo(IformfileFilename);
        //    string fileName = filename + docNo + fileInfo.Extension;
        //    string fileNameWithPath = Path.Combine(path, fileName);
        //    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
        //    {
        //        file.CopyTo(stream);
        //    }
        //    return fileNameWithPath;
        //}

    }
}
