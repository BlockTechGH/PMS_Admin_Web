using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Tenantshistory
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Pref { get; set; }
        public string Aptno { get; set; }
        public string Ftype { get; set; }
        public string Btype { get; set; }
        public string Tenantname { get; set; }
        public string Nationality { get; set; }
        public string Contact { get; set; }
        public decimal? Rent { get; set; }
        public string LeaseNo { get; set; }
        public DateTime? Leasestart { get; set; }
        public DateTime? Leaseend { get; set; }
        public DateTime? Movedate { get; set; }
        public DateTime? Keyreturndate { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public int? Moveoutid { get; set; }
        public int? Tid { get; set; }
        public string Status { get; set; }
        public string Forceclose { get; set; }
        public string Fcreason { get; set; }
        public DateTime? Closedate { get; set; }
        public string Fcdocpath { get; set; }
        public string Fcaseen { get; set; }
    }
}
