using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LOI
{
    public class NewLOIModel
    {
        public Loiinformation LOI { get; set; }
        //public List<Nationality> Nationalities { get; set; }
        public bool Sublease { get; set; }
        public bool MultipleApartment { get; set; }
        public decimal AptRent { get; set; }
        public string GroupAptno { get; set; }
        public string GroupPref { get; set; }
        //public bool Annually { get; set; }
        //public bool Semesterly { get; set; }
        //public bool Quarterly { get; set; }
        //public bool Monthly { get; set; }
        public string PaymentMethod { get; set; }
        public int NoOfApt { get; set; }//edit mode
        public List<string> GPrefList { get; set; }//edit mode
        public List<string> EmailLoiToList { get; set; }
        public string EmailLoiTo { get; set; }
        public List<string> EmailLoiCcList { get; set; }
        public string EmailLoiCc { get; set; }
    }
}
