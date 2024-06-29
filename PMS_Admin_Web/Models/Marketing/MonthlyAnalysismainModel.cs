using PMS_Admin_Web.Controllers;
using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class MonthlyAnalysismainModel
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public pglcgl p1command { get; set; }
        public pglcgl p1command1 { get; set; }
        public loi p1command2 { get; set; }
        public loi p1command3 { get; set; }
        //public pglcgl p2command { get; set; }
        //public pglcgl p2command1 { get; set; }
        //public loi p2command2 { get; set; }
        //public loi p2command3 { get; set; }
        //public pc p2command4 { get; set; }
        //public pc p2command5 { get; set; }
    }

    public class pglcgl
    {
        public string count { get; set; }
        public string sum { get; set; }
    }

    public class loi
    {
        public string count { get; set; }
        //public string sum { get; set; }
    }

    public class pc
    {
        //public string count { get; set; }
        public string sum { get; set; }
    }

    public class GetPglCglCountMonthlyReportModel
    {
        public int pglcount { get; set; }
        public int cglcount { get; set; }
    }

    public class ChartDataItem
    {
        public string Type { get; set; }
        public int Count { get; set; }
    }

    public class src
    {
        public List<srclist> Srclist { get; set; }
        public List<string> year { get; set; }
    }

    public class srclist
    {
        public string tot { get; set; }
        public string completedby { get; set; }
        public string category { get; set; }
        public string minbudget { get; set; }
        public string year { get; set; }
    }
}
