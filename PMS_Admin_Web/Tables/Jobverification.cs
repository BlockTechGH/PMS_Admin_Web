using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Jobverification
    {
        public int Id { get; set; }
        public int Jobissueid { get; set; }
        public string Jobdesc { get; set; }
        public string Llreport { get; set; }
        public DateTime Doc { get; set; }
        public string Fca { get; set; }
        public string Pname { get; set; }
        public string Apt { get; set; }
        public DateTime? Dou { get; set; }
        public DateTime? Rptdate { get; set; }
    }
}
