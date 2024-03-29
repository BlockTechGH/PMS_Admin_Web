﻿using System.Collections.Generic;

namespace PMS_Admin_Web.Models.Marketing
{
    public class CancellQueriesModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LEname { get; set; }
        public List<CancellQueries> cancellqueries { get; set; }
    }

    public class CancellQueries
    {
        public string CGLREFNO { get; set; }
        public string CompletedBY { get; set; }
        public string Source { get; set; }
        public string ClientName { get; set; }
        public string Nationality { get; set; }
        public string minbudget { get; set; }
        public string Maxbudget { get; set; }
        public string movingdate { get; set; }
        public string inquirystatus { get; set; }
        public string EnquiryDate { get; set; }
        public string dateofcancel { get; set; }
        public string reasonofcancellation { get; set; }
        public string mobile { get; set; }
    }
}
