using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Pmf
    {
        public int Id { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Pname { get; set; }
        public decimal? Pmf1 { get; set; }
        public decimal? Pmfamt { get; set; }
        public decimal? Pmfdisc { get; set; }
        public string Pmfinv { get; set; }
        public DateTime? Doc { get; set; }
    }
}
