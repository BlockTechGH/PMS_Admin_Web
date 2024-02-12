using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.MovingInOut
{
    public class NewVacatingModel
    {
        public Moveout moveout { get; set; }
        public DateTime moveoutDate { get; set; } = new();
        public string moveoutRemark { get; set; }
        public DateTime rleasestart { get; set; } = new();
        public DateTime rleaseend { get; set; } = new();
        //public List<PropertyList> propertyLists { get; set; } = new();
        //public List<Propertymaster> aptnos { get; set; } = new();
    }
}
