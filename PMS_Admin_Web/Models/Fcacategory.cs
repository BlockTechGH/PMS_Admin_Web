using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Fcacategory
    {
        public int Id { get; set; }
        public string Catsection { get; set; }
        public string Categoryname { get; set; }
        public DateTime Doc { get; set; }
        public DateTime? Dou { get; set; }
        public int? Orderno { get; set; }
    }
}
