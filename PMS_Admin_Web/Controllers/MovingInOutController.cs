using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models;
using Dapper;
using System.Data.SqlClient;
using PMS_Admin_Web.Models.MovingInOut;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using System.IO;

namespace PMS_Admin_Web.Controllers
{
    public class MovingInOutController : Controller
    {
        private RealtorContext context = new();
        private Connection sqlConnectionString = new();

        public MovingInOutController(RealtorContext _context)
        {
            context = _context;
        }

        public List<string> LoadPaNames()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var palists = connection.Query<string>($@"Select distinct PA_NAME PaName from PALIST").ToList();
                connection.Close();
                return palists;
            }
        }
        public List<string> LoadProperties()
        {
            List<string> properties;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                properties = connection.Query<string>(@$"select distinct BldgName from propertymaster where PropertySource='ManagedProperty'").ToList();
                connection.Close();
                return properties;
            }
        }

        public List<string> LoadApartmentNo(string propertyname)
        {
            List<string> aptno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                aptno = connection.Query<string>(@$"select AptNo from propertymaster where BldgName='{propertyname}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public JsonResult LoadRefNo(string propertyname, string aptno)
        {
            //List<string> refno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {

                connection.Open();
                //var refno = connection.Query<RefNoLoadModel>(@$"select p.propertyRef propertyRef,p.cname cname,p.crent crent,reservedfor tenname,leaseno lcno,reservedrent rent,convert(date,rlstart,103) lstart,convert(date,rlend,103) lend
                //                                                from propertymaster p
                //where BldgName='{propertyname}' and p.AptNo='{aptno}'").FirstOrDefault();

                var refno = connection.Query<RefNoLoadModel>($@"select vacatingstatus,PropertyRef,resf,status,cname,CONVERT(date,leasestart,106) as leasestart,CONVERT(date,leaseend ,106) as leaseend
                                                                from propertymaster 
                                                                where PropertySource='ManagedProperty' 
                                                                and BldgName='{propertyname}' and AptNo='{aptno}'").FirstOrDefault();

                RefNoLoadModel model = new();
                model = refno;


                if (refno.leasestart.ToString() == null)
                {
                    model.leasestart1 = null;
                }
                else
                {
                    //DateTime ls = Convert.ToDateTime(refno.leasestart);
                    var startdate = refno.leasestart.ToString("yyyy-MM-dd");
                    model.leasestart1 = startdate;
                }

                if (refno.leaseend.ToString() == null)
                {
                    model.leaseend1 = null;
                }
                else
                {
                    //DateTime le = Convert.ToDateTime(refno.leaseend);
                    var enddate = refno.leaseend.ToString("yyyy-MM-dd");
                    model.leaseend1 = enddate;
                }

                connection.Close();
                return Json(model);
            }
        }

        public IActionResult AttendanceList(PALoginModel pALoginModel)
        {
            
            //pALoginModel.palogins = context.Palogins.Where(s => s.Logtime == DateTime.Now).ToList();

            //pALoginModel.palists = context.Palists.Select(s => s.PaName).Distinct().ToList();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                pALoginModel.palogins = connection.Query<Palogin>("select * from palogin where CONVERT(date,LOGTIME) = CONVERT(date,getdate())").ToList();
                //pALoginModel.palogins = connection.Query<Palogin>($"select * from palogin where CONVERT(date,LOGTIME) = Format(getdate(), 'yyyy-MM-dd')").ToList(); 
                pALoginModel.palists = connection.Query<Palist>($@"Select distinct PA_NAME PaName from PALIST").ToList();
                connection.Close();
            }

            return View(pALoginModel);
        }

        public async Task<IActionResult> NewVacating(NewVacatingModel newVacatingModel)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            if (newVacatingModel.moveout != null)
            {
                try
                {
                    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                    {
                        connection.Open();
                        //using (var trans = connection.BeginTransaction())
                        {
                            var reservationstatus = await connection.QueryAsync<string>($"select reservation from propertymaster where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}'");//,null, trans
                            //if(reservationstatus.FirstOrDefault() == "yes")
                            {
                                var rlease = await connection.QueryAsync<Propertymaster>($"select rlstart,rlend from propertymaster where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}'");//, null, trans
                                if (newVacatingModel.moveoutDate >= DateTime.Today)
                                {
                                    if (reservationstatus.FirstOrDefault() == "yes")
                                    {
                                        if (newVacatingModel.rleasestart < newVacatingModel.moveoutDate)
                                        {
                                            ViewBag.Message = "This property is reserved by this time. Contact Admin or change the Moveoutdate";
                                            goto gotoreturn;
                                            //return View();
                                        }
                                        else
                                        {
                                            //using (var trans = connection.BeginTransaction())
                                            {
                                                var id = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Aptno}','{newVacatingModel.moveout.Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");//, null, trans

                                                //if (!string.IsNullOrEmpty(newVacatingModel.moveout.Vn))
                                                //{
                                                //    //have to insert filename into vn of moveout using the id
                                                //}

                                                await connection.ExecuteAsync($"update propertymaster set updated='YES',moveoutremarks='{newVacatingModel.moveoutRemark}',vacatingstatus='Vacating',moveout=convert(datetime2,'{newVacatingModel.moveoutDate.ToString("yyyy-MM-dd")}',126),moveoutid={id.FirstOrDefault()} where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}' ");//, null, trans

                                                ViewBag.Message = "Successfully Updated";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //using (var trans = connection.BeginTransaction())
                                        {
                                            var id = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Aptno}','{newVacatingModel.moveout.Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");//, null, trans

                                            //if (!string.IsNullOrEmpty(newVacatingModel.moveout.Vn))
                                            //{
                                            //    //have to insert filename into vn of moveout using the id
                                            //}

                                            var update = await connection.ExecuteAsync($"update propertymaster set updated='YES',moveoutremarks='{newVacatingModel.moveoutRemark}',vacatingstatus='Vacating',moveout=convert(datetime2,'{newVacatingModel.moveoutDate.ToString("yyyy-MM-dd")}',126),moveoutid={id.FirstOrDefault()} where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}' ");//, null, trans

                                            ViewBag.Message = "Successfully Updated";
                                        }
                                    }
                                }
                                else
                                {
                                    await connection.ExecuteAsync($"update propertymaster set leaseend=convert(datetime2,'{newVacatingModel.moveoutDate.ToString("yyyy-MM-dd")}',126) where PropertyRef='{newVacatingModel.moveout.Pref}' and leaseend is null ");//, null, trans

                                    var propertymaster = connection.Query<Propertymaster>(@$"select aptno,cname,crent,cftype,cbtype,
                                                                                        CONVERT(VARCHAR(11),leasestart,106) as leasestart,
                                                                                        CONVERT(VARCHAR(11),leaseend,106) as leaseend,cnat,cmob,cleaseno 
                                                                                        from propertymaster 
                                                                                        where PropertyRef='{newVacatingModel.moveout.Pref}'").FirstOrDefault();//, null, trans

                                    if (reservationstatus.FirstOrDefault() == "yes")
                                    {
                                        if (newVacatingModel.rleasestart < newVacatingModel.moveoutDate)
                                        {
                                            ViewBag.Message = "This property is reserved by this time. Contact Admin or change the Moveoutdate";
                                            goto gotoreturn;
                                            //return View();
                                        }
                                        else
                                        {
                                            //using (var trans = connection.BeginTransaction())
                                            {
                                                var id = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Aptno}','{newVacatingModel.moveout.Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");//, null, trans

                                                //if (!string.IsNullOrEmpty(newVacatingModel.moveout.Vn))
                                                //{
                                                //    //have to insert filename into vn of moveout using the id
                                                //}

                                                await connection.ExecuteAsync(@$"insert into tenantshistory(pname,pref,aptno,ftype,btype,TENANTNAME,nationality,
                                                                        contact,rent,lease_no,leasestart,leaseend,movedate,remarks,doc,moveoutid,status)
                                                                        values('{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Aptno}',
                                                                        '{propertymaster.Cftype}','{propertymaster.Cbtype}','{newVacatingModel.moveout.Tenantname}','{propertymaster.Cnat}','{propertymaster.Cmob}','{propertymaster.Crent}',
                                                                        '{propertymaster.Cleaseno}','{propertymaster.Leasestart}','{propertymaster.Leaseend}',convert(datetime2,'{newVacatingModel.moveoutDate.ToString("yyyy-MM-dd")}',126),
                                                                        '{newVacatingModel.moveoutRemark}',getdate(),'{id.FirstOrDefault()}','PENDING')");//, null, trans

                                                await connection.ExecuteAsync(@$"update propertymaster set moveoutremarks='',moveout=null,vacatingstatus='',moveoutid='0',
                                                                        cleaseno='',cname='',cmob='',cnat='',crent='',cftype='',cbtype='',leasestart=null,leaseend=null 
                                                                        where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}' ");//, null, trans

                                                ViewBag.Message = "Successfully Updated";
                                            }

                                        }
                                    }
                                    else
                                    {
                                        //using (var trans = connection.BeginTransaction())
                                        {
                                            var id = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Aptno}','{newVacatingModel.moveout.Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");//, null, trans

                                            //if (!string.IsNullOrEmpty(newVacatingModel.moveout.Vn))
                                            //{
                                            //    //have to insert filename into vn of moveout using the id
                                            //}

                                            await connection.ExecuteAsync(@$"insert into tenantshistory(pname,pref,aptno,ftype,btype,TENANTNAME,nationality,
                                                                        contact,rent,lease_no,leasestart,leaseend,movedate,remarks,doc,moveoutid,status)
                                                                        values('{newVacatingModel.moveout.Pname}','{newVacatingModel.moveout.Pref}','{newVacatingModel.moveout.Aptno}',
                                                                        '{propertymaster.Cftype}','{propertymaster.Cbtype}','{newVacatingModel.moveout.Tenantname}','{propertymaster.Cnat}','{propertymaster.Cmob}','{propertymaster.Crent}',
                                                                        '{propertymaster.Cleaseno}','{propertymaster.Leasestart}','{propertymaster.Leaseend}',convert(datetime2,'{newVacatingModel.moveoutDate.ToString("yyyy-MM-dd")}',126),
                                                                        '{newVacatingModel.moveoutRemark}',getdate(),'{id.FirstOrDefault()}','PENDING')");//, null, trans

                                            await connection.ExecuteAsync(@$"update propertymaster set moveoutremarks='',moveout=null,vacatingstatus='',moveoutid='0',
                                                                        cleaseno='',cname='',cmob='',cnat='',crent='',cftype='',cbtype='',leasestart=null,leaseend=null 
                                                                        where PropertySource='ManagedProperty' and PropertyRef='{newVacatingModel.moveout.Pref}' ");//, null, trans

                                            ViewBag.Message = "Successfully Updated";
                                        }

                                    }

                                    await connection.ExecuteAsync(@$"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby)
                                                                values('NEW VACATING','{newVacatingModel.moveout.Pname} Apt - {newVacatingModel.moveout.Aptno}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}')");//, null, trans

                                    //mail sending to also be done same as in the application
                                }
                            }
                        }
                        
                        connection.Close();
                        //ModelState.Clear();
                        newVacatingModel = new NewVacatingModel();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Unknown Error";
                    logger.Info(ex);
                }
            }
            gotoreturn:
            //return View(newVacatingModel);
            return View();
        }

        public IActionResult MovingInInventory(/*int page = 1, int pageSize = 10*/)
        {
            List<MoveInInventoryViewModel> moveInInventoryViewModels = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                moveInInventoryViewModels = connection.Query<MoveInInventoryViewModel>($@"SELECT propertyref PropertyRef,BldgName PropertyName,aptno AptNo,rftype Type,loino LeaseNo,reservedfor TenantName,rmob ContactNo,rstatus Status,rlstart LeaseStartDate,
                                                                                        CONVERT(VARCHAR(11),indate,106) as MoveInDate ,
                                                                                        rid FROM (select leasetype,propertyref,BldgName,aptno,rftype ,leaseno,case when left(leaseno,2)='LO' then leaseno when left(leaseno,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=leaseno) END AS loino,reservedfor,rmob,rstatus,
                                                                                        CONVERT(VARCHAR(11),rlstart,106) as rlstart,'1' as rid,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =propertymaster.leaseno) as indate from propertymaster  where reservation ='yes' union all SELECT  (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno  when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno ,
                                                                                        CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'2' as rid,(select top 1 CONVERT(VARCHAR(11),movingindate,106)  from movingin where leaseno =propertymaster.cleaseno) as indate  from propertymaster where DATEPART (MM ,leasestart )=DATEPART (MM,GETDATE()) AND DATEPART (YYYY ,leasestart )=DATEPART (YYYY,GETDATE()) union all SELECT (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno  when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno,
                                                                                        CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'3' as rid,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =propertymaster.cleaseno) as indate 
                                                                                        from propertymaster 
                                                                                        where DATEPART (MM ,leasestart )= month(DATEADD(DAY, -30, GETDATE())) 
                                                                                        AND DATEPART (YYYY ,leasestart )= year(DATEADD(DAY, -30, GETDATE())) )SRC where leasetype='New LC' 
                                                                                        ORDER BY convert(datetime, RLSTART, 103)  DESC").ToList();
                connection.Close();
            }
            //PagedList<PMS_Admin_Web.Models.MovingInOut.MoveInInventoryViewModel> model = new PagedList<PMS_Admin_Web.Models.MovingInOut.MoveInInventoryViewModel>(moveInInventoryViewModels, page, pageSize);
            return View(moveInInventoryViewModels);
        }

        public async Task<string> PaLogin(DateTime? logindateTime, string remarks, string PaNamelog)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            { 
                //if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    connection.Open();
                    //try
                    //{
                        var dept = connection.Query<string>($"select Department from users where Usrname='{PaNamelog}'").FirstOrDefault();
                        var exist = await connection.QueryAsync<int>($"select count(*) from PALOGIN where paname='{PaNamelog}' and convert(date,LOGTIME)=CONVERT(DATE, '{logindateTime}') and logmode='LOGIN'");
                        if (exist.FirstOrDefault() <= 0)
                        {
                            //var insert = await connection.ExecuteAsync(@$"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{dept}','{PaNamelog}','LOGIN',CONVERT(datetime,'{logindateTime}', 103),'{remarks}','PRESENT','{Environment.MachineName.ToString()}')");
                            var insert = await connection.ExecuteAsync(@$"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{dept}','{PaNamelog}','LOGIN','{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd hh:mm:ss tt")}','{remarks}','PRESENT','{Environment.MachineName.ToString()}')");
                            if (insert > 0)
                            {
                                ViewBag.Message = "Logged In Successfully";
                            }
                            else
                            {
                                ViewBag.Message = "Logged In Failed";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Login Already Exist";
                        }
                    //}
                    //catch(Exception ex)
                    //{
                    //    ViewBag.Message = "Unknown Error";
                    //}
                    connection.Close();
                }
            }
            return ViewBag.Message;
        }

        public async Task<string> PaLogOut(DateTime? logindateTime, string remarks,string PaNamelog)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    connection.Open();
                    //try
                    //{
                        var dept = connection.Query<string>($"select Department from users where Usrname='{PaNamelog}'").FirstOrDefault();
                        //var exist = connection.Query<int>($"select count(*) from PALOGIN where paname='{PaNamelog}' and convert(date,LOGTIME)=CONVERT(DATE, '{logindateTime}',103) and logmode='LOGOUT'").FirstOrDefault();
                        var exist = connection.Query<int>($"select count(*) from PALOGIN where paname='{PaNamelog}' and convert(date,LOGTIME)='{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd")}' and logmode='LOGOUT'").FirstOrDefault();
                        if (exist <= 0)
                        {
                            //var insert = await connection.ExecuteAsync(@$"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{dept}','{PaNamelog}','LOGOUT',CONVERT(datetime,'{logindateTime}', 103),'{remarks}','PRESENT','{Environment.MachineName.ToString()}')");
                            var insert = await connection.ExecuteAsync(@$"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{dept}','{PaNamelog}','LOGOUT','{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd hh:mm:ss tt")}','{remarks}','PRESENT','{Environment.MachineName.ToString()}')");
                            if (insert > 0)
                            {
                                ViewBag.Message = "Logged Out Successfully";
                            }
                            else
                            {
                                ViewBag.Message = "Logging Out Failed";
                            }
                        }
                        else
                        {
                            var delete = await connection.ExecuteAsync($"DELETE from PALOGIN where paname='{PaNamelog}' and convert(date,LOGTIME)='{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd")}' and logmode='LOGOUT'");
                            //var delete = await connection.ExecuteAsync($"DELETE from PALOGIN where paname='{PaNamelog}' and convert(date,LOGTIME)='{Convert.ToDateTime(logindateTime)}' and logmode='LOGOUT'");
                            if (delete > 0)
                            {
                                var insert = await connection.ExecuteAsync(@$"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{dept}','{PaNamelog}','LOGOUT','{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd hh:mm:ss tt")}','{remarks}','PRESENT','{Environment.MachineName.ToString()}')");
                                if (insert > 0)
                                {
                                    ViewBag.Message = "Logged Out Successfully";
                                }
                                else
                                {
                                    ViewBag.Message = "Logging Out Failed";
                                }
                            }
                            else
                            {
                                ViewBag.Message = "Unknown Error";
                            }
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    ViewBag.Message = "Unknown Error";
                    //}
                    connection.Close();
                }
            }
            return ViewBag.Message;
        }
        
        public string PaAbsent(DateTime? logindateTime, string remarks, string paname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    connection.Open();
                    //try
                    //{
                        var exist = connection.Query<int>($"select count(*) from PALOGIN where paname='{paname}' and convert(date,LOGTIME)=CONVERT(DATE, '{logindateTime}') and logmode='ABSENT'").FirstOrDefault();
                        if (exist <= 0)
                        {
                            //var insert = connection.Execute(@$"INSERT INTO PALOGIN(PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{paname}','ABSENT',CONVERT(datetime,'{logindateTime}', 103),'{remarks}','ABSENT','{Environment.MachineName.ToString()}')");
                            var insert = connection.Execute(@$"INSERT INTO PALOGIN(PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{paname}','ABSENT','{Convert.ToDateTime(logindateTime).ToString("yyyy-MM-dd hh:mm:ss tt")}','{remarks}','ABSENT','{Environment.MachineName.ToString()}')");
                            if (insert > 0)
                            {
                                ViewBag.Message = "Done";
                            }
                            else
                            {
                                ViewBag.Message = "Failed";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Already Exist";
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    ViewBag.Message = "Unknown Error";
                    //}
                    connection.Close();
                }
            }
            return ViewBag.Message;
        }

        public async Task<string> PaUpdateAttendance(int id, string remarks)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    connection.Open();
                    //try
                    //{
                        var update = await connection.ExecuteAsync($"UPDATE PALOGIN SET REMARKS='{remarks}' where ID={id}");
                        if(update==1)
                            ViewBag.Message = "Updated Successfully";
                        else
                            ViewBag.Message = "Failed to Update";
                    //}
                    //catch (Exception ex)
                    //{
                    //    ViewBag.Message = "Unknown Error";
                    //}
                    connection.Close();
                }
            }
            return ViewBag.Message;
        }

        public async Task<string> DeleteAttendanceList(int id)
        {
            //try
            //{
                //var delete = context.Palogins.Find(id);
                //context.Palogins.Remove(delete);
                //context.SaveChanges();

                using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var delete = await connection.ExecuteAsync($"delete from PALOGIN where ID={id}");
                    if (delete == 1)
                        ViewBag.Message = "Deleted Successfully";
                    else
                        ViewBag.Message = "Deletion Failed";

                    connection.Close();
                }
                
            //}
            //catch(Exception ex)
            //{
            //    ViewBag.Message = "Unknown Error";
            //}
            return ViewBag.Message;
        }

        public async Task<IActionResult> MovingInDetails(string pref,string rid, MovingInDetailsModel movingInDetailsModel,IFormFile invrpttxt,IFormFile invlisttxt,IFormFile keystxt,IFormFile acktxt, IFormFile anytxt)
        {
            //MovingInDetailsModel movingInDetailsModel = new();
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("movingindetails started");
            using (var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (movingInDetailsModel.propertymaster == null && movingInDetailsModel.movingin == null)
                    {
                        string leaseno = null, pRef = null;
                        if (rid == "2")
                        {
                            var propertymaster = await connection.QueryAsync<Propertymaster>($"select BldgName,aptno,propertyref,cname,cmob,cleaseno,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend from propertymaster where propertyref='{pref}'");
                            movingInDetailsModel.propertymaster = propertymaster.FirstOrDefault();
                            leaseno = propertymaster.FirstOrDefault().Cleaseno;
                            pRef = propertymaster.FirstOrDefault().PropertyRef;
                            movingInDetailsModel.varref = pRef;
                            
                            logger.Info("rid=2");
                        }
                        if (rid == "1")
                        {
                            var propertymaster = await connection.QueryAsync<Propertymaster>($"Select BldgName,aptno,propertyref,RESERVEDFOR,Rmob,leaseno,CONVERT(VARCHAR(11),RLSTART,106) as leasestart,CONVERT(VARCHAR(11),RLEND,106) as leaseend from propertymaster where propertyref='{pref}'");
                            movingInDetailsModel.propertymaster = propertymaster.FirstOrDefault();
                            leaseno = propertymaster.FirstOrDefault().Leaseno;
                            pRef = propertymaster.FirstOrDefault().PropertyRef;
                            movingInDetailsModel.varref = pRef;
                            
                            logger.Info("rid=1");
                        }
                        if (rid == "3")
                        {
                            var propertymaster = await connection.QueryAsync<Propertymaster>($"Select BldgName,aptno,propertyref,cname,cmob,cleaseno,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend from propertymaster where propertyref='{pref}'");
                            movingInDetailsModel.propertymaster = propertymaster.FirstOrDefault();
                            leaseno = propertymaster.FirstOrDefault().Cleaseno;
                            pRef = propertymaster.FirstOrDefault().PropertyRef;
                            movingInDetailsModel.varref = pRef;
                            
                            logger.Info("rid=3");
                        }

                        var leaseId = connection.Query<int>($"select id from lcinfo where lc_no='{leaseno}'").FirstOrDefault();
                        movingInDetailsModel.varlid = leaseId;
                        logger.Info($"leaseid={movingInDetailsModel.varlid}");

                        var recordCount = connection.Query<int>($"select count(*) from movingin where leaseid={leaseId} and pref='{pRef}'").FirstOrDefault();
                        if ((recordCount) > 0)
                        {
                            var movingin = await connection.QueryAsync<Movingin>($"select CONVERT(VARCHAR(11),movingindate,106) AS movingindate, inventorylist,inventoryreport ,keys ,receipt,other,movingin as Movingin1,remarks from movingin where leaseid='{leaseId}' and pref='{pRef}'");
                            movingInDetailsModel.movingin = movingin.FirstOrDefault();
                            if(movingInDetailsModel.movingin.Movingindate == null)
                            {
                                movingInDetailsModel.movingin.Movingindate = DateTime.Now;
                            }
                            logger.Info("recordcount>0 selecting from movingin");
                        }
                        else
                        {
                            var insertMovingIn = await connection.ExecuteAsync($"insert into movingin(leaseno,leaseid,pref,doc) values('{leaseno}','{leaseId}','{pRef}',getdate())");
                            logger.Info("recordcount<0 insesrting into movingin");
                        }
                    }
                    else
                    {
                        string leaseNo = "";
                        if (movingInDetailsModel.propertymaster.Cleaseno != null)
                        {
                            leaseNo = movingInDetailsModel.propertymaster.Cleaseno;
                        }
                        else
                        {
                            leaseNo = movingInDetailsModel.propertymaster.Leaseno;
                        }

                        string inventoryReport = "", inventoryList = "", keys = "", receipt = "", other = "";

                        string invrptpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movingin/invrpt");
                        if (!Directory.Exists(invrptpath))
                            Directory.CreateDirectory(invrptpath);
                        if (invrpttxt != null)
                        {
                        //    ViewBag.Message = "BROWSE THE NEW FILE";
                        //    //return View();
                        //    goto gotoreturn;
                        //}
                        //else
                        //{
                            string invrptextension = Path.GetExtension(invrpttxt.FileName);
                            string invrptfileName = movingInDetailsModel.varlid + movingInDetailsModel.varref + invrptextension;
                            string invrptfileNameWithPath = Path.Combine(invrptpath, invrptfileName);
                            if (System.IO.File.Exists(invrptfileNameWithPath))
                            {
                                string fileNameWithPathTemp = invrptfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                                //fileNameWithPath = fileNameWithPathTemp;
                            }
                            var invrptstream = new FileStream(invrptfileNameWithPath, FileMode.Create);
                            invrpttxt.CopyTo(invrptstream);
                            invrptstream.Close();
                            inventoryReport = invrptfileNameWithPath;
                            movingInDetailsModel.movingin.Inventoryreport = invrptfileNameWithPath;
                        }

                        string invlistpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movingin/invlist");
                        if (!Directory.Exists(invlistpath))
                            Directory.CreateDirectory(invlistpath);
                        if (invlisttxt != null)
                        {
                        //    ViewBag.Message = "BROWSE THE NEW FILE";
                        //    //return View();
                        //    goto gotoreturn;
                        //}
                        //else
                        //{
                            string invlistextension = Path.GetExtension(invlisttxt.FileName);
                            string invlistfileName = movingInDetailsModel.varlid + movingInDetailsModel.varref + invlistextension;
                            string invlistfileNameWithPath = Path.Combine(invlistpath, invlistfileName);
                            if (System.IO.File.Exists(invlistfileNameWithPath))
                            {
                                string fileNameWithPathTemp = invlistfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                                //fileNameWithPath = fileNameWithPathTemp;
                            }
                            var invliststream = new FileStream(invlistfileNameWithPath, FileMode.Create);
                            invlisttxt.CopyTo(invliststream);
                            invliststream.Close();
                            inventoryList = invlistfileNameWithPath;
                            movingInDetailsModel.movingin.Inventorylist = invlistfileNameWithPath;
                        }

                        string keyspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movingin/keys");
                        if (!Directory.Exists(keyspath))
                            Directory.CreateDirectory(keyspath);
                        if (keystxt != null)
                        {
                        //    ViewBag.Message = "BROWSE THE NEW FILE";
                        //    //return View();
                        //    goto gotoreturn;
                        //}
                        //else
                        //{
                            string keysextension = Path.GetExtension(keystxt.FileName);
                            string keysfileName = movingInDetailsModel.varlid + movingInDetailsModel.varref + keysextension;
                            string keysfileNameWithPath = Path.Combine(keyspath, keysfileName);
                            if (System.IO.File.Exists(keysfileNameWithPath))
                            {
                                string fileNameWithPathTemp = keysfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                                //fileNameWithPath = fileNameWithPathTemp;
                            }
                            var keysstream = new FileStream(keysfileNameWithPath, FileMode.Create);
                            keystxt.CopyTo(keysstream);
                            keysstream.Close();
                            keys = keysfileNameWithPath;
                            movingInDetailsModel.movingin.Keys = keysfileNameWithPath;
                        }

                        string acpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movingin/ac");
                        if (!Directory.Exists(acpath))
                            Directory.CreateDirectory(acpath);
                        if (acktxt != null)
                        {
                        //    ViewBag.Message = "BROWSE THE NEW FILE";
                        //    // return View();
                        //    goto gotoreturn;
                        //}
                        //else
                        //{
                            string acextension = Path.GetExtension(acktxt.FileName);
                            string acfileName = movingInDetailsModel.varlid + movingInDetailsModel.varref + acextension;
                            string acfileNameWithPath = Path.Combine(acpath, acfileName);
                            if (System.IO.File.Exists(acfileNameWithPath))
                            {
                                string fileNameWithPathTemp = acfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                                //fileNameWithPath = fileNameWithPathTemp;
                            }
                            var acstream = new FileStream(acfileNameWithPath, FileMode.Create);
                            acktxt.CopyTo(acstream);
                            acstream.Close();
                            receipt = acfileNameWithPath;
                            movingInDetailsModel.movingin.Receipt = acfileNameWithPath;
                        }

                        string otherpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/movingin/other");
                        if (!Directory.Exists(otherpath))
                            Directory.CreateDirectory(otherpath);
                        if (anytxt != null)
                        {
                            string otherextension = Path.GetExtension(anytxt.FileName);
                            string otherfileName = movingInDetailsModel.varlid + movingInDetailsModel.varref + otherextension;
                            string otherfileNameWithPath = Path.Combine(otherpath, otherfileName);
                            if (System.IO.File.Exists(otherfileNameWithPath))
                            {
                                string fileNameWithPathTemp = otherfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                                //fileNameWithPath = fileNameWithPathTemp;
                            }
                            var otherstream = new FileStream(otherfileNameWithPath, FileMode.Create);
                            anytxt.CopyTo(otherstream);
                            otherstream.Close();
                            other = otherfileNameWithPath;
                        }

                        var leaseId = connection.Query<int>($"select id from lcinfo where lc_no='{leaseNo}'").FirstOrDefault();
                        if (movingInDetailsModel.movingin.Movingindate.HasValue)
                        {
                            var update = await connection.ExecuteAsync($"update movingin set RENTED='YES',movingin='{movingInDetailsModel.movingin.Movingin1}',movingindate='{Convert.ToDateTime(movingInDetailsModel.movingin.Movingindate).ToString("yyyy-MM-dd")}',remarks='{movingInDetailsModel.movingin.Remarks}',inventoryreport='{inventoryReport}',inventorylist='{inventoryList}',keys='{keys}',receipt='{receipt}',other='{other}' where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}' ");
                        }
                        else
                        {
                            var update = await connection.ExecuteAsync($"update movingin set RENTED='YES',movingin='{movingInDetailsModel.movingin.Movingin1}',movingindate=null,remarks='{movingInDetailsModel.movingin.Remarks}',inventoryreport='{inventoryReport}',inventorylist='{inventoryList}',keys='{keys}',receipt='{receipt}',other='{other}' where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}' ");
                        }


                        //if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                        //{
                        //    if (movingInDetailsModel.movingin.Inventoryreport != null)
                        //    {
                        //        inventoryReport = connection.Query<string>($"select inventoryreport from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if(inventoryReport == null || inventoryReport == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Inventory Report";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Inventorylist != null)
                        //    {
                        //        inventoryList = connection.Query<string>($"select inventorylist from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if(inventoryList == null || inventoryList == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Inventory List";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Keys != null)
                        //    {
                        //        keys = connection.Query<string>($"select keys from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if(keys == null || keys == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Keys Acknowledgement";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Receipt != null)
                        //    {
                        //        receipt = connection.Query<string>($"select receipt from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if(receipt == null || receipt == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Access Cards";
                        //            return View();
                        //        }
                        //    }

                        //    ViewBag.Message = "MovingIN details updated successfully with email generation";
                        //}
                        //else
                        //{
                        //    if (movingInDetailsModel.movingin.Inventoryreport != null)
                        //    {
                        //        inventoryReport = connection.Query<string>($"select inventoryreport from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if (inventoryReport == null || inventoryReport == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Inventory Report";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Inventorylist != null)
                        //    {
                        //        inventoryList = connection.Query<string>($"select inventorylist from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if (inventoryList == null || inventoryList == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Inventory List";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Keys != null)
                        //    {
                        //        keys = connection.Query<string>($"select keys from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if (keys == null || keys == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Keys Acknowledgement";
                        //            return View();
                        //        }
                        //    }
                        //    if (movingInDetailsModel.movingin.Receipt != null)
                        //    {
                        //        receipt = connection.Query<string>($"select receipt from movingin where leaseno='{leaseNo}' and leaseid={leaseId} AND PREF='{movingInDetailsModel.propertymaster.PropertyRef}'").FirstOrDefault();
                        //        if (receipt == null || receipt == "")
                        //        {
                        //            ViewBag.Message = "Upload the Document for Access Cards";
                        //            return View();
                        //        }
                        //    }
                        //    ViewBag.Message = "MovingIN details updated successfully with email generation";
                        //}
                        ViewBag.Message = "MovingIN details updated successfully with email generation";
                    }
                }
                catch(Exception e)
                {
                    logger.Info(e);
                }
                gotoreturn:
                connection.Close();
            }

            return View(movingInDetailsModel);
        }

        

        public string MovingInDetailsMailGeneration(string pname,string aptno,string tname,string tleaseno,string tlstart,string tlend,string miidate,string rem, int varlid,string varref,string hiddeninvlisttxt,string hiddeninvrpttxt,string hiddenkeystxt,string hiddenacktxt)//string invrpttxt,string invlisttxt,string keystxt,string acktxt
        {
            string message = "";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string tomailtxt = "", ccmailtxt = "";
                string doc1 = "", doc2 = "", doc3 = "", doc4 = "";

                if (hiddeninvlisttxt == null)
                {
                    doc1 = connection.Query<string>($"select inventoryreport from movingin where leaseno='{tleaseno}' and leaseid={varlid} AND PREF='{varref}'").FirstOrDefault();
                    if (doc1 == null)
                    {
                        message = "Upload the Document for Inventory Report";
                        connection.Close();
                        goto gotoreturn;
                    }
                }
                if (hiddeninvrpttxt == null)
                {
                    doc2 = connection.Query<string>($"select inventorylist from movingin where leaseno='{tleaseno}' and leaseid={varlid} AND PREF='{varref}'").FirstOrDefault();
                    if (doc2 == null)
                    {
                        message = "Upload the Document for Inventory List";
                        connection.Close();
                        goto gotoreturn;
                    }
                }
                if (hiddenkeystxt == null)
                {
                    doc3 = connection.Query<string>($"select keys from movingin where leaseno='{tleaseno}' and leaseid={varlid} AND PREF='{varref}'").FirstOrDefault();
                    if (doc3 == null)
                    {
                        message = "Upload the Document for Keys Acknowledgement";
                        connection.Close();
                        goto gotoreturn;
                    }
                }
                if (hiddenacktxt == null)
                {
                    doc4 = connection.Query<string>($"select receipt from movingin where leaseno='{tleaseno}' and leaseid={varlid} AND PREF='{varref}'").FirstOrDefault();
                    if (doc4 == null)
                    {
                        message = "Upload the Document for Access Cards";
                        connection.Close();
                        goto gotoreturn;
                    }
                }

                if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    var tomail = connection.Query<string>("select email from paemail where status='MIIto' order by id").ToList();
                    foreach(var mailid in tomail)
                    {
                        if (tomailtxt == "")
                        {
                            tomailtxt = mailid;
                        }
                        else
                        {
                            tomailtxt = tomailtxt + ';' + mailid;
                        }
                    }
                    var ccmail = connection.Query<string>("select email from paemail where status='MIIcc' order by id").ToList();
                    foreach(var mailid in ccmail)
                    {
                        if (ccmailtxt == "")
                        {
                            ccmailtxt = mailid;
                        }
                        else
                        {
                            ccmailtxt = ccmailtxt + ';' + mailid;
                        }
                    }

                    Microsoft.Office.Interop.Outlook.Application Outlook = new Microsoft.Office.Interop.Outlook.Application();

                    string strMsg1 = "";
                    strMsg1 = strMsg1 + @$"<html><body><size=12><font face=""Arial Narrow"">Dear All:<br><br>This is the confirmation that the Moving in done in PMS for the above mentioned subject.<br><br>Tenant Name&nbsp;&nbsp;&nbsp;&nbsp;:{tname}<br>Lease NO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tleaseno}<br>Lease Start&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tlstart}<br>Lease End&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tlend}<br><br>Moving In Date&nbsp;:{miidate}<br><br>{rem} <br><br><FONT COLOR=BLUE><B>THIS EMAIL IS GENERATED THROUGH PMS</B></FONT></body></html>";
                    var Mail = Outlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
                    Mail.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                    Mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
                    Mail.Subject = $"{pname} Apt-{aptno}";
                    Mail.To = tomailtxt;
                    Mail.CC = ccmailtxt;
                    Mail.HTMLBody = strMsg1;
                    message = "Mail Generated";
                    ((Microsoft.Office.Interop.Outlook._MailItem)Mail).Display();
                }
                else
                {
                    var tomail = connection.Query<string>("select mailid from emailusers where dept='facilities' and mailstatus in('MIIto') order by id").ToList();
                    foreach (var mailid in tomail)
                    {
                        if (tomailtxt == "")
                        {
                            tomailtxt = mailid;
                        }
                        else
                        {
                            tomailtxt = tomailtxt + ';' + mailid;
                        }
                    }
                    var ccmail = connection.Query<string>("select mailid from emailusers where dept='facilities' and mailstatus in('MIIcc') order by id").ToList();
                    foreach (var mailid in ccmail)
                    {
                        if (ccmailtxt == "")
                        {
                            ccmailtxt = mailid;
                        }
                        else
                        {
                            ccmailtxt = ccmailtxt + ';' + mailid;
                        }
                    }

                    Microsoft.Office.Interop.Outlook.Application Outlook = new Microsoft.Office.Interop.Outlook.Application();

                    string strMsg1 = "";
                    strMsg1 = strMsg1 + @$"<html><body><size=12><font face=""Arial Narrow"">Dear All:<br><br>This is the confirmation that the Moving in done in PMS for the above mentioned subject.<br><br>Tenant Name&nbsp;&nbsp;&nbsp;&nbsp;:{tname}<br>Lease NO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tleaseno}<br>Lease Start&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tlstart}<br>Lease End&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:{tlend}<br><br>Moving In Date&nbsp;:{miidate}<br><br>{rem} <br><br><FONT COLOR=BLUE><B>THIS EMAIL IS GENERATED THROUGH PMS</B></FONT></body></html>";
                    var Mail = Outlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
                    Mail.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
                    Mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
                    Mail.Subject = $"{pname} Apt-{aptno}";
                    Mail.To = tomailtxt;
                    Mail.CC = ccmailtxt;
                    Mail.HTMLBody = strMsg1;
                    
                    Mail.Attachments.Add(hiddeninvlisttxt);
                    message = "Mail Generated";
                    ((Microsoft.Office.Interop.Outlook._MailItem)Mail).Display();
                }

                connection.Close();
            }
            gotoreturn:
            return message;
        }

        public ActionResult ViewLOI(string leaseno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string file = "";
                if (leaseno.Substring(1, 1) == "O")
                {
                    file = connection.Query<string>($"select loipath from loiinformation where loi_no='{leaseno}'").FirstOrDefault();
                    connection.Close();
                    
                }

                if (leaseno.Substring(1, 1) == "C")
                {
                    var mainloino = connection.Query<string>($"select loi_no from lcinfo where lc_no='{leaseno}'").FirstOrDefault();
                    file = connection.Query<string>($"select loipath from loiinformation where loi_no='{mainloino}'").FirstOrDefault();
                    connection.Close();
                }

                if (file != "")
                {
                    byte[] abc = System.IO.File.ReadAllBytes(file);
                    System.IO.File.WriteAllBytes(file, abc);
                    MemoryStream ms = new MemoryStream(abc);
                    return new FileStreamResult(ms, "application/pdf");
                }
                else
                {
                    ViewBag.Message = "LOI document is still not uploaded";
                    //return RedirectToAction("MovingInDetails", "MovingInOut");
                    return View();
                }

            }
        }

        public ActionResult ViewLC(string leaseno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select lcpath from lcinfo where lc_no='{leaseno}'").FirstOrDefault();
                connection.Close();
                if (file != "" && file != null)
                {
                    byte[] abc = System.IO.File.ReadAllBytes(file);
                    System.IO.File.WriteAllBytes(file, abc);
                    MemoryStream ms = new MemoryStream(abc);
                    return new FileStreamResult(ms, "application/pdf");
                }
                else
                {
                    ViewBag.Message = "LC document is still not uploaded";
                    //return RedirectToAction("MovingInDetails", "MovingInOut");
                    return View();
                }
            }
        }

        public JsonResult CheckRentPaid(string propertyName,string aptNo, DateTime moveOutDate)
        {
            CheckRentPaidModel model = new();
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select COUNT(*) from payments where pname='{propertyName}' and aptno='{aptNo}' and month='{moveOutDate.Month}' and year='{moveOutDate.Year}' and paymenttype='RENT'").FirstOrDefault();
                if (recordcount > 0)
                {
                    var payments = connection.Query<Payment>($"select totamt,rno,remarks,month,year,CONVERT(VARCHAR(11),rdate,106) as rdate from payments where pname='{propertyName}' and aptno='{aptNo}' and month='{moveOutDate.Month}' and year='{moveOutDate.Year}' and paymenttype='RENT'").FirstOrDefault();
                    model.rno = payments.Rno;
                    model.totamt = payments.Totamt;
                    model.remarks = payments.Remarks;
                    model.rdate = payments.Rdate;
                    model.month = payments.Month;
                    model.year = payments.Year;
                    model.message = "";
                }
                else
                {
                    model.message = "No records Found";
                }
            }
            return Json(model);
        }
    }

    public class CheckRentPaidModel
    {
        public int? rno { get; set; }
        public decimal totamt { get; set; }
        public string remarks { get; set; }
        public DateTime? rdate { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public string message { get; set; }
    }
}
