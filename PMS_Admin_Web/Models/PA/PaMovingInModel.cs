using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class PaMovingInModel
    {
        public int Id { get; set; }
        public string propertyref { get; set; }
        public string BldgName { get; set; }
        public string aptno { get; set; }
        public string rftype { get; set; }
        public string loino { get; set; }
        public string reservedfor { get; set; }
        public string rmob { get; set; }
        public string rstatus { get; set; }
        public string rlstart { get; set; }
        public DateTime indate { get; set; }
        public string rid { get; set; }
    }
}
