using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class MarketingLOIUpdateModel
    {
        public Loiinformation LOI { get; set; }
        public string statuslbl { get; set; }
        //public string loisigned { get; set; }
        public decimal rentpaid { get; set; }
        public decimal newrent { get; set; }
        public decimal totrentpaid { get; set; }
        public decimal deppaid { get; set; }
        public decimal newdeppaid { get; set; }
        public decimal totdeppaid { get; set; }
        public decimal resfpaid { get; set; }
        public decimal newresfpaid { get; set; }
        public decimal totresfpaid { get; set; }
    }
}
