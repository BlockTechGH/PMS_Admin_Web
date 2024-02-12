using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class WalkinClientInquiryModel
    {
        public Walkininquiry walkininquiry { get; set; }
        public List<Walkininquiry> walkininquiries { get; set; }
        public DateTime? date { get; set; }
        //public DateTime? searchDate { get; set; }
        public string enquiryType { get; set; }
    }
}
