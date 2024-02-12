using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Combineapartment
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Apartments { get; set; }
        public string Pref { get; set; }
        public int? Accid { get; set; }
    }
}
