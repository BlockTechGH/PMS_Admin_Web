using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using PMS_Admin_Web.Tables;

namespace PMS_Admin_Web.Models.Property
{
    public class PropertyAndAreaModel
    {
        public Propertymaster Property { get; set; }
        //public List<PropertyTable> propertyTables { get; set; }
        //public List<PropertyTable> propertyTables1 { get; set; } = new();
        public string Floors { get; set; }
    }

    public class PropertyTable
    {
        public string PropertyName { get; set; }
        public string Floor { get; set; }
        public string Type { get; set; }
        public string Bed { get; set; }
        public int AptNo { get; set; }
    }
}
