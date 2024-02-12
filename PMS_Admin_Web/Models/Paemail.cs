using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Paemail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
