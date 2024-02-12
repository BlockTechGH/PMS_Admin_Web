using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class PropertyModel
    {
        public Propertymaster Property { get; set; }
        public string propref { get; set; }
        public string aptno { get; set; }
        public string floors { get; set; }
        public string status { get; set; }
        public string propertytypes { get; set; }//marketing
        public string bedrooms { get; set; }//marketing
        public string rents { get; set; }//marketing
    }
}
