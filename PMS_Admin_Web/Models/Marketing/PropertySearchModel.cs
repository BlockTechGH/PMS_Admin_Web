using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Marketing
{
    public class PropertySearchModel
    {
        public List<PropertySearchList> propertySearchLists { get; set; }
        public List<Propertymaster> propertymasters { get; set; }
        //public string propertytype { get; set; }
    }

    public class PropertySearchList
    {
        public int Vacant { get; set; }
        public int Occupied { get; set; }
        public int Reserved { get; set; }
        public string BuildingName { get; set; }
        public string Location { get; set; }
        public string PropertySource { get; set; }
    }
}
