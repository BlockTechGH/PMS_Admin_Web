using Dapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.LOI;
using PMS_Admin_Web.Models.Marketing;
using PMS_Admin_Web.Models.Property;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PMS_Admin_Web.Controllers
{
    public class MarketingController : Controller
    {
        private RealtorContext context = new RealtorContext();
        private Connection sqlConnectionString = new();

        public async Task<IActionResult> EnquiryForm(EnquiryFormModel model)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                if (model.pglCgl != null)
                {
                    if (model.pglCgl.PropertyType != "Commercial Space")
                    {
                        if (model.pglCgl.BedroomRequirements == null)
                        {
                            ViewBag.Message = "Enter the Requirements";
                            goto gotoreturn;
                        }
                    }
                    if (model.pglCgl.PropertyType == "Commercial Space")
                    {
                        if (model.pglCgl.SquareMetres == null)
                        {
                            ViewBag.Message = "Enter the Square mtrs";
                            goto gotoreturn;
                        }
                    }
                    if (model.pglCgl.InquiryDate == null)
                    {
                        ViewBag.Message = "Enter the Inquiry Date";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MoveInDate == null)
                    {
                        ViewBag.Message = "Move in date is Required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.EnquirySource == null)
                    {
                        ViewBag.Message = "Source is Required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.Nationality1 == null)
                    {
                        ViewBag.Message = "Nationality is Required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.ClientName == null)
                    {
                        ViewBag.Message = "Client Name is required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.Mobile == null)
                    {
                        ViewBag.Message = "Mobile No is required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.PropertyType == null)
                    {
                        ViewBag.Message = "Property Type is required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MinBudget == null)
                    {
                        ViewBag.Message = "Enter the Minimum Budget";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MaxBudget == null)
                    {
                        ViewBag.Message = "Enter the Maximum Budget";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.AllocatedTo == null)
                    {
                        ViewBag.Message = "Alloted to Information is required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.Email == null)
                    {
                        ViewBag.Message = "Email Id is Required";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.ClientType == null)
                    {
                        ViewBag.Message = "Specify this inquiry is Corporate or Individual";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MinBudget == 0)
                    {
                        ViewBag.Message = "Minimum Budget cannot be 0";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MaxBudget == 0)
                    {
                        ViewBag.Message = "Maximum Budget cannot be 0";
                        goto gotoreturn;
                    }
                    if (model.pglCgl.MinBudget > model.pglCgl.MaxBudget)
                    {
                        ViewBag.Message = "Maximum Budget cannot be less than the Minimum Budget";
                        goto gotoreturn;
                    }
                    model.pglCgl.PropertySource = "";

                    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                    {
                        connection.Open();
                        var ledetails = connection.Query<Leinfo>($"select Nametitle,LEname,lemail,LEMob from LEInfo where lename='{model.pglCgl.AllocatedTo}' ").FirstOrDefault();
                        if (model.pglCgl.EnquiryType == "PGL")
                        {
                            var reccount1 = connection.Query<int>($"select count(*) from PGL where Pglrefno='{model.pglCgl.ReferenceNo}' ").FirstOrDefault();
                            if (reccount1 == 0)
                            {
                                var insert = await connection.ExecuteAsync(@$"INSERT INTO PGL(Enquirytype,PGLREFNO,ClientName,ContactPerson,Address,Email,Mobile,Phone,Nationality,Nationality2,PropertySource,Propertytype,MinBudget,Maxbudget,Bed,Bath,Furnished,Pool,Tv,Internet,Gym,Parking,Garden,Balcony,Maidroom,Driverroom,Interest,Sqmtrs,Block,movingdate,Completedby,Source,othersource,OtherInfo,Doc,EnquiryDate,InquiryStatus)
                            VALUES('{model.pglCgl.ClientType}','{model.pglCgl.ReferenceNo}','{model.pglCgl.ClientName}','{model.pglCgl.Company}','{model.pglCgl.Address}','{model.pglCgl.Email}','{model.pglCgl.Mobile}','{model.pglCgl.Landline}','{model.pglCgl.Nationality1}','{model.pglCgl.Nationality2}','{model.pglCgl.PropertySource}','{model.pglCgl.PropertyType}','{model.pglCgl.MinBudget}','{model.pglCgl.MaxBudget}','{model.pglCgl.BedroomRequirements}','{model.pglCgl.Bathroom}','{model.pglCgl.Furnished}','{model.pglCgl.Pool}','{model.pglCgl.SatelliteTv}','{model.pglCgl.Internet}','{model.pglCgl.Gym}','{model.pglCgl.Parking}','{model.pglCgl.Garden}','{model.pglCgl.Balcony}','{model.pglCgl.MaidRoom}','{model.pglCgl.DriverRoom}','{model.pglCgl.SelectedAreas}','{model.pglCgl.SquareMetres}','{model.pglCgl.SelectedBlocks}',CONVERT(date, '{model.pglCgl.MoveInDate}', 103),'{model.pglCgl.AllocatedTo}','{model.pglCgl.EnquirySource}','{model.pglCgl.OtherSource}','{model.pglCgl.OtherInformation}',getdate(),CONVERT(date,'{model.pglCgl.InquiryDate}', 103),'Open')");
                                ViewBag.Message = "PGL is created Successfully";
                                goto gotoreturn;
                            }
                            else
                            {
                                ViewBag.Message = "This PGL already Exist";
                            }
                        }
                        else if (model.pglCgl.EnquiryType == "CGL")
                        {
                            var reccount1 = connection.Query<int>($"select count(*) from cgl where cglrefno='{model.pglCgl.ReferenceNo}' ").FirstOrDefault();
                            if (reccount1 == 0)
                            {
                                var insert = await connection.ExecuteAsync(@$"INSERT INTO CGL(Enquirytype,CGLREFNO,ClientName,ContactPerson,Address,Email,Mobile,Phone,Nationality,Nationality2,PropertySource,Propertytype,MinBudget,Maxbudget,Bed,Bath,Furnished,Pool,Tv,Internet,Gym,Parking,Garden,Balcony,Maidroom,Driverroom,Interest,Sqmtrs,Block,movingdate,Completedby,Source,othersource,OtherInfo,Doc,EnquiryDate,InquiryStatus)
                            VALUES('{model.pglCgl.ClientType}','{model.pglCgl.ReferenceNo}','{model.pglCgl.ClientName}','{model.pglCgl.Company}','{model.pglCgl.Address}','{model.pglCgl.Email}','{model.pglCgl.Mobile}','{model.pglCgl.Landline}','{model.pglCgl.Nationality1}','{model.pglCgl.Nationality2}','{model.pglCgl.PropertySource}','{model.pglCgl.PropertyType}','{model.pglCgl.MinBudget}','{model.pglCgl.MaxBudget}','{model.pglCgl.BedroomRequirements}','{model.pglCgl.Bathroom}','{model.pglCgl.Furnished}','{model.pglCgl.Pool}','{model.pglCgl.SatelliteTv}','{model.pglCgl.Internet}','{model.pglCgl.Gym}','{model.pglCgl.Parking}','{model.pglCgl.Garden}','{model.pglCgl.Balcony}','{model.pglCgl.MaidRoom}','{model.pglCgl.DriverRoom}','{model.pglCgl.SelectedAreas}','{model.pglCgl.SquareMetres}','{model.pglCgl.SelectedBlocks}',CONVERT(date,'{model.pglCgl.MoveInDate}', 103),'{model.pglCgl.AllocatedTo}','{model.pglCgl.EnquirySource}','{model.pglCgl.OtherSource}','{model.pglCgl.OtherInformation}',getdate(),CONVERT(date,'{model.pglCgl.InquiryDate}', 103),'Open')");
                                ViewBag.Message = "CGL is created Successfully";
                                goto gotoreturn;
                            }
                            else
                            {
                                ViewBag.Message = "This CGL already Exist";
                            }
                        }
                        connection.Close();
                    }
                }
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
                {
                    ViewBag.Message = "Access denied";
                    goto gotoreturn;
                }
                if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
                {
                    ViewBag.Message = "Access denied";
                    goto gotoreturn;
                }

                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
                {
                    goto gotoreturn;
                }
                else if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") != "Admin")
                {
                    goto gotoreturn;
                }
                else
                {
                    ViewBag.Message = "Access denied";
                }
            }
            catch(Exception e)
            {
                logger.Info(e.Message);
            }
            

            gotoreturn:
            return View();
        }

        public IActionResult ViewEnquiry()
        {
            return View();
        }

        public IActionResult ViewPGL()
        {
            List<Pgl> model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator")
                {
                    model = connection.Query<Pgl>($"SELECT top 100 source,PGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM PGL where InquiryStatus='Open' order by id desc").ToList();

                }
                else if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
                {
                    model = connection.Query<Pgl>($"SELECT top 100 source,PGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM PGL where InquiryStatus='Open' order by id desc").ToList();

                }
                else
                {
                    model = connection.Query<Pgl>($"SELECT top 100 source,PGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM PGL where InquiryStatus='Open' and completedby='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();

                }
                connection.Close();
            }
            return View(model);
        }

        public IActionResult ViewCGL()
        {
            List<Cgl> model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator")
                {

                    model = connection.Query<Cgl>($"SELECT top 100 source,CGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM CGL where InquiryStatus='Open' order by id desc").ToList();
                }
                else if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
                {

                    model = connection.Query<Cgl>($"SELECT top 100 source,CGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM CGL where InquiryStatus='Open' order by id desc").ToList();
                }
                else
                {

                    model = connection.Query<Cgl>($"SELECT top 100 source,CGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,enquirydate,Updatedon, case when updatedon is null then '0' when Updatedon >= DATEADD(day,-3, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM CGL where InquiryStatus='Open' and completedby='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();
                }
                connection.Close();
            }
            return View(model);
        }

        public string DeletePGLEnquiry(string pglrefNo)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string msg = "";
                var delete = connection.Execute($"delete from pgl where pglrefno='{pglrefNo}'");
                if (delete == 1)
                {
                    var insert = connection.Execute($"insert into deltedinquiries(inqno,userinfo,deletingdate)values('{pglrefNo}','{HttpContext.Session.GetString("CurrentUsername")}',getdate())");
                    msg = "Enquiry is deleted";
                }
                connection.Close();
                return msg;
            }
        }

        public string DeleteCGLEnquiry(string cglrefNo)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string msg = "";
                var delete = connection.Execute($"delete from cgl where cglrefno='{cglrefNo}'");
                if (delete == 1)
                {
                    var insert = connection.Execute($"insert into deltedinquiries(inqno,userinfo,deletingdate)values('{cglrefNo}','{HttpContext.Session.GetString("CurrentUsername")}',getdate())");
                    msg = "Enquiry is deleted";
                }
                connection.Close();
                return msg;
            }
        }

        public async Task<IActionResult> UpdatePGL(string pglrefno, UpdatePGLModel model)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (model.pgl != null)
                    {
                        if (model.pgl.ClientName == null)
                        {
                            ViewBag.Message = "Enter the Client Name";
                            goto gotoreturn;
                        }
                        if (model.pgl.Mobile == null)
                        {
                            ViewBag.Message = "Enter the Mobile NO";
                            goto gotoreturn;
                        }
                        if (model.pgl.Propertytype == null)
                        {
                            ViewBag.Message = "Enter the Property Type";
                            goto gotoreturn;
                        }
                        if (model.pgl.Propertytype == "Commercial Space")
                        {
                            if (model.pgl.Sqmtrs == null)
                            {
                                ViewBag.Message = "Enter the Square mtrs";
                                goto gotoreturn;
                            }
                        }
                        if (model.pgl.Bed == null)
                            model.pgl.Bed = "-";
                        if (model.pgl.Gym == null)
                            model.pgl.Gym = "-";
                        if (model.pgl.Pool == null)
                            model.pgl.Pool = "-";
                        if (model.pgl.Furnished == null)
                            model.pgl.Furnished = "-";
                        if (model.pgl.Garden == null)
                            model.pgl.Garden = "-";
                        if (model.pgl.Balcony == null)
                            model.pgl.Balcony = "-";
                        if (model.pgl.Parking == null)
                            model.pgl.Parking = "-";
                        if (model.pgl.Internet == null)
                            model.pgl.Internet = "-";
                        if (model.pgl.Tv == null)
                            model.pgl.Tv = "-";
                        if (model.pgl.Maidroom == null)
                            model.pgl.Maidroom = "-";
                        if (model.pgl.Driverroom == null)
                            model.pgl.Driverroom = "-";
                        if (model.pgl.Othersource == null)
                            model.pgl.Othersource = "-";

                        try
                        {
                            var update = await connection.ExecuteAsync($"UPDATE PGL set ClientName='{model.pgl.ClientName}',contactperson='{model.pgl.ContactPerson}',Address='{model.pgl.Address}',Email='{model.pgl.Email}',Mobile='{model.pgl.Mobile}',Phone='{model.pgl.Phone}',Propertytype='{model.pgl.Propertytype}',MinBudget='{model.pgl.MinBudget}',Maxbudget='{model.pgl.Maxbudget}',Bed='{model.pgl.Bed.Trim()}',Bath='{model.pgl.Bath}',Furnished='{model.pgl.Furnished.Trim()}',Pool='{model.pgl.Pool.Trim()}',Tv='{model.pgl.Tv.Trim()}',Internet='{model.pgl.Internet.Trim()}',Gym='{model.pgl.Gym.Trim()}',Parking='{model.pgl.Parking.Trim()}',Garden='{model.pgl.Garden.Trim()}',Balcony='{model.pgl.Balcony.Trim()}',Interest='{model.pgl.Interest}',BLOCK='{model.pgl.Block}',Nationality='{model.pgl.Nationality}',Nationality2='{model.pgl.Nationality2}',SpecialInstructions='{model.pgl.SpecialInstructions}',OtherInfo='{model.pgl.OtherInfo}',EnquiryDate=CONVERT(date,'{model.pgl.EnquiryDate}', 103),movingdate=CONVERT(date,'{model.pgl.Movingdate}', 103),Source='{model.pgl.Source.Trim()}',othersource='{model.pgl.Othersource.Trim()}',Maidroom='{model.pgl.Maidroom.Trim()}',Driverroom='{model.pgl.Driverroom.Trim()}',Sqmtrs='{model.pgl.Sqmtrs}' WHERE pglrefno='{model.refno}' ");
                            ViewBag.Message = "PGL is update along with its new Information";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "Unknown Error";
                            Console.WriteLine(ex);
                        }

                    }
                    else
                    {
                        model.actiondetails = connection.Query<Actiondetail>($"select id,REFNO,Date,Actions,id from actiondetails where refno='{pglrefno}' order by doc desc ").ToList();
                        model.driverScheduels = connection.Query<DriverScheduel>($"SELECT [L.EName] LEname,Date,FromTime,ToTime,DriverName,Remarks,comments,id FROM DriverScheduel WHERE ReferenceNo='{pglrefno}' order by id desc ").ToList();
                        var pgl = connection.Query<Pgl>($"select enquirytype,PGLREFNO,ClientName,contactperson,Address,Email,Mobile,Phone,Nationality,Nationality2,Propertytype,MinBudget,Maxbudget,Bed,Bath,Pool,Furnished,Tv,Internet,Gym,Parking,Garden,Balcony,Maidroom,Driverroom,Sqmtrs,Interest,Block,Doc,EnquiryDate,movingdate,completedby,Source,othersource,OtherInfo,SpecialInstructions,Remarks,Inquirystatus from pgl where pglrefno='{pglrefno}' ").FirstOrDefault();
                        model.pgl = pgl;
                        model.refno = pgl.Pglrefno;
                        //model.pgl.Pglrefno = pgl.Pglrefno;
                        model.pgl.InquiryStatus = pgl.InquiryStatus.Trim();
                        model.pgl.Gym = pgl.Gym.Trim();
                        model.pgl.Pool = pgl.Pool.Trim();
                        model.pgl.Furnished = pgl.Furnished.Trim();
                        model.pgl.Garden = pgl.Garden.Trim();
                        model.pgl.Balcony = pgl.Balcony.Trim();
                        model.pgl.Parking = pgl.Parking.Trim();
                        model.pgl.Internet = pgl.Internet.Trim();
                        model.pgl.Tv = pgl.Tv.Trim();
                        model.pgl.Maidroom = pgl.Maidroom.Trim();
                        model.pgl.Driverroom = pgl.Driverroom.Trim();
                        model.pgl.Source = pgl.Source.TrimEnd();
                        model.pgl.Othersource = pgl.Othersource.TrimEnd();
                    }
                }
                catch (Exception e)
                {
                    logger.Info(e.Message);
                }
                
                connection.Close();
            }

            gotoreturn:
            return View(model);
        }

        public async Task<IActionResult> UpdateCGL(string cglrefno, UpdateCGLModel model)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (model.cgl != null)
                    {
                        if (model.cgl.ClientName == null)
                        {
                            ViewBag.Message = "Enter the Client Name";
                            goto gotoreturn;
                        }
                        if (model.cgl.Mobile == null)
                        {
                            ViewBag.Message = "Enter the Mobile NO";
                            goto gotoreturn;
                        }
                        if (model.cgl.Propertytype == null)
                        {
                            ViewBag.Message = "Enter the Property Type";
                            goto gotoreturn;
                        }
                        if (model.cgl.Propertytype == "Commercial Space")
                        {
                            if (model.cgl.Sqmtrs == null)
                            {
                                ViewBag.Message = "Enter the Square mtrs";
                                goto gotoreturn;
                            }
                        }
                        if (model.cgl.Bed == null)
                            model.cgl.Bed = "-";
                        if (model.cgl.Gym == null)
                            model.cgl.Gym = "-";
                        if (model.cgl.Pool == null)
                            model.cgl.Pool = "-";
                        if (model.cgl.Furnished == null)
                            model.cgl.Furnished = "-";
                        if (model.cgl.Garden == null)
                            model.cgl.Garden = "-";
                        if (model.cgl.Balcony == null)
                            model.cgl.Balcony = "-";
                        if (model.cgl.Parking == null)
                            model.cgl.Parking = "-";
                        if (model.cgl.Internet == null)
                            model.cgl.Internet = "-";
                        if (model.cgl.Tv == null)
                            model.cgl.Tv = "-";
                        if (model.cgl.Maidroom == null)
                            model.cgl.Maidroom = "-";
                        if (model.cgl.DriverRoom == null)
                            model.cgl.DriverRoom = "-";
                        if (model.cgl.Othersource == null)
                            model.cgl.Othersource = "-";
                        try
                        {
                            var update = await connection.ExecuteAsync($"UPDATE CGL set ClientName='{model.cgl.ClientName}',contactperson='{model.cgl.ContactPerson}',Address='{model.cgl.Address}',Email='{model.cgl.Email}',Mobile='{model.cgl.Mobile}',Phone='{model.cgl.Phone}',Propertytype='{model.cgl.Propertytype}',MinBudget='{model.cgl.MinBudget}',Maxbudget='{model.cgl.Maxbudget}',Bed='{model.cgl.Bed.Trim()}',Bath='{model.cgl.Bath}',Furnished='{model.cgl.Furnished.Trim()}',Pool='{model.cgl.Pool.Trim()}',Tv='{model.cgl.Tv.Trim()}',Internet='{model.cgl.Internet.Trim()}',Gym='{model.cgl.Gym.Trim()}',Parking='{model.cgl.Parking.Trim()}',Garden='{model.cgl.Garden.Trim()}',Balcony='{model.cgl.Balcony.Trim()}',Interest='{model.cgl.Interest}',BLOCK='{model.cgl.Block}',Nationality='{model.cgl.Nationality}',Nationality2='{model.cgl.Nationality2}',SpecialInstructions='{model.cgl.SpecialInstructions}',OtherInfo='{model.cgl.OtherInfo}' ,EnquiryDate=CONVERT(date,'{model.cgl.EnquiryDate}', 103),movingdate=CONVERT(date,'{model.cgl.Movingdate}', 103),Source='{model.cgl.Source.Trim()}',othersource='{model.cgl.Othersource.Trim()}',Maidroom='{model.cgl.Maidroom.Trim()}',Driverroom='{model.cgl.DriverRoom.Trim()}',Sqmtrs='{model.cgl.Sqmtrs}' WHERE cglrefno='{model.refno}' ");
                            ViewBag.Message = "CGL is updated along with the new Information";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "Unknown Error";
                            Console.WriteLine(ex);
                        }
                    }
                    else
                    {
                        model.actiondetails = connection.Query<Actiondetail>($"select id,REFNO,Date,Actions,id from actiondetails where refno='{cglrefno}' order by doc desc ").ToList();
                        model.driverScheduels = connection.Query<DriverScheduel>($"SELECT [L.EName] LEname,Date,FromTime,ToTime,DriverName,Remarks,comments,id FROM DriverScheduel WHERE ReferenceNo='{cglrefno}' order by id desc ").ToList();
                        var cgl = connection.Query<Cgl>($"select CGLREFNO,enquirytype,ClientName,contactperson,Address,Email,Mobile,Phone,Nationality,Nationality2,Propertytype,MinBudget,Maxbudget,Bed,Bath,Pool,Furnished,Tv,Internet,Gym,Parking,Garden,Balcony,Maidroom,Driverroom,Sqmtrs,Interest,Block,Doc,EnquiryDate,movingdate,completedby,Source,othersource,OtherInfo,SpecialInstructions,Remarks,Inquirystatus from cgl where CGLrefno='{cglrefno}' ").FirstOrDefault();
                        model.cgl = cgl;
                        model.refno = cgl.Cglrefno;
                        model.cgl.InquiryStatus = cgl.InquiryStatus.Trim();
                        model.cgl.Gym = cgl.Gym.Trim();
                        model.cgl.Pool = cgl.Pool.Trim();
                        model.cgl.Furnished = cgl.Furnished.Trim();
                        model.cgl.Garden = cgl.Garden.Trim();
                        model.cgl.Balcony = cgl.Balcony.Trim();
                        model.cgl.Parking = cgl.Parking.Trim();
                        model.cgl.Internet = cgl.Internet.Trim();
                        model.cgl.Tv = cgl.Tv.Trim();
                        model.cgl.Maidroom = cgl.Maidroom.Trim();
                        model.cgl.DriverRoom = cgl.DriverRoom.Trim();
                        model.cgl.Source = cgl.Source.TrimEnd();
                        model.cgl.Othersource = cgl.Othersource.TrimEnd();
                    }
                }
                catch (Exception e) 
                {
                    logger.Info(e.Message);
                }
                
                connection.Close();
            }
            gotoreturn:
            return View(model);
        }

        public async Task<string> CancelEnquiry(string popuprsntxt,string popupreasontxt,string referenceNo,DateTime popupdate)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                var findinquiry = referenceNo.Substring(0, 3);
                connection.Open();
                //if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
                {
                    if (findinquiry == "CGL")
                    {
                        var update = await connection.ExecuteAsync($"update cgl set InquiryStatus='Cancelled' , ReasonofCancellation='{popupreasontxt}',cancellationstatus='{popuprsntxt}',Dateofcancel='{popupdate}' where CGLREFNO='{referenceNo}' ");
                        if (update == 1)
                        {
                            message = "Enquiry is Cancelled";
                        }
                        else
                        {
                            message = "Unknown Error";
                        }
                    }
                    if (findinquiry == "PGL")
                    {
                        var update = await connection.ExecuteAsync($"update pgl set InquiryStatus='Cancelled' , ReasonofCancellation='{popupreasontxt}',cancellationstatus='{popuprsntxt}',Dateofcancel='{popupdate}' where PGLREFNO='{referenceNo}' ");
                        if (update == 1)
                        {
                            message = "Enquiry is Cancelled";
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

        public string LoadMarketingLOINo()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loino = "";
                var LOIRECORDCOUNT = connection.Query<int>("select count(*) from LOIInformation").FirstOrDefault();
                if (LOIRECORDCOUNT == 0)
                {
                    loino = "LOI_001";
                }
                else
                {
                    var MAXID = connection.Query<int>("select max(id) from LOIInformation").FirstOrDefault();
                    var NEXID = MAXID + 1;
                    loino = "LOI_00" + NEXID;
                }
                connection.Close();

                return loino;
            }
        }

        public async Task<IActionResult> MarketingLOI(string refno, string type, MarketingLOIModel model)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    if (model.LOI != null)
                    {
                        if (model.LOI.ClientName == null)
                        {
                            ViewBag.Message = "Enter the Client Name";
                            goto gotoreturn;
                        }
                        if (model.LOI.ClientSource == null)
                        {
                            ViewBag.Message = "Enter the Source";
                            goto gotoreturn;
                        }
                        if (model.LOI.PropertySource == null)
                        {
                            ViewBag.Message = "Enter Property Source";
                            goto gotoreturn;
                        }
                        if (model.LOI.PropertyName == null)
                        {
                            ViewBag.Message = "Property Name is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.PropertyRefNo == null)
                        {
                            ViewBag.Message = "Property Reference is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.Rent == 0)
                        {
                            ViewBag.Message = "Rent is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.ClientResf == 0)
                        {
                            ViewBag.Message = "Client RESF is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.Aptno == null)
                        {
                            ViewBag.Message = "Enter the Apartment NO";
                            goto gotoreturn;
                        }
                        if (model.LOI.Inqno == null)
                        {
                            ViewBag.Message = "Inquiry No is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.Cnationality == null)
                        {
                            ViewBag.Message = "Enter the Nationality";
                            goto gotoreturn;
                        }
                        if (model.LOI.Leasedate == null)
                        {
                            ViewBag.Message = "Enter the Lease Starting Date";
                            goto gotoreturn;
                        }
                        if (model.LOI.Loisigndate == null)
                        {
                            ViewBag.Message = "LOI Signed date is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.Leaseenddate == null)
                        {
                            ViewBag.Message = "Enter the Lease End date";
                            goto gotoreturn;
                        }
                        if (model.LOI.LoiStatus == null)
                        {
                            ViewBag.Message = "Enter the LOI Status";
                            goto gotoreturn;
                        }
                        if (model.LOI.LoiStatus == null)
                        {
                            ViewBag.Message = "Enter the LOI Status";
                            goto gotoreturn;
                        }
                        if (model.LOI.Lename == null)
                        {
                            ViewBag.Message = "LE Name is Required";
                            goto gotoreturn;
                        }
                        if (model.LOI.Req == null)
                        {
                            ViewBag.Message = "Enter the Requirements";
                            goto gotoreturn;
                        }
                        if (model.LOI.Fur == null)
                        {
                            ViewBag.Message = "Enter the Furnishing Option";
                            goto gotoreturn;
                        }
                        if (model.LOI.Area == null)
                        {
                            ViewBag.Message = "Enter the Area";
                            goto gotoreturn;
                        }
                        if (model.LOI.Ap == null)
                        {
                            ViewBag.Message = "Enter the Agreement Period";
                            goto gotoreturn;
                        }
                        if (model.LOI.Lcsigned == null)
                        {
                            ViewBag.Message = "Specify the LC is Signed or Not";
                            goto gotoreturn;
                        }
                        if (model.rentpaid > model.LOI.Rent)
                        {
                            ViewBag.Message = "Paid rent cannot be more than the total RENT";
                            goto gotoreturn;
                        }
                        if (model.deppaid > model.LOI.Deposit)
                        {
                            ViewBag.Message = "Paid Deposit cannot be more than the total Deposit";
                            goto gotoreturn;
                        }
                        if (model.resfpaid > model.LOI.ClientResf)
                        {
                            ViewBag.Message = "Paid RESF cannot be more than the total RESF";
                            goto gotoreturn;
                        }
                        if (model.LOI.Lcsigned == "YES")
                        {
                            model.LOI.LoiStatus = "Approved";
                        }
                        else
                        {
                            model.LOI.LoiStatus = "Open";
                        }

                        var LOIRECORDCOUNT = connection.Query<int>($"select count(*) from LOIInformation where loino='{model.LOI.LoiNo}'").FirstOrDefault();
                        if (LOIRECORDCOUNT == 0)
                        {
                            var insert = await connection.ExecuteAsync(@$"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,PropertyName,PropertyRefNo,aptno,area,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,clientRESF,LLRESF,TotClients,SearchedProperty,TotOccupants,LoiDate,LoiStatus,doc,docsubmitted,lcsigned,payment,paystatus,payremarks,DEPT,DEPTUSR,loiremarks,duerent,duedeposit,dueresf)
                            values('Internal','{model.LOI.Inqno}','{model.LOI.Lename}','{model.LOI.ClientName}','{model.LOI.Cmob}','{model.LOI.Cnationality}','{model.LOI.ClientCompany}','{model.LOI.ClientSource}','{model.LOI.Req}','{model.LOI.Fur}','{model.LOI.PropertySource}','{model.LOI.PropertyName}','{model.LOI.PropertyRefNo}','{model.LOI.Aptno}','{model.LOI.Area}',Convert(DATE,'{model.LOI.Leasedate}',103),Convert(DATE,'{model.LOI.Leaseenddate}',103),Convert(DATE,{model.LOI.Loisigndate},103),'{model.LOI.Ap}','{model.LOI.Aptype}',
                            '{model.LOI.Renttobepaidby}','{model.LOI.Rent}','{model.LOI.Securitydepositpaidby}','{model.LOI.Deposit}','{model.LOI.Feetobepaidby}','{model.LOI.ClientResf}','{model.LOI.Llresf}','1','0','1',getdate(),'{model.LOI.LoiStatus}',getdate(),'-','{model.LOI.Lcsigned}','-','-','{model.LOI.Payremarks}','MARKETING','{HttpContext.Session.GetString("CurrentUsername")}','{model.LOI.Loiremarks}','{model.LOI.Duerent}','{model.LOI.Duedeposit}','{model.LOI.Dueresf}')");
                            ViewBag.Message = "LOI is generated Successfully";
                        }
                        else
                        {
                            ViewBag.Message = "This LOI is already generated";
                            goto gotoreturn;
                        }
                        var findinquiry = model.LOI.Inqno.Substring(0, 3);
                        if (findinquiry == "CGL")
                        {
                            var cgl = await connection.ExecuteAsync($"update cgl set InquiryStatus='Approved' where CGLREFNO='{model.LOI.Inqno}' ");
                        }
                        else if (findinquiry == "PGL")
                        {
                            var pgl = await connection.ExecuteAsync($"update pgl set InquiryStatus='Approved' where PGLREFNO='{model.LOI.Inqno}' ");
                        }
                    }
                    else
                    {
                        var reccount = connection.Query<int>($"select count(*) from loiinformation where inqno='{refno}'").FirstOrDefault();
                        if (reccount > 0)
                        {
                            ViewBag.Message = "LOI is already generated for this Inquiry";
                            goto gotoreturn;
                        }

                        if (type == "PGL")
                        {
                            var pgl = connection.Query<Pgl>($"select * from pgl where Pglrefno='{refno}'").FirstOrDefault();
                            model.LOI = new();
                            model.LOI.ClientName = pgl.ClientName;
                            model.LOI.ClientCompany = pgl.ContactPerson;
                            model.LOI.ClientSource = pgl.Source;
                            model.LOI.Lename = pgl.Completedby;
                            model.LOI.Cnationality = pgl.Nationality;
                            model.LOI.Cmob = pgl.Mobile;
                            model.LOI.Inqno = refno;
                            model.statuslbl = pgl.InquiryStatus;
                        }
                        else
                        {
                            var cgl = connection.Query<Cgl>($"select * from cgl where Cglrefno='{refno}'").FirstOrDefault();
                            model.LOI = new();
                            model.LOI.ClientName = cgl.ClientName;
                            model.LOI.ClientCompany = cgl.ContactPerson;
                            model.LOI.ClientSource = cgl.Source;
                            model.LOI.Lename = cgl.Completedby;
                            model.LOI.Cnationality = cgl.Nationality;
                            model.LOI.Cmob = cgl.Mobile;
                            model.LOI.Inqno = refno;
                            model.statuslbl = cgl.InquiryStatus;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                ViewBag.Message = "Unknown Error";
                Console.WriteLine(ex);
            }

            gotoreturn:
            return View(model);
        }

        public IActionResult SearchLOI()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                ListLOIModel model = new();
                if(HttpContext.Session.GetString("CurrentUserRole") == "BDO")
                {
                    ViewBag.Message = "Access denied";
                }
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator")
                {
                    model.loiinformations = connection.Query<Loiinformation>($"select top 50 LoiNo,inqno,LEName,loisigndate,ClientName,ClientSource,PropertySource,PropertyName,PropertyRefNo,paystatus,Leasedate,case when lcsigned='YES' then 'CLOSED' else 'PENDING' end as LoiStatus from loiInformation where dept='MARKETING' and LoiStatus not in ('Cancelled') order by id desc").ToList();
                }
                else
                {
                    model.loiinformations = connection.Query<Loiinformation>($"select top 50 LoiNo,inqno,LEName,loisigndate,ClientName,ClientSource,PropertySource,PropertyName,PropertyRefNo,paystatus,Leasedate,case when lcsigned='YES' then 'CLOSED' else 'PENDING' end as LoiStatus from loiInformation where dept='MARKETING' and LoiStatus not in ('Cancelled') and lename='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();
                }
                model.noofrows = model.loiinformations.Count();
                connection.Close();
                return View(model);
            }
        }

        public async Task<IActionResult> MarketingLOIUpdate(string loino, MarketingLOIUpdateModel model)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (model.LOI == null)
                {
                    model.LOI = connection.Query<Loiinformation>($"select *, loi_no LoiNo1 from loiinformation where loino='{loino}'").FirstOrDefault();
                    model.rentpaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end  amt from paymentvoucher where inqno='{model.LOI.Inqno}' and [amt-type]='RENT'").FirstOrDefault();
                    model.deppaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end  amt from paymentvoucher where inqno='{model.LOI.Inqno}' and [amt-type]='DEPOSIT'").FirstOrDefault();
                    model.resfpaid = connection.Query<decimal>($"select case when sum(amt) is null then '0' else sum(amt) end  amt from paymentvoucher where inqno='{model.LOI.Inqno}' and [amt-type]='RESF'").FirstOrDefault();
                }
                else
                {
                    if (model.LOI.PropertySource == null)
                    {
                        ViewBag.Message = "Enter Property Souce";
                        goto gotoreturn;
                    }
                    if (model.LOI.PropertyName == null)
                    {
                        ViewBag.Message = "Property Name is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.PropertyRefNo == null)
                    {
                        ViewBag.Message = "Property Reference is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.Rent == 0)
                    {
                        ViewBag.Message = "Rent is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.Inqno == null)
                    {
                        ViewBag.Message = "Inquiry Number is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.ClientName == null)
                    {
                        ViewBag.Message = "Provide Client Name";
                        goto gotoreturn;
                    }
                    if (model.LOI.LoiStatus == "Approved")
                    {
                        if (model.LOI.Payremarks == null)
                        {
                            ViewBag.Message = "Provide Client Name";
                            goto gotoreturn;
                        }
                    }
                    if (model.LOI.ClientSource == null)
                    {
                        ViewBag.Message = "Source is mandatory";
                        goto gotoreturn;
                    }
                    if (model.LOI.Area == null)
                    {
                        ViewBag.Message = "Location is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.Req == null)
                    {
                        ViewBag.Message = "Enter the Requirements";
                        goto gotoreturn;
                    }
                    if (model.LOI.Fur == null)
                    {
                        ViewBag.Message = "Enter the Furnishing Option";
                        goto gotoreturn;
                    }
                    if (model.LOI.Leasedate == null)
                    {
                        ViewBag.Message = "Enter the Leasing Date";
                        goto gotoreturn;
                    }
                    if (model.LOI.Leaseenddate == null)
                    {
                        ViewBag.Message = "Enter the Lease Ending Date";
                        goto gotoreturn;
                    }
                    if (model.LOI.Loisigndate == null)
                    {
                        ViewBag.Message = "LOI Signed Date is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.Loisigndate == null)
                    {
                        ViewBag.Message = "LOI Signed Date is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.LoiStatus == null)
                    {
                        ViewBag.Message = "Enter the LOI Status";
                        goto gotoreturn;
                    }
                    if (model.LOI.Lename == null)
                    {
                        ViewBag.Message = "LE Name is Required";
                        goto gotoreturn;
                    }
                    if (model.LOI.Ap == null)
                    {
                        ViewBag.Message = "Enter the Agreement Period";
                        goto gotoreturn;
                    }

                    model.totrentpaid = model.rentpaid + model.newrent;
                    model.totdeppaid = model.newdeppaid + model.deppaid;
                    model.totresfpaid = model.newresfpaid + model.resfpaid;

                    if (model.LOI.LoiStatus == "Approved")
                    {
                        if (model.LOI.Lcsigned == "Yes")
                        {
                            model.LOI.LoiStatus = "Approved";
                        }
                        else
                        {
                            model.LOI.LoiStatus = "Open";
                        }
                    }

                    var update = await connection.ExecuteAsync($"UPDATE [dbo].[LOIInformation] SET EnquiryType='Internal',Cnationality='{model.LOI.Cnationality}',clientname='{model.LOI.ClientName}',clientcompany='{model.LOI.ClientCompany}',[ClientSource] ='{model.LOI.ClientSource}',req='{model.LOI.Req}',fur='{model.LOI.Fur}',area='{model.LOI.Area}',ap='{model.LOI.Ap}',aptype='{model.LOI.Aptype}',[PropertySource] ='{model.LOI.PropertySource}' ,[PropertyName] ='{model.LOI.PropertyName}' ,[PropertyRefNo] ='{model.LOI.PropertyRefNo}' ,[Aptno] ='{model.LOI.Aptno}' ,[Leasedate] =Convert(DATE,'{model.LOI.Leasedate}',103),[Renttobepaidby] ='-',[rent] ='{model.LOI.Rent}',[Securitydepositpaidby] ='-',[deposit] ='{model.LOI.Deposit}' ,[feetobepaidby] ='-',[clientRESF] ='{model.LOI.ClientResf}',[LLRESF] ='{model.LOI.Llresf}' ,[TotClients] ='1' ,[SearchedProperty] ='0',[TotOccupants] ='1' ,[LoiStatus] ='{model.LOI.LoiStatus}' , updateddate=getdate(),Leaseenddate=Convert(DATE,'{model.LOI.Leaseenddate}',103),loisigndate=Convert(DATE,'{model.LOI.Loisigndate}',103),docsubmitted='-',lcsigned='{model.LOI.Lcsigned}',payment='-',paystatus='-',payremarks='{model.LOI.Payremarks}',loi_no='{model.LOI.LoiNo1}',lc_no='{model.LOI.LcNo}',duerent='{model.LOI.Duerent}',duedeposit='{model.LOI.Duedeposit}',dueresf='{model.LOI.Dueresf}',loiremarks='{model.LOI.Loiremarks}' WHERE LoiNo='{model.LOI.LoiNo}' ");
                    if (model.LOI.LoiStatus == "Cancelled")
                    {
                        ViewBag.Message = "LOI status is cancelled so the new changes will not be updated in payments";
                    }
                    else
                    {
                        ViewBag.Message = "LOI is updated successfully";
                    }
                }
                connection.Close();
            }
            gotoreturn:
            return View(model);
        }


        public async Task<IActionResult> Property(PropertyModel model)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    if (model.Property != null)
                    {
                        model.Property.AptNo = model.aptno.ToString();
                        model.Property.Floors = model.floors.ToString();
                        model.Property.Status = model.status.ToString();

                        if (model.Property.Areasize == null)
                        {
                            model.Property.Areasize = "0";
                        }
                        if (model.Property.Units == null)
                        {
                            model.Property.Units = "0";
                        }
                        if (model.Property.BlockName.Length > 2)
                        {
                            ViewBag.Message = "Block name cannot contain more than 2 digits";
                            goto gotoreturn;
                        }
                        if (model.Property.AptNo.Length > 4)
                        {
                            ViewBag.Message = "Apartment number cannot contain more than 4 digits";
                            goto gotoreturn;
                        }
                        if (model.Property.Floors.Length > 3)
                        {
                            ViewBag.Message = "Floor number cannot contain more than 3 digits";
                            goto gotoreturn;
                        }

                        if (model.Property.PropertySource == null)
                        {
                            ViewBag.Message = "Property Source is Required";
                            goto gotoreturn;
                        }
                        if (model.Property.PropertyType == null)
                        {
                            ViewBag.Message = "Property Type is Required";
                            goto gotoreturn;
                        }
                        if (model.Property.Location == null)
                        {
                            ViewBag.Message = "Enter the Location";
                            goto gotoreturn;
                        }

                        if (model.Property.PropertySource == "ManagedProperty")
                        {
                            if (model.Property.BlockName == null)
                            {
                                ViewBag.Message = "Provide the Block Name";
                                goto gotoreturn;
                            }
                            if (model.Property.Floors == null)
                            {
                                ViewBag.Message = "Enter the Floor Info";
                                goto gotoreturn;
                            }
                            if (model.Property.Bed == null)
                            {
                                ViewBag.Message = "Enter the BedRooms Type";
                                goto gotoreturn;
                            }
                            if (model.Property.Bath == null)
                            {
                                ViewBag.Message = "Enter Bathroom details";
                                goto gotoreturn;
                            }
                            if (model.Property.AptNo == null)
                            {
                                ViewBag.Message = "Enter the Apartment No";
                                goto gotoreturn;
                            }
                            if (model.Property.Amount == null)
                            {
                                ViewBag.Message = "Rent is required";
                                goto gotoreturn;
                            }
                        }

                        if (model.Property.PropertySource == "ManagedProperty")
                        {
                            ViewBag.Message = "Managed Property Cannot be created from Marketing.";
                            goto gotoreturn;
                        }

                        if (model.Property.PropertySource == "StandAloneProperty")
                        {
                            if (model.Property.Searchdate == null)
                            {
                                ViewBag.Message = "Enter the Search Date";
                                goto gotoreturn;
                            }
                            if (model.Property.Availabledate == null)
                            {
                                ViewBag.Message = "Enter the Available Date";
                                goto gotoreturn;
                            }
                            if (model.Property.Source == null)
                            {
                                ViewBag.Message = "Provide the Source";
                                goto gotoreturn;
                            }
                            if (model.Property.Vistedby == null)
                            {
                                ViewBag.Message = "Provide visited by Info";
                                goto gotoreturn;
                            }
                            if (model.Property.BldgName == null)
                            {
                                ViewBag.Message = "Bulding Name is required";
                                goto gotoreturn;
                            }

                            if (model.Property.PropertyType != "Commercial Space" && model.Property.PropertyType != "Commercial" && model.Property.PropertyType != "Residential" && model.Property.PropertyType != "Studio")
                            {
                                if (model.Property.Bed == null)
                                {
                                    ViewBag.Message = "Enter the Bedroom type";
                                    goto gotoreturn;
                                }
                            }

                            if (model.Property.Resf == null)
                            {
                                ViewBag.Message = "Enter RESF Option is required";
                                goto gotoreturn;
                            }
                        }

                        if (model.Property.Status == null)
                        {
                            ViewBag.Message = "Status is required";
                            goto gotoreturn;
                        }
                        if (model.Property.Imagepath == null)
                        {
                            ViewBag.Message = "Picture Path is not mentioned";
                            goto gotoreturn;
                        }

                        int reccount = 0;
                        if (model.Property.PropertySource == "StandAloneProperty")
                        {
                            reccount = connection.Query<int>($"select count(*) from propertymaster where propertysource='{model.Property.PropertySource}' and location='{model.Property.Location}' and BldgName='{model.Property.BldgName}' and  blockname='{model.Property.BlockName}' ").FirstOrDefault();
                        }
                        else
                        {
                            reccount = connection.Query<int>($"select count(*) from propertymaster where propertysource='{model.Property.PropertySource}' and location='{model.Property.Location}' and PropertyType='{model.Property.PropertyType}' and BldgName='{model.Property.BldgName}' and  blockname='{model.Property.BlockName}' and floors='{model.Property.Floors}' and aptno='{model.Property.AptNo}' ").FirstOrDefault();
                        }

                        if (reccount == 0)
                        {
                            string[] type = model.propertytypes.Split(',');
                            string[] bed = model.bedrooms.Split(',');
                            string[] rent = model.rents.Split(',');
                            if (type.Count() > 0 && bed.Count() > 0 && rent.Count() > 0)
                            {
                                for (int i = 0; i < type.Count(); i++)
                                {
                                    var insert = await connection.ExecuteAsync($@"INSERT INTO [dbo].[propertymaster]([PropertyRef],[PropertySource],[Location],[PropertyType],[BldgName],[paci],[BlockName],[streetname],[BldgNo],[Floors],[AptNo],[Units],[Bed],[Bath],[Balcony],[Kitchen],[LivingRoom],[StudyRoom],[MaidRoom],[Areasize],[Amount],[SalesType],[Pool],[Parking],[Garden],[Internet],[Gym],[Security],[Furnished],[PlayArea],[CabelTv],[CCTV],[OSN],[Ac],[pdate],[Status],[Description],[source],[poc],[pocnumber],[sqmtrs],[facilities],[ppersqmtr],[searchdate],[availabledate],[updatedby],[vistedby],[imagepath],[doc],[resf],[seaview],[kwtaccept]) 
                                values('{model.propref}','{model.Property.PropertySource}','{model.Property.Location}','{type[i]}','{model.Property.BldgName}','{model.Property.Paci}','{model.Property.BlockName}','{model.Property.StreetName}','{model.Property.BldgNo}','{model.Property.Floors}','{model.Property.AptNo}','{model.Property.Units}','{bed[i]}','{model.Property.Bath}','{model.Property.Balcony}','{model.Property.Kitchen}','{model.Property.LivingRoom}','{model.Property.StudyRoom}','{model.Property.MaidRoom}','{model.Property.Areasize}','{rent[i]}','{model.Property.SalesType}','{model.Property.Pool}','{model.Property.Parking}',
                                '{model.Property.Garden}','{model.Property.Internet}','{model.Property.Gym}','{model.Property.Security}','{model.Property.Furnished}','{model.Property.PlayArea}','{model.Property.CabelTv}','{model.Property.Cctv}','{model.Property.Osn}','{model.Property.Ac}',getdate(),'{model.Property.Status}','{model.Property.Description}','{model.Property.Source}','{model.Property.Poc}','{model.Property.Pocnumber}','{model.Property.Sqmtrs}','-','{model.Property.Ppersqmtr}',Convert(Date,'{model.Property.Searchdate}',103),Convert(Date,'{model.Property.Availabledate}',103),'{model.Property.Updatedby}','{model.Property.Vistedby}','{model.Property.Imagepath}',getdate(),'{model.Property.Resf}','{model.Property.Seaview}','{model.Property.Kwtaccept}')");
                                }
                                ViewBag.Message = "Property Created";
                            }
                            else
                            {
                                if (model.Property.Amount == null)
                                {
                                    ViewBag.Message = "Rent is required";
                                    goto gotoreturn;
                                }
                                if (model.Property.Bed == null)
                                {
                                    ViewBag.Message = "Bedroom is required";
                                    goto gotoreturn;
                                }

                                var insert = await connection.ExecuteAsync($@"INSERT INTO [dbo].[propertymaster]([PropertyRef],[PropertySource],[Location],[PropertyType],[BldgName],[paci],[BlockName],[streetname],[BldgNo],[Floors],[AptNo],[Units],[Bed],[Bath],[Balcony],[Kitchen],[LivingRoom],[StudyRoom],[MaidRoom],[Areasize],[Amount],[SalesType],[Pool],[Parking],[Garden],[Internet],[Gym],[Security],[Furnished],[PlayArea],[CabelTv],[CCTV],[OSN],[Ac],[pdate],[Status],[Description],[source],[poc],[pocnumber],[sqmtrs],[facilities],[ppersqmtr],[searchdate],[availabledate],[updatedby],[vistedby],[imagepath],[doc],[resf],[seaview],[kwtaccept]) 
                            values('{model.propref}','{model.Property.PropertySource}','{model.Property.Location}','{model.Property.PropertyType}','{model.Property.BldgName}','{model.Property.Paci}','{model.Property.BlockName}','{model.Property.StreetName}','{model.Property.BldgNo}','{model.Property.Floors}','{model.Property.AptNo}','{model.Property.Units}','{model.Property.Bed}','{model.Property.Bath}','{model.Property.Balcony}','{model.Property.Kitchen}','{model.Property.LivingRoom}','{model.Property.StudyRoom}','{model.Property.MaidRoom}','{model.Property.Areasize}','{model.Property.Amount}','{model.Property.SalesType}',
                            '{model.Property.Pool}','{model.Property.Parking}','{model.Property.Garden}','{model.Property.Internet}','{model.Property.Gym}','{model.Property.Security}','{model.Property.Furnished}','{model.Property.PlayArea}','{model.Property.CabelTv}','{model.Property.Cctv}','{model.Property.Osn}','{model.Property.Ac}',getdate(),'{model.Property.Status}','{model.Property.Description}','{model.Property.Source}','{model.Property.Poc}','{model.Property.Pocnumber}','{model.Property.Sqmtrs}','-','{model.Property.Ppersqmtr}',Convert(Date,'{model.Property.Searchdate}',103),Convert(Date,'{model.Property.Availabledate}',103),'{model.Property.Updatedby}','{model.Property.Vistedby}','{model.Property.Imagepath}',getdate(),'{model.Property.Resf}','{model.Property.Seaview}','{model.Property.Kwtaccept}')");

                                ViewBag.Message = "Property Created";
                            }
                        }
                        else
                        {
                            ViewBag.Message = "This Property is aready available";
                        }
                    }
                }
                catch(Exception ex)
                {
                    logger.Info(ex.Message);
                }
                
                connection.Close();
            }
            gotoreturn:
            return View();
        }

        public IActionResult PropertySearch()
        {
            if(HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
            {
                ViewBag.Message = "Access denied";
            }
            return View();
        }

        public IActionResult PropertySearchMP()
        {
            PropertySearchModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //model.propertySearchLists = connection.Query<PropertySearchList>($"select sum(vacant) as Vacant ,sum(occpied) as Occupied ,sum(reserved) as Reserved,bldgname BuildingName,Location from (select 1 as vacant,0 as occpied,0 as reserved ,BldgName,Location from propertymaster where PropertySource ='managedproperty' and Status ='Available' union all select 0 as vacant,1 as occpied,0 as reserved,BldgName ,Location from propertymaster where id in (select id from propertymaster where reservation is null or reservation ='' or len(reservation)=0 )and PropertySource ='managedproperty' and Status ='NotAvailable' union all select 0 as vacant,0 as occpied,1 as reserved,BldgName,Location from propertymaster where PropertySource ='managedproperty' and Status ='NotAvailable' and  reservation ='yes')src group by BldgName ,Location order by bldgname").ToList();
                model.propertySearchLists = connection.Query<PropertySearchList>($"select sum(vacant) as Vacant ,sum(occpied) as Occupied ,sum(reserved) as Reserved,bldgname BuildingName,Location,PropertySource from (select 1 as vacant,0 as occpied,0 as reserved ,BldgName,Location,PropertySource from propertymaster where PropertySource ='managedproperty' and Status ='Available' union all select 0 as vacant,1 as occpied,0 as reserved,BldgName ,Location,PropertySource from propertymaster where id in (select id from propertymaster where reservation is null or reservation ='' or len(reservation)=0 )and PropertySource ='managedproperty' and Status ='NotAvailable' union all select 0 as vacant,0 as occpied,1 as reserved,BldgName,Location,PropertySource from propertymaster where PropertySource ='managedproperty' and Status ='NotAvailable' and  reservation ='yes')src group by BldgName ,Location,PropertySource order by bldgname").ToList();
                model.propertymasters = null;
                connection.Close();
            }
            return View(model);
        }


        public IActionResult PropertySearchMP2(string propertyname)//aptlist of a property
        {
            PropertySearchModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.propertymasters = connection.Query<Propertymaster>($"select PropertyRef,BldgName,BlockName,Floors,AptNo,Bed,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,Status from propertymaster where BldgName='{propertyname}' and propertysource='managedproperty' order by PropertyRef,BldgName,BlockName,Floors,Bed ").ToList();
                model.propertySearchLists = null;
                connection.Close();
            }
            return View(model);
        }

        public IActionResult PropertySearchSP()
        {
            PropertySearchModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.propertymasters = connection.Query<Propertymaster>($"select id,propertyref,BldgName,status,PropertyType,Location,BlockName,Bed,Bath,Furnished,Kitchen,Amount,PropertySource from propertymaster where PropertySource='StandAloneProperty' and status='Available' order by ID desc ").ToList();
                model.propertySearchLists = null;
                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> PropertyDetail(string propertyref, string propertysource, string bed, string propertytype, int id, PropertyModel model)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (model.Property == null)
                {
                    model.Property = new();
                    if (propertysource == "StandAloneProperty")
                    {
                        var property = connection.Query<Propertymaster>($"select [PropertyRef],kwtaccept,[seaview],[PropertySource],[Location],[PropertyType],[BldgName],[paci],[BlockName],[streetname],[bldgno],[Floors],[AptNo],[Units],[Bed],[Bath],[Balcony],[Kitchen],[LivingRoom],[StudyRoom],[MaidRoom],[Areasize],[Amount],[SalesType],[Pool],[Parking],[Garden],[Internet],[Gym],[Security],[Furnished],[PlayArea],[CabelTv],[CCTV],[Ac],[osn],[pdate],[Status],[Description],[source],[poc],[pocnumber],[sqmtrs],ppersqmtr,[searchdate],[availabledate],[updatedby],[vistedby],[imagepath],[doc],[resf] from Propertymaster where PropertyRef='{propertyref}' and PropertyType='{propertytype}' and bed='{bed}' and id={id}").FirstOrDefault();
                        model.Property = property;
                        model.status = property.Status;
                        model.aptno = property.AptNo;
                        model.floors = property.Floors;
                        model.Property.Kitchen = property.Kitchen.Trim();
                        model.Property.Balcony = property.Balcony.Trim();
                        model.Property.Parking = property.Parking.Trim();
                        model.Property.Pool = property.Pool.Trim();
                        model.Property.Gym = property.Gym.Trim();
                        model.Property.Garden = property.Garden.Trim();
                        model.Property.Osn = property.Osn.Trim();
                        model.Property.Cctv = property.Cctv.Trim();
                        model.Property.Internet = property.Internet.Trim();
                        model.Property.CabelTv = property.CabelTv.Trim();
                        model.Property.Security = property.Security.Trim();
                        model.Property.LivingRoom = property.LivingRoom.Trim();
                        model.Property.StudyRoom = property.StudyRoom.Trim();
                        model.Property.MaidRoom = property.MaidRoom.Trim();
                        model.Property.PlayArea = property.PlayArea.Trim();
                        model.Property.Ac = property.Ac.Trim();
                        model.Property.Seaview = property.Seaview.Trim();
                        model.Property.Kwtaccept = property.Kwtaccept.Trim();
                        //have to do file loading
                    }
                    else
                    {
                        var property = connection.Query<Propertymaster>($"select [PropertyRef],kwtaccept,[seaview],[PropertySource],[Location],[PropertyType],[BldgName],[paci],[BlockName],[streetname],[bldgno],[Floors],[AptNo],[Units],[Bed],[Bath],[Balcony],[Kitchen],[LivingRoom],[StudyRoom],[MaidRoom],[Areasize],[Amount],[SalesType],[Pool],[Parking],[Garden],[Internet],[Gym],[Security],[Furnished],[PlayArea],[CabelTv],[CCTV],[Ac],[osn],[pdate],[Status],[Description],[source],[poc],[pocnumber],[sqmtrs],ppersqmtr,[searchdate],[availabledate],[updatedby],[vistedby],[imagepath],[doc],[resf] from Propertymaster where PropertyRef='{propertyref}' ").FirstOrDefault();
                        model.Property = property;
                        model.status = property.Status;
                        model.aptno = property.AptNo;
                        model.floors = property.Floors;
                        if (property.Kitchen != null)
                            model.Property.Kitchen = property.Kitchen.Trim();
                        else
                            model.Property.Kitchen = "";

                        if (property.Balcony != null)
                            model.Property.Balcony = property.Balcony.Trim();
                        else
                            model.Property.Balcony = "";

                        if (property.Kwtaccept != null)
                            model.Property.Kwtaccept = property.Kwtaccept.Trim();
                        else
                            model.Property.Kwtaccept = "";

                        if (property.Seaview != null)
                            model.Property.Seaview = property.Seaview.Trim();
                        else
                            model.Property.Seaview = "";

                        if (property.Ac != null)
                            model.Property.Ac = property.Ac.Trim();
                        else
                            model.Property.Ac = "";

                        if (property.PlayArea != null)
                            model.Property.PlayArea = property.PlayArea.Trim();
                        else
                            model.Property.PlayArea = "";

                        if (property.MaidRoom != null)
                            model.Property.MaidRoom = property.MaidRoom.Trim();
                        else
                            model.Property.MaidRoom = "";

                        if (property.StudyRoom != null)
                            model.Property.StudyRoom = property.StudyRoom.Trim();
                        else
                            model.Property.StudyRoom = "";

                        if (property.LivingRoom != null)
                            model.Property.LivingRoom = property.LivingRoom.Trim();
                        else
                            model.Property.LivingRoom = "";

                        if (property.Security != null)
                            model.Property.Security = property.Security.Trim();
                        else
                            model.Property.Security = "";

                        if (property.CabelTv != null)
                            model.Property.CabelTv = property.CabelTv.Trim();
                        else
                            model.Property.CabelTv = "";

                        if (property.Internet != null)
                            model.Property.Internet = property.Internet.Trim();
                        else
                            model.Property.Internet = "";

                        if (property.Cctv != null)
                            model.Property.Cctv = property.Cctv.Trim();
                        else
                            model.Property.Cctv = "";

                        if (property.Osn != null)
                            model.Property.Osn = property.Osn.Trim();
                        else
                            model.Property.Osn = "";

                        if (property.Garden != null)
                            model.Property.Garden = property.Garden.Trim();
                        else
                            model.Property.Garden = "";

                        if (property.Gym != null)
                            model.Property.Gym = property.Gym.Trim();
                        else
                            model.Property.Gym = "";

                        if (property.Pool != null)
                            model.Property.Pool = property.Pool.Trim();
                        else
                            model.Property.Pool = "";

                        if (property.Parking != null)
                            model.Property.Parking = property.Parking.Trim();
                        else
                            model.Property.Parking = "";
                    }
                    //have to do file loading
                }
                else
                {
                    model.Property.AptNo = model.aptno.ToString();
                    model.Property.Floors = model.floors.ToString();
                    model.Property.Status = model.status.ToString();

                    if (model.Property.Areasize == null)
                    {
                        model.Property.Areasize = "0";
                    }
                    if (model.Property.Units == null)
                    {
                        model.Property.Units = "0";
                    }
                    if (model.Property.Location == null)
                    {
                        ViewBag.Message = "Enter the Location";
                        goto gotoreturn;
                    }
                    if (model.Property.PropertySource == null)
                    {
                        ViewBag.Message = "Property Source is Required";
                        goto gotoreturn;
                    }
                    if (model.Property.PropertyType == null)
                    {
                        ViewBag.Message = "Property Type is Required";
                        goto gotoreturn;
                    }
                    if (model.Property.BldgName == null)
                    {
                        ViewBag.Message = "Bulding Name is required";
                        goto gotoreturn;
                    }
                    if (model.Property.Amount == null)
                    {
                        ViewBag.Message = "Rent is required";
                        goto gotoreturn;
                    }
                    if (model.Property.Status == null)
                    {
                        ViewBag.Message = "Status is required";
                        goto gotoreturn;
                    }
                    if (model.Property.Imagepath == null)
                    {
                        ViewBag.Message = "Picture Path is not mentioned";
                        goto gotoreturn;
                    }

                    if (model.Property.PropertySource == "ManagedProperty")
                    {
                        if (model.Property.BlockName == null)
                        {
                            ViewBag.Message = "Provide the Block Name";
                            goto gotoreturn;
                        }
                        if (model.Property.Floors == null)
                        {
                            ViewBag.Message = "Enter the Floor Info";
                            goto gotoreturn;
                        }
                        if (model.Property.Bed == null)
                        {
                            ViewBag.Message = "Enter the BedRooms available";
                            goto gotoreturn;
                        }
                        if (model.Property.Bath == null)
                        {
                            ViewBag.Message = "Enter Bathroom details";
                            goto gotoreturn;
                        }
                        if (model.Property.AptNo == null)
                        {
                            ViewBag.Message = "Enter the Apartment No";
                            goto gotoreturn;
                        }

                        var update = await connection.ExecuteAsync($"update [propertymaster] set [PropertySource]='{model.Property.PropertySource}',[Location]='{model.Property.Location}',[PropertyType]='{model.Property.PropertyType}',[BldgName]='{model.Property.BldgName}',paci='{model.Property.Paci}',[BlockName]='{model.Property.BlockName}',[Floors]='{model.Property.Floors}',[AptNo]='{model.Property.AptNo}',[Units]='{model.Property.Units}',[Bed]='{model.Property.Bed}',[Bath]='{model.Property.Bath}',[Balcony]='{model.Property.Balcony}',[Kitchen]='{model.Property.Kitchen}',[LivingRoom]='{model.Property.LivingRoom}',[StudyRoom]='{model.Property.StudyRoom}',[MaidRoom]='{model.Property.MaidRoom}',[Areasize]='{model.Property.Areasize}',[Amount]='{model.Property.Amount}',[SalesType]='{model.Property.SalesType}',[Pool]='{model.Property.Pool}',[Parking]='{model.Property.Parking}',[Garden]='{model.Property.Garden}',[Internet]='{model.Property.Internet}',[Gym]='{model.Property.Gym}',[Security]='{model.Property.Security}',[Furnished]='{model.Property.Furnished}',[PlayArea]='{model.Property.PlayArea}',[CabelTv]='{model.Property.CabelTv}',[CCTV]='{model.Property.Cctv}',[osn]='{model.Property.Osn}',[Ac]='{model.Property.Ac}',[Status]='{model.Property.Status}',[Description]='{model.Property.Description}',[source]='{model.Property.Source}',[poc]='{model.Property.Poc}',[sqmtrs]='{model.Property.Sqmtrs}',[ppersqmtr]='{model.Property.Ppersqmtr}',[searchdate]='{model.Property.Searchdate}',[availabledate]='{model.Property.Availabledate}',[updatedby]='{model.Property.Updatedby}',[vistedby]='{model.Property.Vistedby}',[imagepath]='{model.Property.Imagepath}' , dou=getdate(),seaview='{model.Property.Seaview}',kwtaccept='{model.Property.Kwtaccept}' where propertyref='{model.Property.PropertyRef}'");
                        ViewBag.Message = $"Property with Reference {model.Property.PropertyRef} is Modified";
                    }
                    else
                    {
                        if (model.Property.Searchdate == null)
                        {
                            ViewBag.Message = "Enter the Search Date";
                            goto gotoreturn;
                        }
                        if (model.Property.Source == null)
                        {
                            ViewBag.Message = "Provide the Source";
                            goto gotoreturn;
                        }
                        if (model.Property.Vistedby == null)
                        {
                            ViewBag.Message = "Provide visited by Info";
                            goto gotoreturn;
                        }
                        if (model.Property.Resf == null)
                        {
                            ViewBag.Message = "Enter RESF Option is required";
                            goto gotoreturn;
                        }

                        var recordcount = connection.Query<int>($"select count(*) from propertymaster where Propertysource='StandAloneProperty' and propertyref='{model.Property.PropertyRef}' and bed='{model.Property.Bed}' and [PropertyType]='{model.Property.PropertyType}' AND ID NOT IN({model.Property.Id}) ").FirstOrDefault();
                        if (recordcount > 0)
                        {
                            ViewBag.Message = "This type of bed is already available";
                            goto gotoreturn;
                        }
                        else
                        {
                            var update = await connection.ExecuteAsync($"update [propertymaster] set [PropertySource]='{model.Property.PropertySource}',[Location]='{model.Property.Location}',[PropertyType]='{model.Property.PropertyType}',[BldgName]='{model.Property.BldgName}',paci='{model.Property.Paci}',[BlockName]='{model.Property.BlockName}',streetname='{model.Property.StreetName}',bldgno='{model.Property.BldgNo}',[Floors]='{model.Property.Floors}',[AptNo]='{model.Property.AptNo}',[Units]='{model.Property.Units}',[Bed]='{model.Property.Bed}',[Bath]='{model.Property.Bath}',[Balcony]='{model.Property.Balcony}',[Kitchen]='{model.Property.Kitchen}',[LivingRoom]='{model.Property.LivingRoom}',[StudyRoom]='{model.Property.StudyRoom}',[MaidRoom]='{model.Property.MaidRoom}',[Areasize]='{model.Property.Areasize}',[Amount]='{model.Property.Amount}',[SalesType]='{model.Property.SalesType}',[Pool]='{model.Property.Pool}',[Parking]='{model.Property.Parking}',[Garden]='{model.Property.Garden}',[Internet]='{model.Property.Internet}',[Gym]='{model.Property.Gym}',[Security]='{model.Property.Security}',[Furnished]='{model.Property.Furnished}',[PlayArea]='{model.Property.PlayArea}',[CabelTv]='{model.Property.CabelTv}',[CCTV]='{model.Property.Cctv}',[osn]='{model.Property.Osn}',[Ac]='{model.Property.Ac}',[Status]='{model.Property.Status}',[Description]='{model.Property.Description}',[source]='{model.Property.Source}',[poc]='{model.Property.Poc}',[pocnumber]='{model.Property.Pocnumber}',[sqmtrs]='{model.Property.Sqmtrs}',[ppersqmtr]='{model.Property.Ppersqmtr}',[searchdate]='{model.Property.Searchdate}',[availabledate]='{model.Property.Availabledate}',[updatedby]='{model.Property.Updatedby}',[vistedby]='{model.Property.Vistedby}',[imagepath]='{model.Property.Imagepath}' , dou=getdate(),resf='{model.Property.Resf}' ,seaview='{model.Property.Seaview}',kwtaccept='{model.Property.Kwtaccept}' where propertyref='{model.Property.PropertyRef}' and id={model.Property.Id}");
                            ViewBag.Message = $"Property with Reference {model.Property.PropertyRef} is Modified";
                        }
                    }
                }
                connection.Close();
            }
            gotoreturn:
            return View(model);
        }

        public IActionResult DriverAvailability()
        {
            DriverAvailabilityModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //model.Driver1 = connection.Query<string>("select drivername from drivers where id=1").FirstOrDefault();
                //model.Driver2 = connection.Query<string>("select drivername from drivers where id=2").FirstOrDefault();
                //model.Driver3 = connection.Query<string>("select drivername from drivers where id=3").FirstOrDefault();

                model.Drivername = connection.Query<string>("select drivername from drivers where id in (1,2,3)").ToList();
                connection.Close();
            }
            return View(model);
        }

        public JsonResult BtnViewDriverAvailability(DateTime datetxt)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var date = datetxt.ToString("yyyy-MM-dd");
                //logger.Info(datetxt);
//#if DEBUG
//                var date = datetxt.ToString("yyyy-MM-dd");
//#else
//                    var date = datetxt.ToString("yyyy/MM/dd");
//#endif

                logger.Info(date);
                connection.Open();
                //var grid1 = connection.Query<DriverScheduel>($"select fromtime,totime,referenceno,Drivername,driverid,case when groupleyesno='TRUE' then grouple else  [L.ENAME] end as LEname,STYPE,id from DriverScheduel where DriverName in ('Sanjeeva Reddy','Mahesh Dudella','Basha Syed') and date='{date}'").ToList();
                var grid1 = connection.Query<DriverScheduel>($"select fromtime,totime,referenceno,Drivername,driverid,case when groupleyesno='TRUE' then grouple else  [L.ENAME] end as LEname,STYPE,id from DriverScheduel where date='{date}'").ToList();
                connection.Close();
                return Json(grid1);
            }
        }

        public List<string> GetPROPERTNAMEForDriverAvailability(int sid)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var propertname = connection.Query<string>($"select PROPERTNAME from ptv where sid='{sid}'").ToList();
                connection.Close();
                return propertname;
            }
        }

        public IActionResult ContactedCorporatesMain()
        {
            if(HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
            {
                ViewBag.Message = "Access denied";
            }
            return View();
        }

        public async Task<IActionResult> ContactedCorporates(ContactedCorporatesModel model)
        {
            if (model.cc != null)
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    if (model.cc.Ccname == null)
                    {
                        ViewBag.Message = "Company Name is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Cname == null)
                    {
                        model.cc.Cname = "-";
                    }
                    if (model.cc.Cdesg == null)
                    {
                        model.cc.Cdesg = "-";
                    }
                    if (model.cc.Cmobile == null)
                    {
                        ViewBag.Message = "Contact NO is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Fdat == null)
                    {
                        ViewBag.Message = "Followup date is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Cdate == null)
                    {
                        ViewBag.Message = "Contacted date is required";
                        goto gotoreturn;
                    }
                    if (model.appointment)
                    {
                        model.cc.Appointment = "YES";
                        if (model.cc.Appdate == null)
                        {
                            ViewBag.Message = "Enter the appointment date";
                            goto gotoreturn;
                        }
                    }
                    else
                    {
                        model.cc.Appointment = "NO";
                    }

                    var MAXID = await connection.QueryAsync<int>($"insert into cc(ccname,cname,cmobile,cdesg,cdate,appointment,appdate,fdat,doc,lename,remarks)values('{model.cc.Ccname}','{model.cc.Cname}','{model.cc.Cmobile}','{model.cc.Cdesg}','{model.cc.Cdate}','{model.cc.Appointment}','{model.cc.Appdate}','{model.cc.Fdat}',getdate(),'{HttpContext.Session.GetString("CurrentUsername")}','{model.cc.Remarks}'); SELECT CAST(SCOPE_IDENTITY() as int )");
                    if (model.cc.Fdat == null)
                    {
                        var updateFdat = await connection.ExecuteAsync($"update cc set fdat=null where id={MAXID.FirstOrDefault()}");
                    }
                    if (model.cc.Appdate == null)
                    {
                        var updateAppdate = await connection.ExecuteAsync($"update cc set appdate=null where id={MAXID.FirstOrDefault()}");
                    }
                    ViewBag.Message = "Contacts Saved Sucessfully!";
                    connection.Close();
                }
            }

            gotoreturn:
            return View();
        }

        public IActionResult ContactedCorporatesList()
        {
            List<Cc> model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator")
                {
                    model = connection.Query<Cc>("select ccname,cname,cmobile,cdesg,cdate,appointment,appdate,CONVERT(VARCHAR(11),fdat,106) as fdat,doc,lename,id from cc order by id desc").ToList();
                }
                else
                {
                    var recordcount = connection.Query<int>($"select count(*) as rec from cc where lename='{HttpContext.Session.GetString("CurrentUsername")}'").FirstOrDefault();
                    if (recordcount > 0)
                    {
                        model = connection.Query<Cc>($"select ccname,cname,cmobile,cdesg,cdate,appointment,appdate,CONVERT(VARCHAR(11),fdat,106) as fdat,doc,lename,id from cc where lename='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();
                    }
                }
                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateContactedCorporates(int id, ContactedCorporatesModel model)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (model.cc == null)
                {
                    var cc = connection.Query<Cc>($"select ccname,cname,cmobile,cdesg,CONVERT(VARCHAR(11),cdate,106) as cdate,appointment,appdate,CONVERT(VARCHAR(11),fdat,106) as fdat,doc,lename,id,remarks from cc where id={id}").FirstOrDefault();
                    model.cc = cc;
                    if (cc.Appointment.Trim() == "YES")
                    {
                        model.appointment = true;
                    }
                    else
                    {
                        model.appointment = false;
                    }
                }
                else
                {
                    if (model.cc.Ccname == null)
                    {
                        ViewBag.Message = "Company Name is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Cmobile == null)
                    {
                        ViewBag.Message = "Contact NO is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Fdat == null)
                    {
                        ViewBag.Message = "Followup date is required";
                        goto gotoreturn;
                    }
                    if (model.cc.Cname == null)
                    {
                        model.cc.Cname = "-";
                    }
                    if (model.cc.Cdesg == null)
                    {
                        model.cc.Cdesg = "-";
                    }
                    if (model.appointment)
                    {
                        model.cc.Appointment = "YES";
                        if (model.cc.Appdate == null)
                        {
                            ViewBag.Message = "Enter the appointment date";
                            goto gotoreturn;
                        }
                        else
                        {
                            //var update = await connection.ExecuteAsync($"update cc set ccname='{model.cc.Ccname}',cname='{model.cc.Cname}',cmobile='{model.cc.Cmobile}',cdesg='{model.cc.Cdesg}',cdate='{model.cc.Cdate}',appointment='{model.cc.Appointment}',appdate='{model.cc.Appdate}',fdat='{model.cc.Fdat}',remarks='{model.cc.Remarks}'  where id='{model.cc.Id}'");
                        }
                    }
                    else
                    {
                        model.cc.Appointment = "NO";
                        //var update = await connection.ExecuteAsync($"update cc set ccname='" & Trim(companytxt.Text) & "',cname='" & Trim(nametxt.Text) & "',cmobile='" & Trim(mobtxt.Text) & "',cdesg='" & Trim(desgtxt.Text) & "',cdate='" & Trim(cmadetxt.Text) & "',appointment='" & appmade & "',appdate='" & Trim(appdatetxt.Text) & "',fdat='" & Trim(fdatetxt.Text) & "',remarks='" & Trim(remtxt.Text) & "'  where id='" & ccid & "'");
                    }
                    var update = await connection.ExecuteAsync($"update cc set ccname='{model.cc.Ccname}',cname='{model.cc.Cname}',cmobile='{model.cc.Cmobile}',cdesg='{model.cc.Cdesg}',cdate=Convert(Date,'{model.cc.Cdate}',103),appointment='{model.cc.Appointment}',appdate=Convert(Date,'{model.cc.Appdate}',103),fdat=Convert(Date,'{model.cc.Fdat}',103),remarks='{model.cc.Remarks}' where id='{model.cc.Id}'");
                    ViewBag.Message = "Updated Successfully";

                }
                connection.Close();
            }

            gotoreturn:
            return View(model);
        }

        public IActionResult DailyProgress()
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            if (HttpContext.Session.GetString("CurrentUserRole") != "BDO")
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    List<DailyProgressModel> model = new();
                    connection.Open();
                    try
                    {
                        model = connection.Query<DailyProgressModel>($"select fromtime,totime,referenceno,Drivername,clentname,driverid,[L.ENAME],remarks,STYPE,id,comments from DriverScheduel where date=Convert(Date,'{DateTime.Today}',103) AND  DRIVERID NOT IN(1,2,3) ").ToList();
                        model = connection.Query<DailyProgressModel>($"select fromtime,totime,reason remarks,advertising,username,otherreason comments from adminactivities where date=Convert(Date,'{DateTime.Today}',103) ").ToList();
                    }
                    catch (Exception e)
                    {
                        logger.Info(e.Message);
                    }

                    connection.Close();
                    return View(model);
                }
            }
            else
            {
                ViewBag.Message = "Access denied";
                return View();
            }

        }

        public List<DailyProgressModel> DailyProgressSearch(string date)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<DailyProgressModel> model = new();
                model = connection.Query<DailyProgressModel>($"select fromtime,totime,referenceno refno,Drivername drivername,clentname clientname,driverid,[L.ENAME] lename,remarks,STYPE scheduletype,id,comments from DriverScheduel where date='{date}' AND DRIVERID NOT IN(1,2,3) ").ToList();
                model = connection.Query<DailyProgressModel>($"select fromtime,totime,reason remarks,advertising,username lename,otherreason comments,id from adminactivities where date='{date}' ").ToList();
                connection.Close();
                return model;
            }
        }

        public IActionResult Adhoc()
        {
            return View();
        }

        public IActionResult DriverScheduling()
        {
            return View();
        }

        public async Task<string> SaveDriverScheduling(string date, string ft, string fromtime, string tt, string totime, string scheduletype, string clientname, string contactno, string referenceno, string address, string drivername, bool multipleLe, string lenamelist, string grouplename, string lename, string reasonPN, string otherreason, string detailsRemarks, string otherinstructionComments)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            string message = "";
            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();

                    //DateTime dt1 = DateTime.ParseExact(ft, "HH:mm", null, DateTimeStyles.None);
                    //string fromtime = dt1.ToString("hh:mm tt");
                    //DateTime dt2 = DateTime.ParseExact(tt, "hh:mm tt", null, DateTimeStyles.None);
                    //string totime = dt2.ToString("hh:mm tt");

                    var tourrec = connection.Query<int>($"select count(*) from (SELECT * FROM DriverScheduel WHERE '{ft}'  between ft and tt  or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where drivername='{drivername}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                    var DID = connection.Query<int>($"SELECT ID FROM DRIVERS WHERE DriverName='{drivername}'").FirstOrDefault();

                    if (multipleLe)
                    {
                        //var tourrec = connection.Query<int>($"select count(*) from (SELECT * FROM DriverScheduel WHERE '{ft}'  between ft and tt  or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where drivername='{drivername}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                        //var DID = connection.Query<int>($"SELECT ID FROM DRIVERS WHERE DriverName='{drivername}'").FirstOrDefault();

                        if (tourrec == 0)
                        {
                            var grplelist = grouplename.Split(',').ToList();
                            foreach (var le in grplelist)
                            {
                                var MAXID = await connection.QueryAsync<int>($"insert into DriverScheduel(Date,Stype,FromTime,ToTime,ft,tt,DriverId,DriverName,[L.EName],ReferenceNo,ClentName,remarks,Mobile,doc,Otherreason,Comments,propertyname,grouple,groupleyesno)values('{Convert.ToDateTime(date).ToString("yyyy-MM-dd")}','{scheduletype}','{fromtime}','{totime}','{ft}','{tt}','{DID}','{drivername}','{le}','{referenceno}','{clientname}','{detailsRemarks}','{contactno}',getdate(),'{otherreason}','{otherinstructionComments}','{reasonPN}','{grouplename}','TRUE'); SELECT CAST(SCOPE_IDENTITY() as int )");
                                var insertPTV = await connection.ExecuteAsync($"INSERT INTO PTV(SID,PROPERTYREF,PROPERTNAME,RENT,POC,SOURCE,LOCATION,pdesc)VALUES('{MAXID.FirstOrDefault()}','','{address}','','','','','')");
                            }
                            message = "This Activity is Created under the Driver Schedule";
                        }
                        else
                        {
                            message = "Driver is busy with other schedules";
                            goto gotoreturn;
                        }
                    }
                    else
                    {
                        var recordcount = connection.Query<int>($"select count(*) from (SELECT * FROM adminactivities WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')adminactivities where username='{lename}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                        if (recordcount > 0)
                        {
                            message = "LE is alloted in other Admin activity for the Specified time";
                            goto gotoreturn;
                        }
                        var lecheckcount = connection.Query<int>($"select count(*) as reccount from (SELECT * FROM DriverScheduel WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where [L.EName]='{lename}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                        if (lecheckcount > 0)
                        {
                            message = "LE is alloted in other activity for the Specified time";
                            goto gotoreturn;
                        }

                        if (tourrec == 0)
                        {
                            var MAXID = await connection.QueryAsync<int>($"insert into DriverScheduel(Date,Stype,FromTime,ToTime,ft,tt,DriverId,DriverName,[L.EName],ReferenceNo,ClentName,remarks,Mobile,doc,Otherreason,Comments,propertyname)values('{Convert.ToDateTime(date).ToString("yyyy-MM-dd")}','{scheduletype}','{fromtime}','{totime}','{ft}','{tt}','{DID}','{drivername}','{lename}','{referenceno}','{clientname}','{detailsRemarks}','{contactno}',getdate(),'{otherreason}','{otherinstructionComments}','{reasonPN}'); SELECT CAST(SCOPE_IDENTITY() as int )");
                            var insertPTV = await connection.ExecuteAsync($"INSERT INTO PTV(SID,PROPERTYREF,PROPERTNAME,RENT,POC,SOURCE,LOCATION,pdesc)VALUES('{MAXID.FirstOrDefault()}','','{address}','','','','','')");
                            message = "This Activity is Created under the Driver Schedule";
                        }
                        else
                        {
                            message = "Driver is busy with other schedules";
                            goto gotoreturn;
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                message = "Unknown Error";
                Console.WriteLine(ex);
                logger.Info(ex.Message);
            }

            gotoreturn:
            return message;
        }

        public string GroupLeNameAdding(string date, string ft, string tt, string lenamelist)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();



                var recordcount = connection.Query<int>($"select count(*) from (SELECT * FROM adminactivities WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')adminactivities where username='{lenamelist}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                if (recordcount > 0)
                {
                    message = "LE is alloted in other Admin activity for the Specified time";
                    goto gotoreturn;
                }
                var lecheckcount = connection.Query<int>($"select count(*) as reccount from (SELECT * FROM DriverScheduel WHERE '{ft}' between ft and tt  or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where [L.EName]='{lenamelist}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                if (lecheckcount > 0)
                {
                    message = "This LE is alloted in other activity for the this Specified time";
                    goto gotoreturn;
                }

                //var grplelist = grouplename.Split(',').ToList();
                //foreach(var le in grplelist)
                //{
                //    if(lenamelist == le)
                //    {
                //        message = "Selected LE is already added in the List";
                //        goto gotoreturn;
                //    }
                //}

                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public IActionResult ViewAdhoc()
        {
            List<DriverScheduel> model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
                {
                    model = connection.Query<DriverScheduel>($"Select date,fromtime,totime,[L.EName] LEname,clentname,PROPERTYNAME,REMARKS,COMMENTS,otherreason,id from DriverScheduel where stype='AD-HOC ACTIVITIES' order by id desc").ToList();
                }
                else if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
                {
                    model = connection.Query<DriverScheduel>($"Select date,fromtime,totime,[L.EName] LEname,clentname,PROPERTYNAME,REMARKS,COMMENTS,otherreason,id from DriverScheduel where stype='AD-HOC ACTIVITIES' order by id desc").ToList();
                }
                else
                {
                    model = connection.Query<DriverScheduel>($"Select date,fromtime,totime,[L.EName] LEname,PROPERTYNAME,clentname,REMARKS,COMMENTS,otherreason,id from DriverScheduel where [l.ename]='{HttpContext.Session.GetString("CurrentUsername")}' and stype='AD-HOC ACTIVITIES' order by id desc").ToList();
                }
                connection.Close();
            }
            return View(model);
        }

        public string DeleteAdhoc(int id)
        {
            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var delete = connection.Execute($"DELETE FROM DriverScheduel WHERE ID={id} and stype='AD-HOC ACTIVITIES'");
                    connection.Close();
                    if (delete == 1)
                        ViewBag.Message = "Activity is Deleted";
                    else
                        ViewBag.Message = "Activity is not Deleted";
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Unknown Error";
                Console.WriteLine(ex);
            }

            return ViewBag.Message;
        }

        public IActionResult UpdateDriverScheduling(int id)
        {
            DriverSchedulingModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();

                var driverScheduel = connection.Query<DriverScheduel>($"select Date,Stype,FromTime,ToTime,ft,tt,DriverId,DriverName,[L.EName] LEname,ReferenceNo,ClentName,remarks,Mobile,Otherreason,Comments,propertyname,grouple from DriverScheduel where id={id} ").FirstOrDefault();

                model.driverScheduel = new();
                model.driverScheduel = driverScheduel;
                model.replacele = driverScheduel.LEname;
                //model.driverScheduel.FromTime = driverScheduel.Ft.ToString();
                //model.driverScheduel.ToTime = driverScheduel.Tt.ToString();

                connection.Close();
            }
            return View(model);
        }

        public async Task<string> UpdateDriverSchedule(int Id, string replacele, string date, string ft, string FromTime, string tt, string ToTime, string scheduletype, string clientname, string contactno, string referenceno, string address, string drivername, bool multipleLe, string lenamelist, string grouplename, string lename, string reasonPN, string otherreason, string detailsRemarks, string otherinstructionComments, string returntime)
        {
            var message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    //DateTime dt1 = DateTime.ParseExact(ft, "HH:mm", null, DateTimeStyles.None);
                    //var FromTime = dt1.ToString("hh:mm tt");
                    //DateTime dt2 = DateTime.ParseExact(tt, "HH:mm", null, DateTimeStyles.None);
                    //var ToTime = dt2.ToString("hh:mm tt");

                    if (grouplename != null)
                    {
                        if (grouplename.Contains(replacele))
                        {
                            grouplename = grouplename.Replace(replacele, lename);
                        }
                    }
                    var recordcount = connection.Query<int>($"select count(*) from (SELECT * FROM adminactivities WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')adminactivities where username='{lename}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}')").FirstOrDefault();
                    if (recordcount > 0)
                    {
                        message = "LE is alloted in other Admin activity for the Specified time";
                        goto gotoreturn;
                    }

                    var lecheckcount = connection.Query<int>($"select count(*) from (SELECT * FROM DriverScheduel WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where [L.EName]='{lename}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}') and id not in('{Id}')").FirstOrDefault();
                    if (lecheckcount > 0)
                    {
                        message = "LE is alloted in other activity for the Specified time";
                        goto gotoreturn;
                    }

                    var tourrec = connection.Query<int>($"select count(*) from (SELECT * FROM DriverScheduel WHERE '{ft}' between ft and tt or '{tt}' between ft and tt or ft between '{ft}' and '{tt}')DriverScheduel where drivername='{drivername}' and date='{date}' and tt not in('{ft}') and ft not in('{tt}') and id not in('{Id}') and groupleyesno not in('true')").FirstOrDefault();
                    var DID = connection.Query<int>($"SELECT ID FROM DRIVERS WHERE DriverName='{drivername}'").FirstOrDefault();
                    if (tourrec == 0)
                    {
                        var update = await connection.ExecuteAsync($"update DriverScheduel set Date=Convert(Date,'{Convert.ToDateTime(date)}',103),Stype='{scheduletype}',FromTime='{FromTime}',ToTime='{ToTime}',ft='{ft}',tt='{tt}',DriverId='{DID}',DriverName='{drivername}',[L.EName]='{lename}',ReferenceNo='{referenceno}',ClentName='{clientname}',remarks='{detailsRemarks}',Mobile='{contactno}',dou=getdate(),Otherreason='{otherreason}',Comments='{otherinstructionComments}',propertyname='{reasonPN}',grouple='{grouplename}',returntime='{returntime}' where id={Id}");
                        var updatePTV = await connection.ExecuteAsync($"update ptv set PROPERTYREF='',PROPERTNAME='{address}',RENT='',POC='',SOURCE='',LOCATION='',pdesc='' where sid={Id}");
                        message = "This Schedule is Updated succesfully!";
                    }
                    else
                    {
                        message = "Driver is BC with other schedules";
                        goto gotoreturn;
                    }
                }
                catch (Exception ex)
                {
                    message = "Unknown Error";
                    Console.WriteLine(ex);
                }
                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public async Task<string> AddLESubmit(string date, string ft, string FromTime, string tt, string ToTime, string scheduletype, string clientname, string contactno, string referenceno, string address, string drivername, bool multipleLe, string lenamelist, string grouplename, string lename, string reasonPN, string otherreason, string detailsRemarks, string otherinstructionComments, string namecombo, string groupletxt)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                try
                {
                    //DateTime dt1 = DateTime.ParseExact(ft, "HH:mm", null, DateTimeStyles.None);
                    //var FromTime = dt1.ToString("hh:mm tt");
                    //DateTime dt2 = DateTime.ParseExact(tt, "HH:mm", null, DateTimeStyles.None);
                    //var ToTime = dt2.ToString("hh:mm tt");

                    var DID = connection.Query<int>($"SELECT ID FROM DRIVERS WHERE DriverName='{drivername}'").FirstOrDefault();

                    var MAXID = await connection.QueryAsync<int>($"insert into DriverScheduel(Date,Stype,FromTime,ToTime,ft,tt,DriverId,DriverName,[L.EName],ReferenceNo,ClentName,remarks,Mobile,doc,Otherreason,Comments,propertyname,grouple,groupleyesno)values(Convert(DATE,'{Convert.ToDateTime(date)}',103),'{scheduletype}','{FromTime}','{ToTime}','{ft}','{tt}','{DID}','{drivername}','{namecombo}','{referenceno}','{clientname}','{detailsRemarks}','{contactno}',getdate(),'{otherreason}','{otherinstructionComments}','{reasonPN}','{groupletxt}','TRUE'); SELECT CAST(SCOPE_IDENTITY() as int )");
                    var insertPTV = await connection.ExecuteAsync($"INSERT INTO PTV(SID,PROPERTYREF,PROPERTNAME,RENT,POC,SOURCE,LOCATION,pdesc)VALUES('{MAXID.FirstOrDefault()}','','{address}','','','','','')");
                    message = "Leasing executive is added in the Scheduled activity";
                }
                catch (Exception ex)
                {
                    message = "Unknown Error";
                    Console.WriteLine(ex);
                }
                connection.Close();
            }
            return message;
        }

        public IActionResult AdminFunction()
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
            {
                ViewBag.Message = "Access denied";
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
            {
                goto gotoreturn;
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "User")
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

        public async Task<IActionResult> AdminFunctions(AdminFunctionsModel model)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                if (model.adminactivity != null)
                {
                    try
                    {
                        if (model.adminactivity.Fromtime == null)
                        {
                            ViewBag.Message = "Enter the From time";
                            goto gotoreturn;
                        }
                        if (model.adminactivity.Totime == null)
                        {
                            ViewBag.Message = "Enter the To time";
                            goto gotoreturn;
                        }
                        if (model.adminactivity.Reason == null)
                        {
                            ViewBag.Message = "Reason is Required";
                            goto gotoreturn;
                        }
                        if (model.adminactivity.Reason == "Other")
                        {
                            if (model.adminactivity.OtherReason == null)
                            {
                                ViewBag.Message = "Enter the Other Reson";
                                goto gotoreturn;
                            }
                        }
                        if (model.adminactivity.Advertising == null)
                        {
                            ViewBag.Message = "Advertising is required";
                            goto gotoreturn;
                        }

                        string Ft = model.adminactivity.Fromtime;
                        string Tt = model.adminactivity.Totime;

                        DateTime dt1 = DateTime.ParseExact(Ft, "HH:mm", null, DateTimeStyles.None);
                        model.adminactivity.Fromtime = dt1.ToString("hh:mm tt");
                        DateTime dt2 = DateTime.ParseExact(Tt, "HH:mm", null, DateTimeStyles.None);
                        model.adminactivity.Totime = dt2.ToString("hh:mm tt");

                        var lecheckcount = connection.Query<int>($"select count(*) from (SELECT * FROM DriverScheduel WHERE '{Ft}' between ft and tt or '{Tt}' between ft and tt or ft between '{Ft}' and '{Tt}')DriverScheduel where [L.EName]='{model.adminactivity.Username}' and date=Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103) and tt not in('{Ft}') and ft not in('{Tt}')").FirstOrDefault();
                        if (lecheckcount > 0)
                        {
                            ViewBag.Message = "LE is alloted in other activity for the Specified time";
                            goto gotoreturn;
                        }

                        var recordcount = connection.Query<int>($"select count(*) as rec from (SELECT * FROM adminactivities WHERE '{Ft}' between ft and tt or '{Tt}' between ft and tt or ft between '{Ft}' and '{Tt}')adminactivities where username='{model.adminactivity.Username}' and date=Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103) and tt not in('{Ft}') and ft not in('{Tt}')").FirstOrDefault();
                        if (recordcount == 0)
                        {
                            var insert = await connection.ExecuteAsync($"insert into [adminactivities] (date,fromtime,totime,ft,tt,Reason,OtherReason,Advertising,username,doc)values(Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103),'{model.adminactivity.Fromtime}','{model.adminactivity.Totime}','{Ft}','{Tt}','{model.adminactivity.Reason}','{model.adminactivity.OtherReason}','{model.adminactivity.Advertising}','{model.adminactivity.Username}',getdate())");
                            ViewBag.Message = "Admin Activity is added";
                        }
                        else
                        {
                            ViewBag.Message = "Already there is an activity alloted for this LE on this day for this timings.";
                            goto gotoreturn;
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Unknown Error";
                        Console.WriteLine(ex);
                    }
                }
            }

            gotoreturn:
            return View();
        }

        public IActionResult ViewAdminFunction()
        {
            List<Adminactivity> model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
                {
                    model = connection.Query<Adminactivity>($"select * from [adminactivities] order by id desc").ToList();
                }
                else
                {
                    model = connection.Query<Adminactivity>($"select * from [adminactivities] where username='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();
                }
                connection.Close();
            }
            return View(model);
        }

        public string DeleteAdminFunction(int id)
        {
            string message = "";
            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var delete = connection.Execute($"Delete from adminactivities where id={id}");
                    connection.Close();
                    if (delete == 1)
                        message = "This activity is deleted";
                    else
                        message = "Activity is not Deleted";
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                message = "Unknown Error";
                Console.WriteLine(ex);
            }

            return message;
        }

        public async Task<IActionResult> EditAdminFunction(int id, AdminFunctionsModel model)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (model.adminactivity == null)
                {
                    var adminactivity = connection.Query<Adminactivity>($"select * from adminactivities where id={id}").FirstOrDefault();

                    model.adminactivity = adminactivity;
                    model.adminactivity.Fromtime = adminactivity.Ft.ToString();
                    model.adminactivity.Totime = adminactivity.Tt.ToString();
                }
                else
                {
                    if (model.adminactivity.Fromtime == null)
                    {
                        ViewBag.Message = "Enter the From time";
                        goto gotoreturn;
                    }
                    if (model.adminactivity.Totime == null)
                    {
                        ViewBag.Message = "Enter the To time";
                        goto gotoreturn;
                    }
                    if (model.adminactivity.Reason == null)
                    {
                        ViewBag.Message = "Reason is Required";
                        goto gotoreturn;
                    }
                    if (model.adminactivity.Reason == null)
                    {
                        ViewBag.Message = "Reason is Required";
                        goto gotoreturn;
                    }
                    if (model.adminactivity.Reason == "Other")
                    {
                        if (model.adminactivity.OtherReason == null)
                        {
                            ViewBag.Message = "Enter the Other Reson";
                            goto gotoreturn;
                        }
                    }
                    if (model.adminactivity.Advertising == null)
                    {
                        ViewBag.Message = "Advertising is required";
                        goto gotoreturn;
                    }

                    string Ft = model.adminactivity.Fromtime;
                    string Tt = model.adminactivity.Totime;

                    DateTime dt1 = DateTime.ParseExact(Ft, "HH:mm", null, DateTimeStyles.None);
                    model.adminactivity.Fromtime = dt1.ToString("hh:mm tt");
                    DateTime dt2 = DateTime.ParseExact(Tt, "HH:mm", null, DateTimeStyles.None);
                    model.adminactivity.Totime = dt2.ToString("hh:mm tt");

                    var lecheckcount = connection.Query<int>($"select count(*) as reccount from (SELECT * FROM DriverScheduel WHERE '{Ft}' between ft and tt or '{Tt}' between ft and tt or ft between '{Ft}' and '{Tt}')DriverScheduel where [L.EName]='{model.adminactivity.Username}' and date=Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103) and tt not in('{Ft}') and ft not in('{Tt}')").FirstOrDefault();
                    if (lecheckcount > 0)
                    {
                        ViewBag.Message = "LE is alloted in other activity for the Specified time";
                        goto gotoreturn;
                    }

                    var recordcount = connection.Query<int>($"select count(*) from adminactivities where date=Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103) and fromtime='{model.adminactivity.Fromtime}' and totime='{model.adminactivity.Totime}' and username='{model.adminactivity.Username}' and id not in('{model.adminactivity.Id}')").FirstOrDefault();
                    if (recordcount == 0)
                    {
                        var update = await connection.ExecuteAsync($"update [adminactivities] set date=Convert(DATE,'{Convert.ToString(model.adminactivity.Date)}',103),fromtime='{model.adminactivity.Fromtime}',totime='{model.adminactivity.Totime}',ft='{Ft}',tt='{Tt}',Reason='{model.adminactivity.Reason}',OtherReason='{model.adminactivity.OtherReason}',Advertising='{model.adminactivity.Advertising}',username='{model.adminactivity.Username}',dou=getdate() where id='{model.adminactivity.Id}'");
                        ViewBag.Message = "This Activity is updated with the new Information";
                    }
                    else
                    {
                        ViewBag.Message = "Already there is an activity scheduled on this day for this time.";
                    }
                }
                connection.Close();
            }

            gotoreturn:
            return View(model);
        }

        public IActionResult ViewInActive()
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
            {
                ViewBag.Message = "Access denied";
                goto gotoreturn;
            }
            if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
            {
                ViewBag.Message = "Access denied";
                goto gotoreturn;
            }

            gotoreturn:
            return View();
        }

        public IActionResult InActivePGL()
        {
            ViewInActiveModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.pgls = connection.Query<Pgl>($"SELECT PGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,Doc,Updatedon, case when updatedon is null then '0' when  Updatedon >= DATEADD(day,-7, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM PGL where InquiryStatus='Cancelled' order by id desc ").ToList();
                model.cgls = new();
                connection.Close();
            }
            return View(model);
        }

        public IActionResult InActiveCGL()
        {
            ViewInActiveModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.pgls = new();
                model.cgls = connection.Query<Cgl>($"SELECT CGLREFNO,Clientname,Mobile ,Propertytype ,MinBudget ,Maxbudget ,Completedby ,Doc,Updatedon, case when updatedon is null then '0' when  Updatedon >= DATEADD(day,-7, GETDATE()) then '1' else 3 end as result,case when len(updatedon) is null then 0 else 1 end as mycase FROM CGL where InquiryStatus='Cancelled' order by id desc ").ToList();
                connection.Close();
            }
            return View(model);
        }

        public async Task<string> OpenPGL(string refid)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var updatestatus = await connection.ExecuteAsync($"update PGL SET INQUIRYSTATUS='Open',wasinactive='yes',Reasonofcancellation='',cancellationstatus='' where pglrefno='{refid}' and INQUIRYSTATUS='Cancelled' ");
                message = "Enquiry is Open Now";
                connection.Close();
            }
            return message;
        }

        public async Task<string> OpenCGL(string refid)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var updatestatus = await connection.ExecuteAsync($"update CGL SET INQUIRYSTATUS='Open',wasinactive='yes',Reasonofcancellation='',cancellationstatus='' where cglrefno='{refid}' and INQUIRYSTATUS='Cancelled' ");
                message = "Enquiry is Open Now";
                connection.Close();
            }
            return message;
        }

        public IActionResult Other()
        {
            if (HttpContext.Session.GetString("CurrentUserRole") == "BDO")
            {
                ViewBag.Message = "Access denied";
                goto gotoreturn;
            }

            if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
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

        public async Task<string> SaveSource(string source)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from source where source='{source}'").FirstOrDefault();
                if (reccount > 0)
                {
                    message = "This source is already available";
                    goto gotoreturn;
                }
                else
                {
                    var insert = await connection.ExecuteAsync($"insert into source(source)values('{source}')");
                    message = "Source is Created";
                }
                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public async Task<string> SaveOtherSource(string othersource)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from source where source='{othersource}'").FirstOrDefault();
                if (reccount > 0)
                {
                    message = "This is already available in main source list";
                    goto gotoreturn;
                }
                var reccount1 = connection.Query<int>($"select count(*) from othersource where name='{othersource}'").FirstOrDefault();
                if (reccount1 > 0)
                {
                    message = "This is already available in Other source list";
                    goto gotoreturn;
                }

                var insert = await connection.ExecuteAsync($"insert into othersource(name,doc)values('{othersource}',getdate())");
                message = "OtherSource is Created";

                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public async Task<string> SaveNationality(string nationality)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from Nationality where nationalityname='{nationality}'").FirstOrDefault();
                if (reccount > 0)
                {
                    message = "This Nationality is already available";
                    goto gotoreturn;
                }

                var insert = await connection.ExecuteAsync($"insert into Nationality(nationalityname)values('{nationality}')");
                message = "Nationality is Created";

                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public async Task<string> SaveArea(string area)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var reccount = connection.Query<int>($"select count(*) from Area where Areaname='{area}'").FirstOrDefault();
                if (reccount > 0)
                {
                    message = "This Area is already available";
                    goto gotoreturn;
                }

                var insert = await connection.ExecuteAsync($"insert into Area(Areaname)values('{area}')");
                message = "Area is Created";

                connection.Close();
            }

            gotoreturn:
            return message;
        }

        public async Task<string> ChangePassword(string apppwdtxt)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var update = await connection.ExecuteAsync($"update passwordaccess set apppwd='{apppwdtxt}'");
                connection.Close();
                message = "Your Apppassword is changed";
            }
            return message;
        }

        public async Task<string> CreateUser(string ppUsername, string ppPassword, string ppRole)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var RECORDCOUNT = connection.Query<int>($"SELECT COUNT(*) FROM USERS WHERE USRNAME='{ppUsername}'").FirstOrDefault();
                if (RECORDCOUNT == 0)
                {
                    var insert = await connection.ExecuteAsync($"insert into users(usrname,password,role,Department)values('{ppUsername}','{ppPassword}','{ppRole}','Marketing')");
                    message = "User is created successfully";
                }
                else
                {
                    message = "This user is already created";
                }
                connection.Close();
            }
            return message;
        }

        public JsonResult LoadUserDetails(string pmUsername)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var model = connection.Query<User>($"SELECT PASSWORD,ROLE FROM USERS WHERE USRNAME='{pmUsername}'").FirstOrDefault();
                connection.Close();
                return Json(model);
            }
        }

        public async Task<string> UpdateUser(string pmUsername, string pmPassword, string pmRole)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var update = await connection.ExecuteAsync($"update users set password='{pmPassword}',role='{pmRole}' where USRNAME='{pmUsername}'");
                message = "User is modified successfully";
                connection.Close();
            }
            return message;
        }

        public async Task<string> DeleteUser(string pmUsername)
        {
            string message = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var update = await connection.ExecuteAsync($"delete from users where usrname='{pmUsername}' and department='Marketing'");
                message = "User is deleted";
                connection.Close();
            }
            return message;
        }

        public JsonResult LoadDriverDetails()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var drivers = connection.Query<Driver>($"select id,DriverName,Mob,Phone,Address from drivers order by id").ToList();
                connection.Close();
                return Json(drivers);
            }
        }

        public JsonResult LoadDriverDetail(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var driver = connection.Query<Driver>($"select id,DriverName,Mob,Phone,Address from drivers where id={id}").FirstOrDefault();
                connection.Close();
                return Json(driver);
            }
        }

        public async Task<string> SaveDriver(string ppDrivername, string ppAddress, string ppMobile, string ppPhone)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var rec = connection.Query<int>($"select count(*) from drivers where Drivername='{ppDrivername}'").FirstOrDefault();
                if (rec == 0)
                {
                    var insert = await connection.ExecuteAsync($"insert into drivers(DriverName,Mob,Phone,Address,doc)values('{ppDrivername}','{ppMobile}','{ppPhone}','{ppAddress}',getdate())");
                    message = "New Driver is Created";
                }
                else
                {
                    message = "This Driver is already Exist";
                }
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateDriver(int driverId, string ppDrivername, string ppAddress, string ppMobile, string ppPhone)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update drivers set drivername='{ppDrivername}',mob='{ppMobile}',phone='{ppPhone}',address='{ppAddress}' where id={driverId} ");
                message = "Driver Details are Updated";
                connection.Close();
                return message;
            }
        }

        public JsonResult LoadLeDetails()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var les = connection.Query<Leinfo>($"select id,Lename,lemob,LEMAIL FROM LEINFO").ToList();
                connection.Close();
                return Json(les);
            }
        }

        public JsonResult LoadLeDetail(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var le = connection.Query<Leinfo>($"select * FROM LEINFO where id={id}").FirstOrDefault();
                connection.Close();
                return Json(le);
            }
        }

        public async Task<string> SaveLe(string leName, string leTitle, string leAddress, string leMobile, string lePhone, string leEmail)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var insert = await connection.ExecuteAsync($"insert into LEinfo(LEName, Nametitle,LEMob,Phone,LEmail,Address,Deleted)VALUES('{leName}','{leTitle}','{leMobile}','{lePhone}','{leEmail}','{leAddress}','-')");
                message = "Leasing Executive is Created";
                connection.Close();
                return message;
            }
        }

        public async Task<string> UpdateLe(int leId, string leName, string leTitle, string leAddress, string leMobile, string lePhone, string leEmail)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update LEINFO set Nametitle='{leTitle}',LENAME='{leName}',LEMOB='{leMobile}',lemail='{leEmail}',phone='{lePhone}',address='{leAddress}' where id={leId} ");
                message = "LE Details are Updated";
                connection.Close();
                return message;
            }
        }

        public async Task<string> ChangeCgl(string cgl,string newcgl)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                if(newcgl.Trim().Substring(0,3) != "CGL")
                {
                    message = "format should be CGL current year - number for example CGL" + DateTime.Today.Year + "-...";
                    goto gotoreturn;
                }
                if(newcgl.Length > 12)
                {
                    message = "New No is Too big";
                    goto gotoreturn;
                }
                var recordcount = connection.Query<int>($"SELECT COUNT(*) FROM CGL WHERE CGLREFNO='{newcgl}'").FirstOrDefault();
                if(recordcount > 0)
                {
                    message = newcgl + "This NO is already available delete it first if the inquiry is OPEN";
                    goto gotoreturn;
                }    

                var update = await connection.ExecuteAsync($"Update cgl set cglrefno='{newcgl}' where cglrefno='{cgl}'");
                message = "CGL NO is changed successfully";
                connection.Close();

                gotoreturn:
                return message;
            }
        }

        public async Task<string> ChangePgl(string pgl, string newpgl)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                if (newpgl.Trim().Substring(0, 3) != "PGL")
                {
                    message = "format should be PGL current year - number for example PGL" + DateTime.Today.Year + "-...";
                    goto gotoreturn;
                }
                if (newpgl.Length > 12)
                {
                    message = "New No is Too big";
                    goto gotoreturn;
                }
                var recordcount = connection.Query<int>($"SELECT COUNT(*) FROM PGL WHERE PGLREFNO='{newpgl}'").FirstOrDefault();
                if (recordcount > 0)
                {
                    message = $"{newpgl} This NO is already available delete it first if the inquiry is OPEN";
                    goto gotoreturn;
                }

                var update = await connection.ExecuteAsync($"Update Pgl set Pglrefno='{newpgl}' where Pglrefno='{pgl}'");
                message = "PGL NO is changed successfully";
                connection.Close();

                gotoreturn:
                return message;
            }
        }

        public List<string> LoadSources(string sourcemode)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if(sourcemode == "others")
                {
                    var othersources = connection.Query<string>($"select name from otherSource").ToList();
                    //var othersources = connection.Query<Othersource>($"select ROW_NUMBER()over(order by id) as no,name from otherSource").ToList();
                    connection.Close();
                    return othersources;
                }
                else
                {
                    var sources = connection.Query<string>($"select source source1 from Source").ToList();
                    //var sources = connection.Query<Source>($"select ROW_NUMBER()over(order by source) as no,source source1 from Source").ToList();
                    connection.Close();
                    return sources;
                }
                //connection.Close();
            }
        }

        public string DeleteSource(string source, string sourcemode)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                if (sourcemode == "others")
                {
                    var deleteothersources = connection.Execute($"Delete from othersource where name='{source}'");
                    message = "Selected OtherSource is deleted successfully";
                }
                else
                {
                    var deletesources = connection.Execute($"Delete from source where source='{source}'");
                    message = "Selected Source is deleted successfully";
                }
                connection.Close();
                return message;
            }
        }

        public string DeleteNationality(string nation)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var deletesources = connection.Execute($"Delete from nationality where nationalityname='{nation}'");
                message = "Selected Nationality is deleted successfully";
                connection.Close();
                return message;
            }
        }

        public string DeleteArea(string area)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var deletesources = connection.Execute($"Delete from area where areaname='{area}'");
                message = "Selected Area is deleted successfully";
                connection.Close();
                return message;
            }
        }

        public IActionResult Reports()
        {
            if(HttpContext.Session.GetString("CurrentUserRole") == "BDO")
            {
                ViewBag.Message = "Access denied";

            }

            if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
            {
                ViewBag.Message = "Access denied";
            }

            return View();
        }

        public async Task<IActionResult> LOImonthlyrptpaid(string fromdate, string todate)
        {
            LOImonthlyrptpaidModel model = new();
            model.lOImonthlyrptpaids = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.lOImonthlyrptpaids = await connection.Query<LOImonthlyrptpaid>($@"select LEFT(INQNO,3),APTNO,inqno,ClientName,ClientCompany,ClientSource,PropertyName,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,monname,deposit,rent,clientresf,llresf,client,sp,payment,paystatus,payremarks from(
                                                                                 select APTNO,inqno,ClientName,ClientCompany,CASE WHEN clientsource='Other' THEN case when left(inqno,3)='CGL' THEN (SELECT OTHERSOURCE FROM CGL WHERE CGLREFNO=INQNO)  WHEN LEFT(INQNO,3)='PGL' then (SELECT OTHERSOURCE FROM PGL WHERE PGLREFNO=INQNO) END else clientsource end as clientsource,PropertyName,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,month(leasedate) as monname,payment,
                                                                                 case when propertysource='StandAloneProperty' then deposit else deposit-duedeposit end as deposit,case when propertysource='StandAloneProperty' then rent else  rent-duerent  end as  rent,
                                                                                 clientresf-dueresf as clientresf,llresf,paystatus,payremarks,case when ClientSource =lename then '1' else null end as client,case when propertysource='StandAloneProperty' then '1' else null end as sp
                                                                                 from loiinformation where lcsigned='YES' and leasedate between '{fromdate}' and '{todate}')src where enquirytype='Internal'
                                                                                 group by inqno,ClientName,ClientCompany,ClientSource,PropertyName,aptno,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,monname,deposit,rent,clientresf,llresf,client,sp,payment,paystatus,payremarks
                                                                                 order by Leasedate").ToListAsync();

                model.distinctLeNames = model.lOImonthlyrptpaids.Select(e => e.lename).Distinct().ToList();
                model.FromDate = fromdate;
                model.ToDate = todate;

                

                List<string> total = model.lOImonthlyrptpaids.Select(x => x.clientresf).ToList();
                List<decimal> intList = total.Select(decimal.Parse).ToList();
                model.TotalReceived = intList.Sum();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> LOImonthlyrpt(string fromdate, string todate)
        {
            LOImonthlyrptModel model=new LOImonthlyrptModel();
            model.lOImonthlyrpts = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.lOImonthlyrpts = await connection.Query<LOImonthlyrpt>($@"select Aptno,inqno,ClientName,ClientCompany,ClientSource,PropertyName,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,monname,deposit,rent,clientresf,llresf,client,sp,payment,paystatus,payremarks from(
                                                                         select Aptno,inqno,ClientName,ClientCompany,ClientSource,PropertyName,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,month(leasedate) as monname,payment,case when loistatus='Cancelled' then deposit-duedeposit else deposit end as deposit,case when loistatus='Cancelled' then rent-duerent else rent  end as rent,case when loistatus='Cancelled' then clientRESF -dueresf  else clientRESF end as clientresf,llresf,paystatus,payremarks,
                                                                         case when ClientSource =lename then '1' else null end as client,case when propertysource='StandAloneProperty' then '1' else null end as sp
                                                                         from loiinformation where lcsigned='YES' and leasedate between '{fromdate}' and '{todate}' )src  where enquirytype='Internal'
                                                                         group by inqno,ClientName,ClientCompany,ClientSource,PropertyName,Aptno,PropertySource,loistatus,EnquiryType,Leasedate,loisigndate,lename,monname,deposit,rent,clientresf,llresf,client,sp,payment,paystatus,payremarks
                                                                         order by Leasedate").ToListAsync();

                model.distinctLeNames = model.lOImonthlyrpts.Select(e => e.lename).Distinct().ToList();
                model.FromDate = fromdate;
                model.ToDate = todate;



                List<string> total = model.lOImonthlyrpts.Select(x => x.clientresf).ToList();
                List<decimal> intList = total.Select(decimal.Parse).ToList();
                model.TotalExpected = intList.Sum();

                connection.Close();
            }
            return View(model);
        }

        public GetPglCglCountMonthlyReportModel GetPglCglCountMonthlyReport(string fromdate, string todate)
        {
            GetPglCglCountMonthlyReportModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.pglcount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM PGL WHERE ENQUIRYDATE BETWEEN '{fromdate}' AND '{todate}'").FirstOrDefault();
                model.cglcount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM CGL WHERE ENQUIRYDATE BETWEEN '{fromdate}' AND '{todate}'").FirstOrDefault();
                connection.Close();
            }
            return model;
        }

        public IActionResult MonthlyAnalysismain(string fromdate, string todate)
        { 
            return View(); 
        }

        public async Task<IActionResult> AllQueriesOnly(string fromdate, string todate)
        { 
            AllQueriesOnlyModel model = new AllQueriesOnlyModel();
            model.FromDate=fromdate;
            model.ToDate=todate;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.allqueries = new();
                model.allqueries = await connection.Query<AllQueriesOnly>($@"select CGLREFNO,mobile,contactperson,CompletedBY,Source,bed,propertytype,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select cglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select cglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select cglrefno) and date=(select max(date) from Actiondetails where refno =(select cglrefno))) as actionsdone
                                                                     from cgl 
                                                                    where EnquiryDate between '{fromdate}' and '{todate}'
                                                                    union
                                                                    select pglrefno,mobile,contactperson,CompletedBY,Source,bed,propertytype,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select pglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select pglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select pglrefno) and date=(select max(date) from Actiondetails where refno =(select pglrefno))) as actionsdone
                                                                     from pgl
                                                                    where EnquiryDate between '{fromdate}' and '{todate}'
                                                                    order by CompletedBY").ToListAsync();

                model.NoOfEnquiries = model.allqueries.Count();
                connection.Close();
            }
            return View(model); 
        }

        public async Task<IActionResult> OpenQueriesnole(string fromdate, string todate)
        {
            OpenQueriesnoleModel model = new();
            model.FromDate = fromdate;
            model.ToDate = todate;
            
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.openqueriesnoles = new();
                model.openqueriesnoles = await connection.Query<OpenQueriesnole>($@"select CGLREFNO,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select cglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select cglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select cglrefno) and date=(select max(date) from Actiondetails where refno =(select cglrefno))) as actionsdone
                                                                     from cgl 
                                                                    where inquirystatus='Open' AND EnquiryDate between '{fromdate}' and '{todate}'
                                                                    union
                                                                    select pglrefno,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select pglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select pglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select pglrefno) and date=(select max(date) from Actiondetails where refno =(select pglrefno))) as actionsdone
                                                                     from pgl
                                                                    where inquirystatus='Open' AND EnquiryDate between '{fromdate}' and '{todate}'
                                                                    order by enquirydate").ToListAsync();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> OpenQueries(string fromdate, string todate, string lename)
        {
            OpenQueriesModel model = new();
            model.FromDate = fromdate;
            model.ToDate = todate;
            model.LEname = lename;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.openqueries = new();
                model.openqueries = await connection.Query<OpenQueries>($@"select CGLREFNO,mobile,CompletedBY,Source,bed,propertytype,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select cglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select cglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select cglrefno) and date=(select max(date) from Actiondetails where refno =(select cglrefno))) as actionsdone
                                                                     from cgl 
                                                                    where inquirystatus='Open' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'
                                                                    union
                                                                    select pglrefno,mobile,CompletedBY,Source,bed,propertytype,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,(select max(date) from DriverScheduel where referenceno in (select pglrefno)) as ptdate,
                                                                    (select max(date) from Actiondetails where refno =(select pglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select pglrefno) and date=(select max(date) from Actiondetails where refno =(select pglrefno))) as actionsdone
                                                                     from pgl
                                                                    where inquirystatus='Open' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'
                                                                    order by enquirydate").ToListAsync();
                
                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> CancellQueriesnole(string fromdate, string todate)
        {
            CancellQueriesnoleModel model = new();
            model.FromDate = fromdate;
            model.ToDate = todate;

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.cancellqueriesnoles = new();
                model.cancellqueriesnoles = await connection.Query<CancellQueriesnole>($@"select CGLREFNO,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,Mobile  from cgl 
                                                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}' 
                                                                                    union
                                                                                    select pglrefno,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,Mobile from pgl
                                                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}'").ToListAsync();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> CancellQueries(string fromdate, string todate, string lename)
        {
            CancellQueriesModel model = new();
            model.FromDate = fromdate;
            model.ToDate = todate;
            model.LEname = lename;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.cancellqueries = new();
                model.cancellqueries = await connection.Query<CancellQueries>($@"select CGLREFNO,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,mobile  from cgl 
                                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'
                                                                    union
                                                                    select pglrefno,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,mobile from pgl
                                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'").ToListAsync();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> Dlogs(string fromdate, string drivername)
        {
            DlogsModel model = new();
            model.FromDate = fromdate;
            model.Drivername = drivername;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                //model.driverformats = new();
                //model.driverformats = await connection.Query<CancellQueries>($@"select CGLREFNO,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,mobile  from cgl 
                //                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'
                //                                                    union
                //                                                    select pglrefno,CompletedBY,Source,ClientName,Nationality,minbudget,Maxbudget,movingdate,inquirystatus,EnquiryDate,dateofcancel,reasonofcancellation,mobile from pgl
                //                                                    where inquirystatus='Cancelled' AND EnquiryDate between '{fromdate}' and '{todate}' and completedby='{lename}'").ToListAsync();

                var chkgrid = connection.Query<DriverScheduel>($@"select FromTime,ToTime,DriverName,[L.EName] LEname,ReferenceNo,ClentName,PropertyName,Address,Mobile,id,grouple from DriverScheduel where Date='{fromdate}' and drivername='{drivername.ToLower()}' order by tt").ToList();
                var idgrid = connection.Query<IdgridModel>($"Select distinct ST2.sid, substring( ( Select ','+ST1.propertname  AS [text()] From dbo.ptv  ST1  Where ST1.SID = ST2.sid ORDER BY ST1.sid For XML PATH ('') ), 2, 1000) [address] From dbo.ptv ST2 where sid in(select id from DriverScheduel where Date='{fromdate}' and drivername='{drivername}') ").ToList();

                var update = connection.Execute(@"update driverformat set timings='',lename='',refno='',cname='',pname='',sdid=''");

                for(int i = 0; i < chkgrid.Count; i++)
                {
                    if (chkgrid[i].Address == null)
                    {
                        var update1 = connection.Execute($@"update driverformat set timings='{chkgrid[i].FromTime + "-" + chkgrid[i].ToTime}',dname='{chkgrid[i].DriverName}',lename='{chkgrid[i].LEname}',refno='{chkgrid[i].ReferenceNo}',cname='{chkgrid[i].ClentName}',pname='{chkgrid[i].PropertyName}',sdid='{chkgrid[i].Id}' where ftime='{chkgrid[i].FromTime}'");
                    }
                    else
                    {
                        var update1 = connection.Execute($@"update driverformat set timings='{chkgrid[i].FromTime + "-" + chkgrid[i].ToTime}',dname='{chkgrid[i].DriverName}',lename='{chkgrid[i].Address}',refno='{chkgrid[i].ReferenceNo}',cname='{chkgrid[i].ClentName}',pname='{chkgrid[i].PropertyName}',sdid='{chkgrid[i].Id}' where ftime='{chkgrid[i].FromTime}'");
                    }

                    var fid = connection.Query<int>($@"select id from driverformat where ftime='{chkgrid[i].FromTime}'").FirstOrDefault();
                    var tid = connection.Query<int>($@"select id from driverformat where ftime='{chkgrid[i].ToTime}'").FirstOrDefault();

                    var update2 = connection.Execute($@"update driverformat set timings='-----',dname='-----',lename='-----',refno='-----',cname='-----',pname='-----' where id>'{fid}' and id<='{tid}'");
                }

                for (int j = 0; j < idgrid.Count; j++)
                {
                    var update3 = connection.Execute($@"update driverformat set pname='{idgrid[j].address}' where sdid='{idgrid[j].sid}'");
                }

                model.driverformats = await connection.Query<Driverformat>($@"select * from driverformat").ToListAsync();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> SourceProcessOther(string othersource)
        {
            SourceProcessModel model = new();
            model.SourceName = othersource;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.sourceprocesses = new();
                model.sourceprocesses = await connection.Query<SourceProcess>($@"select source,cglrefno,'-',completedby,ClientName ,mobile,contactperson,propertytype,Bed,InquiryStatus,
                (select max(date) from DriverScheduel where referenceno in (select cglrefno)) as ptdate,(select top 1 stype from DriverScheduel where referenceno in (select cglrefno)) as stype,(select top 1 remarks from DriverScheduel where referenceno in (select cglrefno)) as remarks,
                (select max(date) from Actiondetails where refno =(select cglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select cglrefno) and date=(select max(date) from Actiondetails where refno =(select cglrefno))) as actionsdone
                   from cgl where othersource='{othersource}' 
                union
                select  source,pglrefno,'-',completedby,ClientName ,mobile,contactperson,propertytype,Bed,InquiryStatus 
                ,(select max(date) from DriverScheduel where referenceno in (select pglrefno)) as ptdate,(select top 1 stype from DriverScheduel where referenceno in (select pglrefno)) as stype,(select top 1 remarks from DriverScheduel where referenceno in (select pglrefno)) as remarks,
                (select max(date) from Actiondetails where refno =(select pglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select pglrefno) and date=(select max(date) from Actiondetails where refno =(select pglrefno))) as actionsdone from pgl where othersource='{othersource}'  ").ToListAsync();

                connection.Close();
            }
            return View(model);
        }

        public async Task<IActionResult> SourceProcess(string source)
        {
            SourceProcessModel model = new();
            model.SourceName = source;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //connection.Open();
                await connection.OpenAsync();
                model.sourceprocesses = new();
                model.sourceprocesses = await connection.Query<SourceProcess>($@"select source,cglrefno,'-',completedby,ClientName ,mobile,contactperson,propertytype,Bed,InquiryStatus,
                (select max(date) from DriverScheduel where referenceno in (select cglrefno)) as ptdate,(select top 1 stype from DriverScheduel where referenceno in (select cglrefno)) as stype,(select top 1 remarks from DriverScheduel where referenceno in (select cglrefno)) as remarks,
                (select max(date) from Actiondetails where refno =(select cglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select cglrefno) and date=(select max(date) from Actiondetails where refno =(select cglrefno))) as actionsdone
                   from cgl where source='{source}' and inquirystatus not in('Approved')
                union
                select  source,pglrefno,'-',completedby,ClientName ,mobile,contactperson,propertytype,Bed,InquiryStatus 
                ,(select max(date) from DriverScheduel where referenceno in (select pglrefno)) as ptdate,(select top 1 stype from DriverScheduel where referenceno in (select pglrefno)) as stype,(select top 1 remarks from DriverScheduel where referenceno in (select pglrefno)) as remarks,
                (select max(date) from Actiondetails where refno =(select pglrefno)) as actiondate,(select top 1 actions from Actiondetails where refno =(select pglrefno) and date=(select max(date) from Actiondetails where refno =(select pglrefno))) as actionsdone from pgl where source='{source}'  and inquirystatus not in('Approved')
                union
                select ClientSource ,inqno,LoiNo,ClientName ,cmob,ClientCompany ,req,PropertyName ,aptno,loistatus,'','',loinote,'','' from LOIInformation where ClientSource ='{source}'
                order by cglrefno").ToListAsync();

                //model.sourceprocesses.AddRange(sourceprocesses);

                connection.Close();
            }
            return View(model);
        }

        public IActionResult ClosedLeads()
        {
            ClosedLeadsModel model = new();

            if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Scheduler")
            {
                ViewBag.Message = "Access denied";
                goto gotoreturn;
            }

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.fclosed = connection.Query<int>($"select count(*) from (select * from LOIInformation where duerent<rent or duedeposit<deposit or dueresf <clientresf)src where loisigndate between CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(getdate())-1),getdate()),101) and CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(DATEADD(mm,1,getdate()))),DATEADD(mm,1,getdate())),101) and loistatus in('Approved') ").FirstOrDefault();
                model.parclosed = connection.Query<int>($"select count(*) from (select * from LOIInformation where duerent<rent or duedeposit<deposit or dueresf <clientresf)src where loisigndate between CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(getdate())-1),getdate()),101) and CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(DATEADD(mm,1,getdate()))),DATEADD(mm,1,getdate())),101) and loistatus in('Open') ").FirstOrDefault();
                model.totclosed = connection.Query<int>($"select count(*) from (select * from LOIInformation where duerent<rent or duedeposit<deposit or dueresf <clientresf)src where loisigndate between CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(getdate())-1),getdate()),101) and CONVERT(VARCHAR(25),DATEADD(dd,-(DAY(DATEADD(mm,1,getdate()))),DATEADD(mm,1,getdate())),101) and loistatus in('Approved','Open') ").FirstOrDefault();
                connection.Close();
            }
            gotoreturn:
            return View(model);
        }

        public IActionResult Questions(string inqno)
        {
            //using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    QuestionsModel model = new();
            //    connection.Open();
            //    var recordcount = connection.Query<int>($"select count(*) as rec from clientquestions where refno='{inqno}'").FirstOrDefault();
            //    if(recordcount > 0)
            //    {
            //        model.recordcount = recordcount;
            //        model.clientquestion = connection.Query<Clientquestion>($"select * from clientquestions where refno='{inqno}'").FirstOrDefault();
            //    }
            //    connection.Close();
            //    return View(model);
            //}
            QuestionsModel model = new();
            model.inqno = inqno;
            return View(model);
        }

        public JsonResult LoadQuestions(string inqno)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                Clientquestion model = new();
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from clientquestions where refno='{inqno}'").FirstOrDefault();
                if (recordcount > 0)
                {
                    model = connection.Query<Clientquestion>($"select * from clientquestions where refno='{inqno}'").FirstOrDefault();
                    connection.Close();
                    return Json(model);
                }
                else
                {
                    connection.Close();
                    return Json(recordcount);
                }
                
            }
        }

        public async Task<string> SaveQuestions(string inqno,string fname,string lname,string number,string currentloc,string whenapt,string company,string undercontract,string LONGTXT,string bed,string type,string married,string wife,string REQTXT,string inkwt,string idea,string ploc,string kids,string TIMETXT,string KIDSTXT,string SCHOOLTXT,string maid,string newb,string pet,string KINDTXT,string otheragent,string chkseenapts,string FEEDBACKTXT,string facilities,string distance,string dimen,string notice,string final,string find,string findwhere)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from clientquestions where refno='{inqno}'").FirstOrDefault();
                if (recordcount > 0)
                {
                    message = "Questionnaire is already exist for this inquiry";
                }
                else
                {
                    var insert = await connection.ExecuteAsync($"insert into clientquestions(refno,fname,lname,number,currentloc,whenapt,company,contract,howlong,bed,type,maritial,wife,wifereq,inkwt,idea,ploc,kids,kvisit,school,closeschool,maid,newbuild,pet,kind,otheragent,seen,feedbacks,facilities,distance,dimen,notice,final,doc,find,findwhere)values('{inqno}','{fname}','{lname}','{number}','{currentloc}','{whenapt}','{company}','{undercontract}','{LONGTXT}','{bed}','{type}','{married}','{wife}','{REQTXT}','{inkwt}','{idea}','{ploc}','{kids}','{TIMETXT}','{KIDSTXT}','{SCHOOLTXT}','{maid}','{newb}','{pet}','{KINDTXT}','{otheragent}','{chkseenapts}','{FEEDBACKTXT}','{facilities}','{distance}','{dimen}','{notice}','{final}',getdate(),'{find}','{findwhere}')");
                    if (insert == 1)
                    {
                        message = "Saved Successfully";
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

        public async Task<string> UpdateQuestions(string inqno, string fname, string lname, string number, string currentloc, string whenapt, string company, string undercontract, string LONGTXT, string bed, string type, string married, string wife, string REQTXT, string inkwt, string idea, string ploc, string kids, string TIMETXT, string KIDSTXT, string SCHOOLTXT, string maid, string newb, string pet, string KINDTXT, string otheragent, string chkseenapts, string FEEDBACKTXT, string facilities, string distance, string dimen, string notice, string final, string find, string findwhere)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var update = await connection.ExecuteAsync($"update clientquestions set fname='{fname}',lname='{lname}',number='{number}',currentloc='{currentloc}',whenapt='{whenapt}',company='{company}',contract='{undercontract}',howlong='{LONGTXT}',bed='{bed}',type='{type}',maritial='{married}',wife='{wife}',wifereq='{REQTXT}',inkwt='{inkwt}',idea='{idea}',ploc='{ploc}',kids='{kids}',kvisit='{TIMETXT}',school='{KIDSTXT}',closeschool='{SCHOOLTXT}',maid='{maid}',newbuild='{newb}',pet='{pet}',kind='{KINDTXT}',otheragent='{otheragent}',seen='{chkseenapts}',feedbacks='{FEEDBACKTXT}',facilities='{facilities}',distance='{distance}',dimen='{dimen}',notice='{notice}',final='{final}',find='{find}',findwhere='{findwhere}' where refno='{inqno}'");
                if (update == 1)
                {
                    message = "Modified Successfully";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

    }
}
