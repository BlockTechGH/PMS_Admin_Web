using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Note
    {
        public int Id { get; set; }
        public string Sec { get; set; }
        public string Statement { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Note1 { get; set; }
    }
}
