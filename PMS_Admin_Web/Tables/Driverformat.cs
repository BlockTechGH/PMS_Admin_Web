using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Driverformat
    {
        public int Id { get; set; }
        public string Ftime { get; set; }
        public string Timings { get; set; }
        public string Dname { get; set; }
        public string Lename { get; set; }
        public string Refno { get; set; }
        public string Cname { get; set; }
        public string Pname { get; set; }
        public int? Sdid { get; set; }
    }
}
