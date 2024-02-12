using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Asrchange
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Feature { get; set; }
        public string Old { get; set; }
        public string New { get; set; }
        public int? State { get; set; }
        public DateTime? Doc { get; set; }
        public string Changeby { get; set; }
        public string ViewByAdmin { get; set; }
        public string ViewBySpa { get; set; }
    }
}
