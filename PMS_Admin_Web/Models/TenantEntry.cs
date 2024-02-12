using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class TenantEntry
    {
        public int Id { get; set; }
        public string TeTname { get; set; }
        public string TeLcno { get; set; }
        public string TeCid { get; set; }
        public DateTime TeDoc { get; set; }
        public string Pname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
