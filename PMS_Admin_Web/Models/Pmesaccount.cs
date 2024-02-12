using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Pmesaccount
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public DateTime? Invdate { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Aptno { get; set; }
        public string Invno { get; set; }
        public decimal? Totamt { get; set; }
        public string Stname { get; set; }
        public string Accstname { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public string Status { get; set; }
        public string Accstatus { get; set; }
    }
}
