using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class UpdateComplaintModel
    {
        public Missue missue { get; set; }
        public List<MissueFollowup> missueFollowups { get; set; }
        public List<string> EmailListTo { get; set; }
        public string EmailTo { get; set; }
        public List<string> EmailListCC { get; set; }
        public string EmailCC { get; set; }
        public int reccount { get; set; }
        public string StatusfromMaintenance { get; set; }
        public string Attender { get; set; }
        public string RemarksfromMaintenance { get; set; }
        //public string OutsourceContractor { get; set; }
        //public string Handyman { get; set; }
        //public string ITAdminOther { get; set; }
        //public string Landlord { get; set; }
        public string aptstatus { get; set; }
        public int mrequestCount { get; set; }
        public string[] hiddenItempurchasedlist { get; set; }
        public List<Itempurchased> itempurchasedList { get; set; }
        //public List<string> qtypurchasedList { get; set; }
        public string[] itemarray { get; set; }
        public string[] qtyarray { get; set; }
        public string defaultworkDescription { get; set; }
        
        
    }
}
