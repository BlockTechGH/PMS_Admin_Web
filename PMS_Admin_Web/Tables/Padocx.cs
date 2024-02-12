using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Padocx
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string DocType { get; set; }
        public string DocPath { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime? Doc { get; set; }
    }
}
