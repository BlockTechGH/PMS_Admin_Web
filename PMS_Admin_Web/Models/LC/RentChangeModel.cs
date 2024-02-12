using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.LC
{
    public class RentChangeModel
    {
        public Rentchange rentchange { get; set; }
        public string PropertyName { get; set; }
        public string PropertySource { get; set; }
        public string AptNo { get; set; }
        public string TenantName { get; set; }
        public string Rent { get; set; }
        public string LeaseNo { get; set; }
        public string LeaseTenantName { get; set; }
        public string LcRent { get; set; }
        //public List<Propertymaster> propertynames { get; set; }
        public bool NoLC { get; set; }
        public string ChangeInLC { get; set; }
        public string ChangeinRent { get; set; }
        public List<string> MailToList { get; set; }
        public string MailTo { get; set; }
        public string MailCC { get; set; } = "business@q8realtor.com; operations@q8realtor.com";
        public IFormFile postedFile { get; set; }

        //public List<Propertymaster> aptnos { get; set; } = new();
        //public List<Lcinfo> lcnos { get; set; } = new();
    }
}
