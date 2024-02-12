using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Rented
    {
        public int Id { get; set; }
        public string Pref { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public DateTime? Movein { get; set; }
        public DateTime? Lcdate { get; set; }
        public string Pstatus { get; set; }
        public DateTime? Pdate { get; set; }
        public string Rstatus { get; set; }
        public DateTime? Rdate { get; set; }
        public string Remarks { get; set; }
        public string Highlighting { get; set; }
        public string Note { get; set; }
        public DateTime? Doc { get; set; }
        public string Newlyrented { get; set; }
        public int? Tid { get; set; }
    }
}
