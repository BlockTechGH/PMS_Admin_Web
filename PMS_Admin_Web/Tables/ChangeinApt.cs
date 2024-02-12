using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class ChangeinApt
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Bldg { get; set; }
        public string Apt { get; set; }
        public DateTime Doc { get; set; }
        public string System { get; set; }
    }
}
