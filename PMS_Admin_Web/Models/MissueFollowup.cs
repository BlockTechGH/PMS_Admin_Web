using System;
using System.Collections.Generic;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class MissueFollowup
    {
        public int FollowupId { get; set; }
        public int FollowupIssueid { get; set; }
        public string FollowupRemarks { get; set; }
        public DateTime FollowupDoc { get; set; }
        public string FollowupBy { get; set; }
    }
}
