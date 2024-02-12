using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Periodexpense
    {
        public int Id { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal Amt { get; set; }
        public DateTime Doc { get; set; }
        public int Gridnum { get; set; }
    }
}
