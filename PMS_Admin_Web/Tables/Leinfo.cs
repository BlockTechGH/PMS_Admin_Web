using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Leinfo
    {
        public int Id { get; set; }
        public string Refno { get; set; }
        public string Nametitle { get; set; }
        public string Lename { get; set; }
        public string Lemob { get; set; }
        public string Phone { get; set; }
        public string Lemail { get; set; }
        public string Address { get; set; }
        public string Rightsassigned { get; set; }
        public int Deleted { get; set; }
    }
}
