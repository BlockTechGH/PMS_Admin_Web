using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.PA;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using PMS_Admin_Web.Models.LOI;
using PMS_Admin_Web.Models.MovingInOut;
using Aspose.Email;
using Aspose.Email.Clients.Smtp;
using Aspose.Email.Clients;
using PMS_Admin_Web.Models.LC;
using System.Reflection;
using PMS_Admin_Web.Models.Property;
using System.IO;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Data;

namespace PMS_Admin_Web.Controllers
{
    public class PAController : Controller
    {
        private Connection sqlConnectionString = new();
        private RealtorContext context = new RealtorContext();

        public PAController(RealtorContext _context)
        {
            context = _context;
        }

        public IActionResult PaLOI()
        {
            return View();
        }

        public async Task<IActionResult> MaintenanceIssue(int page = 1, int pageSize = 10)
        {
            MaintenanceIssueModel maintenanceIssueModel = new();
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var missueList = await connection.QueryAsync<Missue>(@$"SELECT category, ID,PNAME, CONVERT(VARCHAR(11),doc,106) as doc, ISSUE,STATUS,REMARKS,
                                                                    case when REQUEST='SEEN' then 'SEEN BY MAINTENANCE' else request end as request,APTLOCATION,
                                                                    case when (select count(*) from fschedule where issueid=missue.id) >0 and status=(select status from fschedule where issueid=missue.id) then 1 when (select count(*) from fschedule where issueid=missue.id) >0 and status<>(select status from fschedule where issueid=missue.id) then 0 else 2 end as compare 
                                                                    FROM missue 
                                                                    where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' 
                                                                    and status not in ('DONE') 
                                                                    order by aptlocation,id desc");
                maintenanceIssueModel.missueList = missueList.ToList();
                //PagedList<PMS_Admin_Web.Models.Missue> model = new PagedList<PMS_Admin_Web.Models.Missue>(missue.ToList(), page, pageSize);
                var recordcount = connection.Query<int>($"select count(*) from missue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and status not in('Done')").FirstOrDefault();
                if(recordcount > 0)
                {
                    maintenanceIssueModel.issuemaxid = connection.Query<int>($"select max(id) from missue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and status not in('Done')").FirstOrDefault();
                }
                connection.Close();
                return View(maintenanceIssueModel);
            }
            
        }

        public async Task<IActionResult> AddComplaint(AddComplaintModel addComplaintModel)
        {
            if(addComplaintModel.missue != null)
            {
                using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var reccount = connection.Query<int>($"SELECT COUNT(*) FROM maintenceblock WHERE PREF=(select propertyref from propertymaster where bldgname='{addComplaintModel.missue.Pname}' and aptno='{addComplaintModel.missue.Aptlocation}')").FirstOrDefault();
                    if(reccount > 0)
                    {
                        ViewBag.Message = "Maintenance is blocked for this Apartment;Contact accounts department";
                        goto gotoview;
                    }

                    if(!addComplaintModel.IssueLocation)
                    {
                        if(addComplaintModel.missue.Aptlocation == null)
                        {
                            ViewBag.Message = "Select the apartment";
                            goto gotoview;
                        }
                    }
                    else
                    {
                        if (addComplaintModel.apttxt == null)
                        {
                            ViewBag.Message = "Mention the location";
                            goto gotoview;
                        }
                    }

                    if(addComplaintModel.missue.Issue == null)
                    {
                        ViewBag.Message = "Type the Issue";
                        goto gotoview;
                    }

                    if(addComplaintModel.missue.Status == null)
                    {
                        ViewBag.Message = "Status is required";
                        goto gotoview;
                    }

                    if(addComplaintModel.missue.Pl == null)
                    {
                        ViewBag.Message = "Enter the Priority level";
                        goto gotoview;
                    }

                    if(addComplaintModel.missue.Category == null)
                    {
                        ViewBag.Message = "Select the Category";
                        goto gotoview;
                    }

                    int noofrec = 0;
                    var recordcount = connection.Query<int>($"select count(*) from missue where year(doc)=year(getdate())").FirstOrDefault();
                    if(recordcount > 0)
                    {
                        noofrec = recordcount + 1;
                    }
                    else
                    {
                        noofrec = 1;
                    }

                    var issueno = "M" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + noofrec;

                    if(!addComplaintModel.IssueLocation)
                    {
                        //here in the app they did it as multiple aptno, but i did not following it
                        var insertMissue = await connection.ExecuteAsync($"insert into missue(issueno,pref,aptlocation,pname,category,issue,status,remarks,doc,PANAME,PL,FCASEEN) values('{issueno}','{addComplaintModel.missue.Pref}','{addComplaintModel.missue.Aptlocation}','{addComplaintModel.missue.Pname}','{addComplaintModel.missue.Category}','{addComplaintModel.missue.Issue}','{addComplaintModel.missue.Status}','{addComplaintModel.missue.Remarks}',getdate(),'{HttpContext.Session.GetString("CurrentUsername")}','{addComplaintModel.missue.Pl}','no')");
                    }
                    else
                    {
                        var insertMissue = await connection.ExecuteAsync($"insert into missue(issueno,pref,aptlocation,pname,category,issue,status,remarks,doc,PANAME,PL,FCASEEN) values('{issueno}','-','{addComplaintModel.apttxt}','{addComplaintModel.missue.Pname}','{addComplaintModel.missue.Category}','{addComplaintModel.missue.Issue}','{addComplaintModel.missue.Status}','{addComplaintModel.missue.Remarks}',getdate(),'{HttpContext.Session.GetString("CurrentUsername")}','{addComplaintModel.missue.Pl}','no')");
                    }

                    ViewBag.Message = $"Issue is registered Successfully and the Complaint NO is {issueno}";
                    connection.Close();
                }
            }
            gotoview:
            return View();
        }

