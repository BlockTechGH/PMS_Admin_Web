using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class ListRentChangeModel
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string AptNo { get; set; }
        public string LCNo { get; set; }
        public decimal OldRent { get; set; }
        public decimal NewRent { get; set; }
        public DateTime EffectFrom { get; set; }
        public DateTime Created { get; set; }
    }
}
