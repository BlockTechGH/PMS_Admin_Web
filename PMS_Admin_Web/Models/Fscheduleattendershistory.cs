using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Fscheduleattendershistory
    {
        public int Attid { get; set; }
        public int Scdlid { get; set; }
        public TimeSpan? Fromt { get; set; }
        public TimeSpan? Tot { get; set; }
        public int? Historyid { get; set; }
    }
}
