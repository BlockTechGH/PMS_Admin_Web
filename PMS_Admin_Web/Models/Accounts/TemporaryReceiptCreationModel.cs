using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class TemporaryReceiptCreationModel
    {
        public Paymentvoucher paymentvoucher { get; set; }
        public bool MultipleApartment { get; set; }
        public string GroupAptno { get; set; }
        public bool MultiplePayment { get; set; }
        public string other { get; set; }
        public string totalamount { get; set; }
        public string rentpaid { get; set; }
        public string depositpaid { get; set; }
        public string clresfpaid { get; set; }
        public string llresfpaid { get; set; }
        public string loirent { get; set; }
        public string loideposit { get; set; }
        public string loiclresf { get; set; }
        public string loillresf { get; set; }
        public string propertysource { get; set; }
        public List<string> tomailidlist { get; set; }
        public string tomailid { get; set; }
        public List<string> ccmailidlist { get; set; }
        public string ccmailid { get; set; }
    }
}
