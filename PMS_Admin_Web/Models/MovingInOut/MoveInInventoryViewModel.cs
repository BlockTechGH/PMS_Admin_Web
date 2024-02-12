using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.MovingInOut
{
    public class MoveInInventoryViewModel
    {
        public string PropertyRef { get; set; }
        public string PropertyName { get; set; }
        public string AptNo { get; set; }
        public string Type { get; set; }
        public string LeaseNo { get; set; }
        public string TenantName { get; set; }
        public string ContactNo { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public string Status { get; set; }
        public string MoveInDate { get; set; }
        public string rid { get; set; }

    }
}
