using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Periodexpensesentry
    {
        public int Id { get; set; }
        public int? Pmeid { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal Amt { get; set; }
        public DateTime Doc { get; set; }
        public int Gridnum { get; set; }
    }
}
