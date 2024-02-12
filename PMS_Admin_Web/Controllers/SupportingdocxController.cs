using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models.Supportingdocx;
using System.Data.SqlClient;
using Dapper;

namespace PMS_Admin_Web.Controllers
{
    public class SupportingdocxController : Controller
    {
        private Connection sqlConnectionString = new();

        public IActionResult Cashreceived(string pname,string d1,string d2)
        {
            CashreceivedModel model = new();
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.cashreceivedList = connection.Query<Cashreceived>(@$"select aptno,tname,(select orderid from propertymaster where propertyref=(select pref from accountspm where payments.aptno=accountsPM.aptno and payments.pname=accountsPM.pname)) as orderid,rno,mrent,convert(varchar(24),format(rdate,'dd-MMM-yyyy'),113) as rdate, convert(varchar(24),format(rentdatefrom,'dd-MMM-yyyy'),113) as rentdatefrom,convert(varchar(24),format(rentdateto,'dd-MMM-yyyy'),113) rentdateto,paymenttype,cash as amt
                                                                        from payments
                                                                        where pname = '{pname}' and rdate between '{d1}' and '{d2}' and cash > 0
                                                                        order by orderid").ToList();
                connection.Close();
            }
            return View();
        }
    }

}
