using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class ViewEnquiryModel
    {
        public List<Pgl> pgls { get; set; }
        public List<Cgl> cgls { get; set; }
        public string SearchKeyword { get; set; }
    }
}
