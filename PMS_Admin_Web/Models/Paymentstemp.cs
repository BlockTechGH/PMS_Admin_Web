using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Paymentstemp
    {
        public int Id { get; set; }
        public string Rno { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Tname { get; set; }
        public decimal? Mrent { get; set; }
        public DateTime? Lstart { get; set; }
        public DateTime? Lend { get; set; }
        public DateTime Rdate { get; set; }
        public DateTime? Rentdatefrom { get; set; }
        public DateTime? Rentdateto { get; set; }
        public decimal? Rent { get; set; }
        public decimal? Sd { get; set; }
        public decimal? Resf { get; set; }
        public decimal? Internet { get; set; }
        public decimal? Other { get; set; }
        public decimal? Additional { get; set; }
        public string Category { get; set; }
        public decimal Cash { get; set; }
        public decimal Knet { get; set; }
        public decimal? Cheque { get; set; }
        public decimal Totamt { get; set; }
        public string Description { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
