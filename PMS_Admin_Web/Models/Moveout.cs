using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Moveout
    {
        public int Id { get; set; }
        public string Pref { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Tenantname { get; set; }
        public string Vn { get; set; }
        public string Moistatus { get; set; }
        public string Moi { get; set; }
        public string Moilist { get; set; }
        public DateTime? Moidate { get; set; }
        public string Rentpaid { get; set; }
        public string Lc { get; set; }
        public string Sd { get; set; }
        public string Sdfrom { get; set; }
        public string Pmodeofsd { get; set; }
        public string Acards { get; set; }
        public string Keys { get; set; }
        public string Sat { get; set; }
        public string Refurbbrkdown { get; set; }
        public string Fileclosd { get; set; }
        public string Bmreason { get; set; }
        public string Bmapproved { get; set; }
        public string Bmapproval { get; set; }
        public string Sdendorsement { get; set; }
        public string Tenantrefurb { get; set; }
        public decimal? Rmat { get; set; }
        public decimal? Sdamt { get; set; }
        public decimal? Armat { get; set; }
        public string Reduction { get; set; }
        public string Waved { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
        public string Updatedby { get; set; }
        public DateTime? Ardate { get; set; }
        public string Sparemarks { get; set; }
    }
}
