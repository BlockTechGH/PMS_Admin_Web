using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace PMS_Admin_Web.Models.LC
{
    public class VacatingsAndTlcModel
    {
        public VacatingsAndTlcTab ActiveTab { get; set; }

        public List<VacatingsAndTlcGridModel> vacatingList { get; set; }
        public List<VacatingsAndTlcGridModel> pendingVacating { get; set; }
        public List<VacatingsAndTlcGridModel> tlc { get; set; }
    }

    public enum VacatingsAndTlcTab
    {
        VacatingList,
        PendingVacating,
        TLC
    }

    public class VacatingsAndTlcGridModel
    {
        public int Id { get; set; }//vacatingId for tlc
        public string PropertyName { get; set; }
        public string Pref { get; set; }
        public string AptNo { get; set; }
        public string Type { get; set; }
        public string Bed { get; set; }
        public string TenantName { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public string ContactNo { get; set; }
        public string Nationality { get; set; }
        public DateTime MoveOutOn { get; set; }
    }
}
