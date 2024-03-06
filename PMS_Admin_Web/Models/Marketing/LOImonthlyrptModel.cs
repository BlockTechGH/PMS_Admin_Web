using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class LOImonthlyrptModel
    {
        public List<LOImonthlyrpt> lOImonthlyrpts { get; set; }
        public List<string> distinctLeNames { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public decimal TotalExpected { get; set; }
    }

    public class LOImonthlyrpt
    {
        public string inqno { get; set; }
        public string Aptno { get; set; }
        public string ClientName { get; set; }
        public string ClientCompany { get; set; }
        public string ClientSource { get; set; }
        public string PropertyName { get; set; }
        public string PropertySource { get; set; }
        public string loistatus { get; set; }
        public string EnquiryType { get; set; }
        public string Leasedate { get; set; }
        public string loisigndate { get; set; }
        public string lename { get; set; }
        public string monname { get; set; }
        public string deposit { get; set; }
        public string rent { get; set; }
        public string clientresf { get; set; }
        public string llresf { get; set; }
        public string client { get; set; }
        public string sp { get; set; }
        public string payment { get; set; }
        public string paystatus { get; set; }
        public string payremarks { get; set; }
    }
}
