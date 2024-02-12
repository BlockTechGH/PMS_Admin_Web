using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Manalysis
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Pname { get; set; }
        public int? Orderno { get; set; }
        public string Locationgroup { get; set; }
    }
}
