using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Log
    {
        public int? Id { get; set; }
        public string Loginandout { get; set; }
        public DateTime? Time { get; set; }
        public string Module { get; set; }
        public string Action { get; set; }
        public string Name { get; set; }
    }
}
