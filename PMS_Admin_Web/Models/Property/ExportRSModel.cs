using PMS_Admin_Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class ExportRSModel
    {
        public string MAXLBL { get; set; }
        public List<ExportRSClassModel> detailgrid { get; set; }
        public string bname { get; set; }
    }
}
