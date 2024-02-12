using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Supportingdocx
{
    public class CashreceivedModel
    {
        public string pname { get; set; }
        public string d1 { get; set; }
        public string d2 { get; set; }
        public List<Cashreceived> cashreceivedList { get; set; }
        public string printdate { get; set; }

    }

    public class Cashreceived
    {
        public string aptno { get; set; }
        public string tname { get; set; }
        public string orderid { get; set; }
        public string rno { get; set; }
        public string mrent { get; set; }
        public string rdate { get; set; }
        public string rentdatefrom { get; set; }
        public string rentdateto { get; set; }
        public string paymenttype { get; set; }
        public string amt { get; set; }

    }
}
