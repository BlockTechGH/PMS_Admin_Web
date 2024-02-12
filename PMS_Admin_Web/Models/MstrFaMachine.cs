using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class MstrFaMachine
    {
        public int? MacId { get; set; }
        public string MacNumber { get; set; }
        public string MacName { get; set; }
        public DateTime? MacValid { get; set; }
        public decimal? MacValue { get; set; }
    }
}
