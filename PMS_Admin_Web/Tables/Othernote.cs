﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Tables
{
    public partial class Othernote
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string Remark { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Category { get; set; }
    }
}
