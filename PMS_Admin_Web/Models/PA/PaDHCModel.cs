using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMS_Admin_Web.Models.PA
{
    public class PaDHCModel:Controller
    {
        public List<string> DHCHeader { get; set; }
        public List<string> DHCHeaderTrim { get; set; }
        public List<string> pfFloor { get; set; }
        public int recordcount { get; set; }
        public List<dhcbodypopupModel> popupModel { get; set; }
    }

    public class dhcbodypopupModel
    {
        public string location { get; set; }
        public string flooring { get; set; }
        public string furniture { get; set; }
        public string watertank { get; set; }
        public string swimming { get; set; }
        public string gym { get; set; }
        public string lobby { get; set; }
        public string drain { get; set; }
        public string ac { get; set; }
        public string parking { get; set; }
        public string autogate { get; set; }
        public string rollershutter { get; set; }
        public string plants { get; set; }
        public string stair { get; set; }
        public string corridor { get; set; }
        public string lighting { get; set; }
        public string hvac { get; set; }
        public string transformer { get; set; }
        public string firealarm { get; set; }
        public string fireexits { get; set; }
        public string firehose { get; set; }
        public string lift { get; set; }
        public string garbage { get; set; }
        public string staffacc { get; set; }
        public string staffpresent { get; set; }
    }
}
