using Microsoft.AspNetCore.Mvc;
using PMS_Admin_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
//using PMS_Admin_Web.Tables;
using System.Data.SqlClient;
using System.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PMS_Admin_Web.Controllers
{
    public class AuthController : Controller
    {
        //LoginDAL loginDAL = new();
        //q8RealtorContext context = new();
        private Connection sqlConnectionString = new();
        public CurrentUser currentUser = new CurrentUser();

        //public AuthController(q8RealtorContext q8RealtorContext)
        //{
        //    context = q8RealtorContext;
        //}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            logger.Info("inside Login()");
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                if (HttpContext.Session.GetString("CurrentUsername") != null)
                {
                    goto gotohome;
                }
                int result = -1;
                if(loginViewModel.Department == "PA")
                {
                    var res = await connection.QueryAsync<int>(@$"SELECT COUNT(1) FROM users WHERE Usrname = '{loginViewModel.Uname}' AND Password='{loginViewModel.Pwd}' and Role='{loginViewModel.Department}'");
                    result = res.FirstOrDefault();
                }
                else
                {
                    try
                    {
                        //logger.Info("sqlConnectionString.ConnectionString>>" + sqlConnectionString.ConnectionString);

                        var res = await connection.QueryAsync<int>(@$"SELECT COUNT(1) FROM users WHERE Usrname = '{loginViewModel.Uname}' AND Password='{loginViewModel.Pwd}' and Department='{loginViewModel.Department}'");
                        result = res.FirstOrDefault();

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                   
                }
                if (result == 1)
                {
                    connection.Open();
                    var cUser = connection.Query<CurrentUser>(@$"select Usrname as Username,Department,Role from users where Usrname='{loginViewModel.Uname}' AND Password='{loginViewModel.Pwd}'").FirstOrDefault();
                    connection.Close();
                    HttpContext.Session.SetString("CurrentUsername", cUser.Username);
                    HttpContext.Session.SetString("CurrentUserDepartment", cUser.Department);
                    HttpContext.Session.SetString("CurrentUserRole", cUser.Role);
                }
                else
                {
                    TempData["msg"] = "Username or Password or Department is incorrect!";
                    goto gotoview;
                }

                gotohome:
                if (HttpContext.Session.GetString("CurrentUsername") == "btsupport")
                {
                    return RedirectToAction("BTSupport", "BlockTechSupport");
                }
                if (HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    return RedirectToAction("PAIndex", "Home");
                }
                else if (HttpContext.Session.GetString("CurrentUserDepartment") == "Marketing")
                {
                    if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator" && HttpContext.Session.GetString("CurrentUsername") == "Admin")
                    {
                        //if(HttpContext.Session.GetString("CurrentUsername") == "Admin")
                        //{
                        //      return RedirectToAction("MarketingAdmin", "Home");
                        //}

                        //have to do show reminder
                        return RedirectToAction("MarketingAdmin", "Home");
                    }
                    else if (HttpContext.Session.GetString("CurrentUserRole") == "BDO" && HttpContext.Session.GetString("CurrentUsername") == "BDO")
                    {
                        //if(HttpContext.Session.GetString("CurrentUsername") == "BDO")
                        //{
                        //      return RedirectToAction("MarketingAdmin", "Home");
                        //}
                        return RedirectToAction("MarketingAdmin", "Home");
                    }
                    //if (HttpContext.Session.GetString("CurrentUserRole") == "Administrator")
                    //{
                    //    //if(HttpContext.Session.GetString("CurrentUsername") == "Admin")
                    //    //{
                    //    //      return RedirectToAction("MarketingAdmin", "Home");
                    //    //}
                    //    return RedirectToAction("MarketingAdmin", "Home");
                    //}
                    else
                    {
                        //have to do show reminder
                        return RedirectToAction("MarketingLeasingExecutive", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }

            gotoview:
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUsername");
            HttpContext.Session.Remove("CurrentUserDepartment");
            HttpContext.Session.Remove("CurrentUserRole");

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
