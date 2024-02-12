using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class PropertyList
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public DateTime? Doc { get; set; }
    }
}
