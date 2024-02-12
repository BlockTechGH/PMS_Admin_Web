using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Bulkprinting
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public DateTime? Date { get; set; }
        public int? Rno { get; set; }
        public string Aptno { get; set; }
        public string Tname { get; set; }
        public DateTime? Fromperiod { get; set; }
        public DateTime? Toperiod { get; set; }
        public string Ptype { get; set; }
        public decimal? Mrent { get; set; }
        public string Mrentinwords { get; set; }
        public decimal? Prent { get; set; }
        public DateTime? Doc { get; set; }
    }
}
