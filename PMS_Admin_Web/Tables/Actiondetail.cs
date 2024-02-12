using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Actiondetail
    {
        public int Id { get; set; }
        public string Refno { get; set; }
        public string Actions { get; set; }
        public string Lename { get; set; }
        public string Date { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public string Adminremarks { get; set; }
    }
}
