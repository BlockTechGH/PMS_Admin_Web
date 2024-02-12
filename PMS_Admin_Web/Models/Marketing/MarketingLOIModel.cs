using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class MarketingLOIModel
    {
        public Loiinformation LOI { get; set; }
        public string statuslbl { get; set; }
        //public string loisigned { get; set; }
        public decimal rentpaid { get; set; }
        public decimal deppaid { get; set; }
        public decimal resfpaid { get; set; }
    }
}
