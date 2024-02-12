using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Adminactivity
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Fromtime { get; set; }
        public string Totime { get; set; }
        public TimeSpan? Ft { get; set; }
        public TimeSpan? Tt { get; set; }
        public string Reason { get; set; }
        public string OtherReason { get; set; }
        public string Advertising { get; set; }
        public string Username { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
