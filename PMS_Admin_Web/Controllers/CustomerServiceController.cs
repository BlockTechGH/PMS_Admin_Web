using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.LC;
using PMS_Admin_Web.Models.LOI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PMS_Admin_Web.Controllers
{
    public class CustomerServiceController : Controller
    {
        private Connection sqlConnectionString = new();

        public async Task<IActionResult> CsLOI(/*int page = 1, int pageSize = 10*/)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                ListLOIModel listLOIModel = new();
                
                {
                    var loiinformations = await connection.QueryAsync<Loiinformation>($"select top 50 LoiNo,loi_no LoiNo1,inqno,LEName,ClientName,ClientSource,PropertySource,PropertyName,PropertyRefNo,CONVERT(VARCHAR(11),Leasedate ,106) as Leasedate,LoiStatus,loimailstatus,Lccreate from loiInformation order by id desc");
                    listLOIModel.loiinformations = loiinformations.ToList();
                    //PagedList<PMS_Admin_Web.Models.Loiinformation> model = new PagedList<PMS_Admin_Web.Models.Loiinformation>(loi, page, pageSize);
                    listLOIModel.nocancelled = connection.Query<int>($"select count(*) from loiInformation where id in(select top 50 id from loiInformation order by id desc) and LoiStatus ='Cancelled' ").FirstOrDefault();
                    listLOIModel.noprogress = connection.Query<int>($"select count(*) from loiInformation where id in(select top 50 id from loiInformation order by id desc) and LoiStatus not in('Cancelled') ").FirstOrDefault();
                    return View(listLOIModel);
                }
                
                
            }

            //List<PMS_Admin_Web.Models.Loiinformation> loiinformations = context.Loiinformations.OrderByDescending(x => x.Id).ToList();
            ////var results = q.OrderByDescending(x => x.StatusId == 3 ? x.ReserveDate : x.LastUpdateDate);
            //PagedList<PMS_Admin_Web.Models.Loiinformation> model = new PagedList<PMS_Admin_Web.Models.Loiinformation>(loiinformations, page, pageSize);
            //return View(model);

        }

        public async Task<IActionResult> CsLC(/*int page = 1, int pageSize = 10*/)/*, string year = ""*/
        {
            //List<PMS_Admin_Web.Models.Lcinfo> lcinfo = context.Lcinfos.OrderByDescending(x => x.Id).ToList();
            ListLCModel listLCModel = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var lcinfo = await connection.QueryAsync<Lcinfo>(@$"select id,LC_no as LcNo,loi_no as LoiNo,Pname,Aptno,Tenname,Nationality,
                                                                            CONVERT(VARCHAR(11),Leasestart,106) as Leasestart,
                                                                            CONVERT(VARCHAR(11),Leaseend,106) as Leaseend,
                                                                            cast(deposit as decimal(34,3)) as deposit,
                                                                            cast(Rent as decimal(34,3)) Rent,
                                                                            cast(RESF as decimal(34,3)) RESF,
                                                                            CONVERT(VARCHAR(11),LCMadeon,106) as LCMadeon,ownRemarks,remarks,Source,
                                                                            CONVERT(VARCHAR(11),Orginallcsent,106) as originalllcsent,
                                                                            CONVERT(VARCHAR(11),rvsent,106) as rvsent 
                                                                            from lcinfo 
                                                                            where DATEPART(yyyy, lcmadeon)= datepart(yyyy, getdate())
                                                                             order by LC_no desc"); /*where DATEPART(yyyy, lcmadeon)= datepart(yyyy, getdate())*/
                listLCModel.lcinfos = lcinfo.ToList();
                listLCModel.cancelno = connection.Query<int>($"select count(*) from lcinfo where DATEPART(yyyy,lcmadeon)=datepart(yyyy,getdate()) and remarks='Cancelled' ").FirstOrDefault();
                listLCModel.appno = connection.Query<int>($"select count(*) from (select * from lcinfo where remarks not in('Cancelled') or remarks is null)src where DATEPART(yyyy,lcmadeon)=datepart(yyyy,getdate())").FirstOrDefault();


                //PagedList<PMS_Admin_Web.Models.Lcinfo> model = new PagedList<PMS_Admin_Web.Models.Lcinfo>(lcinfo.ToList(), page, pageSize);
                connection.Close();
                return View(listLCModel);
            }
        }
    }
}
