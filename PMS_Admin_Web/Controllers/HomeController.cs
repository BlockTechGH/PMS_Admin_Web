using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMS_Admin_Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("CurrentUsername") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }

        public IActionResult PAIndex()
        {
            if (HttpContext.Session.GetString("CurrentUsername") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }

        public IActionResult MarketingAdmin()
        {
            if (HttpContext.Session.GetString("CurrentUsername") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }

        public IActionResult MarketingLeasingExecutive()
        {
            if (HttpContext.Session.GetString("CurrentUsername") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
