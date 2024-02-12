using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Tname { get; set; }
        public string Aptno { get; set; }
        public decimal? Rent { get; set; }
        public string Bed { get; set; }
        public string Type { get; set; }
        public DateTime? Lstart { get; set; }
        public DateTime? Lend { get; set; }
        public DateTime? Mout { get; set; }
        public DateTime? Doc { get; set; }
    }
}
