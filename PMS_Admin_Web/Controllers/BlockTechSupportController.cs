using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PMS_Admin_Web.Controllers
{
    public class BlockTechSupportController : Controller
    {
        public IActionResult BTSupport()
        {
            return View();
        }
    }
}
