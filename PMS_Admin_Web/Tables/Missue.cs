using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Missue
    {
        public int Id { get; set; }
        public string IssueNo { get; set; }
        public string Pref { get; set; }
        public string Aptlocation { get; set; }
        public string Pname { get; set; }
        public string Category { get; set; }
        public string Issue { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string Request { get; set; }
        public DateTime? Requestdate { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public DateTime? Wcdate { get; set; }
        public string Itempurchase { get; set; }
        public string Workdescription { get; set; }
        public string Wdstatus { get; set; }
        public string Additionalwork { get; set; }
        public string Paname { get; set; }
        public string Pl { get; set; }
        public string Jobid { get; set; }
        public int? Qty { get; set; }
        public string Tname { get; set; }
        public string Contactno { get; set; }
        public string Lcno { get; set; }
        public string Tavailability { get; set; }
        public string Contractortype { get; set; }
        public string Contractorname { get; set; }
        public string Fcaseen { get; set; }
        public DateTime? Underobservationstart { get; set; }
        public DateTime? Underobservationstop { get; set; }
        public string Fcaquestion { get; set; }
        public string Pareply { get; set; }
        public string Wmr { get; set; }
        public string Cwmr { get; set; }
        public DateTime? PaDoneOn { get; set; }
    }
}
