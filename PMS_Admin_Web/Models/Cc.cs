using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Cc
    {
        public int Id { get; set; }
        public string Ccname { get; set; }
        public string Cname { get; set; }
        public string Cmobile { get; set; }
        public string Cdesg { get; set; }
        public DateTime? Cdate { get; set; }
        public string Appointment { get; set; }
        public DateTime? Appdate { get; set; }
        public DateTime? Fdat { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public string Lename { get; set; }
        public string Remarks { get; set; }
    }
}
