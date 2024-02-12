using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class RENTSTModel
    {
        public List<accgridModel> accgrid { get; set; }
        public string totrent { get; set; }
        public string ACTRENT { get; set; }
        public string bname { get; set; }
        public string rented { get; set; }
        public string reserved { get; set; }
        public string vacant { get; set; }
        public List<string> aptlist { get; set; }

    }

    public class accgridModel
    {
        public int id { get; set; }
        public string tname { get; set; }
        public string pname { get; set; }
        public string aptno { get; set; }
        public string pref { get; set; }
        public string nat { get; set; }
        public string bed { get; set; }
        public string bath { get; set; }
        public string rent { get; set; }
        public string actrent { get; set; }
        public string mpay { get; set; }
        public string ftype { get; set; }
        public string corpinv { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string updated { get; set; }
        public string leasestart { get; set; }
        public string leaseend { get; set; }
        public string slease { get; set; }

    }
}
