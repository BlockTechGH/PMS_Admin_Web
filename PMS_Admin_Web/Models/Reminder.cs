using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Reminder
    {
        public int Id { get; set; }
        public string Reminderno { get; set; }
        public string Enqno { get; set; }
        public string Usr { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime Doc { get; set; }
        public string Status { get; set; }
    }
}
