using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class PropertiesMenu_Model
    {

    }

    public class ManagedPropertyModel
    {
        public List<ManagedProperty> ManagedProperties { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public class ReservedApartmentsModel
    {
        public List<ReservedApartment> ReservedApartments { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public class VacatingApartmentsModel
    {
        public List<VacatingApartment> VacatingApartments { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public class AvailableApartmentsModel
    {
        public List<AvailableApartment> AvailableApartments { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public class TLCModel
    {
        public List<TLC> TLCs { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public class SubleaseModel
    {
        public List<Sublease> Subleases { get; set; }
        public int TotalNoOfProperties { get; set; }
        public int TotalNoOfVacant { get; set; }
        public int TotalNoOfOccupied { get; set; }
        public string SearchKeyword { get; set; }
        public string SearchMode { get; set; }
    }

    public enum EnumPropertyMode
    {
        PropertyName,
        Location,
        LCNo,
        TenantName,
        ReservedClientName

    }
}
