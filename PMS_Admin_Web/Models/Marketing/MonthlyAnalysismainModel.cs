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
        public string tot { get; set; }
        public string completedby { get; set; }
        public string category { get; set; }
        public string minbudget { get; set; }
        public string year { get; set; }
    }

    public class resfclosedleads
    {
        public List<resfclosedleadslist> list { get; set; }
        

        public List<string> completedbynames { get; set; }
    }

    public class resfclosedleadslist
    {
        public string REF { get; set; }
        public string completedby { get; set; }
        public string year { get; set; }
        public string Xpgl { get; set; }
        public string Xcgl { get; set; }
        public string PGLCASE { get; set; }
        public string CGLCASE { get; set; }
        public string pglresf { get; set; }
        public string cglresf { get; set; }
        public string resf { get; set; }
        public string MB { get; set; }
        public string resfper { get; set; }

        public string count { get; set; }
    }

    //public class inquirybudget
    //{
    //    public List<inquirybudgetlist> list { get; set; }
    //    public List<string> typelist { get; set; }
        
    //}
    public class inquirybudgetlist
    {
        public string refno { get; set; }
        public string Budget { get; set; }
        public string type { get; set; }
        public string Propertytype { get; set; }
        public string CountPgl { get; set; }
        public string CountCgl { get; set; }
    }

    public class natsrc
    {
        public string nationality { get; set; }
        public string occurrence { get; set; }
    }

    public class srcdata 
    {
        public string clientsource { get; set; }
        public string totalinquiry { get; set; }
        public string totalclosed { get; set; }
    }

    public class srclead
    {
        public string assignedinq { get; set; }
        public string ptmade { get; set; }
        public string Completedby { get; set; }
        public string closedlead { get; set; }
    }

}
