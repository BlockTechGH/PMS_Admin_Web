using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Pmesreq
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Cat { get; set; }
        public string Description { get; set; }
        public string Mapt { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Refno { get; set; }
        public decimal? Uprice { get; set; }
        public string Qty { get; set; }
        public decimal? Amt { get; set; }
        public DateTime? Doc { get; set; }
    }
}
