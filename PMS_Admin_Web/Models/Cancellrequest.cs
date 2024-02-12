using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Cancellrequest
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Issue { get; set; }
        public string Reason { get; set; }
        public DateTime? Cdate { get; set; }
    }
}
