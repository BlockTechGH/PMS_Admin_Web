using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PMS_Admin_Web.Tables;

namespace PMS_Admin_Web.Models.Property
{
    public class ListPropertiesModel : PageModel
    {
        public List<Propertymaster> Properties { get; set; }
    }
}
