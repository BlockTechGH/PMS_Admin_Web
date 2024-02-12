using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class PmesstaccountsModel
    {
        public List<Pmesaccount> PMEGRID { get; set; }
        public string pmemonth { get; set; }
        public string pmeyear { get; set; }
        public string PMEaccSTATUS { get; set; }
        public decimal PMETOT { get; set; }
        public string accpme { get; set; }
        public string bldngname { get; set; }
    }
}
