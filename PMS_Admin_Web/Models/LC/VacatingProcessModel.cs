using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class VacatingProcessModel
    {
        //public int Id { get; set; }
        //public string PropertyName { get; set; }
        //public string AptNo { get; set; }
        //public string PropRef { get; set; }
        //public string TenantName { get; set; }
        //public DateTime MoveOutDate { get; set; }
        //public string Remarks { get; set; }
        //public int MoveOutId { get; set; }
        //public DateTime KeyReturnDate { get; set; }
        //public string bmapproval { get; set; }
        //public string bmapproved { get; set; }

        public Moveout moveout { get; set; }
        public string TenantName { get; set; }
        public string PropertyName { get; set; }
        public string AptNo { get; set; }
        public string PropertyRef { get; set; }
        public DateTime MoveOutDate { get; set; }
        public string DateMoveOut { get; set; }
        public DateTime KeyReturnDate { get; set; }
        public string DateKeyReturn { get; set; }
        public string Remarks { get; set; }
        public string ForceClose { get; set; }

    }
}
