using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class MstrFaTreatment
    {
        public int? TreatId { get; set; }
        public string TreatName { get; set; }
        public string TreatArName { get; set; }
        public decimal? TreatCost { get; set; }
        public decimal? TreatPr1 { get; set; }
        public decimal? TreatPr2 { get; set; }
        public decimal? TreatPr3 { get; set; }
        public decimal? TreatDisc1 { get; set; }
        public decimal? TreatDisc2 { get; set; }
        public string TreatRemarks { get; set; }
    }
}
