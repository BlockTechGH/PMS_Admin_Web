using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Sublease
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public decimal? Rent { get; set; }
        public DateTime? Lstart { get; set; }
        public DateTime? Lend { get; set; }
        public string Pref { get; set; }
        public string Lno { get; set; }
        public string Slno { get; set; }
        public DateTime? Doc { get; set; }
        public int? Active { get; set; }
        public string Nationality { get; set; }
        public decimal? Lcrent { get; set; }
        public decimal? Actualrent { get; set; }
        public string Paymode { get; set; }
        public string Ftype { get; set; }
        public string Ttype { get; set; }
        public string Bed { get; set; }
        public string Status { get; set; }
    }
}
