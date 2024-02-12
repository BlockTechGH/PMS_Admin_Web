using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using static PMS_Admin_Web.ModelDataAnnotations.CustomDataAnnotations;

namespace PMS_Admin_Web.Models.Users
{
    public class CreateUserModel
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        public string Username { get; set; }
        public string PaUsername { get; set; }
        //[RequiredIf("Id", null, ErrorMessage = "Password Field Is Required")]
        //[Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required]
        public string Role { get; set; }
        //[Required]
        public string Department { get; set; }
        public string PaDepartment { get; set; }
        public string Designation { get; set; }
        public int LogMode { get; set; }
    }
}
