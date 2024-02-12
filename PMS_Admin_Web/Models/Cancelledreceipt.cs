using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Cancelledreceipt
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Voucherno { get; set; }
        public string Status { get; set; }
    }
}
