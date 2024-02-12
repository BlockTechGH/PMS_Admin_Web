using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Walkininquiry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public string Status { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Pname { get; set; }
        public string Seenbyfca { get; set; }
    }
}
