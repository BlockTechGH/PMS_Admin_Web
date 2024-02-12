using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class ListLCModel
    {
        public List<Lcinfo> lcinfos { get; set; }
        public int rowcount { get; set; }
        public int cancelno { get; set; }
        public int appno { get; set; }
    }
}
