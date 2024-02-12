using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Usrname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int? Logmode { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
