using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Pgl
    {
        public int Id { get; set; }
        public string Enquirytype { get; set; }
        public string Pglrefno { get; set; }
        public string ClientName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int? Phone { get; set; }
        public string Nationality { get; set; }
        public string Nationality2 { get; set; }
        public string PropertySource { get; set; }
        public string Propertytype { get; set; }
        public decimal? MinBudget { get; set; }
        public decimal? Maxbudget { get; set; }
        public string Bed { get; set; }
        public int? Bath { get; set; }
        public string Furnished { get; set; }
        public string Pool { get; set; }
        public string Tv { get; set; }
        public string Maidroom { get; set; }
        public string Driverroom { get; set; }
        public string Internet { get; set; }
        public string Gym { get; set; }
        public string Parking { get; set; }
        public string Garden { get; set; }
        public string Balcony { get; set; }
        public int? Sqmtrs { get; set; }
        public string Interest { get; set; }
        public string Block { get; set; }
        public DateTime? Movingdate { get; set; }
        public string Completedby { get; set; }
        public string Source { get; set; }
        public string Othersource { get; set; }
        public string OtherInfo { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? EnquiryDate { get; set; }
        public DateTime? Updatedon { get; set; }
        public string SpecialInstructions { get; set; }
        public string Remarks { get; set; }
        public string InquiryStatus { get; set; }
        public string ReasonofCancellation { get; set; }
        public DateTime? DateofCancel { get; set; }
        public string Cancellationstatus { get; set; }
        public string ReasontocancelLe { get; set; }
        public DateTime? DateofCancelLe { get; set; }
        public string Wasinactive { get; set; }
    }
}
