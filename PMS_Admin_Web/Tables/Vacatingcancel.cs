using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Vacatingcancel
    {
        public int Id { get; set; }
        public string Tname { get; set; }
        public string Pref { get; set; }
        public DateTime? Doc { get; set; }
        public string Remarks { get; set; }
        public string Lcno { get; set; }
        public string Vnpath { get; set; }
        public string Bmapprovalpath { get; set; }
        public string Canceldocpath { get; set; }
    }
}
