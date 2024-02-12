using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Privatemileage
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Drivername { get; set; }
        public int Openmil { get; set; }
        public int Closemil { get; set; }
        public int Totmil { get; set; }
        public TimeSpan Timein { get; set; }
        public TimeSpan Timeout { get; set; }
        public string Drivinghrs { get; set; }
        public int Privatemileage1 { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
