using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class AccountsPm
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Pref { get; set; }
        public string Aptno { get; set; }
        public string Leaseno { get; set; }
        public int? Tid { get; set; }
        public int? Ptid { get; set; }
        public string Tname { get; set; }
        public decimal? Mrent { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public decimal? Prent { get; set; }
        public int? Orderid { get; set; }
        public string Csremarks { get; set; }
        public string Csstatus { get; set; }
        public DateTime? Cstime { get; set; }
        public string Combine { get; set; }
    }
}
