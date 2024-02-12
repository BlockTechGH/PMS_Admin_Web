using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Fscheduleattender
    {
        public int? Id { get; set; }
        public int? Sid { get; set; }
        public string Attender { get; set; }
        public TimeSpan? Fromtime { get; set; }
        public TimeSpan? Totime { get; set; }
    }
}
