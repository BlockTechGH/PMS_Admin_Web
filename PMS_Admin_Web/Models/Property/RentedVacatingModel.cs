using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models.Property
{
    public class RentedVacatingModel
    {
        public List<mastergridModel> mastergrid { get; set; }
        public List<rentedgridModel> rentedgrid { get; set; }
        public List<Tenantshistory> tlcgrid { get; set; }
        public List<rgridModel> rgrid { get; set; }
        public List<Payment> ACCGRID { get; set; }
        public List<Payment> VFROMACCGRID { get; set; }
        public int pmcount { get; set; }
        public int rentedcount { get; set; }
        public int reservedcount { get; set; }
        public int vacatingcount { get; set; }
        public DateTime rentdatetxt { get; set; }
        public string bname { get; set; }
    }

    public class rgridModel
    {
        public int Id { get; set; }
        public string ptype { get; set; }
        public string inqtype { get; set; }
        public string leasestart { get; set; }
        public string leaseend { get; set; }
        public string APTNO { get; set; }
        public string bed { get; set; }
        public string TNAME { get; set; }
        public string TRENT { get; set; }
        public string tnat { get; set; }
        public string ttype { get; set; }
    }

    public class mastergridModel:Propertymaster
    {
        public string tmode { get; set; }
        public string tctype { get; set; }
    }

    public class rentedgridModel:Rented
    {
        public string leaseend { get; set; }
        public string tname { get; set; }
        public string trent { get; set; }
        public string tnat { get; set; }
        public string ttype { get; set; }
        public string tbed { get; set; }
        public string tbath { get; set; }
        public string tmode { get; set; }
        public string tctype { get; set; }
    }
}
