using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LOI
{
    public class ListLOIModel
    {
        public List<Loiinformation> loiinformations { get; set; }
        public int nocancelled { get; set; }
        public int noprogress { get; set; }
        public int noofrows { get; set; }
    }
}
