using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LOI
{
    public class LOINoFixingModel
    {
        public List<Loiinformation> SelectLOINo { get; set; } = new();
        public string OldLOINo { get; set; }
        public string NewLOINo { get; set; }

    }
}
