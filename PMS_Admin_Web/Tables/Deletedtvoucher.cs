using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Deletedtvoucher
    {
        public int Id { get; set; }
        public string Loino { get; set; }
        public string Inqno { get; set; }
        public string LoiNo1 { get; set; }
        public string LcNo { get; set; }
        public string Voucherno { get; set; }
        public string AmtType { get; set; }
        public decimal Amt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string Comments { get; set; }
        public DateTime Doc { get; set; }
        public string Dept { get; set; }
        public string User { get; set; }
        public DateTime? Datefrom { get; set; }
        public DateTime? Dateto { get; set; }
        public string Ptype { get; set; }
        public string Tname { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Knet { get; set; }
        public decimal? Cheque { get; set; }
        public decimal? Bt { get; set; }
        public string Amtinwords { get; set; }
        public string Flag { get; set; }
    }
}
