using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Workstatus
    {
        public int Id { get; set; }
        public string Pref { get; set; }
        public string Painting { get; set; }
        public DateTime? Datecompp { get; set; }
        public string Paintrem { get; set; }
        public string Refurbishing { get; set; }
        public DateTime? Datecompr { get; set; }
        public string Refurbrem { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
