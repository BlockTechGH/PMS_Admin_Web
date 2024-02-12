using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class AddComplaintModel
    {
        public Missue missue { get; set; }
        public string apttxt { get; set; }
        public bool IssueLocation { get; set; }
    }
}
