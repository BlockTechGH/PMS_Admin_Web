using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Fschedule
    {
        public int Id { get; set; }
        public string Mid { get; set; }
        public string Pname { get; set; }
        public string Multipleapt { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Otype { get; set; }
        public string Description { get; set; }
        public string Tavailable { get; set; }
        public TimeSpan? Fromtime { get; set; }
        public TimeSpan? Totime { get; set; }
        public DateTime? Sdate { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime? Cdate { get; set; }
        public DateTime? Issuedate { get; set; }
        public string Attender { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public int? Issueid { get; set; }
        public string Seenbyfca { get; set; }
    }
}
