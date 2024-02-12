using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PMS_Admin_Web.Tables;
using System.Web;
using PMS_Admin_Web.Models.Property;
using ReflectionIT.Mvc.Paging;
using X.PagedList;
using System.Data;
using System.Data.SqlClient;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.Users;
using Microsoft.AspNetCore.Http;
using PMS_Admin_Web.Repository;
using PMS_Admin_Web.Enum;
using Dapper;

namespace PMS_Admin_Web.Controllers
{
    public class PropertyController : Controller
    {
        //private q8RealtorContext context = new();
        private RealtorContext context = new();
        private Connection sqlConnectionString = new();

        public PropertyController(RealtorContext _context)
        {
            context = _context;
        }

        public async Task<List<string>> PropertyNames()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertynames = await connection.QueryAsync<string>("select distinct BldgName from propertymaster where PropertySource='ManagedProperty'");
                connection.Close();
                return propertynames.ToList();
            }
        }

        //public IActionResult Add(PropertyAndAreaModel propertyAndAreaModel)
        //{
            
        //    propertyAndAreaModel.propertyTables = new();
        //    int i = 1;
        //    int floor = 0, aptno = 0;
        //    using(var connection = new SqlConnection(sqlConnectionString.ConnectionString))
        //    {
        //        connection.Open();
        //        var propertyExist = connection.Query<int>($"select COUNT(*) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}'").FirstOrDefault();
        //        if(propertyExist == 0)
        //        {
        //            floor = 1;
        //        }
        //        else
        //        {
        //            var Lastflooradded = connection.Query<string>($"select MAX(Floors) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}' and Floors not in ('g','gf','b','m','f','s','t','-','rt','BSMT','shop')").FirstOrDefault();
        //            var Lastaptadded = connection.Query<string>($"select MAX(AptNo) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}'").FirstOrDefault();
        //            floor = Convert.ToInt32(Lastflooradded);
        //            aptno = Convert.ToInt32(Lastaptadded);
        //            floor++;
        //        }

        //        while (i <= Convert.ToInt16(propertyAndAreaModel.Property.Units))
        //        {
        //            PropertyTable propertyTable = new();
        //            propertyTable.PropertyName = propertyAndAreaModel.Property.BldgName;
        //            propertyTable.Floor = floor.ToString();
        //            propertyTable.Type = propertyAndAreaModel.Property.Furnished;
        //            propertyTable.Bed = propertyAndAreaModel.Property.Bed;
        //            if(aptno == 0)
        //            {
        //                propertyTable.AptNo = i;
        //            }
        //            else
        //            {
        //                propertyTable.AptNo = ++aptno;
        //            }
                    

        //            propertyAndAreaModel.propertyTables.Add(propertyTable);
        //            i++;
        //        }
        //        connection.Close();
        //    }
            
        //    ViewData["propertyList"] = propertyAndAreaModel.propertyTables;
            
        //    propertyAndAreaModel.Areas = (from area in context.Areas select area).ToList();

        //    return View("CreateProperty", propertyAndAreaModel);
        //}

        //public IActionResult Clear(PropertyAndAreaModel propertyAndAreaModel)
        //{
        //    ModelState.Clear();
        //    propertyAndAreaModel.propertyTables = new();
        //    propertyAndAreaModel.Areas = (from area in context.Areas select area).ToList();
        //    return View("CreateProperty", propertyAndAreaModel);
        //}

        [HttpPost]
        public async Task<ActionResult> CreateProperty(PropertyAndAreaModel propertyAndAreaModel)
        {
            if (propertyAndAreaModel.Property != null)
            {
                try
                {
                    int aptno = 0;
                    // No id so we add it to database
                    if (propertyAndAreaModel.Property.Id <= 0)
                    {
                        using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                        {
                            connection.Open();
                            //var propertyExist = connection.Query<int>($"select COUNT(*) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}'").FirstOrDefault();
                            //if (propertyExist == 0)
                            //{
                            //    floor = 1;
                            //}
                            //else
                            //{
                            //    var Lastflooradded = connection.Query<string>($"select MAX(Floors) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}' and Floors not in ('g','gf','b','m','f','s','t','-','rt','BSMT','shop')").FirstOrDefault();
                            //    var Lastaptadded = connection.Query<string>($"select MAX(AptNo) from propertymaster where BldgName='{propertyAndAreaModel.Property.BldgName}'").FirstOrDefault();
                            //    floor = Convert.ToInt32(Lastflooradded);
                            //    aptno = Convert.ToInt32(Lastaptadded);
                            //    floor++;
                            //}
                            for(int floor = 1; floor <= Convert.ToInt16(propertyAndAreaModel.Floors); floor++)
                            {
                                for (int i = 1; i <= Convert.ToInt16(propertyAndAreaModel.Property.Units); i++)
                                {
                                    var pcount = connection.Query<int>(@"select right(propertyref,len(propertyref)-8)+1 as pcount 
                                                                   from propertymaster 
                                                                   where id= (select max(id) from propertymaster where propertysource ='Managedproperty')").FirstOrDefault();
                                    var aptNo = ++aptno;
                                    propertyAndAreaModel.Property.PropertySource = "ManagedProperty";
                                    propertyAndAreaModel.Property.PropertyRef = "MPL" + DateTime.Today.Year + "_00" + pcount;
                                    propertyAndAreaModel.Property.Floors = floor.ToString();
                                    propertyAndAreaModel.Property.AptNo = aptNo.ToString();
                                    propertyAndAreaModel.Property.Pdate = DateTime.Now.Date;
                                    propertyAndAreaModel.Property.Doc = DateTime.Now.Date;
                                    propertyAndAreaModel.Property.Status = "Available";
                                    propertyAndAreaModel.Property.Crent = (decimal?)0.00;
                                    propertyAndAreaModel.Property.Reservedrent = (decimal?)0.00;
                                    propertyAndAreaModel.Property.Rented = "No";

                                    var inserted = await connection.ExecuteAsync($@"insert into propertymaster(PropertyType,BlockName,bldgno,propertyref,propertysource,
                                                                location,furnished,bldgname,streetname,paci,floors,aptno,units,bed,[Bath],[Balcony],
                                                                [Kitchen],[LivingRoom],[StudyRoom],[MaidRoom],[Pool],[Parking],[Garden],[Internet],
                                                                [Gym],[Security],[PlayArea],[CabelTv],[CCTV],[OSN],[Ac],[pdate],[Description],[source],
                                                                [facilities],[updatedby],[vistedby],[doc],[resf],[seaview],status,crent,reservedrent,amount,rented) 
                                                                values('{propertyAndAreaModel.Property.PropertyType}','{propertyAndAreaModel.Property.BlockName}','{propertyAndAreaModel.Property.BldgNo}','{propertyAndAreaModel.Property.PropertyRef}','{propertyAndAreaModel.Property.PropertySource}',
                                                                '{propertyAndAreaModel.Property.Location}','{propertyAndAreaModel.Property.Furnished}','{propertyAndAreaModel.Property.BldgName}','{propertyAndAreaModel.Property.StreetName}','{propertyAndAreaModel.Property.Paci}','{propertyAndAreaModel.Property.Floors}','{propertyAndAreaModel.Property.AptNo}','{propertyAndAreaModel.Property.Units}','{propertyAndAreaModel.Property.Bed}','{propertyAndAreaModel.Property.Bath}','{propertyAndAreaModel.Property.Balcony}',
                                                                '{propertyAndAreaModel.Property.Kitchen.Trim()}','{propertyAndAreaModel.Property.LivingRoom.Trim()}','{propertyAndAreaModel.Property.StudyRoom.Trim()}','{propertyAndAreaModel.Property.MaidRoom.Trim()}','{propertyAndAreaModel.Property.Pool.Trim()}','{propertyAndAreaModel.Property.Parking.Trim()}','{propertyAndAreaModel.Property.Garden.Trim()}','{propertyAndAreaModel.Property.Internet.Trim()}',
                                                                '{propertyAndAreaModel.Property.Gym.Trim()}','{propertyAndAreaModel.Property.Security.Trim()}','{propertyAndAreaModel.Property.PlayArea}','{propertyAndAreaModel.Property.CabelTv.Trim()}','{propertyAndAreaModel.Property.Cctv.Trim()}','{propertyAndAreaModel.Property.Osn.Trim()}','{propertyAndAreaModel.Property.Ac.Trim()}',CONVERT(date, '{propertyAndAreaModel.Property.Pdate}', 103),'{propertyAndAreaModel.Property.Description.Trim()}','{propertyAndAreaModel.Property.Source.Trim()}',
                                                                '{propertyAndAreaModel.Property.Facilities}','{propertyAndAreaModel.Property.Updatedby.Trim()}','{propertyAndAreaModel.Property.Vistedby.Trim()}',CONVERT(date, '{propertyAndAreaModel.Property.Doc}', 103),'{propertyAndAreaModel.Property.Resf.Trim()}','{propertyAndAreaModel.Property.Seaview.Trim()}','{propertyAndAreaModel.Property.Status.Trim()}','{propertyAndAreaModel.Property.Crent}','{propertyAndAreaModel.Property.Reservedrent}','{propertyAndAreaModel.Property.Amount}','{propertyAndAreaModel.Property.Rented.Trim()}')");
                                }
                            }

                            
                            connection.Close();
                            ViewBag.Message = "Property Created";
                        }
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Property Created");
                        
                    }

                    //// Has Id, therefore it's in database so we update
                    else
                    {
                        Propertymaster propertymaster = new();
                        propertymaster = propertyAndAreaModel.Property;

                        context.Propertymasters.Update(propertymaster);
                        await context.SaveChangesAsync();
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Property Updated");
                        ViewBag.Message = "Property Updated";
                    }
                }
                catch(Exception ex)
                {
                    //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown Error");
                    ViewBag.Message = "Unknown Error";
                }
                //ModelState.Clear();
                
            }
            else
                propertyAndAreaModel.Property = new();

            //propertyAndAreaModel.Areas = (from area in context.Areas select area).ToList();
            //propertyAndAreaModel.propertyTables = new();
            return View("CreateProperty", propertyAndAreaModel);
        }

        public async Task<IActionResult> EditProperty(int Id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //var user = context.Users.Find(Id);
                var editPropertyModel = new Models.Property.EditPropertyModel();
                var propertymaster = await connection.QueryAsync<Models.Propertymaster>(@$"select * from propertymaster where id={Id}");
                editPropertyModel.propertymaster = propertymaster.FirstOrDefault();
                var missues = await connection.QueryAsync<Models.Missue>(@$"select aptlocation,category,issue,status,PL ,contractortype ,remarks,id from MISSUE 
                                                                            where pname='{editPropertyModel.propertymaster.BldgName}' and status in ('Pending','In progress')  
                                                                            order by id desc");
                editPropertyModel.missues = missues.ToList();
                return View(editPropertyModel);
            }
        }

        [HttpGet]
        public IActionResult ListProperties(int page=1,int pageSize=10)
        {
            if (HttpContext.Session.GetString("CurrentUsername") == null)
            {
                return RedirectToAction("Login");
            }

            List<PMS_Admin_Web.Models.Propertymaster> propertymasters = context.Propertymasters.Where(x=>x.PropertySource== "ManagedProperty").ToList();
            //PagedList<PMS_Admin_Web.Models.Propertymaster> model = new PagedList<PMS_Admin_Web.Models.Propertymaster>(propertymasters, page, pageSize);
            return View(propertymasters);
        }

        [HttpGet]
        public string DeleteProperty(int Id)
        {
            try
            {
                var deleteProperty = context.Propertymasters.Find(Id);
                context.Propertymasters.Remove(deleteProperty);
                context.SaveChangesAsync();
                ViewBag.Message = "Successfully Deleted";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }
            return ViewBag.Message;
        }

        public async Task<List<string>> LoadPropertynameForSearch()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertynameResult = await connection.QueryAsync<string>($"select distinct BldgName from propertymaster where PropertySource='ManagedProperty'");
                connection.Close();
                return propertynameResult.ToList();
            }
        }

        public async Task<List<Propertymaster>> SearchProperty(string searchProperty)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertynameResult = await connection.QueryAsync<Propertymaster>($"select * from propertymaster where BldgName = '{searchProperty}'");
                connection.Close();
                return propertynameResult.ToList();
            }
        }

        public async Task<IActionResult> ListPropertyApt(string buildingName)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertymasters = await connection.QueryAsync<ListPropertyAptModel>(@$"select id,reservedfor ,rnat,reservedrent ,rftype ,rbtype ,CONVERT(VARCHAR(11),rlstart,106) as rlstart,
                                                                                    CONVERT(VARCHAR(11),rlend,106) as rlend ,rmob,PropertyRef,BldgName,BlockName,
                                                                                    case when Floors like 'B%' then floors when Floors like 'M%' then floors when Floors like 'G%' then floors when Floors like 'T%' then floors else floors+'F' end AS Floors,AptNo,Bed,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,cleaseno,
                                                                                    case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(crent as decimal(34,3)) end as payablerent,
                                                                                    case when status='NotAvailable' and reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when  status is null and reservation='yes' then 'Reserved' when  status='NotAvailable' and reservation='' then 'Occupied' when status='NotAvailable' and reservation is null then 'Occupied' else status end as status,left(cbtype,1) as cbtype,cftype,cname,
                                                                                    case when crent is null then 0 else crent end as crent,
                                                                                    CONVERT(VARCHAR(11),leasestart,106) as leasestart,
                                                                                    CONVERT(VARCHAR(11),leaseend ,106) as leaseend,vacatingstatus,
                                                                                    CONVERT(VARCHAR(11),moveout,106) as moveout 
                                                                                    from propertymaster
                                                                                    where BldgName='{buildingName}' and propertysource='ManagedProperty' 
                                                                                    order by orderid");
                connection.Close();
                return View(propertymasters.ToList());
            }
        }

        public async Task<List<string>> LoadAreas()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var areas = await connection.QueryAsync<string>(@$"select AreaName from area");
                connection.Close();
                return areas.ToList();
            }
        }

        public async Task<IActionResult> IssueDetails(int id)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var missue = await connection.QueryAsync<Missue>(@$"select requestdate,CONVERT(VARCHAR(11),wcdate,106) as wcdate, workdescription,
                                                        additionalwork,itempurchase,category,doc,contractortype,contractorname,APTLOCATION,
                                                        status,issue,remarks,paname,pl,request,pref,tname,contactno,lcno,tavailability 
                                                        from missue st2 
                                                        where id={id}");
                Missue missue1 = missue.FirstOrDefault();
                connection.Close();
                return View(missue1);
            }
        }

        

    }
}
