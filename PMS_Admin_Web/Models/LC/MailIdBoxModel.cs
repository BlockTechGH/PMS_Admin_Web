using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class MailIdBoxModel
    {
        public List<Emailuser> TO { get; set; }
        public List<Emailuser> CC { get; set; }
        public Emailuser emailuser { get; set; }
    }
}
