using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LOI
{
    public class LOIModel
    {
        public List<Propertymaster> reservegrid { get; set; }
        public int asrchange { get; set; }
        public List<string> tenantrid { get; set; }
        public string tenantlbl { get; set; }
    }
}
