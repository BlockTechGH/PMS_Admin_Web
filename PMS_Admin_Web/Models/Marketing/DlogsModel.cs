using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class DlogsModel
    {
        public string Drivername { get; set; }
        public string FromDate { get; set; }
        public List<Driverformat> driverformats { get; set; }
    }

    public class IdgridModel
    {
        public string sid { get; set; }
        public string address { get; set; }
    }
}
