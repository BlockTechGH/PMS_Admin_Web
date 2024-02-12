using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class Rentedapartment
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Aptno { get; set; }
        public string Pref { get; set; }
        public string Status { get; set; }
        public DateTime? Movingindate { get; set; }
        public DateTime? Lcdate { get; set; }
        public string Paintstatus { get; set; }
        public DateTime? Pcompleted { get; set; }
        public string Rstatus { get; set; }
        public DateTime? Rcompleted { get; set; }
        public string Remarks { get; set; }
        public DateTime? Doc { get; set; }
        public DateTime? Dou { get; set; }
    }
}
