using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class UpdatePGLModel
    {
        public Pgl pgl { get; set; }
        public string refno { get; set; }
        public List<Actiondetail> actiondetails { get; set; }
        public List<DriverScheduel> driverScheduels { get; set; }
    }
}
