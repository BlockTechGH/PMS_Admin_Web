using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Deltedinquiry
    {
        public int Id { get; set; }
        public string Inqno { get; set; }
        public string Userinfo { get; set; }
        public DateTime? Deletingdate { get; set; }
    }
}
