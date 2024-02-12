using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Vme
    {
        public int Id { get; set; }
        public DateTime? Invdate { get; set; }
        public string Attstaff { get; set; }
        public string Vehiclename { get; set; }
        public string Workdesc { get; set; }
        public int? Qty { get; set; }
        public string Invno { get; set; }
        public string Refno { get; set; }
        public decimal? Amt { get; set; }
        public DateTime? Doc { get; set; }
        public string St { get; set; }
        public string Exp { get; set; }
        public int? Stid { get; set; }
        public string Type { get; set; }
        public DateTime? Smonth { get; set; }
        public string Vno { get; set; }
        public string Drivername { get; set; }
        public string Odometer { get; set; }
    }
}
