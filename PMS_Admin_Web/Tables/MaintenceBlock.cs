using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class MaintenceBlock
    {
        public int Id { get; set; }
        public string Pref { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public DateTime? Doc { get; set; }
        public string Reason { get; set; }
    }
}
