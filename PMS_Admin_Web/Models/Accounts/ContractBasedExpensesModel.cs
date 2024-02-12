using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class ContractBasedExpensesModel
    {
        public List<Pmesst> PMEsreqGRID { get; set; }
        public decimal MAINTOTAMTXT { get; set; }
        public List<Periodexpensesentry> periodexpensesentry { get; set; }
    }
}
