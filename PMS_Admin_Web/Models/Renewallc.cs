using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Renewallc
    {
        public int? Id { get; set; }
        public string Leaseno { get; set; }
        public DateTime? Leasestart { get; set; }
        public DateTime? Leaseend { get; set; }
        public DateTime? Doc { get; set; }
        public int Myid { get; set; }
    }
}
