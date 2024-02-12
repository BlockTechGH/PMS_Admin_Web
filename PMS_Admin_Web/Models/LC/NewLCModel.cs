using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class NewLCModel
    {
        public Lcinfo LcInfo { get; set; }
        //public List<Nationality> Nationalities { get; set; }
        public List<string> PropertyNames { get; set; }
        public bool Sublease { get; set; }
        public bool MultipleApartment { get; set; }
        public decimal? AptRent { get; set; }
        public string GroupAptno { get; set; }
        public string GroupPref { get; set; }
        //public bool Annually { get; set; }
        //public bool Semesterly { get; set; }
        //public bool Quarterly { get; set; }
        //public bool Monthly { get; set; }
        public string PaymentMethod { get; set; }
        public string filetxt { get; set; }
        public string UpdatedRenewalLC { get; set; }
        public int NoOfApt { get; set; }//edit mode
        public string  LCNo { get; set; }//edit mode
        public List<string> GPrefList { get; set; }//edit mode
        public string oldCivilId { get; set; }//edit mode
        public string renewlcno { get; set; }//renewal LC
        public DateTime? renewls { get; set; }//renewal LC
        public DateTime? renewle { get; set; }//renewal LC
    }

}
