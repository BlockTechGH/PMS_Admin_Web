using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Lcchange
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Feature { get; set; }
        public string Reason { get; set; }
        public DateTime? ChangeDateTime { get; set; }
        public string Status { get; set; }
        public string Changeby { get; set; }
        public string Oldupdate { get; set; }
        public string Lcno { get; set; }
    }
}
