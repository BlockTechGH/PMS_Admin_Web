using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.PA
{
    public class PrintReceiptModel
    {
        public Bulkprinting bulkprinting { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string date { get; set; }
    }
}
