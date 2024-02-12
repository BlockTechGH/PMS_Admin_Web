using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Autologout
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Dept { get; set; }
        public TimeSpan Hours { get; set; }
    }
}
