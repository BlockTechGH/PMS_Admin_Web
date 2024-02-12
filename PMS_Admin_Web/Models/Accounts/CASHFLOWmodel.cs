using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class CASHFLOWmodel
    {
        public string totrenttxt { get; set; }
        public string occtxt { get; set; }
        public string pmftxt { get; set; }
        public string mexptxt { get; set; }
        public string othrexptxt { get; set; }
        public string totalrenttxt { get; set; }
        public string totalexptxt { get; set; }
        public string totexppertxt { get; set; }
        public string gtotaltxt { get; set; }
        public string cbexptxt { get; set; }
        public List<categorygridmodel> categorygrid { get; set; }
    }

    public class categorygridmodel
    {
        public string amt { get; set; }
        public string cat { get; set; }
    }
}
