using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Ptv
    {
        public int? Sid { get; set; }
        public string Propertyref { get; set; }
        public string Propertname { get; set; }
        public decimal? Rent { get; set; }
        public string Poc { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
        public string Pdesc { get; set; }
    }
}
