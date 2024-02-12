using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Templogin
    {
        public DateTime TempDate { get; set; }
        public string TempEmpNr { get; set; }
        public string TempEmpName { get; set; }
        public TimeSpan? TempStart { get; set; }
        public TimeSpan? TempIn { get; set; }
        public TimeSpan? TempOut { get; set; }
        public int? TempLates { get; set; }
        public string TempRemarks { get; set; }
    }
}
