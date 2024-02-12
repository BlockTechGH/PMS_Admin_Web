using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Dhc
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public DateTime? DhcSubmitted { get; set; }
    }
}
