using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Leasehistory
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Progress { get; set; }
        public string Lcno { get; set; }
        public DateTime? Doc { get; set; }
    }
}
