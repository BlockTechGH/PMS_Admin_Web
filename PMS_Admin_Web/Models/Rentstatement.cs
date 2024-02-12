using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Rentstatement
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Tname { get; set; }
        public string Nat { get; set; }
        public string Bed { get; set; }
        public string Bath { get; set; }
        public decimal Rent { get; set; }
        public decimal? Actualrent { get; set; }
        public string Mpay { get; set; }
        public string Ftype { get; set; }
        public string Corpinv { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime Doc { get; set; }
        public string Updated { get; set; }
        public string Status { get; set; }
        public string Asr { get; set; }
        public string Asrstatus { get; set; }
        public string Asrftype { get; set; }
        public string Close { get; set; }
        public string Vtenant { get; set; }
        public DateTime? Leasestart { get; set; }
        public DateTime? Leaseend { get; set; }
    }
}
