using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class PaVacatingListModel
    {
        public List<Propertymaster> vacatinglist { get; set; }
        public List<Tenantshistory> pendingvacatinglist { get; set; }
        public List<Tenantshistory> tlc { get; set; }
    }
}
