using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class OpenQueriesModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LEname { get; set; }
        public List<OpenQueries> openqueries { get; set; }
    }

    public class OpenQueries
    {
        public string CGLREFNO { get; set; }
        public string mobile { get; set; }
        public string CompletedBY { get; set; }
        public string Source { get; set; }
        public string bed { get; set; }
        public string propertytype { get; set; }
        public string ClientName { get; set; }
        public string Nationality { get; set; }
        public string minbudget { get; set; }
        public string Maxbudget { get; set; }
        public string movingdate { get; set; }
        public string inquirystatus { get; set; }
        public string EnquiryDate { get; set; }
        public string ptdate { get; set; }
        public string actiondate { get; set; }
        public string actionsdone { get; set; }
    }
}
