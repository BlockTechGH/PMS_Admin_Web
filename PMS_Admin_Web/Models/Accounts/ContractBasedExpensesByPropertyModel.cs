using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class ContractBasedExpensesByPropertyModel
    {
        public List<Pmesst> pmepropgrid { get; set; }
        public decimal MAINTOTAMTXT { get; set; } = 0;
    }
}
