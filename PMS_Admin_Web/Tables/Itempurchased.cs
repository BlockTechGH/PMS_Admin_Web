using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Itempurchased
    {
        public int Id { get; set; }
        public int? Iid { get; set; }
        public string Itemdesc { get; set; }
        public string Qty { get; set; }
    }
}
