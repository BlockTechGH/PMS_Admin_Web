using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Receipt
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Tname { get; set; }
        public decimal? Amt { get; set; }
        public string Rno { get; set; }
        public int? Sno { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? Doc { get; set; }
        public string Mode { get; set; }
        public string State { get; set; }
    }
}
