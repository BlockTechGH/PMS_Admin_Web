using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Mrequest
    {
        public int? Id { get; set; }
        public string Aptlocation { get; set; }
        public string Pname { get; set; }
        public string Issue { get; set; }
        public string Statusseen { get; set; }
        public string Availabletime { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public string Pl { get; set; }
        public string Paname { get; set; }
    }
}
