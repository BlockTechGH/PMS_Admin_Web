using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Uname { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }
        [Required]
        public string Department { get; set; }
    }

    //public class LoginDAL
    //{
        //SqlConnection con = new SqlConnection("Server=(local)\\SQLEXPRESS;Initial Catalog=q8Realtor;Integrated Security=True;MultipleActiveResultSets=True");
        //public int LoginCheck(LoginViewModel ad)
        //{
        //    SqlCommand com = new SqlCommand("SP_Login", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.Parameters.AddWithValue("@Admin_id", ad.Username);
        //    com.Parameters.AddWithValue("@Password", ad.Password);
        //    com.Parameters.AddWithValue("@Department", ad.Department);
        //    SqlParameter oblogin = new SqlParameter();
        //    oblogin.ParameterName = "@Isvalid";
        //    oblogin.SqlDbType = SqlDbType.Bit;
        //    oblogin.Direction = ParameterDirection.Output;
        //    com.Parameters.Add(oblogin);
        //    con.Open();
        //    com.ExecuteNonQuery();
        //    int res = Convert.ToInt32(oblogin.Value);
        //    con.Close();
        //    return res;
        //}
    //}
}
