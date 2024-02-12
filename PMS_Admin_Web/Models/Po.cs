using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Po
    {
        public int Poid { get; set; }
        public DateTime? Podate { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Description { get; set; }
        public decimal? Amt { get; set; }
        public string Staff { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
