using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models;
//using PMS_Admin_Web.Tables;

namespace PMS_Admin_Web.Models.Users
{
    public class ListUserModel : PageModel
    {
        public List<PMS_Admin_Web.Models.User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
    }
}
