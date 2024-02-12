using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PMS_Admin_Web.Models.PA
{
    public class MaintenanceIssueModel
    {
        //public X.PagedList.IPagedList<PMS_Admin_Web.Models.Missue> missueViewPagedList { get; set; }
        //public PagedList<PMS_Admin_Web.Models.Missue> missuePagedList { get; set; }
        public List<Missue> missueList { get; set; }
        public int issuemaxid { get; set; }
    }
}
