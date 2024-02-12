using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Temppayment
    {
        public int Id { get; set; }
        public int? Rno { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Tname { get; set; }
        public decimal? Mrent { get; set; }
        public DateTime? Rdate { get; set; }
        public DateTime? Rentdatefrom { get; set; }
        public DateTime? Rentdateto { get; set; }
        public string Paymenttype { get; set; }
        public decimal Cash { get; set; }
        public decimal Knet { get; set; }
        public decimal? Cheque { get; set; }
        public decimal? Bt { get; set; }
        public decimal Totamt { get; set; }
        public decimal? Discamt { get; set; }
        public string Description { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public int? Collectionmonth { get; set; }
        public int? Collectionyear { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? Dou { get; set; }
        public DateTime? Banked { get; set; }
        public string Entrymode { get; set; }
        public string Remarks { get; set; }
        public int? Advid { get; set; }
        public string Remstatus { get; set; }
        public string Highlight { get; set; }
        public string Pvno { get; set; }
        public string Exclude { get; set; }
        public int? Sortid { get; set; }
        public string Amtinwords { get; set; }
        public string Chqno { get; set; }
        public string Bankname { get; set; }
        public int? Oldsd { get; set; }
    }
}
