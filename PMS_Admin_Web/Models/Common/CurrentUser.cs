using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models
{
    public class CurrentUser
    {
        public string Username { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}
