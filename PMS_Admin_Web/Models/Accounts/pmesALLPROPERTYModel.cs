using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class pmesALLPROPERTYModel
    {
        public List<Pmesaccount> PMEGRID { get; set; }
        public List<Pmesaccount> DG { get; set; }
        public decimal totamt { get; set; }
        public string pmemonth { get; set; }
        public string pmeyear { get; set; }
    }
}
