using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PMS_Admin_Web
{
    public class Connection
    {
        //public string ConnectionString { get; set; } = @"Server=(local)\SQLEXPRESS;Database=q8Realtor;Trusted_Connection=True;MultipleActiveResultSets=true";
        //public string ConnectionString { get; set; } = @"Server=(local)\SQLEXPRESS;Database=Realtor;Trusted_Connection=True;MultipleActiveResultSets=true";
        //public string ConnectionString { get; set; } = @"Server=EC2AMAZ-VA1R1C2\SQLEXPRESS;Database=Realtor;Trusted_Connection=True;MultipleActiveResultSets=true";
        public string ConnectionString { get; set; } = @"Server=EC2AMAZ-VA1R1C2\SQLEXPRESS;Database=Realtor;User Id=btadmin;Password=btadmin@1;MultipleActiveResultSets=true";
        

    }
}
