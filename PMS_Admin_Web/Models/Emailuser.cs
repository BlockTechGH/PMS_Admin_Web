using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Emailuser
    {
        public int Id { get; set; }
        public string Mailid { get; set; }
        public string Dept { get; set; }
        public string Mailstatus { get; set; }
    }
}
