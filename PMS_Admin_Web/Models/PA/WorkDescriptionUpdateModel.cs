using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class WorkDescriptionUpdateModel
    {
        public Missue missue { get; set; }
        public List<Itempurchased> itemspurchased { get; set; }
        public List<string> EmailListTo { get; set; }
        public string EmailTo { get; set; }
        public List<string> EmailListCC { get; set; }
        public string EmailCC { get; set; }
        public int reccount { get; set; }
        
        public string aptstatus { get; set; }
        public int mrequestCount { get; set; }
        
        public string defaultworkDescription { get; set; }
    }
}
