using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class ManagedProperty
    {
        public string PropertyName { get; set; }
        public string Location { get; set; }
        public int NoOfApartment { get; set; }
        public int NoOfApartmentVacant { get; set; }
        public int NoOfApartmentOccupied { get; set; }
    }

    public class ReservedApartment
    {
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string AptNo { get; set; }
        public string Status { get; set; }
        public string LCType { get; set; }
        public string LOIorLCNo { get; set; }
        public string ReservedUnder { get; set; }
        public string LCRent { get; set; }
        public string Payable { get; set; }
        public string AptType { get; set; }
        public string Bed { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }
        public string VacatingStatus { get; set; }
        public string MoveOutOn { get; set; }
        public string ViewHistory { get; set; }
        public string TenantCurrentlyStaying { get; set; }
    }

    public class VacatingApartment
    {
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string AptNo { get; set; }
        public string Status { get; set; }
        public string LeaseNo { get; set; }
        public string TenantName { get; set; }
        public string LCRent { get; set; }
        public string Payable { get; set; }
        public string AptType { get; set; }
        public string Bed { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }
        public string VacatingStatus { get; set; }
        public string MoveOutOn { get; set; }
        public string ViewHistory { get; set; }
        public string Reserved { get; set; }
    }

    public class AvailableApartment
    {
        public int id { get; set; }
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string AptNo { get; set; }
        public string Status { get; set; }
        public string LeaseNo { get; set; }
        public string TenantName { get; set; }
        public string LCRent { get; set; }
        public string Payable { get; set; }
        public string AptType { get; set; }
        public string Bed { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }
        public string VacatingStatus { get; set; }
        public string MoveOutOn { get; set; }
        public string ViewHistory { get; set; }
        //public string Reserved { get; set; }
    }

    public class TLC
    {
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string AptNo { get; set; }
        public string AptType { get; set; }
        public string Bed { get; set; }
        public string TenantName { get; set; }
        public string Nationality { get; set; }
        public string ContactNo { get; set; }
        public string LCRent { get; set; }
        public string LeaseNo { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }
        public string MoveOutOn { get; set; }
        public string KeyReturnOn { get; set; }
    }

    public class Sublease
    {
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string AptNo { get; set; }
        public string Status { get; set; }
        public string LeaseNo { get; set; }
        public string TenantName { get; set; }
        public string LCRent { get; set; }
        public string AptType { get; set; }
        public string Bed { get; set; }
        public string LeaseStart { get; set; }
        public string LeaseEnd { get; set; }
    }
}
