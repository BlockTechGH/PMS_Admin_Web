using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Renewalreason
    {
        public int Id { get; set; }
        public string Reasons { get; set; }
        public DateTime Doc { get; set; }
    }
}
