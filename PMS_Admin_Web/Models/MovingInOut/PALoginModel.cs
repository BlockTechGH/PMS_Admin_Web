using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.MovingInOut
{
    public class PALoginModel
    {
        public List<Palogin> palogins { get; set; } = new();
        public Palogin palogin { get; set; } = new();
        public List<Palist> palists { get; set; } = new();
    }
}
