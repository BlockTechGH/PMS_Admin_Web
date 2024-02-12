using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class DailyTask
    {
        public int Id { get; set; }
        public string Refno { get; set; }
        public string Inquiryno { get; set; }
        public string ClientnameandNo { get; set; }
        public DateTime? Attendeddate { get; set; }
        public string Req { get; set; }
        public string Actiontaken { get; set; }
        public string Nextsteps { get; set; }
        public string Remarks { get; set; }
        public string List { get; set; }
    }
}
