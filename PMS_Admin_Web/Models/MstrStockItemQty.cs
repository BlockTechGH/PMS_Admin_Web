using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class MstrStockItemQty
    {
        public string StockItemCode { get; set; }
        public int? StockItemQty { get; set; }
        public int? StockItemLoc { get; set; }
    }
}
