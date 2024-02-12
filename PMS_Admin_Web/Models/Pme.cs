using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Pme
    {
        public int Id { get; set; }
        public DateTime? Invdate { get; set; }
        public string Cat { get; set; }
        public string Workdesc { get; set; }
        public string Pname { get; set; }
        public string Mapt { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public double? Qty { get; set; }
        public string Invno { get; set; }
        public decimal? Amt { get; set; }
        public string Staff { get; set; }
        public DateTime? Doc { get; set; }
        public string Typeofpme { get; set; }
    }
}
