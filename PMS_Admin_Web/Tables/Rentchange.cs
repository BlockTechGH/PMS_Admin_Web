using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Rentchange
    {
        public int Id { get; set; }
        public string Lcno { get; set; }
        public string Pref { get; set; }
        public decimal? Oldrent { get; set; }
        public decimal? Newrent { get; set; }
        public DateTime? Effectfrom { get; set; }
        public string Remarks { get; set; }
        public string Approvednotice { get; set; }
        public DateTime? Doc { get; set; }
        public int? Rentchangeid { get; set; }
    }
}
