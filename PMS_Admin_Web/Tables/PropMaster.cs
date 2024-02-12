using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class PropMaster
    {
        public int Id { get; set; }
        public string PropertyRefNo { get; set; }
        public string Propertytype { get; set; }
        public string Propname { get; set; }
        public string Area { get; set; }
        public string Block { get; set; }
        public string Location { get; set; }
        public string Bed { get; set; }
        public int Sqmt { get; set; }
        public string Furnished { get; set; }
        public string Kitchen { get; set; }
        public string Cctv { get; set; }
        public string Pool { get; set; }
        public string Gym { get; set; }
        public string Parking { get; set; }
        public string Garden { get; set; }
        public string Balcony { get; set; }
        public string Internet { get; set; }
        public string Osn { get; set; }
        public int? Days { get; set; }
        public decimal? Chargeinkd { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public decimal? Rent { get; set; }
        public string Poc { get; set; }
        public int? Sqmtrs { get; set; }
        public string Facilities { get; set; }
        public decimal? Ppersqmtr { get; set; }
        public string Source { get; set; }
        public string Visitedby { get; set; }
        public string Status { get; set; }
        public DateTime? Dateofavailability { get; set; }
        public DateTime? Updatedon { get; set; }
        public string Updatedby { get; set; }
        public string Eshot { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public byte[] Image { get; set; }
    }
}
