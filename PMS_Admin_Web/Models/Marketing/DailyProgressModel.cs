using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class DailyProgressModel
    {
        public string id { get; set; }
        public string lename { get; set; }
        public string scheduletype { get; set; }
        public string fromtime { get; set; }
        public string totime { get; set; }
        public string refno { get; set; }
        public string clientname { get; set; }
        public string remarks { get; set; }
        public string comments { get; set; }
        public string drivername { get; set; }
    }
}
