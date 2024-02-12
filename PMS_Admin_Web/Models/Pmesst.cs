using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Pmesst
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
        public string Uprice { get; set; }
        public string Qty { get; set; }
        public decimal? Amt { get; set; }
        public string No { get; set; }
        public string Exp { get; set; }
        public DateTime? Doc { get; set; }
        public int? Stid { get; set; }
        public string Invno { get; set; }
        public DateTime? Smonth { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Exptype { get; set; }
        public string Accounts { get; set; }
    }
}
