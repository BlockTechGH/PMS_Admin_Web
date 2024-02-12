using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class EditPropertyModel
    {
        public Propertymaster propertymaster { get; set; }
        public List<Missue> missues { get; set; }
    }
}
