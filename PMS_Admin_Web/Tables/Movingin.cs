using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Movingin
    {
        public int Id { get; set; }
        public string Pref { get; set; }
        public string Leaseno { get; set; }
        public int? Leaseid { get; set; }
        public string Movingin1 { get; set; }
        public DateTime? Movingindate { get; set; }
        public string Inventorylist { get; set; }
        public string Inventoryreport { get; set; }
        public string Keys { get; set; }
        public string Receipt { get; set; }
        public string Other { get; set; }
        public DateTime? Doc { get; set; }
        public string Remarks { get; set; }
        public string Rented { get; set; }
    }
}
