using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models.Property;
//using PMS_Admin_Web.Tables;
using System.Data.SqlClient;
using Dapper;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.LOI;
using PMS_Admin_Web.Models.LC;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace PMS_Admin_Web.Controllers
{
    public class MenuViewController : Controller
    {
        //private q8RealtorContext context = new q8RealtorContext();
        private RealtorContext context = new RealtorContext();
        private Connection sqlConnectionString = new();

        public MenuViewController(RealtorContext _context)
        {
            context = _context;
        }

        public IActionResult Settings()
        {
            return View();
        }

        public IActionResult LOI()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                LOIModel model = new();
                model.reservegrid = connection.Query<Propertymaster>("select * from propertymaster where propertysource='ManagedProperty' and rlstart <=convert(varchar(10),getdate(),101) and rstatus='LOI Created'").ToList();
                model.asrchange = connection.Query<int>("SELECT count(*) FROM ASRCHANGES WHERE VIEWBYADMIN='0'").FirstOrDefault();
                var recordcount = connection.Query<int>($"select count(*) as rec from lcinfo where  ORGINALLCSENT <= DATEADD(day,-3, GETDATE()) and LCRECEIVEDFROMTENANTON is null and lcpath is null and year (lcmadeon)=year(getdate())").FirstOrDefault();
                if(recordcount > 0)
                {
                    model.tenantrid = connection.Query<string>("select lc_no from lcinfo where ORGINALLCSENT <= DATEADD(day,-3, GETDATE()) and LCRECEIVEDFROMTENANTON is null and lcpath is null and year (lcmadeon)=year(getdate())").ToList();
                    model.tenantlbl = "Above mentioned LC's are given to get the signature of Tenant";
                }
                connection.Close();
                return View(model);
            }
        }



        public IActionResult LC()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                LOIModel model = new();
                model.asrchange = connection.Query<int>("SELECT count(*) FROM ASRCHANGES WHERE VIEWBYADMIN='0'").FirstOrDefault();
                var recordcount = connection.Query<int>("select count(*) as rec from lcinfo where ORGINALLCSENT <= DATEADD(day,-3, GETDATE()) and LCRECEIVEDFROMTENANTON is null and lcpath is null and year (lcmadeon)=year(getdate())").FirstOrDefault();
                if(recordcount > 0)
                {
                    model.tenantrid = connection.Query<string>("select lc_no from lcinfo where ORGINALLCSENT <= DATEADD(day,-3, GETDATE()) and LCRECEIVEDFROMTENANTON is null and lcpath is null and year (lcmadeon)=year(getdate())").ToList();
                    model.tenantlbl = "Above mentioned LC's are given to get the signature of Tenant";
                }
                connection.Close();
                return View(model);
            }
            
        }

        public IActionResult Properties()
        {
            return View();
        }
        
        public IActionResult ManagedProperty()
        {
            ManagedPropertyModel model = new();

            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.ManagedProperties = connection.Query<ManagedProperty>(@$"select pm.BldgName as PropertyName, Location, count(*) as NoOfApartment, (select count(*) from propertymaster where PropertySource='ManagedProperty' and status='Available' and BldgName = pm.BldgName ) as NoOfApartmentVacant, (select count(*) from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable' and BldgName = pm.BldgName ) as NoOfApartmentOccupied
                                                                                from propertymaster pm 
                                                                                where PropertySource='ManagedProperty' 
                                                                                group by BldgName, Location 
                                                                                order by BldgName").ToList();
                
                model.TotalNoOfProperties = model.ManagedProperties.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();
                
            }
            
            return View(model);
        }

        public IActionResult ReservedApartments()
        {
            ReservedApartmentsModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.ReservedApartments = connection.Query<ReservedApartment>(@$"select id,reservedfor as ReservedUnder ,leasetype  as LCType,rnat,reservedrent as LCRent,rftype as AptType,rbtype as Bed,
                                                                                CONVERT(VARCHAR(11),rlstart,106) as LeaseStart ,leaseno as LOIorLCNo,CONVERT(VARCHAR(11),rlend,106) as LeaseEnd ,
                                                                                rmob,PropertyRef,BldgName as PropertyName,BlockName,Floors as Floor,AptNo,Bed as Beds,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,cleaseno,
                                                                                case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(reservedrent as decimal(34,3)) end as Payable,
                                                                                case when status='NotAvailable' and reservation='yes' then 'Reserved' when  status='Available' and reservation='yes' then 'Reserved' when status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and reservation='' then 'Occupied' when  status='NotAvailable' and reservation is null then 'Occupied' else status end as Status,
                                                                                cbtype,cftype,cname,case when crent is null then 0 else crent end as crent,CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend ,106) as lend,vacatingstatus,CONVERT(VARCHAR(11),moveout,106) as MoveOutOn 
                                                                                from propertymaster 
                                                                                where propertysource='ManagedProperty' and reservation='yes' order by BldgName,id").ToList();
                
                model.TotalNoOfProperties = model.ReservedApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();

            }

            return View(model);
        }

        public IActionResult VacatingApartments()
        {
            VacatingApartmentsModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.VacatingApartments = connection.Query<VacatingApartment>(@$"select reservedfor,rnat,reservedrent ,rftype ,rbtype ,
                                                                                CONVERT(VARCHAR(11),rlstart,106) as rlstart ,
                                                                                CONVERT(VARCHAR(11),rlend,106) as rlend ,
                                                                                rmob,PropertyRef,BldgName as PropertyName,
                                                                                BlockName,Floors as Floor,AptNo,Bed,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,cleaseno as LeaseNo,
                                                                                case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(crent as decimal(34,3)) end as Payable,
                                                                                case when  status='NotAvailable' and  reservation='yes' then 'Reserved' when  status='Available' and  reservation='yes' then 'Reserved' when  status is null and  reservation='yes' then 'Reserved' when status='NotAvailable' and  reservation='' then 'Occupied' when  status='NotAvailable' and  reservation is null then 'Occupied' else  status end as Status,
                                                                                cbtype,cftype as AptType,cname as TenantName,
                                                                                case when crent is null then 0 else crent end as LCRent,
                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend ,106) as LeaseEnd,
                                                                                vacatingstatus as VacatingStatus,
                                                                                CONVERT(VARCHAR(11),moveout,106) as MoveOutOn 
                                                                                from propertymaster 
                                                                                where propertysource='ManagedProperty' and vacatingstatus='Vacating' order by convert(datetime, moveout, 103) ASC").ToList();
                
                model.TotalNoOfProperties = model.VacatingApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();

            }

            return View(model);
        }

        public IActionResult AvailableApartments()
        {
            AvailableApartmentsModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.AvailableApartments = connection.Query<AvailableApartment>(@"select id,cname as TenantName,reservedfor ,rnat,crent,reservedrent ,rftype ,rbtype ,
                                                                                CONVERT(VARCHAR(11),rlstart,106) as rlstart ,CONVERT(VARCHAR(11),rlend,106) as rlend ,
                                                                                rmob,PropertyRef,BldgName as PropertyName,BlockName,Floors as Floor,AptNo,Bed,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,cleaseno as LeaseNo,
                                                                                case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(crent as decimal(34,3)) end as Payable,
                                                                                case when status='NotAvailable' and  reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when  status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and reservation='' then 'Occupied' when  status='NotAvailable' and reservation is null then 'Occupied' else status end as Status,
                                                                                cbtype,cftype as AptType,cname,case when crent is null then 0 else crent end as LCRent,
                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend ,106) as LeaseEnd,vacatingstatus as VacatingStatus,
                                                                                CONVERT(VARCHAR(11),moveout,106) as MoveOutOn 
                                                                                from propertymaster 
                                                                                where propertysource='ManagedProperty' and status='Available' and id in(select id from propertymaster where vacatingstatus not in('Vacating') or vacatingstatus is null) order by BldgName,id ").ToList();
                
                model.TotalNoOfProperties = model.AvailableApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();

            }

            return View(model);
        }

        public IActionResult TLC()
        {
            TLCModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                model.TLCs = connection.Query<TLC>(@"select pref,id,pname as PropertyName,aptno as AptNo,ftype as AptType,btype as Bed,lease_no as LeaseNo,
                                                                                tenantname as TenantName,rent as LCRent,nationality as Nationality,contact as ContactNo,
                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend,106) as LeaseEnd,
                                                                                CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
                                                                                CONVERT(VARCHAR(11),keyreturndate,106) as KeyReturnOn 
                                                                                from tenantshistory 
                                                                                order by id desc").ToList();
                
                model.TotalNoOfProperties = model.TLCs.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();

            }

            return View(model);
        }

        public IActionResult Sublease()
        {
            SubleaseModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                
                model.Subleases = connection.Query<PMS_Admin_Web.Models.Property.Sublease>(@"select cast(LCRENT as decimal(34,3)) as LCRent,NAME as TenantName,PNAME as PropertyName,APTNO as AptNo,
                                                                                CONVERT(VARCHAR(11),LSTART,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),LEND,106) as LeaseEnd,
                                                                                (SELECT FLOORS FROM PROPERTYMASTER WHERE PROPERTYREF=PREF) AS Floor
                                                                                ,LNO as LeaseNo,FTYPE AS AptType,BED AS Bed,PREF 
                                                                                FROM SUBLEASES 
                                                                                WHERE ACTIVE=1 
                                                                                order by PNAME").ToList();
                model.TotalNoOfProperties = model.Subleases.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();
                connection.Close();

            }

            return View(model);
        }

        public IActionResult ASRSearchProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<Asrchange> model = new();
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "facilities")
                {
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYSPA='0' ORDER BY ID DESC").ToList();
                }
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "Administration")
                {
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYADMIN='0' ORDER BY ID DESC").ToList();
                }
                connection.Close();
                return View(model);
            }
        }

        public JsonResult BtnViewAllASRSearchProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<Asrchange> model = new();
                connection.Open();
                model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES ORDER BY ID DESC").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BtnCurrentMonthChangeASRSearchProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<Asrchange> model = new();
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "facilities")
                {
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYSPA='0' AND MONTH(DOC)=MONTH(GETDATE()) AND YEAR(DOC)=YEAR(GETDATE()) ORDER BY ID DESC").ToList();
                    //model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE MONTH(DOC)=MONTH(GETDATE()) AND YEAR(DOC)=YEAR(GETDATE()) ORDER BY ID DESC").ToList();
                }
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "Administration")
                {
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYADMIN='0' AND MONTH(DOC)=MONTH(GETDATE()) AND YEAR(DOC)=YEAR(GETDATE()) ORDER BY ID DESC").ToList();
                    //model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE MONTH(DOC)=MONTH(GETDATE()) AND YEAR(DOC)=YEAR(GETDATE()) ORDER BY ID DESC").ToList();
                }
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BtnLastMonthChangeASRSearchProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<Asrchange> model = new();
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "facilities")
                {
                    //model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYSPA='0' AND MONTH(DOC)=MONTH(DATEADD(m, -1, getdate())) AND YEAR(DOC)=YEAR(DATEADD(m, -1, getdate())) ORDER BY ID DESC").ToList();
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE MONTH(DOC)=MONTH(DATEADD(m, -1, getdate())) AND YEAR(DOC)=YEAR(DATEADD(m, -1, getdate())) ORDER BY ID DESC").ToList();
                }
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "Administration")
                {
                    //model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE VIEWBYADMIN='0' AND MONTH(DOC)=MONTH(DATEADD(m, -1, getdate())) AND YEAR(DOC)=YEAR(DATEADD(m, -1, getdate())) ORDER BY ID DESC").ToList();
                    model = connection.Query<Asrchange>("SELECT * FROM ASRCHANGES WHERE MONTH(DOC)=MONTH(DATEADD(m, -1, getdate())) AND YEAR(DOC)=YEAR(DATEADD(m, -1, getdate())) ORDER BY ID DESC").ToList();
                }
                connection.Close();
                return Json(model);
            }
        }

        public string BtnChangesSeenASRSearchProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "facilities")
                {
                    var recordcount = connection.Query<int>("SELECT COUNT(*) AS REC FROM asrchanges WHERE viewbyspa=0").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        var update = connection.Execute("update asrchanges set viewbyspa=1 where viewbyspa=0");
                        if(update == 1)
                        {
                            message = "Changes seen";
                        }
                        else
                        {
                            message = "Unknown Error";
                        }
                    }
                    else
                    {
                        message = "No ASR changes";
                    }
                }
                if (HttpContext.Session.GetString("CurrentUserDepartment") == "Administration")
                {
                    var recordcount = connection.Query<int>("SELECT COUNT(*) AS REC FROM asrchanges WHERE viewbyadmin=0").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        var update = connection.Execute("update asrchanges set viewbyadmin=1 where viewbyadmin=0");
                        if (update == 1)
                        {
                            message = "Changes seen";
                        }
                        else
                        {
                            message = "Unknown Error";
                        }
                    }
                    else
                    {
                        message = "No ASR changes";
                    }
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult ExportRS(string buildingName)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ExportRSModel model = new();
                connection.Open();
                model.bname = buildingName;
                var monthyear = connection.Query<Rentstatement>($"SELECT MAX(MONTH) AS MONTH,MAX(YEAR) AS YEAR FROM Rentstatement WHERE YEAR=(SELECT MAX(YEAR) FROM Rentstatement WHERE pname='{buildingName}') AND pname='{buildingName}'").FirstOrDefault();
                model.MAXLBL = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthyear.Month) + "-" + monthyear.Year.ToString();

                model.detailgrid = connection.Query<ExportRSClassModel>($"select PropertyRef,AptNo,Bed,Bath,case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(crent as decimal(34,3))  end as payablerent,(select case when enqtype='Corporate Enquiry' then 'Corporate' when enqtype='Individual Enquiry' then 'Individual' end as enqtype from lcinfo where lc_no=cleaseno) as enqtype,(select case when pmode='SP' then 'Semi-Annually' when pmode='MP' then 'Monthly'  when pmode='QP' then 'Quaterly' when pmode='AP' then 'Annually' end as pmode from lcinfo where lc_no=cleaseno) as mode,cnat,case when cbtype is null then case when bed like 'M%' then 'Mulhaq' when bed like 'Stu%' then 'Mulhaq' when bed like 'R%' then 'RT' when bed like 'Sh%' then 'Shop' when bed like 'Bas%' then 'Basement' else left(bed,1)+' BDR' end when cbtype='' then case when bed like 'M%' then 'Mulhaq' when bed like 'Stu%' then 'Mulhaq' when bed like 'R%' then 'RT' when bed like 'Sh%' then 'Shop' when bed like 'Bas%' then 'Basement' else left(bed,1)+' BDR' end else  case when cbtype like 'M%' then 'Mulhaq' when cbtype like 'Stu%' then 'Mulhaq' when cbtype like 'R%' then 'RT' when cbtype like 'Sh%' then 'Shop' when cbtype like 'Bas%' then 'Basement' else left(cbtype,1)+' BDR' end  end as cbtype ,bath,case when cftype is null then case when furnished='Un Furnished' then 'UF' when furnished='Semi Furnished' then 'SF' when furnished='Fully Furnished' then 'FF' else furnished end when cftype='' then case when furnished='Un Furnished' then 'UF' when furnished='Semi Furnished' then 'SF' when furnished='Fully Furnished' then 'FF' else furnished end else case when cftype='Un Furnished' then 'UF' when cftype='Semi Furnished' then 'SF' when cftype='Fully Furnished' then 'FF' else cftype end end as cftype,case when (select enqtype from lcinfo where lc_no=cleaseno)='Corporate Enquiry' then (select compaddress from lcinfo where lc_no=cleaseno ) else cname end as cname,case when crent is null then 0 else cast(crent as decimal(34,3)) end as crent, case when (select count(*) from renewallc where leaseno =cleaseno )>0 and (select lctype from lcinfo where lc_no=cleaseno)='Renewal LC' then (select CONVERT(VARCHAR(11),leasestart,106) from renewallc where myid=(select min(myid) from renewallc where id=(select id from renewallc where leaseno=cleaseno)))else CONVERT(VARCHAR(11),leasestart,106) end as lstart, case when (select count(*) from renewallc where leaseno =cleaseno )>0 and (select lctype from lcinfo where lc_no=cleaseno)='Renewal LC' then (select CONVERT(VARCHAR(11),leaseend ,106) from renewallc where myid=(select min(myid) from renewallc where id=(select id from renewallc where leaseno=cleaseno)))else CONVERT(VARCHAR(11),leaseend ,106) end as lend,case when status='NotAvailable' and reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and reservation='' then 'Rented' when status='NotAvailable' and reservation is null then 'Rented' when status='Available' and vacatingstatus='Vacating' then 'Rented' else 'Vacant' end as status,case when propertyref in(select pref from subleases where pname='{buildingName}' and status='yes') then 'yes' else 'no' end as slease from propertymaster where BldgName='{buildingName}' and propertysource='ManagedProperty' order by orderid").ToList();
                foreach (var item in model.detailgrid)
                {
                    if(item.slease=="yes")
                    {
                        var subleasedetailgrid = connection.Query<ExportRSClassModel>($"select name cname,nationality cnat,case when lcrent is null then '0.000' else cast(lcrent as decimal(34,3)) end as crent,case when actualrent is null then '0.000' else cast(actualrent as decimal(34,3)) end as payablerent,bed cbtype,paymode mode,ftype cftype,ttype enqtype from subleases where pref='{item.propertyref}' and status='yes'").FirstOrDefault();
                        model.detailgrid.Add(subleasedetailgrid);
                    }
                }
                connection.Close();
                return View(model);
            }
        }

        public int BtnViewChanges1ExportRS(string bname,DateTime statementmonth)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var rentreporecord = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' AND month='{statementmonth.Month}' and year='{statementmonth.Year}'").FirstOrDefault();
                connection.Close();
                return rentreporecord;
            }
        }

        public JsonResult BtnViewChange2ExportRS(string bname, DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                DateTime m = statementmonth.AddMonths(1);
                var mastergrid = connection.Query<mastergridmodelExportRS>($"select propertyref,aptno,cname,cnat,cast(crent as decimal(34,3)) as crent,(select cast(payable as decimal(34,3)) from lcinfo where lc_no=cleaseno) as payable,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,cbtype ,bath,(select lctype from lcinfo where lc_no=cleaseno) as lctype from propertymaster where month(leasestart)='{m.Month}' and year(leasestart)='{m.Year}' and bldgname='{bname}'").ToList();
                connection.Close();
                return Json(mastergrid);
            }
        }

        public JsonResult BtnViewChange3ExportRS(string bname, DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var n = statementmonth;
                var tlcgrid = connection.Query<tlcgridmodelExportRS>($"select pref,id,pname,aptno,case when btype like 'Mul%' then 'Mulhaq' when btype like 'Stu%' then 'Mulhaq' when btype like 'R%' then 'RT' when btype like 'Sh%' then 'Shop' when btype like 'Bas%' then 'Basement' else left(btype,1)+' BDR' end as btype,case  when ftype='Un Furnished' then 'UF' when ftype='Semi Furnished' then 'SF' when ftype='Fully Furnished' then 'FF' else ftype  end as ftype,tenantname,nationality,contact,bath,LCrent,payable,pmode,enqtype,leasestart,leaseend,movedate, keyreturndate from (select pref,(select bath from propertymaster where propertyref=pref) as bath,id,pname,aptno,(select btype from lcinfo where lc_no=lease_no) as btype,(select fstatus from lcinfo where lc_no=lease_no) as ftype,cast(RENT as decimal(34,3)) as LCrent,(select cast(payable as decimal(34,3))  from lcinfo where lc_no=lease_no) as payable,(select Tenname  from lcinfo where lc_no=lease_no) as tenantname ,(select Nationality  from lcinfo where lc_no=lease_no) as nationality,contact, case when (select  count(*) from renewallc where leaseno =lease_no )>0 then (select  CONVERT(VARCHAR(11),leasestart,106) from renewallc where myid=(select min(myid) from renewallc where id=(select id from renewallc where leaseno=lease_no))) else (select CONVERT(VARCHAR(11),leasestart,106) from lcinfo where lc_no=lease_no)  end as leasestart, case when (select  count(*) from renewallc where leaseno =lease_no )>0 then (select  CONVERT(VARCHAR(11),leaseend,106) from renewallc where myid=(select min(myid) from renewallc where id=(select id from renewallc where leaseno=lease_no))) else (select CONVERT(VARCHAR(11),leaseend,106) from lcinfo where lc_no=lease_no) end as leaseend,(select case when pmode='SP' then 'Semi-Annually' when pmode='MP' then 'Monthly'  when pmode='QP' then 'Quaterly' when pmode='AP' then 'Annually' end as pmode from lcinfo where lc_no=lease_no) as pmode,(select case when enqtype='Corporate Enquiry' then 'Corporate' when enqtype='Individual Enquiry' then 'Individual' end as enqtype from lcinfo where lc_no=lease_no) as enqtype,CONVERT(VARCHAR(11),movedate,106) as movedate,CONVERT(VARCHAR(11),keyreturndate,106) as keyreturndate  from tenantshistory  where month(movedate)='{n.Month}' and year(movedate)='{n.Year}' and pname='{bname}' )src").ToList();
                connection.Close();
                return Json(tlcgrid);
            }
        }

        public string BtnUVRSFExportRS(string bname,string pref)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var showflat = connection.Query<string>($"select showflat from propertymaster where propertyref='{pref}' and bldgname='{bname}'").FirstOrDefault();
                connection.Close();
                return showflat.Trim();
            }
        }

        public int BtnExport1ExportRS(string bname, DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}'").FirstOrDefault();
                connection.Close();
                return recordcount;
            }
        }

        public async Task<string> BtnExport2ExportRS(string bname, DateTime statementmonth,int dglength,string[] detailgridclassaptno,string[] detailgridclassname, string[] detailgridclassnationality,string[] detailgridclassmrent,string[] detailgridclassprent,string[] detailgridclassbed,string[] detailgridclassbath,string[] detailgridclassmode,string[] detailgridclassfur,string[] detailgridclasscorpind,string[] detailgridclassleasestart,string[] detailgridclassleaseend,string[] detailgridclasspropertyref)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                int insert = 0, update1 = 0, update2 = 0;
                connection.Open();
                var delete = connection.Execute($"delete from rentstatement where pname='{bname}' and  month='{statementmonth.Month}' and year='{statementmonth.Year}'");
                for(int i=0; i<dglength; i++)
                {
                    insert = await connection.ExecuteAsync($"insert into rentstatement(pname,aptno,pref,tname,nat,rent,actualrent,bed,bath,mpay,ftype,corpinv,month,year,doc,[close],leasestart,leaseend)values('{bname}','{detailgridclassaptno[i]}','{detailgridclasspropertyref[i]}','{detailgridclassname[i]}','{detailgridclassnationality[i]}','{detailgridclassmrent[i]}','{detailgridclassprent[i]}','{detailgridclassbed[i]}','{detailgridclassbath[i]}','{detailgridclassmode[i]}','{detailgridclassfur[i]}','{detailgridclasscorpind[i]}','{statementmonth.Month}','{statementmonth.Year}',getdate(),'NO','{detailgridclassleasestart[i]}','{detailgridclassleaseend[i]}')");
                    if(detailgridclassleasestart[i].Length == 0)
                    {
                        update1 = await connection.ExecuteAsync($"update rentstatement set leasestart=null where id=(select max(id) from rentstatement where pname='{bname}') and pname='{bname}'");
                    }
                    if (detailgridclassleaseend[i].Length == 0)
                    {
                        update2 = await connection.ExecuteAsync($"update rentstatement set leaseend=null where id=(select max(id) from rentstatement where pname='{bname}') and pname='{bname}'");
                    }

                    if(insert == 1)
                    {
                        message = "Old Entry is removed and new entry is registered";
                    }
                    else
                    {
                        message = "Unknown Error";
                    }
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnExport3ExportRS(string bname, DateTime statementmonth, int dglength, string[] detailgridclassaptno, string[] detailgridclassname, string[] detailgridclassnationality, string[] detailgridclassmrent, string[] detailgridclassprent, string[] detailgridclassbed, string[] detailgridclassbath, string[] detailgridclassmode, string[] detailgridclassfur, string[] detailgridclasscorpind, string[] detailgridclassleasestart, string[] detailgridclassleaseend, string[] detailgridclasspropertyref)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                int insert = 0;
                connection.Open();
                var delete = connection.Execute($"delete from rentstatement where pname='{bname}' and  month='{statementmonth.Month}' and year='{statementmonth.Year}'");
                for (int i = 0; i < dglength; i++)
                {
                    insert = await connection.ExecuteAsync($"insert into rentstatement(pname,aptno,pref,tname,nat,rent,actualrent,bed,bath,mpay,ftype,corpinv,month,year,doc,[close],leasestart,leaseend)values('{bname}','{detailgridclassaptno[i]}','{detailgridclasspropertyref[i]}','{detailgridclassname[i]}','{detailgridclassnationality[i]}','{detailgridclassmrent[i]}','{detailgridclassprent[i]}','{detailgridclassbed[i]}','{detailgridclassbath[i]}','{detailgridclassmode[i]}','{detailgridclassfur[i]}','{detailgridclasscorpind[i]}','{statementmonth.Month}','{statementmonth.Year}',getdate(),'NO','{detailgridclassleasestart[i]}','{detailgridclassleaseend[i]}')");
                    if (insert == 1)
                    {
                        message = "New entry is Created";
                    }
                    else
                    {
                        message = "Unknown Error";
                    }
                }
                connection.Close();
                return message;
            }
        }

        public JsonResult BtnViewFromRentStatementExportRS(string bname, DateTime statementmonth)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var model = connection.Query<ViewFromRentStatementModelExportRS>($"select [close],ID,PNAME,APTNO,PREF,TNAME,NAT,BED,BATH,cast(RENT as decimal(34,3)) as RENT,cast(ACTUALRENT as decimal(34,3)) as ACTUALRENT,MPAY,FTYPE,status,CORPINV,MONTH,YEAR,UPDATED,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,case when aptno in(select aptno from subleases where pname='{bname}') then 'yes' else 'no' end as slease from rentstatement where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' order by id ").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public async Task<JsonResult> BtnVerifyWithAccountsExportRS(string bname, DateTime statementmonth, int dglength, string[] detailgridclasspropertyref, string[] detailgridclassprent, string[] detailgridclassmrent)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                BtnVerifyWithAccountsModelExportRS model = new();
                model.recordcount = new();
                model.collectedacc = new();
                model.collectedrs = new();
                model.mrentacc = new();
                model.mrentrs = new();
                for (int i = 0; i < dglength; i++)
                {
                    var recordcount = await connection.QueryAsync<int>($"select count(*) as rec from payments where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' and paymenttype='RENT' and aptno=(select aptno from accountspm where pref='{detailgridclasspropertyref[i]}' and pname='{bname}')");
                    model.recordcount.Add(recordcount.FirstOrDefault());
                    if (recordcount.FirstOrDefault() > 0)
                    {
                        var collectedacc = await connection.QueryAsync<string>($"select totamt from payments where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' and paymenttype='RENT' and aptno=(select aptno from accountspm where pref='{detailgridclasspropertyref[i]}' and pname='{bname}')");
                        model.collectedacc.Add(collectedacc.FirstOrDefault());
                        var collectedrs = detailgridclassprent[i];
                        model.collectedrs.Add(collectedrs);
                        var mrentacc = await connection.QueryAsync<string>($"select mrent from payments where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' and paymenttype='RENT' and aptno=(select aptno from accountspm where pref='{detailgridclasspropertyref[i]}' and pname='{bname}')");
                        model.mrentacc.Add(mrentacc.FirstOrDefault());
                        var mrentrs = detailgridclassmrent[i];
                        model.mrentrs.Add(mrentrs);
                    }
                    else
                    {
                        model.collectedacc.Add("");
                        model.collectedrs.Add("");
                        model.mrentacc.Add("");
                        model.mrentrs.Add("");
                    }
                }
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BtnRentFixAccountsExportRS(string bname, DateTime statementmonth, int dglength, string[] detailgridclasspropertyref, string[] detailgridclassprent)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                BtnRentFixAccountsModelExportRS model = new();
                model.collectedacc = new();
                model.collectedrs = new();
                model.detailgridprent = new();
                decimal totrent = 0;
                connection.Open();
                for (int i = 0; i < dglength; i++)
                {
                    var collectedacc = connection.Query<decimal>($"select totamt from payments where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' and aptno=(select aptno from accountspm where pref='{detailgridclasspropertyref[i]}')").FirstOrDefault();
                    model.collectedacc.Add(collectedacc);
                    var collectedrs = Convert.ToDecimal(detailgridclassprent[i]);
                    model.collectedrs.Add(collectedrs);
                    if (collectedacc != collectedrs)
                    {
                        if (Convert.ToInt16(collectedacc) > 0)
                        {
                            var detailgridprent = connection.Query<string>($"select totamt from payments where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}' and aptno=(select aptno from accountspm where pref='{detailgridclasspropertyref[i]}')").FirstOrDefault();
                            model.detailgridprent.Add(detailgridprent);
                        }
                        else
                        {
                            model.detailgridprent.Add("");
                        }
                    }
                    else
                    {
                        model.detailgridprent.Add("");
                    }
                    totrent = totrent + Convert.ToDecimal(detailgridclassprent[i]);
                    model.PAYRLBL = totrent;
                }
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BtnCheckRentChangeApprovalExportRS(string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<BtnCheckRentChangeApprovalModelExportRS> model = new();
                connection.Open();
                var rentchagecount = connection.Query<int>($"select count(*) as rec from rentchange where PREF IN(SELECT PROPERTYREF FROM PROPERTYMASTER WHERE BLDGNAME='{bname}')").FirstOrDefault();
                if(rentchagecount > 0)
                {
                    model = connection.Query<BtnCheckRentChangeApprovalModelExportRS>($"select top 10 CONVERT(VARCHAR(11),effectfrom,106) as effectfrom ,oldrent,newrent,approvednotice,id,(select aptno from propertymaster where propertyref=pref) as aptno from rentchange where PREF IN(SELECT PROPERTYREF FROM PROPERTYMASTER WHERE BLDGNAME='{bname}') order by id DESC").ToList();
                    connection.Close();
                    return Json(model);
                }
                else
                {
                    var message = "No Rent Change done for this property";
                    connection.Close();
                    return Json(message);
                }
            }
        }

        public string BtnShowRemarksExportRS(string bname, DateTime statementmonth)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string remark = "";
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from othernote where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}'").FirstOrDefault();
                if(recordcount > 0)
                {
                    remark = connection.Query<string>($"select remark from othernote where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}'").FirstOrDefault();
                }
                connection.Close();
                return remark;
            }
        }

        public async Task<string> BtnSaveRemarkExportRS(string bname, DateTime statementmonth, string remark)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = await connection.ExecuteAsync($"delete from othernote where pname='{bname}' and month='{statementmonth.Month}' and year='{statementmonth.Year}'");
                var insert = await connection.ExecuteAsync($"INSERT INTO othernote(PNAME,REMARK,month,year)VALUES('{bname}' ,'{remark}','{statementmonth.Month}','{statementmonth.Year}')");
                if(insert == 1)
                {
                    message = "Note Saved for particular month";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult RENTST(string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                RENTSTModel model = new();
                connection.Open();
                model.bname = bname;
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month =(select  max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}'))").FirstOrDefault();
                var update = connection.Execute($"update rentstatement set asr='' where pname='{bname}'");
                if(reccount == 0)
                {
                    model.accgrid = connection.Query<accgridModel>(@$"SELECT CASE WHEN TID<>0 THEN (SELECT TNAME FROM TDETAILS WHERE ID=TID) ELSE CNAME END AS TNAME , APTNO,CASE WHEN TID<>0 THEN (SELECT TNAT FROM TDETAILS WHERE ID=TID) ELSE CNAT  END AS NAT, CASE WHEN TID<>0 THEN (SELECT TBED FROM TDETAILS WHERE ID=TID) ELSE CBTYPE END AS BED, CASE WHEN TID<>0 THEN (SELECT TRENT FROM TDETAILS WHERE ID=TID) ELSE CRENT  END AS RENT, CASE WHEN TID<>0 THEN (SELECT TTYPE FROM TDETAILS WHERE ID=TID) ELSE CFTYPE END AS FTYPE,PROPERTYREF PREF FROM PROPERTYMASTER where BLDGNAME='{bname}'").ToList();
                    model.totrent = connection.Query<string>($"SELECT sum(crent) as crent FROM PROPERTYMASTER where BLDGNAME='{bname}'").FirstOrDefault();
                    //panel9 visible true
                }
                else
                {
                    model.accgrid = connection.Query<accgridModel>($"select [close],ID,PNAME,APTNO,PREF,TNAME,NAT,BED,BATH,cast(RENT as decimal(34,3)) as RENT,cast(ACTUALRENT as decimal(34,3)) as ACTRENT,MPAY,FTYPE,CORPINV,MONTH,YEAR,UPDATED,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,case when aptno in(select aptno from subleases where pname='{bname}') then 'yes' else 'no' end as slease from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month =(select max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}')) order by id").ToList();
                    var rent = connection.Query<Rentstatement>($"select sum(RENT) as rent,SUM(ACTUALRENT) as ACTUALRENT from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month =(select max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}'))").FirstOrDefault();
                    model.totrent = rent.Rent.ToString();
                    model.ACTRENT = rent.Actualrent.ToString();
                }

                model.vacant = connection.Query<string>($"select COUNT(*) AS REC from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month =(select max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}')) AND TNAME='VACANT'").FirstOrDefault();
                model.reserved = connection.Query<string>($"select COUNT(*) AS REC from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month =(select max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}')) AND TNAME='RESERVED'").FirstOrDefault();
                model.rented = connection.Query<string>($"select COUNT(*) AS REC from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}') and month=(select max(month) from Rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement where pname='{bname}')) AND TNAME NOT IN ('RESERVED','VACANT')").FirstOrDefault();

                model.aptlist = connection.Query<string>($"SELECT APTNO FROM PROPERTYMASTER WHERE BLDGNAME='{bname}'").ToList();

                connection.Close();
                return View(model);
            }
        }

        public JsonResult BtnViewRENTST(string bname, DateTime month)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                RENTSTModel model = new();
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and month='{month.Month}' and year='{month.Year}'").FirstOrDefault();
                if(reccount == 0)
                {
                    connection.Close();
                    return Json(0);
                }
                else
                {
                    model.accgrid = connection.Query<accgridModel>($"select [close],ID,PNAME,APTNO,PREF,TNAME,NAT,BED,BATH,cast(RENT as decimal(34,3)) as RENT,cast(actualrent as decimal(34,3)) as actualrent,MPAY,FTYPE,CORPINV,MONTH,YEAR,UPDATED,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,case when aptno in(select aptno from subleases where pname='{bname}') then 'yes' else 'no' end as slease from rentstatement where pname='{bname}' and month='{month.Month}' and year='{month.Year}'").ToList();
                    var rent = connection.Query<Rentstatement>($"select sum(rent) as rent,SUM(ACTUALRENT) as ACTUALRENT from rentstatement where pname='{bname}' and month='{month.Month}' and year='{month.Year}'").FirstOrDefault();
                    model.totrent = rent.Rent.ToString();
                    model.ACTRENT = rent.Actualrent.ToString();
                    connection.Close();
                    return Json(model);
                }
            }
        }

        //public JsonResult call_refreshRENTST(string bname, string smonth, string syear)
        //{
        //    return Json(0);
        //}

        public async Task<string> BtnBedUpdateSelectedRecordfromPMRENTST(string bname,int selectedids)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var pref = connection.Query<string>($"select pref from RENTSTATEMENT where id={selectedids}").FirstOrDefault();
                var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET bed=(select CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+' BR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+' BR' end  ELSE  case when left(CBTYPE,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+' BR' end END AS  CBTYPE from propertymaster where BldgName ='{bname}' and propertyref='{pref}' )  WHERE ID={selectedids} AND PNAME='{bname}'");
                if(update == 1)
                {
                    message = "Updated";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnBedUpdateAllfromPrevRSRENTST(string bname, int[] id, string[] pref, int aglength)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < aglength; i++)
                {
                    var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET bed=(select CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+' BR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+' BR' end  ELSE  case when left(CBTYPE,1)='M' then 'MULHAQ' when left(CBTYPE,1)='S' then 'SHOP' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+' BR' end END AS  CBTYPE from propertymaster where BldgName ='{bname}' and propertyref='{pref[i]}' ) WHERE ID={id[i]} AND PNAME='{bname}'");
                    if(update==0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                message = "Updated";
                
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnBathUpdateSelectedRecordRENTST(string bname, int selectedids, string bathtxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET bath='{bathtxt}' WHERE ID={selectedids} AND PNAME='{bname}'");
                if (update == 1)
                {
                    message = "Updated";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnBathUpdateAllRecordRENTST(string bname, int[] id, int aglength, string bathtxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < aglength; i++)
                {
                    var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET bath='{bathtxt}' WHERE ID={id[i]} AND PNAME='{bname}'");
                    if (update == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                message = "Updated";

                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnVacantRENTST(string bname, int selectedids)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET actualrent=0.000,TNAME='VACANT',NAT='',RENT='0.000',MPAY='',leasestart=NULL,leaseend=NULL,corpinv='' WHERE ID={selectedids} AND PNAME='{bname}'");
                if (update == 1)
                {
                    message = "Vacant Updated";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnReservedRENTST(string bname, int selectedids)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET actualrent=0.000,TNAME='RESERVED',NAT='',RENT='0.000',MPAY='',leasestart=NULL,leaseend=NULL WHERE ID={selectedids} AND PNAME='{bname}'");
                if (update == 1)
                {
                    message = "Reserved Updated";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult RentedVacating(DateTime month,string bname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                RentedVacatingModel model = new();
                connection.Open();
                var n = month.AddMonths(-1);
                n = new DateTime(n.Year, n.Month, 1);
                var m = month.AddMonths(1);
                m = new DateTime(m.Year, m.Month, 1);

                model.mastergrid = connection.Query<mastergridModel>($"select CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,aptno,CNAME,CRENT,CNAT,CFTYPE,Case when aptno like 'Sho%' then UPPER(aptno) when AptNo like 'Base%'  then UPPER(AptNo) when aptno like 'Mul%' THEN UPPER(aptno) when aptno like 'Stu%' then UPPER(aptno)  ELSE LEFT(CBTYPE,1)+' BR' end as cbtype,(select tbath from tdetails where id=tid) as bath,(select tmode from tdetails where id=tid) as tmode,(select tctype from tdetails where id=tid) as tctype,tid from propertymaster where bldgname='{bname}' and month(leasestart)=month('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and year(leasestart)=year('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and leasetype='New LC' order by aptno").ToList();
                model.rentedgrid = connection.Query<rentedgridModel>($"select pname,aptno, CONVERT(VARCHAR(11),lcdate,106) as lcdate,(select CONVERT(VARCHAR(11),leaseend,106) from propertymaster where tid=r.tid) as leaseend,(select tname from TDETAILS where id=tid) as tname,(select trent from tdetails where id=tid) as trent,(select tnat from tdetails where id=tid) as tnat,(select ttype from tdetails where id=tid) as ttype,(select case when len(tbed)=1 then tbed+' BR' else tbed end as tbed from tdetails where id=tid) as tbed,(select tbath from tdetails where id=tid) as tbath,(select tmode from tdetails where id=tid) as tmode,(select case when tctype='Corporate Enquiry' then 'Corporate' when  tctype='Individual Enquiry'  then 'Individual' end from tdetails where id=tid) as tctype from rented r where month(lcdate)=month('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and year(lcdate)=year('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and pname='{bname}' ORDER BY APTNO ").ToList();
                model.tlcgrid = connection.Query<Tenantshistory>($"select pref,id,pname,aptno,ftype,btype,tenantname,nationality,contact,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),movedate,106) as movedate,CONVERT(VARCHAR(11),keyreturndate,106) as keyreturndate from tenantshistory where month(movedate)='{n.Month}' and year(movedate)='{n.Year}' and pname='{bname}'").ToList();
                model.rgrid = connection.Query<rgridModel>($"SELECT case when ptype='SP' then 'Semi-Annually' when ptype='MP' then 'Monthly' when ptype='QP' then 'Quaterly' when ptype='AP' then 'Yearly' end as ptype,case when inqtype='Individual Enquiry' then 'Individual' when inqtype='Corporate Enquiry' then 'Corporate' else inqtype end as inqtype,leasestart ,leaseend ,APTNO,Case when aptno like 'Sho%' then UPPER(aptno) when AptNo like 'Base%'  then UPPER(AptNo) when aptno like 'Mul%' THEN UPPER(aptno) when aptno like 'Stu%' then UPPER(aptno)  ELSE LEFT(bed,1)+' BR' end as bed,TNAME,TRENT,tnat,ttype  FROM (select CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,aptno,cname as tname,crent  as trent,cnat  as tnat,cftype as ttype,cbtype as bed,case when cleaseno  like 'LC%' then (select enqtype from lcinfo where lc_no=cleaseno ) when cleaseno  like 'LOI%' then (select enquirytype from LOIInformation where loi_no=cleaseno  )else '' end as inqtype,case when cleaseno  like 'LC%' then (select pmode from lcinfo where lc_no=cleaseno ) when cleaseno  like 'LOI%' then '' else '' end as ptype from propertymaster where month(leasestart)='{m.Month}' and year(leasestart)='{m.Year}' and bldgname='{bname}' and leasetype='New LC' union  select CONVERT(VARCHAR(11),rlstart ,106) as leasestart,CONVERT(VARCHAR(11),rlend,106) as leaseend,aptno,reservedfor  as tname,reservedrent   as trent,rnat   as tnat,rftype  as ttype,rbtype as bed,case when leaseno  like 'LC%' then (select enqtype from lcinfo where lc_no=leaseno ) when leaseno  like 'LOI%' then (select enquirytype from LOIInformation where loi_no=leaseno  )else '' end as inqtype,case when leaseno  like 'LC%' then (select pmode from lcinfo where lc_no=leaseno ) when leaseno  like 'LOI%' then '' else '' end as ptype from propertymaster where rtrim(reservation)='yes' and rstatus not in('LC Renewal') and bldgname='{bname}')SRC ").ToList();
                model.ACCGRID = connection.Query<Payment>($"select aptno,tname, cast(totamt as decimal(34,3)) AS totamt,CONVERT(VARCHAR(11),RENTDATEFROM,106) as RENTDATEFROM,CONVERT(VARCHAR(11),RENTDATETO,106) as RENTDATETO from payments where month =month('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and year=year('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and aptno in (select aptno  from accountsPM  where pref in (select  pref  from rented r where month(lcdate)=month('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}') and year(lcdate)=year('{Convert.ToDateTime(month).ToString("yyyy-MM-dd")}'))  and pname ='{bname}')  and pname ='{bname}' and paymenttype ='RENT'").ToList();
                model.VFROMACCGRID = connection.Query<Payment>($"select aptno,tname,description, cast(totamt as decimal(34,3)) AS totamt,CONVERT(VARCHAR(11),RENTDATEFROM,106) as RENTDATEFROM,CONVERT(VARCHAR(11),RENTDATETO,106) as RENTDATETO from payments where month ='{n.Month}' and year='{n.Year}' and aptno in (select aptno  from accountsPM  where pref in (select  pref from   tenantshistory where month(movedate)='{n.Month}' and year(movedate)='{n.Year}' and pname ='{bname}' )) and pname ='{bname}' and paymenttype ='RENT'").ToList();

                model.pmcount = model.mastergrid.Count;
                model.rentedcount = model.rentedgrid.Count;
                model.vacatingcount = model.tlcgrid.Count;
                model.reservedcount = model.rgrid.Count;
                model.rentdatetxt = month;
                model.bname = bname;
                connection.Close();
                return View(model);
            }
        }

        public JsonResult BtnRenewalLCRentedVacating(string bname, DateTime rentdatetxt)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var mastergrid = connection.Query<mastergridModel>($"select CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,aptno,cname,crent,cnat,cftype,Case when aptno like 'Sho%' then UPPER(aptno) when AptNo like 'Base%'  then UPPER(AptNo) when aptno like 'Mul%' THEN UPPER(aptno) when aptno like 'Stu%' then UPPER(aptno)  ELSE LEFT(CBTYPE,1)+' BR' END AS cbtype,(select tbath from tdetails where id=tid) as tbath,(select pmode from lcinfo where  lc_no=cleaseno) as tmode,(select enqtype from lcinfo where  lc_no=cleaseno) as tctype,tid from propertymaster where month(leasestart)=month('{Convert.ToDateTime(rentdatetxt).ToString("yyyy-MM-dd")}') and year(leasestart)=year('{Convert.ToDateTime(rentdatetxt).ToString("yyyy-MM-dd")}') and bldgname='{bname}' and leasetype NOT IN('New LC')").ToList();
                connection.Close();
                return Json(mastergrid);
            }
        }

        public async Task<string> UpdateSelectedRecordRentedRentedVacating(string bname, DateTime rentdatetxt,int rentedgridlength,string corpind,string modepay,string aptno,string tname,string nationality,string bedroom,string bathroom,string rent,string furnish,string lstart,string lend)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec  from rentstatement where pname='{bname}'  and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}'").FirstOrDefault();
                if(reccount == 0)
                {
                    message = "Create the Rent Statement first";
                }
                else
                {
                    if (rentedgridlength > 0)
                    {
                        string INQTYPE = "", paytype = "";

                        if (corpind == "Individual Enquiry" || corpind == "Individual")
                            INQTYPE = "Individual";
                        if (corpind == "Corporate Enquiry" || corpind == "Corporate")
                            INQTYPE = "Corporate";

                        if (modepay == "SP" || modepay == "Semi-Annually")
                            paytype = "Semi-Annually";
                        if (modepay == "MP" || modepay == "Monthly")
                            paytype = "Monthly";
                        if (modepay == "QP" || modepay == "Quaterly")
                            paytype = "Quarterly";
                        if (modepay == "AP" || modepay == "Yearly")
                            paytype = "Yearly";

                        var update = await connection.ExecuteAsync($"update rentstatement set vtenant='as of {lstart}',tname='{tname}',nat='{nationality}',BED='{bedroom}',BATH='{bathroom}',rent='{rent}',MPAY='{paytype}',FTYPE='{furnish}',leasestart='{lstart}',leaseend='{lend}',CORPINV='{INQTYPE}',updated='NR' WHERE pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}' AND APTNO ='{aptno}'");
                        if(update == 1)
                        {
                            message = "Updated successfully";
                        }
                        else
                        {
                            message = "Unknown Error";
                        }
                    }
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnUpdateAllRentedRentedVacating(string bname, DateTime rentdatetxt, int rentedgridlength, string[] corpind, string[] modepay, string[] aptno, string[] tname, string[] nationality, string[] bedroom, string[] bathroom, string[] rent, string[] furnish, string[] lstart, string[] lend)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec  from rentstatement where pname='{bname}'  and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}'").FirstOrDefault();
                if (reccount == 0)
                {
                    message = "Create the Rent Statement first";
                }
                else
                {
                    if (rentedgridlength > 0)
                    {
                        for(int i=0;i< rentedgridlength; i++)
                        {
                            string INQTYPE = "", paytype = "";

                            if (corpind[i] == "Individual Enquiry" || corpind[i] == "Individual")
                                INQTYPE = "Individual";
                            if (corpind[i] == "Corporate Enquiry" || corpind[i] == "Corporate")
                                INQTYPE = "Corporate";

                            if (modepay[i] == "SP" || modepay[i] == "Semi-Annually")
                                paytype = "Semi-Annually";
                            if (modepay[i] == "MP" || modepay[i] == "Monthly")
                                paytype = "Monthly";
                            if (modepay[i] == "QP" || modepay[i] == "Quaterly")
                                paytype = "Quarterly";
                            if (modepay[i] == "AP" || modepay[i] == "Yearly")
                                paytype = "Yearly";

                            var update = await connection.ExecuteAsync($"update rentstatement set vtenant='as of {lstart[i]}',tname='{tname[i]}',nat='{nationality[i]}',BED='{bedroom[i]}',BATH='{bathroom[i]}',rent='{rent[i]}',MPAY='{paytype}',FTYPE='{furnish[i]}',leasestart='{lstart[i]}',leaseend='{lend[i]}',CORPINV='{INQTYPE}',updated='NR' WHERE pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}' AND APTNO ='{aptno[i]}'");
                            if (update == 0)
                            {
                                message = "Unknown Error";
                                goto gotoreturn;
                            }
                        }
                        message = "Updated successfully";
                    }
                }

                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateReservedRentedVacating(string bname, DateTime rentdatetxt, int rgridclasslength, string[] aptno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}'").FirstOrDefault();
                if (reccount == 0)
                {
                    message = "Create the Rent Statement first";
                }
                else
                {
                    if (rgridclasslength > 0)
                    {
                        for (int i = 0; i < rgridclasslength; i++)
                        {
                            var update = await connection.ExecuteAsync($"update rentstatement set tname='RESERVED',nat='',rent='0.000',MPAY='',CORPINV='',updated='R',leasestart=null,leaseend=null WHERE pname='{bname}'  and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}' AND APTNO ='{aptno[i]}'");
                            if (update == 0)
                            {
                                message = "Unknown Error";
                                goto gotoreturn;
                            }
                        }
                        message = "Updated";
                    }
                }
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateReservedClientSelectedRentedVacating(string bname, DateTime rentdatetxt, int rgridclasslength, string corpind, string modepay, string aptno, string tname, string nationality, string bedroom, string bathroom, string rent, string furnish, string lstart, string lend)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}'").FirstOrDefault();
                if (reccount == 0)
                {
                    message = "Create the Rent Statement first";
                }
                else
                {
                    if (rgridclasslength > 0)
                    {
                        var update = await connection.ExecuteAsync($"update rentstatement set tname='{tname}',nat='{nationality}',BED='{bedroom}',rent='{rent}',CORPINV='{corpind}',mpay='{modepay}',updated='R',leasestart='{lstart}',leaseend='{lend}' WHERE pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}' AND APTNO ='{aptno}'");
                        if (update == 1)
                        {
                            message = "Updated";
                        }
                        else
                        {
                            message = "Unknown Error";
                        }
                    }
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnUpdateVacatedRentedVacating(string bname, DateTime rentdatetxt, int tlcgridclasslength, string[] aptno, string[] tname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}'").FirstOrDefault();
                if (reccount == 0)
                {
                    message = "Create the Rent Statement first";
                }
                else
                {
                    if (tlcgridclasslength > 0)
                    {
                        for(int i = 0;i< tlcgridclasslength;i++)
                        {
                            var update = await connection.ExecuteAsync($"update rentstatement set tname='VACANT',vtenant='{tname[i]}',nat='',rent='0.000',MPAY='',leasestart=null,leaseend=null,CORPINV='',updated='V' WHERE pname='{bname}' and month ='{rentdatetxt.Month}' and year ='{rentdatetxt.Year}' AND APTNO ='{aptno[i]}'");
                            if (update == 0)
                            {
                                message = "Unknown Error";
                                goto gotoreturn;
                            }
                        }
                        message = "Updated";
                    }
                }

                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> VAccInsertFromAccRentedVacating(string aptno,string rentpaid,DateTime rentdatetxt)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var n = rentdatetxt.AddMonths(-1);
                n = new DateTime(n.Year, n.Month, 1);
                var insert = await connection.ExecuteAsync($"insert into rentstatement(pref,pname,aptno,tname,nat,bed,bath,mpay,ftype,corpinv,month,year,rent,doc) (select pref,pname,aptno,tname,nat,bed,bath,mpay,ftype,corpinv,'{rentdatetxt.Month}' ,'{rentdatetxt.Year}','{rentpaid}',getdate() from rentstatement where  month='{n.Month}' and year='{n.Year}' and aptno='{aptno}')");
                if (insert == 1)
                {
                    message = "Inserted";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnStatusAnalysisRENTST(int[] id,string bname,int accgridclasslength)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < accgridclasslength; i++)
                {
                    var update1 = await connection.ExecuteAsync($"update rentstatement set ASR='YES',asrstatus=bed WHERE id={id[i]} and pname='{bname}'");
                    if (update1 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update2 = await connection.ExecuteAsync($"update rentstatement set status='Vacant' where id={id[i]} and pname='{bname}' AND TNAME IN('VACANT') AND LEN(BED)>0");
                    if (update2 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update3 = await connection.ExecuteAsync($"update rentstatement set status='Reserved' where id={id[i]} and pname='{bname}' AND TNAME IN('RESERVED') AND LEN(BED)>0");
                    if (update3 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update4 = await connection.ExecuteAsync($"update rentstatement set status='Rented' where id={id[i]} and pname='{bname}' AND TNAME NOT IN('VACANT','RESERVED') AND LEN(BED)>0");
                    if (update4 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                message = "Updated";
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public IActionResult Page()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                connection.Close();
                return View();
            }
        }

        public string BtnOthernoteRENTST(string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"Select count(*) as rec from othernote where pname='{bname}'").FirstOrDefault();
                if(recordcount > 0)
                {
                    var remark = connection.Query<string>($"Select remark from othernote where pname='{bname}'").FirstOrDefault();
                    connection.Close();
                    return remark;
                }
                else
                {
                    var message = "No Records found";
                    connection.Close();
                    return message;
                }
            }
        }

        public async Task<string> BtnSaveOtherremarkRENTST(string bname,string otherremark)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = await connection.ExecuteAsync($"delete from othernote where pname='{bname}'");
                var insert = await connection.ExecuteAsync($"INSERT INTO othernote(PNAME,REMARK)VALUES('{bname}','{otherremark}");
                if (insert == 1)
                    message = "Note Saved";
                else
                    message = "Unknown Error";
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnAsrRENTST(string bname,int accgridclasslength,bool chkgroupbybedrooms,bool chkgroupbytypes,int[] id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < accgridclasslength; i++)
                {
                    if(chkgroupbybedrooms && chkgroupbytypes)
                    {
                        var update1 = await connection.ExecuteAsync($"update rentstatement set ASR='YES',asrstatus=bed+' '+ftype  WHERE  id={id[i]} and pname='{bname}'");
                        if(update1==0)
                        {
                            message = "Unknown Error";
                            goto gotoreturn;
                        }
                    }
                    else if (chkgroupbybedrooms && !chkgroupbytypes)
                    {
                        var update1 = await connection.ExecuteAsync($"update rentstatement set ASR='YES',asrstatus=bed  WHERE  id={id[i]} and pname='{bname}'");
                        if (update1 == 0)
                        {
                            message = "Unknown Error";
                            goto gotoreturn;
                        }
                    }
                    else if (!chkgroupbybedrooms && chkgroupbytypes)
                    {
                        var update1 = await connection.ExecuteAsync($"update rentstatement set ASR='YES',asrstatus=ftype  WHERE  id={id[i]} and pname='{bname}'");
                        if (update1 == 0)
                        {
                            message = "Unknown Error";
                            goto gotoreturn;
                        }
                    }
                    else//if (!chkgroupbybedrooms && !chkgroupbytypes)
                    {
                        var update1 = await connection.ExecuteAsync($"update rentstatement set ASR='YES',asrstatus=ftype  WHERE  id={id[i]} and pname='{bname}'");
                        if (update1 == 0)
                        {
                            message = "Unknown Error";
                            goto gotoreturn;
                        }
                    }

                    var update2 = await connection.ExecuteAsync($"update rentstatement set asrftype='UF' WHERE FTYPE='Unfurnished' AND id={id[i]} and pname='{bname}'");
                    if (update2 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update3 = await connection.ExecuteAsync($"update rentstatement set asrftype='SF' WHERE FTYPE='Semi-furnished' AND id={id[i]} and pname='{bname}'");
                    if (update3 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update4 = await connection.ExecuteAsync($"update rentstatement set asrftype='SF' WHERE FTYPE='Semi furnished' AND id={id[i]} and pname='{bname}'");
                    if (update4 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                    var update5 = await connection.ExecuteAsync($"update rentstatement set asrftype='FF' WHERE FTYPE='Fully Furnished' AND id={id[i]} and pname='{bname}'");
                    if (update5 == 0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                message = "Updated";
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public string BtnAsrNoteRENTST(string bname)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var remark = connection.Query<string>($"select remark from asrnote where pname='{bname}'").FirstOrDefault();
                connection.Close();
                return remark;
            }
        }

        public async Task<string> BtnSaveAsrNoteRENTST(string bname,string asrnote)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = await connection.ExecuteAsync($"delete from ASRNOTE where pname='{bname}'");
                var insert = await connection.ExecuteAsync($"INSERT INTO ASRNOTE(PNAME,REMARK)VALUES('{bname}','{asrnote}')");
                if(insert==1)
                {
                    message = "Note Saved";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult Walkin()
        {
            return View();
        }

        public JsonResult ViewWalkin(DateTime date,string propertyName)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var walkin = connection.Query<Walkininquiry>($"select * from walkininquiry where month='{date.Month}' and year='{date.Year}' and pname='{propertyName}'").ToList();
                connection.Close();
                return Json(walkin);
            }
        }

        public async Task<string> BtnAddWalkin(string propertyName,string names,string nationality,string status,DateTime date)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var insert = await connection.ExecuteAsync($"insert into walkininquiry(name,status,month,year,pname,seenbyfca,nationality)values('{names}','{status}','{date.Month}','{date.Year}','{propertyName}' ,'NO','{nationality}')");
                if(insert==1)
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

        public async Task<string> RemoveWalkin(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = await connection.ExecuteAsync($"DELETE FROM walkininquiry WHERE ID={id}");
                if (delete == 1)
                {
                    message = "Removed";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public int BtnBedBathRENTST(string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec  from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement  where pname='{bname}') and month =(select  max(month) from Rentstatement  where pname='{bname}' and year=(select max(year) from Rentstatement  where pname='{bname}'))").FirstOrDefault();
                connection.Close();
                return reccount;
            }
        }

        public int BtnOnlyRentRENTST(string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec  from rentstatement where pname='{bname}' and year=(select max(year) from Rentstatement  where pname='{bname}') and month =(select  max(month) from Rentstatement  where pname='{bname}' and year=(select max(year) from Rentstatement  where pname='{bname}'))").FirstOrDefault();
                connection.Close();
                return reccount;
            }
        }

        public async Task<string> BtnGetRentFromAccountsRENTST(string bname,int[] id,string[] month,string[] year,string[] pref,int accgridclasslength)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < accgridclasslength; i++)
                {
                    var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET ACTUALRENT=(select case when totamt is null then 0 else totamt end from payments where pname='{bname}' and month='{month[i]}' and year='{year[i]}' AND PAYMENTTYPE='RENT' and aptno=(select aptno from accountspm where pref='{pref[i]}' and  pname='{bname}')) where pname='{bname}' and month='{month[i]}' and year='{year[i]}' and id={id[i]}");
                    if(update==0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                message = "Rent Updated from Accounts";
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public JsonResult BtnCheckPrentFromAccountsRENTST(DateTime month,string bname,int accgridclasslength,string[] pref,decimal[] rent)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                BtnCheckPrentFromAccountsModelRENTST model = new();
                model.collectedacc = new();
                model.collectedrs = new();
                connection.Open();
                for(int i=0;i<accgridclasslength;i++)
                {
                    var collectedacc = connection.Query<decimal>($"select totamt from payments where pname='{bname}' and month='{month.Month}' and year='{month.Year}' and aptno=(select aptno from accountspm where pref='{pref[i]}')").FirstOrDefault();
                    model.collectedacc.Add(collectedacc);
                    model.collectedrs.Add(rent[i]);
                }
                
                connection.Close();
                return Json(model);
            }
        }

        public async Task<string> BtnRevertChangesRENTST(DateTime month, string bname)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and [CLOSE]='NO'").FirstOrDefault();
                if(reccount > 0)
                {
                    var delete = await connection.ExecuteAsync($"delete from rentstatement where pname='{bname}' and [CLOSE]='NO'");
                    var insert = await connection.ExecuteAsync($"insert into rentstatement(pname,aptno,pref,tname,nat,bed,bath,rent,mpay,ftype,corpinv,month,year,doc,[close])select pname,aptno,pref,tname,nat,bed,bath,rent,mpay,ftype,corpinv,'{month.Month}','{month.Year}',doc,[close] from rentstatement where PNAME='{bname}' AND [close]='CLOSED'");
                    if(insert==1)
                    {
                        message = "Reverted";
                    }
                    else
                    {
                        message = "Unknown Error";
                    }
                }
                else
                {
                    message = "Rent Statement is already closed";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnCloseTheStatementRENTST(int[] id, string bname, int accgridclasslength)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                for (int i = 0; i < accgridclasslength; i++)
                {
                    var update = await connection.ExecuteAsync($"UPDATE RENTSTATEMENT SET [close]='CLOSED' WHERE ID={id[i]} AND PNAME='{bname}'");
                    if(update==0)
                    {
                        message = "Unknown Error";
                        goto gotoreturn;
                    }
                }
                var delete1 = await connection.ExecuteAsync($"delete from vacatingnote where pname='{bname}'");
                if(delete1==0)
                {
                    message = "Unknown Error";
                    goto gotoreturn;
                }
                var delete2 = await connection.ExecuteAsync($"delete from vacatingnote where pname='{bname}'");
                if (delete2 == 0)
                {
                    message = "Unknown Error";
                    goto gotoreturn;
                }
                message = "Closed";
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public async Task<string> BtnCreateRENTST(string bname,DateTime rentmonth,int accgridclasslength,string[] aptno,string[] pref,string[] tname,string[] nat,string[] bed,string[] bath,string[] rent,string[] actualrent,string[] mpay,string[] ftype,string[] corpinv,int[] month,int[] year)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                var n = rentmonth.AddMonths(-2);
                n = new DateTime(n.Year, n.Month, 1);
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}'").FirstOrDefault();
                if(recordcount == 0)
                {
                    for(int i = 0; i < accgridclasslength; i++)
                    {
                        var insert = await connection.ExecuteAsync($"insert into rentstatement(pname,aptno,pref,tname,nat,bed,bath,rent,actualrent,mpay,ftype,corpinv,month,year,doc,[close])values('{bname}','{aptno[i]}','{pref[i]}','{tname[i]}','{nat[i]}','{bed[i]}','{bath[i]}','{rent[i]}','{actualrent[i]}','{mpay[i]}','{ftype[i]}','{corpinv[i]}','{rentmonth.Month}','{rentmonth.Year}',getdate(),'NO')");
                        if(insert==0)
                        {
                            message = "Unknown Error";
                            goto gotoreturn;
                        }
                    }
                    message = "Created";
                }
                else
                {
                    var reccount = connection.Query<int>($"select count(*) as rec  from rentstatement where pname='{bname}' and month='{rentmonth.Month}' and year='{rentmonth.Year}'").FirstOrDefault();
                    if(reccount > 0)
                    {
                        message = "Rent statement is already uploaded for this month, do update";
                        goto gotoreturn;
                    }
                    else
                    {
                        var reccount1 = connection.Query<int>($"select count(*) as rec from rentstatement where pname='{bname}' and [CLOSE]='NO'").FirstOrDefault();
                        if(reccount1 > 0)
                        {
                            message = "Close the previous statements";
                            goto gotoreturn;
                        }
                        for (int i = 0; i < accgridclasslength; i++)
                        {
                            var insert = await connection.ExecuteAsync($"insert into rentstatement(pname,aptno,pref,tname,nat,bed,bath,rent,actualrent,mpay,ftype,corpinv,month,year,doc,[close])values('{bname}','{aptno[i]}','{pref[i]}','{tname[i]}','{nat[i]}','{bed[i]}','{bath[i]}','{rent[i]}','{actualrent[i]}','{mpay[i]}','{ftype[i]}','{corpinv[i]}','{rentmonth.Month}','{rentmonth.Year}',getdate(),'NO')");
                            if(insert==0)
                            {
                                message = "Unknown Error";
                                goto gotoreturn;
                            }
                            message = "Rent statement is created successfully";
                            var delete = await connection.ExecuteAsync($"delete from rentstatement where month='{n.Month}' and year='{n.Year}' AND PNAME='{bname}'");
                        }
                    }
                }
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public IActionResult MovingInOut()
        {
            return View();
        }

        public IActionResult CustomerService()
        {
            var sessionrole = HttpContext.Session.GetString("CurrentUserRole");
            var sessiondept = HttpContext.Session.GetString("CurrentUserDepartment");
            if ((sessionrole == "User") || (sessiondept == "Accounts"))
            {
                goto gotoreturn;
            }
            else
            {
                ViewBag.Message = "Access Denied";
            }
            gotoreturn:
            return View();
        }

        public IActionResult Accounting()
        {
            var sessionrole = HttpContext.Session.GetString("CurrentUserRole");
            var sessiondept = HttpContext.Session.GetString("CurrentUserDepartment");
            var sessionusername = HttpContext.Session.GetString("CurrentUsername");
            if (sessiondept == "Accounts" && sessionrole == "Admin")
            {
                goto gotoreturn;
            }
            else if (sessionusername == "btsupport")
            {
                goto gotoreturn;
            }
            else
            {
                ViewBag.Message = "Access denied";
            }

            gotoreturn:
            return View();
        }

    }

    public class BtnCheckPrentFromAccountsModelRENTST
    {
        public List<decimal> collectedacc { get; set; }
        public List<decimal> collectedrs { get; set; }
    }

    public class BtnCheckRentChangeApprovalModelExportRS: Rentchange
    {
        public string aptno { get; set; }
    }

    public class BtnRentFixAccountsModelExportRS
    {
        public List<decimal> collectedacc { get; set; }
        public List<decimal> collectedrs { get; set; }
        public List<string> detailgridprent { get; set; }
        public decimal PAYRLBL { get; set; }

    }

    public class BtnVerifyWithAccountsModelExportRS
    {
        public List<int> recordcount { get; set; }
        public List<string> collectedacc { get; set; }
        public List<string> collectedrs { get; set; }
        public List<string> mrentacc { get; set; }
        public List<string> mrentrs { get; set; }
    }

    public class ViewFromRentStatementModelExportRS: Rentstatement
    {
        public string slease { get; set; }
    }

    public class mastergridmodelExportRS
    {
        public string propertyref { get; set; }
        public string aptno { get; set; }
        public string cname { get; set; }
        public string cnat { get; set; }
        public string crent { get; set; }
        public string payable { get; set; }
        public string leasestart { get; set; }
        public string leaseend { get; set; }
        public string cbtype { get; set; }
        public string bath { get; set; }
        public string lctype { get; set; }
    }

    public class tlcgridmodelExportRS
    {
        public string pref { get; set; }
        public int id { get; set; }
        public string pname { get; set; }
        public string aptno { get; set; }
        public string btype { get; set; }
        public string ftype { get; set; }
        public string tenantname { get; set; }
        public string nationality { get; set; }
        public string contact { get; set; }
        public string bath { get; set; }
        public string LCrent { get; set; }
        public string payable { get; set; }
        public string pmode { get; set; }
        public string enqtype { get; set; }
        public string leasestart { get; set; }
        public string leaseend { get; set; }
        public string movedate { get; set; }
        public string keyreturndate { get; set; }
    }

    public class ExportRSClassModel
    {
        public string propertyref { get; set; }
        public string aptno { get; set; }
        public string bed { get; set; }
        public string bath { get; set; }
        public string payablerent { get; set; }
        public string enqtype { get; set; }
        public string mode { get; set; }
        public string cnat { get; set; }
        public string cbtype { get; set; }
        public string bath1 { get; set; }
        public string cftype { get; set; }
        public string cname { get; set; }
        public string crent { get; set; }
        public string lstart { get; set; }
        public string lend { get; set; }
        public string status { get; set; }
        public string slease { get; set; }
    }
}
