using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.MovingInOut
{
    public class MovingInDetailsModel
    {
        public Propertymaster propertymaster { get; set; }
        public Movingin movingin { get; set; }
        public int varlid { get; set; }
        public string varref { get; set; }
    }
}
