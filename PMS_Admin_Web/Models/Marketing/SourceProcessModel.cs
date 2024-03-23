using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class SourceProcessModel
    {
        public string SourceName { get; set; }
        public List<SourceProcess> sourceprocesses { get; set; }
    }

    public class SourceProcess
    {
        public string source { get; set; }
        public string cglrefno { get; set; }
        public string completedby { get; set; }
        public string ClientName { get; set; }
        public string mobile { get; set; }
        public string contactperson { get; set; }
        public string propertytype { get; set; }
        public string Bed { get; set; }
        public string InquiryStatus { get; set; }
        public string ptdate { get; set; }
        public string stype { get; set; }
        public string remarks { get; set; }
        public string actiondate { get; set; }
        public string actionsdone { get; set; }
    }
}
