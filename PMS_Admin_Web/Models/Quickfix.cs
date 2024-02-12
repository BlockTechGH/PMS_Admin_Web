using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Quickfix
    {
        public int Id { get; set; }
        public string Qtype { get; set; }
        public string Qpname { get; set; }
        public string Qaptno { get; set; }
        public string Qsysname { get; set; }
        public DateTime? Doc { get; set; }
    }
}