        public async Task<IActionResult> UpdateComplaint(int id, UpdateComplaintModel updateComplaintModel)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();

            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    if (updateComplaintModel.missue != null)
                    {
                        if (updateComplaintModel.StatusfromMaintenance != null && updateComplaintModel.missue.Contractortype != "Q8realtor")
                        {
                            ViewBag.Message = "Schedule is sent;but the contractor type is not Q8REALTOR;if you want to change the contractor type maintenance should cancel your request";
                            goto gotoreturn;
                        }

                        if (updateComplaintModel.missue.Contractortype == null)
                        {
                            ViewBag.Message = "Select the Contractor Type";
                            goto gotoreturn;
                        }

                        if (updateComplaintModel.missue.Issue == null)
                        {
                            ViewBag.Message = "Type the Issue";
                            goto gotoreturn;
                        }
                        if (updateComplaintModel.missue.Status == null)
                        {
                            ViewBag.Message = "Status is required";
                        }

                        if (updateComplaintModel.missue.Contractortype == "Q8realtor")
                        {
                            var reqmode = connection.Query<int>($"select case when request is null then '0' else '1' end as request from missue where id='{updateComplaintModel.missue.Id}'").FirstOrDefault();
                            if (reqmode == 0)
                            {
                                ViewBag.Message = "Reminder:Still the Maintenance request is not made for this issue so It cannot be processed unless there is a maintenance request";
                            }
                        }

                        if (updateComplaintModel.missue.Contractortype == "Outside")
                        {
                            if (updateComplaintModel.missue.Contractorname == null)
                            {
                                ViewBag.Message = "Enter the contractor Name";
                                goto gotoreturn;
                            }
                        }
                        if (updateComplaintModel.missue.Contractortype == "Q8realtor")
                        {
                            if (updateComplaintModel.missue.Contractorname != null)
                            {
                                updateComplaintModel.missue.Contractorname = "";
                            }
                        }
                        if (updateComplaintModel.missue.Contractortype == "Handyman")
                        {
                            if (updateComplaintModel.missue.Contractorname == null)
                            {
                                ViewBag.Message = "Enter the Handyman Name";
                                goto gotoreturn;
                            }
                        }

                        if (updateComplaintModel.missue.Status == "Done")
                        {
                            if (updateComplaintModel.missue.Wcdate == null)
                            {
                                ViewBag.Message = "Date of completion is required";
                                goto gotoreturn;
                            }
                            if (updateComplaintModel.missue.Workdescription == null)
                            {
                                ViewBag.Message = "Enter the Description";
                                goto gotoreturn;
                            }
                            //not checking if items purchased?' Yes and No is null
                            if (updateComplaintModel.missue.Itempurchase == "Yes")
                            {
                                if (updateComplaintModel.itempurchasedList.Count() == 0)
                                {
                                    ViewBag.Message = "Enter the Item Description";
                                    goto gotoreturn;
                                }
                            }

                            var updateMissue1 = await connection.ExecuteAsync($"update missue set category='{updateComplaintModel.missue.Category}',contractortype='{updateComplaintModel.missue.Contractortype}',contractorname='{updateComplaintModel.missue.Contractorname}', itempurchase='{updateComplaintModel.missue.Itempurchase}',wcdate='{updateComplaintModel.missue.Wcdate}',workdescription='{updateComplaintModel.missue.Workdescription}',wdstatus='ACTIVE',dou=getdate(),additionalwork='{updateComplaintModel.missue.Additionalwork}' where id={updateComplaintModel.missue.Id}");
                            //item purchased saving is done in another method
                        }

                        if (updateComplaintModel.StatusfromMaintenance != null)
                        {
                            if (updateComplaintModel.missue.Status == "Cancelled")
                            {
                                ViewBag.Message = "Job cannot be cancelled unless it is cancelled from Maintenance.";
                                goto gotoreturn;
                            }
                        }

                        var updateMissue2 = await connection.ExecuteAsync($"update missue set category='{updateComplaintModel.missue.Category}',issue='{updateComplaintModel.missue.Issue}',contractortype='{updateComplaintModel.missue.Contractortype}',contractorname='{updateComplaintModel.missue.Contractorname}',status='{updateComplaintModel.missue.Status}',remarks='{updateComplaintModel.missue.Remarks}',dou=getdate(),additionalwork='{updateComplaintModel.missue.Additionalwork}' where id={updateComplaintModel.missue.Id}");

                        if (updateComplaintModel.StatusfromMaintenance == "SENT")
                        {
                            var updateMREQUEST = connection.ExecuteAsync($"update MREQUEST set issue='{updateComplaintModel.missue.Issue}',availabletime='{updateComplaintModel.missue.Tavailability}',remarks='{updateComplaintModel.missue.Remarks}' where id={updateComplaintModel.missue.Id}");
                        }

                        ViewBag.Message = "Updated Successfully!";

                        if (updateComplaintModel.missue.Status == "Done-Under Observation(48-hrs)")
                        {
                            var updateMissue3 = await connection.ExecuteAsync($"update missue set underobservationstart=getdate(),underobservationstop=getdate()+2 where id='{id}'");
                            ViewBag.Message = "Job is completed but under observation for 2 days.Job Status can be changed after two days only!";
                            goto gotoreturn;
                        }
                    }
                    else
                    {
                        var missue = connection.Query<Missue>($"select id,issueno,underobservationstart,underobservationstop,requestdate, category,doc,contractortype,contractorname,APTLOCATION,status,issue,remarks,paname,pl,request,pref,tname,contactno,lcno,tavailability from missue st2 where id={id}").FirstOrDefault();
                        updateComplaintModel.missue = missue;
                        if (missue.Request == "SEEN")
                            updateComplaintModel.StatusfromMaintenance = "SEEN BY MAINTENANCE";
                        else
                            updateComplaintModel.StatusfromMaintenance = missue.Request;

                        if (missue.Contractortype == "Outside")
                        {
                            //updateComplaintModel.OutsourceContractor = "Outsource Contractor";
                            updateComplaintModel.missue.Contractorname = missue.Contractorname;
                        }


                        if (missue.Contractortype == "Q8realtor")
                        {
                            //updateComplaintModel.ITAdminOther = "ITAdminOther";
                            updateComplaintModel.missue.Contractorname = "";
                        }


                        if (missue.Contractortype == "Handyman")
                        {
                            //updateComplaintModel.Handyman = "Handyman";
                            updateComplaintModel.missue.Contractorname = missue.Contractorname;
                        }


                        //if(missue.Contractortype == "Landlord")
                        //{
                        //    //updateComplaintModel.Landlord = "Landlord";
                        //}


                        if (missue.Status == "Done-Under Observation(48-hrs)")
                        {
                            updateComplaintModel.missue.Underobservationstart = missue.Underobservationstart;
                        }

                        var fschedule = connection.Query<Fschedule>($"select * from fschedule where issueid={id}").FirstOrDefault();
                        if (fschedule != null)
                        {
                            updateComplaintModel.Attender = fschedule.Attender;
                            updateComplaintModel.RemarksfromMaintenance = fschedule.Remarks;
                        }


                        var recordcount = connection.Query<int>($"select count(*) from mrequest where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and id='{id}'").FirstOrDefault();
                        if (recordcount > 0)
                        {
                            var mrequest = connection.Query<Mrequest>($"select remarks from mrequest where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and id='{id}'").FirstOrDefault();
                            updateComplaintModel.RemarksfromMaintenance = mrequest.Remarks;
                            //groupbox2 enabled
                        }
                        else
                        {
                            //groupbox2 disabled
                            updateComplaintModel.mrequestCount = recordcount;
                        }

                        updateComplaintModel.reccount = connection.Query<int>($"select count(*) from fschedule where issueid='{id}'").FirstOrDefault();

                        var followuprecords = connection.Query<int>($"select count(*) from missuefollowup where followup_issueid='{id}'").FirstOrDefault();
                        if (followuprecords > 0)
                        {
                            updateComplaintModel.missueFollowups = connection.Query<MissueFollowup>($"select CONVERT(VARCHAR(11),followup_doc,106) as FollowupDoc,followup_id FollowupId,followup_remarks FollowupRemarks from missuefollowup where followup_issueid='{id}' order by followup_id desc").ToList();

                        }

                        string to = "", cc = "";
                        updateComplaintModel.EmailListTo = connection.Query<string>($"select email from paemail where status='to' order by id").ToList();
                        foreach (var toEmail in updateComplaintModel.EmailListTo)
                        {
                            if (updateComplaintModel.EmailTo == null)
                            {
                                updateComplaintModel.EmailTo = toEmail.Trim();
                                to = toEmail.Trim();
                            }
                            else
                            {
                                updateComplaintModel.EmailTo = to + ";" + toEmail.Trim();
                                to = updateComplaintModel.EmailTo.Trim();
                            }
                        }
                        updateComplaintModel.EmailListCC = connection.Query<string>($"select email from paemail where status='cc' order by id").ToList();
                        foreach (var ccEmail in updateComplaintModel.EmailListCC)
                        {
                            if (updateComplaintModel.EmailCC == null)
                            {
                                updateComplaintModel.EmailCC = ccEmail.Trim();
                                cc = ccEmail.Trim();
                            }
                            else
                            {
                                updateComplaintModel.EmailCC = cc + ";" + ccEmail.Trim();
                                cc = updateComplaintModel.EmailCC.Trim();
                            }
                        }

                        updateComplaintModel.aptstatus = connection.Query<string>($"select case when status='NotAvailable' and reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and  reservation='' then 'Occupied' when  status='NotAvailable' and reservation is null then 'Occupied' else  status end as status from propertymaster where propertyref='{missue.Pref.Trim()}'").FirstOrDefault();
                    }
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                logger.Info($"Error: {ex}");
            }
            

            gotoreturn:
            return View(updateComplaintModel);
        }

        public string InsertMIssueFollowup(int MissueId, string remarks)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                string message = "";
                try
                {
                    var insert = connection.Execute($"insert into missuefollowup (followup_issueid,followup_remarks,followup_doc) values({MissueId},'{remarks}',getdate())");
                    if (insert == 1)
                    {
                        message = "Follow up remarks updated successfully";
                    }
                    else
                    {
                        message = "Follow up remarks update failed";
                    }
                }
                catch(Exception ex)
                {
                    logger.Info($"Error: {ex}");
                    message = "Error";
                }
                connection.Close();
                return message;
            }
        }

        public int SaveItemPurchased(string itemsPurchased, string qtyPurchased, int missueId, int rows)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //char[] array = itemsPurchased.ToArray();
                //List<string> array;
                //for(int j=0; j<rows; j++)
                //{
                //    for(int k=0; k<2; k++)
                //    {

                //    }
                //}

                List<string> arrayItem = itemsPurchased.Split(",").ToList();
                List<string> qtyItem = qtyPurchased.Split(",").ToList();

                int additemspurchased = 0;
                for (int i=0;i<rows; i++)
                {
                    additemspurchased = connection.Execute($"insert into itempurchased(iid,itemdesc,qty) values('{missueId}','{arrayItem[i]}','{qtyItem[i]}')");
                }
                return additemspurchased;
            }
            
        }

        public int UpdateItemPurchased(string itemsPurchased, string qtyPurchased, int missueId, int rows)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<string> arrayItem = itemsPurchased.Split(",").ToList();
                List<string> qtyItem = qtyPurchased.Split(",").ToList();

                ;connection.ExecuteAsync($"delete from itempurchased where iid={missueId}");

                int additemspurchased = 0;
                for (int i = 0; i < rows; i++)
                {
                    additemspurchased = connection.Execute($"insert into itempurchased(iid,itemdesc,qty) values('{missueId}','{arrayItem[i]}','{qtyItem[i]}')");
                }
                return additemspurchased;
            }
        }

        public string HoldComplaint(int id)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var insertRissue = connection.Execute($"INSERT INTO RISSUE SELECT [id],[PREF],[APTLOCATION],[pname],[Category],[ISSUE],[STATUS],[REMARKS],[REQUEST],[requestdate],[doc],[dou],[wcdate],[itempurchase],[workdescription],[wdstatus],[additionalwork],[PANAME],[PL],[JOBID],[qty],[tname],[contactno],[lcno],[tavailability],[contractortype],[contractorname],[fcaseen],[underobservationstart],[underobservationstop],[fcaquestion],[pareply] FROM missue WHERE ID={id}");
                var deleteMissue = connection.Execute($"DELETE FROM missue WHERE ID={id}");
                if(deleteMissue == 1 && insertRissue == 1)
                {
                    ViewBag.Message = "Issue is removed";
                }
            }
            return ViewBag.Message;
        }

        public async Task<JsonResult> Recent100Jobs()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var recentjobs = await connection.QueryAsync<Missue>($"SELECT top 100 ID,PNAME,ISSUE,STATUS,REMARKS,REQUEST,CONVERT(VARCHAR(11),wcdate,106) as wcdate,workdescription,wdstatus,aptlocation,issueno FROM missue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and status in ('DONE') order by convert(datetime, wcdate, 103) desc");
                return Json(recentjobs.ToList());
            }
        }

        public async Task<JsonResult> AllJobs(/*int? page*/)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //var data = await connection.QueryAsync<Missue>($"SELECT ID,PNAME,ISSUE,STATUS,REMARKS,REQUEST,CONVERT(VARCHAR(11),wcdate,106) as wcdate,workdescription,wdstatus,aptlocation,issueno FROM missue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and status in ('DONE') order by id desc");
                var data = await context.Missues.Where(x => x.Pname == HttpContext.Session.GetString("CurrentUserDepartment")).Where(x=>x.Status=="Done").OrderByDescending(x=>x.Id).ToListAsync();
                //return Json(data.ToList());
                return Json(data);

                //if (page > 0)
                //{
                //    page = page;
                //}
                //else
                //{
                //    page = 1;
                //}
                //int start = (int)(page - 1) * pageSize;
                //ViewBag.pageCurrent = page;
                //int totalPage = data.Count();
                //float totalNumsize = (totalPage / (float)pageSize);
                //int numSize = (int)Math.Ceiling(totalNumsize);
                //ViewBag.numSize = numSize;
                //var dataPost = data.OrderByDescending(x => x.Id).Skip(start).Take(pageSize);
                //List<Post> listPost = new List<Post>();
                //listPost = dataPost.ToList();
                //// return Json(listPost);
                //return Json(new { data = listPost, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> ViewHoldComplaints()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                List<Rissue> rissues = new();
                var recordcount = connection.Query<int>($"SELECT count(*) as rec FROM rissue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").FirstOrDefault();
                if(recordcount != 0)
                {
                    var model = await connection.QueryAsync<Rissue>($"SELECT category, ID,PNAME, CONVERT(VARCHAR(11),doc,106) as doc,ISSUE,STATUS,REMARKS,'Removed By '+paname AS request,APTLOCATION FROM rissue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id desc");
                    rissues = model.ToList();
                    //
                }
                //else
                //{
                //   ViewBag.Message = "There is No Issues On Hold";
                //}

                return Json(rissues);
            }
        }

        public IActionResult WMR()
        {
            return View();
        }

        public IActionResult WorkDescriptionUpdate(int id, WorkDescriptionUpdateModel model)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                if(model.missue != null)
                {
                    if(model.missue.Wdstatus == "VERIFIED")
                    {
                        ViewBag.Message = "Job is Verified;cannot modify";
                        goto gotoreturn;
                    }

                    if(model.missue.Wdstatus == "UPDATE")
                    {
                        connection.ExecuteAsync($"update missue set WDSTATUS='CORRECTED' where id={model.missue.Id}");
                    }

                    connection.ExecuteAsync($"update missue set additionalwork='{model.missue.Additionalwork}',itempurchase='{model.missue.Itempurchase}',wcdate='{model.missue.Wcdate}',qty='-',workdescription='{model.missue.Workdescription}',dou=getdate() where id='{model.missue.Id}'");
                }
                else
                {
                    var missue = connection.Query<Missue>($"select additionalwork,pareply,fcaquestion,issue,aptlocation,itempurchase,wdstatus,qty,workdescription,CONVERT(VARCHAR(11),wcdate,106) as wcdate,issueno,contractortype,contractorname from missue where id={id} and pname ='{HttpContext.Session.GetString("CurrentUserDepartment")}'").FirstOrDefault();
                    model.missue = missue;
                    model.missue.Itempurchase = missue.Itempurchase.Trim();
                    var recordcount = connection.Query<int>($"select count(*) as rec from itempurchased where iid={id}").FirstOrDefault();
                    if (recordcount > 0)
                    {
                        model.itemspurchased = connection.Query<Itempurchased>($"select * from itempurchased where iid={id}").ToList();
                    }
                }
                

                //if(model.missue.Wdstatus == "Verified")
            }
            gotoreturn:
            return View(model);
        }

        public JsonResult LoadRefNoAndAptStatus(string Aptno)
        {
            Propertymaster propertymaster = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                
                var recordcount = connection.Query<int>($"Select count(*) from missue where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and aptlocation='{Aptno}' and status not in('Done')").FirstOrDefault();
                if(recordcount > 0)
                {
                    ViewBag.Message = "Already there are some issues registerd under this Apartment;make sure it is not duplicated";
                    goto gotoreturn;
                }

                propertymaster = connection.Query<Propertymaster>($"select PROPERTYREF, case when status='NotAvailable' and reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and reservation='' then 'Occupied' when status='NotAvailable' and reservation is null then 'Occupied' else status end as status from propertymaster where bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and aptno='{Aptno}'").FirstOrDefault();

                connection.Close();
                gotoreturn:
                return Json(propertymaster);
            }

        }

        public async Task<IActionResult> PaNewLOI(NewLOIModel newLOIModel)
        {
            if(newLOIModel.LOI != null)
            {
                using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    if (newLOIModel.LOI.EnquiryType == null)
                    {
                        ViewBag.Message = "Mention It is a Corporate or Individual Client";
                        goto gotoview;
                    }

                    var insertLOI = await connection.ExecuteAsync($"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,PropertyName,PropertyRefNo,aptno,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,clientRESF,LLRESF,docsubmitted,TotClients,SearchedProperty,LoiDate,LoiStatus,doc,dept,deptusr,LOIREMARKS,movingindate)values('{newLOIModel.LOI.EnquiryType}','0000','-','{newLOIModel.LOI.ClientName}','{newLOIModel.LOI.Cmob}','{newLOIModel.LOI.Cnationality}','{newLOIModel.LOI.ClientCompany}','{newLOIModel.LOI.ClientSource}','{newLOIModel.LOI.Req}','{newLOIModel.LOI.Fur}','ManagedProperty','{newLOIModel.LOI.PropertyName}','{newLOIModel.LOI.PropertyRefNo}','{newLOIModel.LOI.Aptno}',CONVERT(date, '{newLOIModel.LOI.Leasedate}', 103),CONVERT(date, '{newLOIModel.LOI.Leaseenddate}', 103),null,'{newLOIModel.LOI.Ap}','{newLOIModel.LOI.Aptype}','-','{newLOIModel.LOI.Rent}','-','{newLOIModel.LOI.Deposit}','-','{newLOIModel.LOI.ClientResf}','0.000','-','-','-',getdate(),'Open',getdate(),'PA','{HttpContext.Session.GetString("CurrentUsername")}','{newLOIModel.LOI.Loiremarks}','{newLOIModel.LOI.Movingindate}')");
                    ViewBag.Message = "LOI is Generated Successfully";

                    var mainloino = connection.Query<string>($"select loino from loiinformation where id=(select max(id) from loiinformation where dept='PA' and Propertyname='{newLOIModel.LOI.PropertyName}')").FirstOrDefault();//having doubt on the purpose of this query
                }
            }

            gotoview:
            return View(newLOIModel);
        }

        public IActionResult PaListLOI(int page = 1, int pageSize = 10)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loiinformations = connection.Query<Loiinformation>($"select LoiNo,loi_no LoiNo1,inqno,LEName,ClientName,ClientSource,PropertySource,PropertyName,APTNO,PropertyRefNo,CONVERT(VARCHAR(11),Leasedate ,106) as Leasedate,CONVERT(VARCHAR(11),Leaseenddate ,106) as Leaseenddate,LoiStatus,loimailstatus from loiInformation where dept='PA' and propertyname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id desc").ToList();
                //PagedList<PMS_Admin_Web.Models.Loiinformation> model = new PagedList<PMS_Admin_Web.Models.Loiinformation>(loiinformations, page, pageSize);
                connection.Close();
                return View(loiinformations);
            }
            
        }

        public async Task<IActionResult> PaEditLOI(string loiNo, NewLOIModel newLOIModel, IFormFile passportfile, IFormFile civilidfile, IFormFile nocvisafile, IFormFile marriagecontractfile, IFormFile moclicensefile, IFormFile civilidASfile, IFormFile salarycertificatefile, IFormFile companysignfile, IFormFile staffemployeefile, IFormFile companyguaranteefile, IFormFile loifile)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (newLOIModel.LOI == null)
                {
                    string to = "", cc = "";
                    newLOIModel.EmailLoiToList = connection.Query<string>($"select email from paemail where status='loito' order by id").ToList();
                    foreach(var email in newLOIModel.EmailLoiToList)
                    {
                        if(newLOIModel.EmailLoiTo == null)
                        {
                            newLOIModel.EmailLoiTo = email.Trim();
                            to = email.Trim();
                        }
                        else
                        {
                            newLOIModel.EmailLoiTo = to + ";" + email.Trim();
                            to = newLOIModel.EmailLoiTo.Trim();
                        }
                    }

                    newLOIModel.EmailLoiCcList = connection.Query<string>($"select email from paemail where status='loicc' order by id").ToList();
                    foreach (var email in newLOIModel.EmailLoiCcList)
                    {
                        if (newLOIModel.EmailLoiCc == null)
                        {
                            newLOIModel.EmailLoiCc = email.Trim();
                            cc = email.Trim();
                        }
                        else
                        {
                            newLOIModel.EmailLoiCc = cc + ";" + email.Trim();
                            cc = newLOIModel.EmailLoiCc.Trim();
                        }
                    }

                    newLOIModel.LOI = connection.Query<Loiinformation>($"SELECT [ID],[EnquiryType],[inqno],[LoiNo],[LEName],[ClientName],[Cmob],[CNationality],[ClientCompany],[ClientSource],[Req],[fur],[PropertySource],[PropertyName],[PropertyRefNo],[Aptno],[Area],CONVERT(VARCHAR(11),[Leasedate] ,106) as Leasedate,CONVERT(VARCHAR(11),[Leaseenddate] ,106) as Leaseenddate ,CONVERT(VARCHAR(11),[loisigndate] ,106) as loisigndate ,[Ap],[Aptype],[Renttobepaidby],[rent],[Securitydepositpaidby],[deposit],[feetobepaidby],[clientRESF],[LLRESF],[TotClients],[SearchedProperty],[TotOccupants],CONVERT(VARCHAR(11),[LoiDate] ,106) as LoiDate,[LoiStatus],[doc],[Updateddate],[docsubmitted],[lcsigned],[paystart],[payment] ,[paystatus],[payremarks],[paymentdate],[loi_no] LoiNo1,[lc_no],[DEPT],[DEPTUSR],[loinote],[lccreate],[loiremarks],[duerent],[duedeposit],[dueresf],[loipath],[pp],[cid],[noc],[mc],[coe],[cosign],[cas],[cidpp],[cg],[moc],CONVERT(VARCHAR(11),[movingindate] ,106) as movingindate FROM [Realtor].[dbo].[LOIInformation] where loino='{loiNo}'").FirstOrDefault();

                }
                else
                {
                    if(newLOIModel.LOI.LoiStatus == "Approved")
                    {
                        ViewBag.Message = "LOI is Approved changes not possible";
                        goto gotoview;
                    }

                    if(newLOIModel.LOI.LoiNo != null)
                    {
                        if (passportfile != null)
                        {
                            string LOIpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/pp");
                            if (!Directory.Exists(LOIpppath))
                                Directory.CreateDirectory(LOIpppath);

                            string ppextension = Path.GetExtension(passportfile.FileName);
                            string ppfileName = newLOIModel.LOI.LoiNo1 + ppextension;
                            string ppfileNameWithPath = Path.Combine(LOIpppath, ppfileName);
                            if (System.IO.File.Exists(ppfileNameWithPath))
                            {
                                string fileNameWithPathTemp = ppfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var ppstream = new FileStream(ppfileNameWithPath, FileMode.Create);
                            passportfile.CopyTo(ppstream);
                            ppstream.Close();
                            newLOIModel.LOI.Pp = ppfileNameWithPath;
                        }


                        if (civilidfile != null)
                        {
                            string LOIcidpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cid");
                            if (!Directory.Exists(LOIcidpath))
                                Directory.CreateDirectory(LOIcidpath);

                            string cidextension = Path.GetExtension(civilidfile.FileName);
                            string cidfileName = newLOIModel.LOI.LoiNo1 + cidextension;
                            string cidfileNameWithPath = Path.Combine(LOIcidpath, cidfileName);
                            if (System.IO.File.Exists(cidfileNameWithPath))
                            {
                                string fileNameWithPathTemp = cidfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var cidstream = new FileStream(cidfileNameWithPath, FileMode.Create);
                            civilidfile.CopyTo(cidstream);
                            cidstream.Close();
                            newLOIModel.LOI.Cid = cidfileNameWithPath;
                        }


                        if (nocvisafile != null)
                        {
                            string LOInocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/noc");
                            if (!Directory.Exists(LOInocpath))
                                Directory.CreateDirectory(LOInocpath);

                            string nocextension = Path.GetExtension(nocvisafile.FileName);
                            string nocfileName = newLOIModel.LOI.LoiNo1 + nocextension;
                            string nocfileNameWithPath = Path.Combine(LOInocpath, nocfileName);
                            if (System.IO.File.Exists(nocfileNameWithPath))
                            {
                                string fileNameWithPathTemp = nocfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var nocstream = new FileStream(nocfileNameWithPath, FileMode.Create);
                            nocvisafile.CopyTo(nocstream);
                            nocstream.Close();
                            newLOIModel.LOI.Noc = nocfileNameWithPath;
                        }


                        if (marriagecontractfile != null)
                        {
                            string LOImcpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/mc");
                            if (!Directory.Exists(LOImcpath))
                                Directory.CreateDirectory(LOImcpath);

                            string mcextension = Path.GetExtension(marriagecontractfile.FileName);
                            string mcfileName = newLOIModel.LOI.LoiNo1 + mcextension;
                            string mcfileNameWithPath = Path.Combine(LOImcpath, mcfileName);
                            if (System.IO.File.Exists(mcfileNameWithPath))
                            {
                                string fileNameWithPathTemp = mcfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var mcstream = new FileStream(mcfileNameWithPath, FileMode.Create);
                            marriagecontractfile.CopyTo(mcstream);
                            mcstream.Close();
                            newLOIModel.LOI.Mc = mcfileNameWithPath;
                        }


                        if (moclicensefile != null)
                        {
                            string LOImocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/moc");
                            if (!Directory.Exists(LOImocpath))
                                Directory.CreateDirectory(LOImocpath);

                            string mocextension = Path.GetExtension(moclicensefile.FileName);
                            string mocfileName = newLOIModel.LOI.LoiNo1 + mocextension;
                            string mocfileNameWithPath = Path.Combine(LOImocpath, mocfileName);
                            if (System.IO.File.Exists(mocfileNameWithPath))
                            {
                                string fileNameWithPathTemp = mocfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var mocstream = new FileStream(mocfileNameWithPath, FileMode.Create);
                            moclicensefile.CopyTo(mocstream);
                            mocstream.Close();
                            newLOIModel.LOI.Moc = mocfileNameWithPath;
                        }


                        if (civilidASfile != null)
                        {
                            string LOIcaspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cas");
                            if (!Directory.Exists(LOIcaspath))
                                Directory.CreateDirectory(LOIcaspath);

                            string casextension = Path.GetExtension(civilidASfile.FileName);
                            string casfileName = newLOIModel.LOI.LoiNo1 + casextension;
                            string casfileNameWithPath = Path.Combine(LOIcaspath, casfileName);
                            if (System.IO.File.Exists(casfileNameWithPath))
                            {
                                string fileNameWithPathTemp = casfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var casstream = new FileStream(casfileNameWithPath, FileMode.Create);
                            civilidASfile.CopyTo(casstream);
                            casstream.Close();
                            newLOIModel.LOI.Cas = casfileNameWithPath;
                        }


                        if (salarycertificatefile != null)
                        {
                            string LOIcoepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/coe");
                            if (!Directory.Exists(LOIcoepath))
                                Directory.CreateDirectory(LOIcoepath);

                            string coeextension = Path.GetExtension(salarycertificatefile.FileName);
                            string coefileName = newLOIModel.LOI.LoiNo1 + coeextension;
                            string coefileNameWithPath = Path.Combine(LOIcoepath, coefileName);
                            if (System.IO.File.Exists(coefileNameWithPath))
                            {
                                string fileNameWithPathTemp = coefileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var coestream = new FileStream(coefileNameWithPath, FileMode.Create);
                            salarycertificatefile.CopyTo(coestream);
                            coestream.Close();
                            newLOIModel.LOI.Coe = coefileNameWithPath;
                        }


                        if (companysignfile != null)
                        {
                            string LOIcosignpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cosign");
                            if (!Directory.Exists(LOIcosignpath))
                                Directory.CreateDirectory(LOIcosignpath);

                            string cosignextension = Path.GetExtension(companysignfile.FileName);
                            string cosignfileName = newLOIModel.LOI.LoiNo1 + cosignextension;
                            string cosignfileNameWithPath = Path.Combine(LOIcosignpath, cosignfileName);
                            if (System.IO.File.Exists(cosignfileNameWithPath))
                            {
                                string fileNameWithPathTemp = cosignfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var cosignstream = new FileStream(cosignfileNameWithPath, FileMode.Create);
                            companysignfile.CopyTo(cosignstream);
                            cosignstream.Close();
                            newLOIModel.LOI.Cosign = cosignfileNameWithPath;
                        }


                        if (staffemployeefile != null)
                        {
                            string LOIcidpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cidpp");
                            if (!Directory.Exists(LOIcidpppath))
                                Directory.CreateDirectory(LOIcidpppath);

                            string cidppextension = Path.GetExtension(staffemployeefile.FileName);
                            string cidppfileName = newLOIModel.LOI.LoiNo1 + cidppextension;
                            string cidppfileNameWithPath = Path.Combine(LOIcidpppath, cidppfileName);
                            if (System.IO.File.Exists(cidppfileNameWithPath))
                            {
                                string fileNameWithPathTemp = cidppfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var cidppstream = new FileStream(cidppfileNameWithPath, FileMode.Create);
                            staffemployeefile.CopyTo(cidppstream);
                            cidppstream.Close();
                            newLOIModel.LOI.Cidpp = cidppfileNameWithPath;
                        }


                        if (companyguaranteefile != null)
                        {
                            string LOIcgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cg");
                            if (!Directory.Exists(LOIcgpath))
                                Directory.CreateDirectory(LOIcgpath);

                            string cgextension = Path.GetExtension(companyguaranteefile.FileName);
                            string cgfileName = newLOIModel.LOI.LoiNo1 + cgextension;
                            string cgfileNameWithPath = Path.Combine(LOIcgpath, cgfileName);
                            if (System.IO.File.Exists(cgfileNameWithPath))
                            {
                                string fileNameWithPathTemp = cgfileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var cgstream = new FileStream(cgfileNameWithPath, FileMode.Create);
                            companyguaranteefile.CopyTo(cgstream);
                            cgstream.Close();
                            newLOIModel.LOI.Cg = cgfileNameWithPath;
                        }


                        if (loifile != null)
                        {
                            string LOIpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/loi");
                            if (!Directory.Exists(LOIpath))
                                Directory.CreateDirectory(LOIpath);

                            string loiextension = Path.GetExtension(loifile.FileName);
                            string loifileName = newLOIModel.LOI.LoiNo1 + loiextension;
                            string loifileNameWithPath = Path.Combine(LOIpath, loifileName);
                            if (System.IO.File.Exists(loifileNameWithPath))
                            {
                                string fileNameWithPathTemp = loifileNameWithPath;
                                System.IO.File.Delete(fileNameWithPathTemp);
                            }
                            var loistream = new FileStream(loifileNameWithPath, FileMode.Create);
                            loifile.CopyTo(loistream);
                            loistream.Close();
                            newLOIModel.LOI.Loipath = loifileNameWithPath;
                        }

                        var updateLOI = await connection.ExecuteAsync($"UPDATE [dbo].[LOIInformation] SET EnquiryType='{newLOIModel.LOI.EnquiryType}',loi_no='{newLOIModel.LOI.LoiNo1}',clientname='{newLOIModel.LOI.ClientName}',cmob='{newLOIModel.LOI.Cmob}',clientcompany='{newLOIModel.LOI.ClientCompany}',[ClientSource] ='{newLOIModel.LOI.ClientSource}',cnationality='{newLOIModel.LOI.Cnationality}',req='{newLOIModel.LOI.Req}',fur='{newLOIModel.LOI.Fur}',ap='{newLOIModel.LOI.Ap}',aptype='{newLOIModel.LOI.Aptype}' ,[PropertyName] ='{newLOIModel.LOI.PropertyName}' ,[PropertyRefNo] ='{newLOIModel.LOI.PropertyRefNo}' ,[Aptno] ='{newLOIModel.LOI.Aptno}' ,[Leasedate] ='{newLOIModel.LOI.Leasedate}',[rent] ='{newLOIModel.LOI.Rent}',[deposit] ='{newLOIModel.LOI.Deposit}' ,[clientRESF] ='{newLOIModel.LOI.ClientResf}' , updateddate=getdate(),Leaseenddate='{newLOIModel.LOI.Leaseenddate}',loiremarks='{newLOIModel.LOI.Loiremarks}',movingindate='{newLOIModel.LOI.Movingindate}',loipath='{newLOIModel.LOI.Loipath}',pp='{newLOIModel.LOI.Pp}',cid='{newLOIModel.LOI.Cid}',noc='{newLOIModel.LOI.Noc}',mc='{newLOIModel.LOI.Mc}',coe='{newLOIModel.LOI.Coe}',cosign='{newLOIModel.LOI.Cosign}',cas='{newLOIModel.LOI.Cas}',cidpp='{newLOIModel.LOI.Cidpp}',cg='{newLOIModel.LOI.Cg}',moc='{newLOIModel.LOI.Moc}' where loino='{newLOIModel.LOI.LoiNo}'");
                        if (updateLOI == 1)
                        {
                            ViewBag.Message = "LOI is updated";
                        }
                        else
                        {
                            ViewBag.Message = "Failed";
                        }
                        
                    }
                }
                connection.Close();
            }
            gotoview:
            return View(newLOIModel);
        }

        public async Task<IActionResult> WalkinClientInquiry(WalkinClientInquiryModel walkinClientInquiryModel)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (walkinClientInquiryModel.walkininquiry != null)
                {
                    if(walkinClientInquiryModel.walkininquiry.Name == null)
                    {
                        ViewBag.Message = "ENTER THE NAME";
                        goto gotoreturn;
                    }
                    if(walkinClientInquiryModel.walkininquiry.Status == null)
                    {
                        ViewBag.Message = "ENTER THE STATUS";
                        goto gotoreturn;
                    }
                    if (walkinClientInquiryModel.walkininquiry.Nationality == null)
                    {
                        ViewBag.Message = "Enter The Nationality";
                        goto gotoreturn;
                    }
                    if(walkinClientInquiryModel.date == null)
                    {
                        ViewBag.Message = "Enter The Date";
                        goto gotoreturn;
                    }

                    var insertWalkininquiry = await connection.ExecuteAsync($"insert into walkininquiry(name,status,month,year,pname,seenbyfca,nationality)values('{walkinClientInquiryModel.walkininquiry.Name}','{walkinClientInquiryModel.walkininquiry.Status}','{Convert.ToDateTime(walkinClientInquiryModel.date).Month}','{Convert.ToDateTime(walkinClientInquiryModel.date).Year}','{HttpContext.Session.GetString("CurrentUserDepartment")}' ,'NO','{walkinClientInquiryModel.walkininquiry.Nationality}')");

                    
                    walkinClientInquiryModel.walkininquiry = null;
                }

                walkinClientInquiryModel.walkininquiries = connection.Query<Walkininquiry>($"select * from walkininquiry where month='{Convert.ToDateTime(walkinClientInquiryModel.date).Month}' and year='{Convert.ToDateTime(walkinClientInquiryModel.date).Year}' and pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
            }
            
            gotoreturn:
            return View(walkinClientInquiryModel);
        }

        public string RemoveWalkinClientInquiry(int id)
        {
            string message = "";
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var deleteWalkinInquiry = connection.Execute($"DELETE FROM walkininquiry WHERE ID='{id}'");
                if(deleteWalkinInquiry == 1)
                {
                    message = "Deleted Successfully";
                }
                else
                {
                    message = "Not Deleted";
                }
                connection.Close();
            }
            return message;
        }

        public List<Walkininquiry> ViewWalkinInquiries(DateTime? date)
        {
            //List<Walkininquiry> walkinInquiries = new();
            //WalkinClientInquiryModel walkinClientInquiryModel = new();
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var walkininquiries = connection.Query<Walkininquiry>($"select * from walkininquiry where month='{Convert.ToDateTime(date).Month}' and year='{Convert.ToDateTime(date).Year}' and pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
                return walkininquiries;
            }
            
        }

        public IActionResult PaMovingIn()
        {
            List<PaMovingInModel> paMovingInModels = new();
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //paMovingInModels = connection.Query<PaMovingInModel>($"SELECT propertyref,BldgName,aptno,rftype ,loino,reservedfor,rmob,rstatus,rlstart, CONVERT(VARCHAR(11),indate,106) as indate,rid FROM ( select leasetype,propertyref,BldgName,aptno,rftype ,leaseno,case when left(leaseno,2)='LO' then leaseno when left(leaseno,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=leaseno) END AS loino,reservedfor,rmob,rstatus,CONVERT(VARCHAR(11),rlstart,106) as rlstart,'1' as rid,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =leaseno) as indate from propertymaster  where reservation ='yes' union all SELECT  (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno ,CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'2' as rid ,(select top 1 CONVERT(VARCHAR(11),movingindate,106) from movingin where leaseno =propertymaster.cleaseno) as indate from propertymaster where DATEPART (MM ,leasestart )=DATEPART (MM,GETDATE()) AND DatePart(YYYY, leasestart) = DatePart(YYYY, GETDATE()) union all SELECT (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno ,CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'3' as rid ,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =propertymaster.cleaseno) as indate from propertymaster where DATEPART (MM ,leasestart )= month(DATEADD(DAY, -30, GETDATE())) AND DATEPART (YYYY ,leasestart )= year(DATEADD(DAY, -30, GETDATE())) )SRC where leasetype='New LC' and bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' ORDER BY convert(datetime, RLSTART, 103) DESC").ToList();
                paMovingInModels = connection.Query<PaMovingInModel>($"SELECT propertyref,BldgName,aptno,rftype ,loino,reservedfor,rmob,rstatus,rlstart, CONVERT(VARCHAR(11),indate,106) as indate,rid FROM ( select leasetype,propertyref,BldgName,aptno,rftype ,leaseno,case when left(leaseno,2)='LO' then leaseno when left(leaseno,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=leaseno) END AS loino,reservedfor,rmob,rstatus,CONVERT(VARCHAR(11),rlstart,106) as rlstart,'1' as rid,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =propertymaster.leaseno) as indate from propertymaster  where reservation ='yes' union all SELECT  (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno ,CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'2' as rid ,(select top 1 CONVERT(VARCHAR(11),movingindate,106) from movingin where leaseno =propertymaster.cleaseno) as indate from propertymaster where DATEPART (MM ,leasestart )=DATEPART (MM,GETDATE()) AND DatePart(YYYY, leasestart) = DatePart(YYYY, GETDATE()) union all SELECT (select lctype from lcinfo where lc_no=cleaseno ) as leasetype,propertyref,BldgName,aptno,CFTYPE,CLEASENO,case when left(CLEASENO,2)='LO' then cleaseno when left(CLEASENO,2)='LC' THEN (SELECT LOI_NO FROM LCINFO WHERE LC_NO=CLEASENO) END AS loino,CNAME,CMOB,cleaseno ,CONVERT(VARCHAR(11),LEASESTART,106) as LEASESTART,'3' as rid ,(select top 1 CONVERT(VARCHAR(11),movingindate,106) as movingindate from movingin where leaseno =propertymaster.cleaseno) as indate from propertymaster where DATEPART (MM ,leasestart )= month(DATEADD(DAY, -30, GETDATE())) AND DATEPART (YYYY ,leasestart )= year(DATEADD(DAY, -30, GETDATE())) )SRC where leasetype='New LC' and bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' ORDER BY convert(datetime, RLSTART, 103) DESC").ToList();
                connection.Close();
            }
            return View(paMovingInModels);
        }

        public ActionResult ViewLOI(string loino)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select loipath from LOIInformation where loi_no='{loino}'").FirstOrDefault();
                connection.Close();
                if (file != "")
                {
                    byte[] abc = System.IO.File.ReadAllBytes(file);
                    System.IO.File.WriteAllBytes(file, abc);
                    MemoryStream ms = new MemoryStream(abc);
                    return new FileStreamResult(ms, "application/pdf");
                }
                else
                {
                    return RedirectToAction("PaMovingIn", "PA");
                }
            }
        }

        public IActionResult PaMovingOut()
        {
            return View();
        }

        public async Task<IActionResult> PaNewVacating(NewVacatingModel newVacatingModel)
        {
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
                }
            }
            gotoreturn:
            //return View(newVacatingModel);
            return View();
        }

        public string NewVacatingEmail(string changeinrent, string propertyname, string aptno, string tenantname, decimal? oldrent, decimal? newrent, DateTime? effectfrom, string remarks, string mailto, string mailcc, string attachment)
        {
            string rentmode1 = "";
            if (changeinrent == "Rent Increase")
            {
                rentmode1 = "Increased";
            }
            else
            {
                rentmode1 = "Decreased";
            }

            MailMessage message = new MailMessage();
            message.Subject = $"Approval for {changeinrent} - {propertyname} - Apt# {aptno}";
            message.HtmlBody = @$"<html>
                                    <body>
                                        <p style=""font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; font-size: 12px"">
                                            <b>Dear ALL:</b>
                                            <br />
                                            <br />
                                            Please find attached<u><b>Approval of {changeinrent}</b></u> for the following:
                                            <br />
                                            <ul>
                                                <li>
                                                    <label style=""color: red;""><u><b>{propertyname} - Apt # {aptno}</b></u></label>
                                                    rented by <u><b><label style=""color: red;""> {tenantname}</label></b></u>
                                                    at KD {oldrent} {rentmode1} to KD {newrent} w.e.f <b>{Convert.ToDateTime(effectfrom).Day} {Convert.ToDateTime(effectfrom).Month} {Convert.ToDateTime(effectfrom).Year}</b>
                                                </li>
                                            </ul>
                                            <br />
                                            <br />
                                            Note: {remarks}
                                            <br />
                                            <br />
                                            This is for your information and records.
                                            <br />
                                            <br />
                                            <br />
                                            <label style=""color:blue""><b>THIS EMAIL IS GENERATED THROUGH PMS</b></label>
                                        </p>
                                    </body>
                                  </html>";
            message.From = new MailAddress("narutouzumaki95415@gmail.com", "Tester", false);
            message.To.Add(new MailAddress(mailto, "Recipient 1", false));
            message.CC.Add(new MailAddress(mailcc, "Recipient 3", false));
            if (attachment != null)
                message.Attachments.Add(new Attachment(attachment));
            message.Save("EmailMessage.eml", SaveOptions.DefaultEml);

            MailMessage msg = MailMessage.Load("EmailMessage.eml");
            SmtpClient client = new SmtpClient();
            client.Host = "mail.server.com";
            client.Username = "username";
            client.Password = "password";
            client.Port = 587;
            client.SecurityOptions = SecurityOptions.SSLExplicit;

            client.SendAsync(msg);

            Console.WriteLine("Sending message...");

            string response = "Mail Sent";
            msg.Dispose();

            return response;
        }

        public async Task<IActionResult> PaVacatingList()
        {
            PaVacatingListModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var vacatingList = await connection.QueryAsync<Propertymaster>(@$"select propertyref,BldgName,aptno,cftype,cbtype,cnat,cname,cmob,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),moveout,106) as moveout,vacatingstatus,case when len((select bmapproval from moveout where pref =propertyref and id=moveoutid))<1 then 0 when len((select bmapproval from moveout where pref =propertyref and id=moveoutid)) is null then '0' else 1 end as record,case when convert(datetime, moveout, 103)<=convert(datetime, getdate(), 103) then 'yes' else 'no' end as todayv from propertymaster where vacatingstatus='Vacating' AND BLDGNAME='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by convert(datetime, moveout, 103) ASC");//id as Id,
                model.vacatinglist = vacatingList.ToList();

                var pendingVacating = await connection.QueryAsync<Tenantshistory>(@$"select pref,id,pname,aptno,ftype,btype,tenantname,nationality,contact,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),movedate,106) as movedate,case when len((select moi from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,case when len((select bmapproval from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord from tenantshistory WHERE PNAME='{HttpContext.Session.GetString("CurrentUserDepartment")}' AND STATUS='PENDING' order by id desc");
                model.pendingvacatinglist = pendingVacating.ToList();

                var tlc = await connection.QueryAsync<Tenantshistory>(@$"select pref,id,pname,aptno,ftype,btype,tenantname,nationality,contact,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),movedate,106) as movedate,case when len((select moi from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,case when len((select bmapproval from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord from tenantshistory WHERE PNAME='{HttpContext.Session.GetString("CurrentUserDepartment")}' AND STATUS='CLOSED' order by convert(datetime, closedate, 103) desc");
                model.tlc = tlc.ToList();

                connection.Close();
            }

            //vacatingsAndTlcModel.tlc = new();
            //var query = await context.Tenantshistories.Where(s => s.Status == "CLOSED").OrderByDescending(s => s.Closedate).ToListAsync();
            //foreach (var item in query)
            //{
            //    VacatingsAndTlcGridModel modelgrid = new();
            //    modelgrid.Id = item.Id;
            //    modelgrid.PropertyName = item.Pname;
            //    modelgrid.AptNo = item.Aptno;
            //    modelgrid.Type = item.Ftype;
            //    modelgrid.Bed = item.Btype;
            //    modelgrid.TenantName = item.Tenantname;
            //    modelgrid.LeaseStartDate = Convert.ToDateTime(item.Leasestart);
            //    modelgrid.LeaseEndDate = Convert.ToDateTime(item.Leaseend);
            //    modelgrid.ContactNo = item.Contact;
            //    modelgrid.Nationality = item.Nationality;
            //    modelgrid.MoveOutOn = Convert.ToDateTime(item.Movedate);
            //    modelgrid.Pref = item.Pref;

            //    vacatingsAndTlcModel.tlc.Add(modelgrid);
            //}
            return View(model);
        }

        public async Task<IActionResult> PaVacatingProcess(int id, string name, string pref)
        {
            VacatingProcessModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (name == "TLC")
                {
                    var tenantHistory = await connection.QueryAsync<Tenantshistory>($@"select CONVERT(VARCHAR(11),keyreturndate,106) as keyreturndate,
                                                                                    pref,pname,aptno,ftype,btype,tenantname,nationality,contact,remarks,
                                                                                    CONVERT(VARCHAR(11),leasestart,106) as leasestart,
                                                                                    CONVERT(VARCHAR(11),leaseend,106) as leaseend,
                                                                                    CONVERT(VARCHAR(11),movedate,106) as movedate,moveoutid,status,forceclose 
                                                                                    from tenantshistory 
                                                                                    where pref='{pref}' and id={id}");
                    model.AptNo = tenantHistory.FirstOrDefault().Aptno;
                    model.PropertyRef = tenantHistory.FirstOrDefault().Pref;
                    model.PropertyName = tenantHistory.FirstOrDefault().Pname;
                    model.MoveOutDate = Convert.ToDateTime(tenantHistory.FirstOrDefault().Movedate);
                    model.TenantName = tenantHistory.FirstOrDefault().Tenantname;
                    model.Remarks = tenantHistory.FirstOrDefault().Remarks;//textbox1 in old code
                    model.KeyReturnDate = Convert.ToDateTime(tenantHistory.FirstOrDefault().Keyreturndate);
                    if (!string.IsNullOrEmpty(tenantHistory.FirstOrDefault().Forceclose))
                        model.ForceClose = tenantHistory.FirstOrDefault().Forceclose.Trim();

                    int varmoveid;
                    if (DBNull.Value.Equals(tenantHistory.FirstOrDefault().Moveoutid))
                    {
                        var moveId = await connection.QueryAsync<int>(@$"insert into moveout(pref,pname,aptno,tenantname,doc) values('{tenantHistory.FirstOrDefault().Pref}','{tenantHistory.FirstOrDefault().Pname}','{tenantHistory.FirstOrDefault().Aptno}','{tenantHistory.FirstOrDefault().Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int) ");
                        varmoveid = moveId.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update tenantshistory set moveoutid='{varmoveid}' where  pref='{pref}' and id={id}");
                    }
                    else
                    {
                        var tenantHis = await connection.QueryAsync<Tenantshistory>($"select pref,pname,aptno,ftype,btype,tenantname,nationality,contact,remarks,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),movedate,106) as movedate,moveoutid from tenantshistory where pref='{pref}' and id={id}");
                        varmoveid = Convert.ToInt32(tenantHis.FirstOrDefault().Moveoutid);
                    }

                    if (varmoveid == 0)
                    {
                        var moveId = await connection.QueryAsync<int>(@$"insert into moveout(pref,pname,aptno,tenantname,doc) values('{tenantHistory.FirstOrDefault().Pref}','{tenantHistory.FirstOrDefault().Pname}','{tenantHistory.FirstOrDefault().Aptno}','{tenantHistory.FirstOrDefault().Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int) ");
                        varmoveid = moveId.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update tenantshistory set moveoutid='{varmoveid}' where  pref='{pref}' and id={id}");

                    }

                    var moveout = await connection.QueryAsync<Moveout>(@$"select SDFROM,PMODEOFSD,
                                                    CASE WHEN SDAMT IS NULL THEN 0 ELSE SDAMT END AS SDAMT,
                                                    CONVERT(VARCHAR(11),ARDATE,106) as ARDATE,
                                                    CONVERT(VARCHAR(11),moidate,106) as moidate,vn,
                                                    case when RMAT is null then 0 else RMAT end as RMAT,
                                                    case when ARMAT is null then 0 else ARMAT end as ARMAT,
                                                    REDUCTION,WAVED,bmapproval,bmreason,bmapproved,keys,
                                                    acards,moi,moilist,refurbbrkdown,moidate,lc,sd,sat,
                                                    rentpaid,moistatus,sdendorsement,tenantrefurb,sparemarks 
                                                    from moveout 
                                                    where id={varmoveid}");
                    model.moveout = moveout.FirstOrDefault();
                }
                else
                {
                    var propertymaster = await connection.QueryAsync<Propertymaster>($"select aptno,propertyref,bldgname,CONVERT(VARCHAR(11),moveout,106) as moveout,cname,moveoutremarks,moveoutid from propertymaster where propertyref='{pref}'");
                    model.AptNo = propertymaster.FirstOrDefault().AptNo;
                    model.PropertyRef = propertymaster.FirstOrDefault().PropertyRef;
                    model.PropertyName = propertymaster.FirstOrDefault().BldgName;
                    model.MoveOutDate = Convert.ToDateTime(propertymaster.FirstOrDefault().Moveout);
                    model.TenantName = propertymaster.FirstOrDefault().Cname;
                    model.Remarks = propertymaster.FirstOrDefault().Moveoutremarks;//textbox1 of old code

                    int varmoveid;
                    if (DBNull.Value.Equals(propertymaster.FirstOrDefault().Moveoutid))
                    {
                        var moveid = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{propertymaster.FirstOrDefault().PropertyRef}','{propertymaster.FirstOrDefault().BldgName}','{propertymaster.FirstOrDefault().AptNo}','{propertymaster.FirstOrDefault().Cname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");
                        varmoveid = moveid.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update propertymaster set moveoutid={varmoveid} where  propertyref='{pref}' ");
                    }
                    else
                    {
                        var proMaster = await connection.QueryAsync<Propertymaster>($"select aptno,propertyref,bldgname,CONVERT(VARCHAR(11),moveout,106) as moveout,cname,moveoutremarks,moveoutid from propertymaster where propertyref='{pref}'");
                        varmoveid = Convert.ToInt32(proMaster.FirstOrDefault().Moveoutid);
                    }

                    if (varmoveid == 0)
                    {
                        //if(model.TenantName == "")
                        //{
                        //    var updateProperty = await connection.ExecuteAsync($"");
                        //}
                        var movevid = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{propertymaster.FirstOrDefault().PropertyRef}','{propertymaster.FirstOrDefault().BldgName}','{propertymaster.FirstOrDefault().AptNo}','{propertymaster.FirstOrDefault().Cname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");
                        varmoveid = movevid.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update propertymaster set moveoutid={varmoveid} where  propertyref='{pref}' ");
                    }

                    var moveout = await connection.QueryAsync<Moveout>(@$"select SDFROM,PMODEOFSD,
                                                                        CASE WHEN SDAMT IS NULL THEN 0 ELSE SDAMT END AS SDAMT,
                                                                        CONVERT(VARCHAR(11),ARDATE,106) as ARDATE,
                                                                        CONVERT(VARCHAR(11),moidate,106) as moidate,vn,
                                                                        case when RMAT is null then 0 else RMAT end as RMAT,
                                                                        case when ARMAT is null then 0 else ARMAT end as ARMAT,
                                                                        REDUCTION,WAVED,bmapproval,bmreason,bmapproved,
                                                                        keys,acards,moi,moilist,refurbbrkdown,moidate,lc,sd,sat,
                                                                        rentpaid,moistatus,sdendorsement,tenantrefurb,sparemarks 
                                                                        from moveout 
                                                                        where id={varmoveid}");
                    model.moveout = moveout.FirstOrDefault();
                }
            }

            return View(model);
        }

        public IActionResult PaDHC()
        {
            //string dept = HttpContext.Session.GetString("CurrentUserDepartment");
            //string modelclass = Regex.Replace(dept, @" ", "");
            //Type classmodel = Type.GetType($"PMS_Admin_Web.Models.{modelclass}");
            //object instance = Activator.CreateInstance(classmodel);
            //HelperclassPname helperclassPname = new();
            //helperclassPname = (HelperclassPname)instance;
            // Class1 obj1 = new Class1();
            //HelperclassPname obj2 = JsonConvert.DeserializeObject<HelperclassPname>(JsonConvert.SerializeObject(instance));

            PaDHCModel model = new();
            model.popupModel = new ();
            using (var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.recordcount = connection.Query<int>($"select count(*) as rec from sysobjects where name='{HttpContext.Session.GetString("CurrentUserDepartment")}' and CONVERT(VARCHAR(11),crdate,106) =CONVERT(VARCHAR(11),getdate(),106)").FirstOrDefault();

                //model.DHCHeader = connection.Query<string>("select pf_features PfFeatures from propertyfeatures where pf_mode='features' order by id").ToList();

                if (model.recordcount > 0)
                {
                    var queryString = $"select location,[Floorings-Walls- Windows-Ceilings-Glass Panel] as panel,Furnitures as f,[Water Tank/ Pump Room] as wt,[Swimming Pool / Area] as sw, [Equipment / Gym] as eg, [Floor / Lobby] as fl,[Drainage System] as ds, [A/C Grills] as acg, [Parking Area] as pa, [Auto Gate Barriers] as agb, [Roller Shutters] as rs, [Plants & Garden] as pg, [Staircases] as sc, [Corridors] as cor, [Lighting System] as ls,[HVAC Rooms] as hvr,[Transformer & Incoming Switch Room] as tisr,[Fire Alarm / Security System] as fass,[Fire Exits] as fe,[Fire Hose Reels, Cabinets & Extinguishers] as frce,[Lift Room] as lr,[Garbage Chutes] as gc,[Staff Accomodation] as sa,[Staff Presentation] as sp from [{HttpContext.Session.GetString("CurrentUserDepartment")}]";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.CommandType = CommandType.Text;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var popup = new dhcbodypopupModel
                                {
                                    location = reader.GetString("location"),
                                    flooring = reader.IsDBNull("panel") ? "" : reader.GetString("panel"),
                                    furniture = reader.IsDBNull("f") ? "" : reader.GetString("f"),
                                    watertank = reader.IsDBNull("wt") ? "" : reader.GetString("wt"),
                                    swimming = reader.IsDBNull("sw") ? "" : reader.GetString("sw"),
                                    gym = reader.IsDBNull("eg") ? "" : reader.GetString("eg"),
                                    lobby = reader.IsDBNull("fl") ? "" : reader.GetString("fl"),
                                    drain = reader.IsDBNull("ds") ? "" : reader.GetString("ds"),
                                    ac = reader.IsDBNull("acg") ? "" : reader.GetString("acg"),
                                    parking = reader.IsDBNull("pa") ? "" : reader.GetString("pa"),
                                    autogate = reader.IsDBNull("agb") ? "" : reader.GetString("agb"),
                                    rollershutter = reader.IsDBNull("rs") ? "" : reader.GetString("rs"),
                                    plants = reader.IsDBNull("pg") ? "" : reader.GetString("pg"),
                                    stair = reader.IsDBNull("sc") ? "" : reader.GetString("sc"),
                                    corridor = reader.IsDBNull("cor") ? "" : reader.GetString("cor"),
                                    lighting = reader.IsDBNull("ls") ? "" : reader.GetString("ls"),
                                    hvac = reader.IsDBNull("hvr") ? "" : reader.GetString("hvr"),
                                    transformer = reader.IsDBNull("tisr") ? "" : reader.GetString("tisr"),
                                    firealarm = reader.IsDBNull("fass") ? "" : reader.GetString("fass"),
                                    fireexits = reader.IsDBNull("fe") ? "" : reader.GetString("fe"),
                                    firehose = reader.IsDBNull("frce") ? "" : reader.GetString("frce"),
                                    lift = reader.IsDBNull("lr") ? "" : reader.GetString("lr"),
                                    garbage = reader.IsDBNull("gc") ? "" : reader.GetString("gc"),
                                    staffacc = reader.IsDBNull("sa") ? "" : reader.GetString("sa"),
                                    staffpresent = reader.IsDBNull("sp") ? "" : reader.GetString("sp")
                                };

                                model.popupModel.Add(popup);
                            }

                            reader.Close();
                        }
                        command.Cancel();
                    }

                }
                model.pfFloor = connection.Query<string>($"select pf_floors from propertyfloors where pf_pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id").ToList();
                connection.Close();
                
            }
            
            return View(model);
        }

        public JsonResult PaDHCBody()
        {
            string dept = HttpContext.Session.GetString("CurrentUserDepartment");
            string modelclass = Regex.Replace(dept, @" ", "");
            Type classmodel = Type.GetType($"PMS_Admin_Web.Models.{modelclass}");
            object instance = Activator.CreateInstance(classmodel);
            //HelperclassPname helperclassPname = new();
            //helperclassPname = (HelperclassPname)instance;
            // Class1 obj1 = new Class1();
            HelperclassPname obj2 = JsonConvert.DeserializeObject<HelperclassPname>(JsonConvert.SerializeObject(instance));

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from sysobjects where name='{HttpContext.Session.GetString("CurrentUserDepartment")}' and CONVERT(VARCHAR(11),crdate,106) =CONVERT(VARCHAR(11),getdate(),106)").FirstOrDefault();
                
                if (recordcount > 0)
                {
                    var dhcbody = connection.Query<HelperclassPname>($"select location,[Floorings-Walls- Windows-Ceilings-Glass Panel] as panel,Furnitures as f,[Water Tank/ Pump Room] as wt,[Swimming Pool / Area] as sw, [Equipment / Gym] as eg, [Floor / Lobby] as fl,[Drainage System] as ds, [A/C Grills] as acg, [Parking Area] as pa, [Auto Gate Barriers] as agb, [Roller Shutters] as rs, [Plants & Garden] as pg, [Staircases] as sc, [Corridors] as cor, [Lighting System] as ls,[HVAC Rooms] as hvr,[Transformer & Incoming Switch Room] as tisr,[Fire Alarm / Security System] as fass,[Fire Exits] as fe,[Fire Hose Reels, Cabinets & Extinguishers] as frce,[Lift Room] as lr,[Garbage Chutes] as gc,[Staff Accomodation] as sa,[Staff Presentation] as sp from ['{HttpContext.Session.GetString("CurrentUserDepartment")}']").ToList();
                }
                else
                {
                    var dhcbody = connection.Query<string>($"select pf_floors from propertyfloors where pf_pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id").ToList();
                }
                connection.Close();

                return Json(0);
            }
        }

        public string PaBtnDHCExport()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from sysobjects where name='{HttpContext.Session.GetString("CurrentUserDepartment")}' and CONVERT(VARCHAR(11),crdate,106) =CONVERT(VARCHAR(11),getdate(),106)").FirstOrDefault();
                if (recordcount > 0)
                {
                    var recordcount1 = connection.Query<int>($"select count(*) as rec from dhc where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and CONVERT(VARCHAR(11),dhc_submitted,106) =CONVERT(VARCHAR(11),getdate(),106)").FirstOrDefault();
                    if(recordcount1>0)
                    {
                        var delete = connection.Execute($"delete from dhc where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and CONVERT(VARCHAR(11),dhc_submitted,106) =CONVERT(VARCHAR(11),getdate(),106)");
                    }
                    var insert = connection.Execute($"insert into dhc(pname,dhc_submitted)values('{HttpContext.Session.GetString("CurrentUserDepartment")}',getdate())");
                    if(insert==1)
                    {
                        message = "Successfully Inserted";
                    }
                }
                connection.Close();
                return message;
            }
        }

        public string SaveDHC(string locations,string floorings,string furnitures,string watertanks,string swimmingpools,string gyms,string lobbys,string drains,string acs,string parkings,string autogates,string rollershutters,string gardens,string stairs,string corridors,string lights,string hvacs,string transformers,string firealarms,string fireexits,string firehoses,string liftrooms,string garbages,string staffaccomodations,string staffpresentations)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                List<string> location = locations.Split(',').ToList();
                List<string> flooring = floorings.Split(',').ToList();
                List<string> furniture = furnitures.Split(',').ToList();
                List<string> watertank = watertanks.Split(',').ToList();
                List<string> swimmingpool = swimmingpools.Split(',').ToList();
                List<string> gym = gyms.Split(',').ToList();
                List<string> lobby = lobbys.Split(',').ToList();
                List<string> drain = drains.Split(',').ToList();
                List<string> ac = acs.Split(',').ToList();
                List<string> parking = parkings.Split(',').ToList();
                List<string> autogate = autogates.Split(',').ToList();
                List<string> rollershutter = rollershutters.Split(',').ToList();
                List<string> garden = gardens.Split(',').ToList();
                List<string> stair = stairs.Split(',').ToList();
                List<string> corridor = corridors.Split(',').ToList();
                List<string> light = lights.Split(',').ToList();
                List<string> hvac = hvacs.Split(',').ToList();
                List<string> transformer = transformers.Split(',').ToList();
                List<string> firealarm = firealarms.Split(',').ToList();
                List<string> fireexit = fireexits.Split(',').ToList();
                List<string> firehose = firehoses.Split(',').ToList();
                List<string> liftroom = liftrooms.Split(',').ToList();
                List<string> garbage = garbages.Split(',').ToList();
                List<string> staffacc = staffaccomodations.Split(',').ToList();
                List<string> staffpre = staffpresentations.Split(',').ToList();
                connection.Open();
                var features = connection.Query<string>($"select pf_features from propertyfeatures where pf_mode='features' and pf_features<>'location' order by id").ToList();
                var reccount = connection.Query<int>($"select count(*) as rec from propertyfloors where pf_pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").FirstOrDefault();
                if(reccount == 0)
                {
                    message = "Upload the floor information";
                    goto gotoreturn;
                }

                var recordcount1 = connection.Query<int>($"select count(*) as rec from sysobjects where name='{HttpContext.Session.GetString("CurrentUserDepartment")}'").FirstOrDefault();
                if (recordcount1 > 0)
                {
                    var deletetable = connection.Execute($"drop table [{HttpContext.Session.GetString("CurrentUserDepartment")}]");
                }
                var createtable = connection.Execute($"create table [{HttpContext.Session.GetString("CurrentUserDepartment")}]  (location varchar(50))");

                
                foreach(var feature in features)
                {
                    var altertable = connection.Execute($"Alter table [{HttpContext.Session.GetString("CurrentUserDepartment")}]  add  [{feature}] varchar(250)");
                }

                List<int> insertlist = new();
                for (int i = 0; i < location.Count(); i++)
                {
                    var insert = connection.Execute($"insert into [{HttpContext.Session.GetString("CurrentUserDepartment")}](location,[{features[0]}],[{features[1]}],[{features[2]}],[{features[3]}],[{features[4]}],[{features[5]}],[{features[6]}],[{features[7]}],[{features[8]}],[{features[9]}],[{features[10]}],[{features[11]}],[{features[12]}],[{features[13]}],[{features[14]}],[{features[15]}],[{features[16]}],[{features[17]}],[{features[18]}],[{features[19]}],[{features[20]}],[{features[21]}],[{features[22]}],[{features[23]}]) values('{location[i]}','{flooring[i]}','{furniture[i]}','{watertank[i]}','{swimmingpool[i]}','{gym[i]}','{lobby[i]}','{drain[i]}','{ac[i]}','{parking[i]}','{autogate[i]}','{rollershutter[i]}','{garden[i]}','{stair[i]}','{corridor[i]}','{light[i]}','{hvac[i]}','{transformer[i]}','{firealarm[i]}','{fireexit[i]}','{firehose[i]}','{liftroom[i]}','{garbage[i]}','{staffacc[i]}','{staffpre[i]}')");
                    insertlist.Add(insert);
                }
                //for (int b = 1; b <= features.Count - 1; b++)
                //{
                //    for (int i = 0; i < locations.Count(); i++)
                //    {
                //        //var update = connection.Execute($"update [{HttpContext.Session.GetString("CurrentUserDepartment")}] set [{features[b]}] ='" & DataGridView1(b, i).Value & "' WHERE location='" & DataGridView1(0, i).Value & "'");
                //    }
                //}

                if (!insertlist.Contains(0))
                {
                    message = "Created Successfully";
                }
                gotoreturn:
                connection.Close();
                return message;
            }
        }

        public IActionResult PaProperties()
        {
            return View();
        }

        public IActionResult PaManagedProperty()
        {
            ManagedPropertyModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.ManagedProperties = connection.Query<ManagedProperty>(@$"select pm.BldgName as PropertyName, Location, count(*) as NoOfApartment, (select count(*) from propertymaster where PropertySource='ManagedProperty' and status='Available' and BldgName = pm.BldgName ) as NoOfApartmentVacant, (select count(*) from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable' and BldgName = pm.BldgName ) as NoOfApartmentOccupied
                                                                                from propertymaster pm 
                                                                                where PropertySource='ManagedProperty' 
                                                                                group by BldgName, Location 
                                                                                order by BldgName").ToList();
                
                model.TotalNoOfProperties = model.ManagedProperties.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public IActionResult PaReservedApartments()
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
                                                                                where propertysource='ManagedProperty' and reservation='yes' and bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by BldgName,id").ToList();//where condition bldgname
                
                model.TotalNoOfProperties = model.ReservedApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public IActionResult PaVacatingApartments()
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
                                                                                where propertysource='ManagedProperty' and vacatingstatus='Vacating' and bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by convert(datetime, moveout, 103) ASC").ToList();//where condition bldgname
                
                model.TotalNoOfProperties = model.VacatingApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public IActionResult PaAvailableApartments()
        {
            AvailableApartmentsModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.AvailableApartments = connection.Query<AvailableApartment>(@$"select id,cname as TenantName,reservedfor ,rnat,crent,reservedrent ,rftype ,rbtype ,
                                                                                CONVERT(VARCHAR(11),rlstart,106) as rlstart ,CONVERT(VARCHAR(11),rlend,106) as rlend ,
                                                                                rmob,PropertyRef,BldgName as PropertyName,BlockName,Floors as Floor,AptNo,Bed,Bath,Balcony,Furnished,Kitchen,Areasize,Security,Amount,cleaseno as LeaseNo,
                                                                                case when len(cleaseno)>1 then (select cast(payable as decimal(34,3)) as payable from lcinfo where lc_no=cleaseno) else cast(crent as decimal(34,3)) end as Payable,
                                                                                case when status='NotAvailable' and  reservation='yes' then 'Reserved' when status='Available' and reservation='yes' then 'Reserved' when  status is null and reservation='yes' then 'Reserved' when status='NotAvailable' and reservation='' then 'Occupied' when  status='NotAvailable' and reservation is null then 'Occupied' else status end as Status,
                                                                                cbtype,cftype as AptType,cname,case when crent is null then 0 else crent end as LCRent,
                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend ,106) as LeaseEnd,vacatingstatus as VacatingStatus,
                                                                                CONVERT(VARCHAR(11),moveout,106) as MoveOutOn 
                                                                                from propertymaster 
                                                                                where propertysource='ManagedProperty' and status='Available' and bldgname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and id in(select id from propertymaster where vacatingstatus not in('Vacating') or vacatingstatus is null) order by BldgName,id ").ToList();//where condition bldgname
                
                model.TotalNoOfProperties = model.AvailableApartments.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public IActionResult PaTLC()
        {
            TLCModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.TLCs = connection.Query<TLC>(@$"select pref,id,pname as PropertyName,aptno as AptNo,ftype as AptType,btype as Bed,lease_no as LeaseNo,
                                                                                tenantname as TenantName,rent as LCRent,nationality as Nationality,contact as ContactNo,
                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),leaseend,106) as LeaseEnd,
                                                                                CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
                                                                                CONVERT(VARCHAR(11),keyreturndate,106) as KeyReturnOn 
                                                                                from tenantshistory 
                                                                                where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'
                                                                                order by id desc").ToList();//where condition bldgname
                model.TotalNoOfProperties = model.TLCs.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public IActionResult PaSublease()
        {
            SubleaseModel model = new();

            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.Subleases = connection.Query<PMS_Admin_Web.Models.Property.Sublease>(@$"select cast(LCRENT as decimal(34,3)) as LCRent,NAME as TenantName,PNAME as PropertyName,APTNO as AptNo,
                                                                                CONVERT(VARCHAR(11),LSTART,106) as LeaseStart,
                                                                                CONVERT(VARCHAR(11),LEND,106) as LeaseEnd,
                                                                                (SELECT FLOORS FROM PROPERTYMASTER WHERE PROPERTYREF=PREF) AS Floor
                                                                                ,LNO as LeaseNo,FTYPE AS AptType,BED AS Bed,PREF 
                                                                                FROM SUBLEASES 
                                                                                WHERE ACTIVE=1 AND PNAME='{HttpContext.Session.GetString("CurrentUserDepartment")}'
                                                                                order by PNAME").ToList();//where condition bldgname


                model.TotalNoOfProperties = model.Subleases.Count();
                model.TotalNoOfVacant = connection.Query<int>(@"select count(*) as TotalNoOfVacant from propertymaster where PropertySource='ManagedProperty' and status='Available'").FirstOrDefault();//where condition bldgname
                model.TotalNoOfOccupied = connection.Query<int>(@"select count(*) as TotalNoOfOccupied from propertymaster where PropertySource='ManagedProperty' and status='NotAvailable'").FirstOrDefault();//where condition bldgname
                connection.Close();

            }

            return View(model);
        }

        public async Task<IActionResult> PaListPropertyApt(string buildingName)
        {
            List<ListPropertyAptModel> models = new();
            if (HttpContext.Session.GetString("CurrentUserDepartment") == buildingName)
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
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
                    models = propertymasters.ToList();
                    connection.Close();
                }
            }
            else
            {
                ViewBag.Message = "No Permission";
            }
            return View(models);
        }

        public async Task<IActionResult> UploadDocuments(UploadDocumentsModel uploadDocumentsModel, IFormFile photo)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                if (photo != null)
                {
                    if(uploadDocumentsModel.dt2 == null)
                    {
                        ViewBag.Message = "Select the Date";
                        goto gotoreturn;
                    }

                    if(uploadDocumentsModel.swimmingpool)
                    {
                        if(photo == null)
                        {
                            ViewBag.Message = "Attach the document for the Swimming Pool Report";
                            goto gotoreturn;
                        }

                        
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/swimmingpool");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        string fileName = HttpContext.Session.GetString("CurrentUserDepartment") + Convert.ToDateTime(uploadDocumentsModel.dt2).Month + Convert.ToDateTime(uploadDocumentsModel.dt2).Year /*+ photo.FileName*/ /*+ fileInfo.Extension*/;
                        string fileNameWithPath = Path.Combine(path, fileName);
                        
                        //if (System.IO.File.Exists(fileNameWithPath))
                        //{
                        //    string fileNameWithPathTemp = fileNameWithPath;
                        //    System.IO.File.Delete(fileNameWithPath);
                        //    fileNameWithPath = fileNameWithPathTemp;
                        //}
                        
                        var stream = new FileStream(fileNameWithPath, FileMode.Create);
                        photo.CopyTo(stream);
                        uploadDocumentsModel.swimmingpoolfile = fileNameWithPath;
                        
                        var recordcount = connection.Query<int>($"select count(*) from padocx where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and month='{Convert.ToDateTime(uploadDocumentsModel.dt2).Month}' and year='{Convert.ToDateTime(uploadDocumentsModel.dt2).Year}' ").FirstOrDefault();
                        if(recordcount > 0)
                        {
                            var delete = await connection.ExecuteAsync($"delete from padocx where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and month='{Convert.ToDateTime(uploadDocumentsModel.dt2).Month}' and year='{Convert.ToDateTime(uploadDocumentsModel.dt2).Year}'");
                            var insert = await connection.ExecuteAsync($"insert into padocx (pname,doc_type,doc_path,month,year,doc)values('{HttpContext.Session.GetString("CurrentUserDepartment")}','SP','{uploadDocumentsModel.swimmingpoolfile}','{Convert.ToDateTime(uploadDocumentsModel.dt2).Month}','{Convert.ToDateTime(uploadDocumentsModel.dt2).Year}',getdate())");
                        }
                        else
                        {
                            var insert = await connection.ExecuteAsync($"insert into padocx (pname,doc_type,doc_path,month,year,doc)values('{HttpContext.Session.GetString("CurrentUserDepartment")}','SP','{uploadDocumentsModel.swimmingpoolfile}','{Convert.ToDateTime(uploadDocumentsModel.dt2).Month}','{Convert.ToDateTime(uploadDocumentsModel.dt2).Year}',getdate())");
                        }

                        ViewBag.Message = "Document Uploaded Successfully";
                    }

                    
                }

                gotoreturn:
                var reccount = connection.Query<int>($"select count(*) from padocx where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and month=month(getdate()) and year=year(getdate())").FirstOrDefault();
                if(reccount > 0)
                {
                    uploadDocumentsModel.padocxes = connection.Query<Padocx>($"select doc_path DocPath from padocx where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' and month=month(getdate()) and year=year(getdate())").ToList();
                }
            }
            
            return View(uploadDocumentsModel);
        }

        public IActionResult PaLoginLogout()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                PALoginModel paLoginModel = new();
                connection.Open();
                paLoginModel.palogins = connection.Query<Palogin>($"select * from palogin where paname='{HttpContext.Session.GetString("CurrentUsername")}' order by id desc").ToList();
                connection.Close();
                return View(paLoginModel);
            }
        }

        public async Task<string> BtnLoginPaLoginLogout(string txtremarks)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) from PALOGIN where paname='{HttpContext.Session.GetString("CurrentUsername")}' and CONVERT(VARCHAR(11),LOGTIME,106) =CONVERT(VARCHAR(11),getdate(),106) and logmode='LOGIN'").FirstOrDefault();
                if (recordcount > 0)
                {
                    message = "There is Login already";
                }
                else
                {
                    var insertlogin = await connection.ExecuteAsync($"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,MODE,sysname)VALUES('{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','LOGIN',SYSDATETIME(),'{txtremarks}','PRESENT','{Environment.MachineName}')");
                    if(insertlogin==1)
                    {
                        message = "LOGIN DONE SUCCESSFULLY";
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

        public async Task<string> BtnLogoutPaLoginLogout1(string txtremarks)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var recordcount = connection.Query<int>($"select count(*) as rec from PALOGIN where paname='{HttpContext.Session.GetString("CurrentUsername")}' and CONVERT(VARCHAR(11),LOGTIME,106) =CONVERT(VARCHAR(11),getdate(),106) and logmode='LOGOUT'").FirstOrDefault();
                if(recordcount > 0)
                {
                    message = "LogOut is already done";
                }
                else
                {
                    var insertlogout = await connection.ExecuteAsync($"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,sysname)VALUES('{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','LOGOUT',SYSDATETIME(),'{txtremarks}','{Environment.MachineName}')");
                    if (insertlogout == 1)
                    {
                        message = "LOGOUT DONE SUCCESSFULLY";
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

        public async Task<string> BtnLogoutPaLoginLogout2(string txtremarks)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var delete = await connection.ExecuteAsync($"DELETE from PALOGIN where paname='{HttpContext.Session.GetString("CurrentUsername")}' and CONVERT(VARCHAR(11),LOGTIME,106) =CONVERT(VARCHAR(11),GETDATE(),106) and logmode='LOGOUT'");
                var insertlogout = await connection.ExecuteAsync($"INSERT INTO PALOGIN(PNAME,PANAME,LOGMODE,LOGTIME,remarks,sysname)VALUES('{HttpContext.Session.GetString("CurrentUserDepartment")}','{HttpContext.Session.GetString("CurrentUsername")}','LOGOUT',SYSDATETIME(),'{txtremarks}','{Environment.MachineName}')");
                if (insertlogout == 1)
                {
                    message = "LOGOUT DONE SUCCESSFULLY";
                }
                else
                {
                    message = "Unknown Error";
                }
                connection.Close();
                return message;
            }
        }

        public IActionResult BulkReceipt()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //List<Bulkprinting> model = new();
                var model = connection.Query<Bulkprinting>("select * from Bulkprinting").ToList();
                connection.Close();
                return View(model);
            }
        }

        public IActionResult PrintReceipt(int id)
        {
            PrintReceiptModel model = new();
            using (var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var bulkprinting = connection.Query<Bulkprinting>($"select * from Bulkprinting where id={id}").FirstOrDefault();
                model.bulkprinting = bulkprinting;
                connection.Close();
            }
            return View(model);
        }

        public IActionResult PrintingReceipt(PrintReceiptModel model)//printingreceipt
        {
            PrintReceiptModel model1 = new();
            model1 = model;
            model1.from = Convert.ToDateTime(model.bulkprinting.Fromperiod).ToShortDateString();
            model1.to = Convert.ToDateTime(model.bulkprinting.Toperiod).ToShortDateString();
            model1.date = Convert.ToDateTime(model.bulkprinting.Date).ToShortDateString();
            return View(model1);
        }

        public JsonResult BulkReceiptView(DateTime fromperiod)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var model = connection.Query<Bulkprinting>($"select * from Bulkprinting where MONTH(fromperiod)='{fromperiod.Month}' and YEAR(fromperiod)='{fromperiod.Year}' and pname='{HttpContext.Session.GetString("CurrentUserDepartment")}' ").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BulkReceipt100Records()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var model = connection.Query<Bulkprinting>($"select top(100) * from Bulkprinting where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
                return Json(model);
            }
        }

        public JsonResult BulkReceiptViewAll()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var model = connection.Query<Bulkprinting>($"select * from Bulkprinting where pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
                return Json(model);
            }
        }

        //public JsonResult BulkReceiptViewCancelled()
        //{
        //    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
        //    {
        //        connection.Open();
        //        var model = connection.Query<Bulkprinting>("select * from Bulkprinting").ToList();
        //        connection.Close();
        //        return Json(model);
        //    }
        //}

        public List<string> LoadSelectPropertyShiftProperty()
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var pc_subpname = connection.Query<string>($"SELECT pc_subpname from PropertyChange where pc_pname='{HttpContext.Session.GetString("CurrentUserDepartment")}'").ToList();
                connection.Close();
                return pc_subpname;
            }
        }

        public string BtnOkShiftProperty(string selectproperty)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                string message = "";
                connection.Open();
                var updatedept = connection.Execute($"update users set Department='{selectproperty}' where Usrname='{HttpContext.Session.GetString("CurrentUsername")}'");
                if(updatedept==1)
                {
                    message = "Property Shifted Successfully!";
                }
                else
                {
                    message = "Property Shift Failed";
                }
                connection.Close();
                return message;
            }
        }
    }

    

}
