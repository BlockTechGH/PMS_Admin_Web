using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Deletedlogin
    {
        public int? Id { get; set; }
        public DateTime? Doc { get; set; }
        public string Paname { get; set; }
        public string Logmode { get; set; }
        public DateTime? Logtime { get; set; }
        public string Remarks { get; set; }
        public string Mode { get; set; }
        public string Sysname { get; set; }
    }
}
