using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class EnquiryFormModel
    {
        public PglCgl pglCgl { get; set; }
    }

    public class PglCgl
    {
        public string EnquiryType { get; set; }
        public string EnquirySource { get; set; }
        public string ReferenceNo { get; set; }
        public string AllocatedTo { get; set; }
        public DateTime? InquiryDate { get; set; }
        public string ClientName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string Nationality1 { get; set; }
        public string Nationality2 { get; set; }
        public DateTime? MoveInDate { get; set; }
        public string PropertyType { get; set; }
        public int? MinBudget { get; set; }
        public int? MaxBudget { get; set; }
        public string BedroomRequirements { get; set; }
        public string Bathroom { get; set; }
        public string Furnished { get; set; }
        public string Garden { get; set; }
        public string Gym { get; set; }
        public string Balcony { get; set; }
        public string Pool { get; set; }
        public string Parking { get; set; }
        public string Internet { get; set; }
        public string SatelliteTv { get; set; }
        public string MaidRoom { get; set; }
        public string DriverRoom { get; set; }
        public string OtherInformation { get; set; }
        public string SelectedAreas { get; set; }
        public string SelectedBlocks { get; set; }
        public string ClientType { get; set; }
        public int? SquareMetres { get; set; }
        public string PropertySource { get; set; }
        public string OtherSource { get; set; }
    }
}
