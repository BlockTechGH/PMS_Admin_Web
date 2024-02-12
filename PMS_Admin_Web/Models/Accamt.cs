using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Accamt
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amt { get; set; }
        public string Dept { get; set; }
        public DateTime? Doc { get; set; }
    }
}
