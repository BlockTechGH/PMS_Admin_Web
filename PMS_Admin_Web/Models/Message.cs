using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public string MMsg { get; set; }
        public string MPname { get; set; }
        public string MDept { get; set; }
        public DateTime MDoc { get; set; }
        public DateTime? MTimeread { get; set; }
        public string MMode { get; set; }
    }
}
