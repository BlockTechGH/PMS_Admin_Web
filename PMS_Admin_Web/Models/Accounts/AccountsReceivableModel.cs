using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Accounts
{
    public class AccountsReceivableModel
    {
        public int id { get; set; }
        public string fromtemp { get; set; }
        public string propertyname { get; set; }
        public string aptno { get; set; }
        public string tname { get; set; }
        public DateTime? rentdatefrom { get; set; }
        public DateTime? rentdateto { get; set; }
        public DateTime? vdate { get; set; }
        public string amt { get; set; }
        public string desc { get; set; }
        public string type { get; set; }
        public bool chkbt { get; set; }
        public string bt { get; set; }
        public bool chkcash { get; set; }
        public string cash { get; set; }
        public bool chkknet { get; set; }
        public string knet { get; set; }
        public bool chkcheque { get; set; }
        public string cheque { get; set; }
        public string monthrent { get; set; }
        public string pref  { get; set; }
        public string vno { get; set; }
        public string rentmonth { get; set; }
        public string rentyear { get; set; }
        public string collectionyear { get; set; }
        public string collectionmonth { get; set; }
        public bool chkhighlight { get; set; }
        public bool chkpending { get; set; }
        public bool chkpayingtocourt { get; set; }
        public bool chkdiscountamount { get; set; }
        public string discountamount { get; set; }
        public bool chkmultipleapartment { get; set; }
        public string groupaptno { get; set; }
        public bool chkpamistake { get; set; }
        public string rbReservedVacatedCurrent { get; set; }
        public bool rbshowresevedtenant { get; set; }
        public bool rbshowvacatedtenant { get; set; }
        public bool rbshowcurrenttenant { get; set; }
        

    }
}
