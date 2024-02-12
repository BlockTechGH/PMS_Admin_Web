using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Pgldetail
    {
        public int Id { get; set; }
        public string Pglno { get; set; }
        public string ContactName { get; set; }
        public decimal? Rent { get; set; }
        public string Rentby { get; set; }
        public string SecurityDepositby { get; set; }
        public string Agentfeeby { get; set; }
        public string Nationality { get; set; }
        public int? NoofOccupants { get; set; }
        public string SpecialInstructions { get; set; }
        public string Remarks { get; set; }
        public DateTime Doc { get; set; }
    }
}
