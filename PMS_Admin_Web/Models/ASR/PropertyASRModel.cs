using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.ASR
{
    public class PropertyASRModel
    {
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string CurrentDate { get; set; }
        public List<Propertymaster1> Command { get; set; }
        public List<string> Floors { get; set; }
        public int NoOfFlats { get; set; }
        public int NoOfFloors { get; set; }
        public List<string> Apts { get; set; }
        //public List<string> AlphaFloorCount { get; set; }
        public int AlphaAptCount { get; set; }
        //public string Gym { get; set; }
        //public string Swimmingpool { get; set; }
        public Propertymaster1 Amenities { get; set; }
        //public List<Propertymaster1> Command23 { get; set; }
        //public List<PMstatistics> Reserved { get; set; }
        //public List<PMstatistics> Rent { get; set; }
        //public List<PMstatistics> Vacant { get; set; }
        public APTSTATUS BDR2 { get; set; }
        public APTSTATUS BDR3 { get; set; }
        public APTSTATUS SHOP { get; set; }
        public APTSTATUS STORE1 { get; set; }
        public APTSTATUS STORE2 { get; set; }
    }

    public class Propertymaster1 : Propertymaster
    {
        public decimal? rent { get; set; }
        public string name { get; set; }
        public decimal? PR { get; set; }
        public string PM { get; set; }
        public string lstart { get; set; }
        public string LEND { get; set; }


        public string APTSTATUS { get; set; }
        public int NO { get; set; }
        public string reservation { get; set; }
        public string BED { get; set; }
        public string vacatingstatus { get; set; }
    }

    public class PMstatistics
    {
        public string APTSTATUS { get; set; }
        public int FF { get; set; }
        public int SF { get; set; }
        public int UF { get; set; }
    }

    public class APTSTATUS
    {
        public PMstatistics Reserved { get; set; }
        public PMstatistics Rented { get; set; }
        public PMstatistics Vacated { get; set; }
    }
}
