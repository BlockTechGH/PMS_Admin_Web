using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class DriverScheduel
    {
        public int Id { get; set; }
        public string Refno { get; set; }
        public DateTime? Date { get; set; }
        public string Stype { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public TimeSpan? Ft { get; set; }
        public TimeSpan? Tt { get; set; }
        public int? DriverId { get; set; }
        public string DriverName { get; set; }
        public string LEname { get; set; }
        public string ReferenceNo { get; set; }
        public string ClentName { get; set; }
        public string PropertyName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Remarks { get; set; }
        public string Comments { get; set; }
        public DateTime? Doc { get; set; }
        public string Otherreason { get; set; }
        public DateTime? Dou { get; set; }
        public string Grouple { get; set; }
        public string Groupleyesno { get; set; }
        public TimeSpan? Returntime { get; set; }
    }
}
