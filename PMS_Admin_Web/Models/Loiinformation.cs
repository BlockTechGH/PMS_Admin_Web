using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Loiinformation
    {
        public int Id { get; set; }
        public string EnquiryType { get; set; }
        public string Inqno { get; set; }
        public string LoiNo { get; set; }
        public string Lename { get; set; }
        public string ClientName { get; set; }
        public string Cmob { get; set; }
        public string Cnationality { get; set; }
        public string ClientCompany { get; set; }
        public string ClientSource { get; set; }
        public string Req { get; set; }
        public string Fur { get; set; }
        public string PropertySource { get; set; }
        public string PropertyName { get; set; }
        public string PropertyRefNo { get; set; }
        public string Aptno { get; set; }
        public string Area { get; set; }
        public DateTime? Leasedate { get; set; }
        public DateTime? Leaseenddate { get; set; }
        public DateTime? Loisigndate { get; set; }
        public string Ap { get; set; }
        public string Aptype { get; set; }
        public string Renttobepaidby { get; set; }
        public decimal Rent { get; set; }
        public string Securitydepositpaidby { get; set; }
        public decimal Deposit { get; set; }
        public string Feetobepaidby { get; set; }
        public decimal ClientResf { get; set; }
        public decimal Llresf { get; set; }
        public int? TotClients { get; set; }
        public int? SearchedProperty { get; set; }
        public int? TotOccupants { get; set; }
        public DateTime LoiDate { get; set; }
        public string LoiStatus { get; set; }
        public string Loimailstatus { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? Updateddate { get; set; }
        public string Docsubmitted { get; set; }
        public string Lcsigned { get; set; }
        public string Paystart { get; set; }
        public string Payment { get; set; }
        public string Paystatus { get; set; }
        public string Payremarks { get; set; }
        public DateTime? Paymentdate { get; set; }
        public string LoiNo1 { get; set; }
        public string LcNo { get; set; }
        public string Dept { get; set; }
        public string Deptusr { get; set; }
        public string Loinote { get; set; }
        public string Lccreate { get; set; }
        public string Loiremarks { get; set; }
        public decimal? Duerent { get; set; }
        public decimal? Duedeposit { get; set; }
        public decimal? Dueresf { get; set; }
        public string Loipath { get; set; }
        public string Pp { get; set; }
        public string Cid { get; set; }
        public string Noc { get; set; }
        public string Mc { get; set; }
        public string Coe { get; set; }
        public string Cosign { get; set; }
        public string Cas { get; set; }
        public string Cidpp { get; set; }
        public string Cg { get; set; }
        public string Moc { get; set; }
        public DateTime? Movingindate { get; set; }
        public string Legal { get; set; }
        public DateTime? DateFrom { get; set; }
    }
}
