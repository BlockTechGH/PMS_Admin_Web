using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Driver
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public string Mob { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Doc { get; set; }
    }
}
