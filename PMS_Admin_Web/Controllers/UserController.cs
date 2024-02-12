using Microsoft.AspNetCore.Mvc;
using PMS_Admin_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PMS_Admin_Web.Tables;
using PMS_Admin_Web.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;
using X.PagedList;
using PMS_Admin_Web.Repository;
using PMS_Admin_Web.Enum;

namespace PMS_Admin_Web.Controllers
{
    public class UserController : Controller
    {
        //private q8RealtorContext context = new q8RealtorContext();
        private RealtorContext context = new RealtorContext();
        private Connection sqlConnectionString = new();

        public UserController(RealtorContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("createuser started");
            return View();
            //return View("CreateUser", new CreateUserModel());
        }

        //[HttpGet]
        public IActionResult EditUser(int Id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                //var user = context.Users.Find(Id);
                var user = connection.Query<Models.User>(@$"select * from users where id={Id}").FirstOrDefault();
                var userModel = new Models.Users.CreateUserModel();
                userModel.Id = user.Id;
                userModel.Username = user.Usrname;
                userModel.Password = user.Password;
                userModel.Department = user.Department;
                userModel.Role = user.Role;
                userModel.Designation = user.Designation;
                return View("CreateUser", userModel);
            }
                
        }

        //[HttpPost]
        public async Task<ActionResult> CreateUser(Models.Users.CreateUserModel createUserModel)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("createuser started");
            try
            {
                if (createUserModel.Password != null)
                {
                    // No id so we add it to database
                    if (createUserModel.Id <= 0)
                    {
                        int IsExist;
                        using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                        {
                            connection.Open();
                            if(createUserModel.Role == "PA")
                                IsExist = connection.Query<int>($"SELECT COUNT(1) FROM users WHERE Usrname = '{createUserModel.PaUsername}'").FirstOrDefault();
                            else
                                IsExist = connection.Query<int>($"SELECT COUNT(1) FROM users WHERE Usrname = '{createUserModel.Username}'").FirstOrDefault();
                            connection.Close();
                        }
                        if (IsExist == 0)
                        {
                            Models.User user = new Models.User();
                            if(createUserModel.Role == "PA")
                            {
                                user.Usrname = createUserModel.PaUsername;
                                user.Department = createUserModel.PaDepartment;
                            }
                            else
                            {
                                user.Usrname = createUserModel.Username;
                                user.Department = createUserModel.Department;
                            }
                                
                            user.Password = createUserModel.Password;
                            
                            user.Role = createUserModel.Role;
                            user.Designation = createUserModel.Designation;

                            context.Users.Add(user);
                            await context.SaveChangesAsync();
                            ModelState.Clear();
                            //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Created User");
                            ViewBag.Message = "Created User";
                        }
                        else
                        {
                            //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Username Exist");
                            ViewBag.Message = "Username Exist";
                        }
                            

                    }
                    // Has Id, therefore it's in database so we update
                    else
                    {
                        //context.Entry(createUserModel).State = EntityState.Modified;
                        Models.User user = new Models.User();
                        user.Id = createUserModel.Id;
                        user.Usrname = createUserModel.Username;
                        if(createUserModel.Password == null)
                        {
                            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                            {
                                connection.Open();
                                user.Password = connection.Query<string>($"select Password from users where id = {createUserModel.Id}").FirstOrDefault();
                                connection.Close();
                            }
                        }
                        else
                            user.Password = createUserModel.Password;

                        user.Department = createUserModel.Department;
                        user.Role = createUserModel.Role;
                        user.Designation = createUserModel.Designation;

                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                        ModelState.Clear();
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Updated User");
                        ViewBag.Message = "Updated User";
                    }
                    
                    
                    //return View("CreateUser", createUserModel);
                    //return RedirectToAction("ListUser");
                    //return RedirectToAction("Index");
                }
                return View(new Models.Users.CreateUserModel());
            }
            catch(Exception ex)
            {
                //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown Error");
                ViewBag.Message = "Unknown Error";
                logger.Info(ex);
                return View();
            }

            
            //return View("CreateUser");
            //return View();

        }

        //[HttpGet]
        public IActionResult ListUser(/*int page = 1, int pageSize = 10*/)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("listuser started");
            List<PMS_Admin_Web.Models.User> user = new();
            try
            {
                user = context.Users.ToList();
                //using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                //{
                //    connection.Open();
                //    user=connection.Query<User>
                //    connection.Close();
                //}
            }
            catch(Exception e)
            {
                logger.Info(e);
            }
            
            //PagedList<PMS_Admin_Web.Models.User> model = new PagedList<PMS_Admin_Web.Models.User>(user, page, pageSize);
            return View(user);
        }
        
        //[HttpGet]
        public string DeleteUser(int Id)
        {
            try
            {
                var deleteUser = context.Users.Find(Id);
                //deleteUser.IsDeleted = true;
                //context.Users.Update(deleteUser);
                context.Users.Remove(deleteUser);
                context.SaveChangesAsync();
                ViewBag.Message = "Successfully Deleted";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }
              
            return ViewBag.Message;
        }

        public string CreatePA(string paname,string empcode)
        {
            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var exist = connection.Query<int>($"select count(1) from PAlist where pa_name='{paname}' and pa_empcode='{empcode}'").FirstOrDefault();
                    if(exist==0)
                    {
                        connection.Execute($"insert into PAlist(pa_name,pa_empcode) values('{paname}','{empcode}')");
                        ViewBag.Message = "PA Created";
                    }
                    else
                    {
                        ViewBag.Message = "Already exists";
                    }
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }
            return ViewBag.Message;
        }

        public List<Palist> LoadPAs()
        {
            var PAs = context.Palists.ToList();
            return PAs;
        }

        public List<string> LoadPADept()
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var paDept = connection.Query<string>("select distinct bldgname from propertymaster where propertysource='Managedproperty'").ToList();
                connection.Close();
                return paDept;
            }
        }

        public async Task<List<PMS_Admin_Web.Models.User>> SearchUser(string searchUsername)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var userResult = await connection.QueryAsync<PMS_Admin_Web.Models.User>($"select * from users where Usrname like '%{searchUsername}%'");
                connection.Close();
                return userResult.ToList();
            }
        }
    }
}
