using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PMS_Admin_Web.Models.PA
{
    public class UploadDocumentsModel
    {
        public Padocx padocx { get; set; }
        public List<Padocx> padocxes { get; set; }
        public IFormFile postedFile { get; set; }
        public DateTime? dt2 { get; set; }
        public bool swimmingpool { get; set; }
        public string swimmingpoolfile { get; set; }
        //public IFormFile swimmingpoolPath { get; set; }
        public bool document2 { get; set; }
        public bool document3 { get; set; }
        public bool document4 { get; set; }
    }
}
