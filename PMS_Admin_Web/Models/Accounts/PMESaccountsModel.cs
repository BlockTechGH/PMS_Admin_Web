using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class PMESaccountsModel
    {
        public List<Pmesaccount> PMEGRID { get; set; }
        public string bldngname { get; set; }
        //public DateTime invoicedate { get; set; }
        //public string statements { get; set; }
    }
}
