using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models.LC;
using PMS_Admin_Web.Models;
using System.Data.SqlClient;
using Dapper;
using X.PagedList;
using Microsoft.AspNetCore.Http;
using Aspose.Email;
using Aspose.Email.Clients.Smtp;
using Aspose.Email.Clients;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PMS_Admin_Web.Controllers
{
    public class LCController : Controller
    {
        private RealtorContext context = new();
        private Connection sqlConnectionString = new();
        private IWebHostEnvironment environment;
        CommonController commonController;

        public LCController(RealtorContext _context, IWebHostEnvironment _environment)
        {
            context = _context;
            environment = _environment;
        }

        public List<string> LoadProperties(string propertysource)
        {
            List<string> properties;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                properties = connection.Query<string>(@$"select distinct BldgName from propertymaster where PropertySource='{propertysource}'").ToList();
                connection.Close();
                return properties;
            }
        }

        public List<string> LoadApartmentNo(string propertyname)
        {
            List<string> aptno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                aptno = connection.Query<string>(@$"select AptNo from propertymaster where BldgName='{propertyname}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public JsonResult LoadRefNo(string propertyname, string aptno)
        {
            //List<string> refno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                
                connection.Open();
                //var refno = connection.Query<RefNoLoadModel>(@$"select p.propertyRef propertyRef,p.cname cname,p.crent crent,l.Tenname tenname,l.LC_no lcno,l.Rent rent
                //                                                from propertymaster p
                //                                                inner join lcinfo l on p.cleaseno=l.LC_no
                //                                                where BldgName='{propertyname}' and p.AptNo='{aptno}'").FirstOrDefault();

                var refno = connection.Query<RefNoLoadModel>(@$"select p.propertyRef propertyRef,p.cname cname,p.crent crent
                                                                from propertymaster p
                                                                where BldgName='{propertyname}' and p.AptNo='{aptno}'").FirstOrDefault();
                connection.Close();
                return Json(refno);
            }
        }

        public List<string> LoadLcNos(int LcYear)
        {
            List<string> LcNos;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                LcNos = connection.Query<string>($"select LC_no from lcinfo where LC_no like '%{LcYear}%' order by id desc").ToList();
                connection.Close();
            }
            return LcNos;
        }

        public JsonResult LoadNameRent(string LcNo)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var LcNameRent = connection.Query<LcNoLoadNameRentModel>(@$"select tenname tenname,rent from lcinfo where LC_no='{LcNo}'").FirstOrDefault();
                connection.Close();
                return Json(LcNameRent);
            }
        }

        public async Task<IActionResult> EditLC(int id, NewLCModel newLCModel, IFormFile passportfile, IFormFile civilidfile, IFormFile nocvisafile, IFormFile marriagecontractfile, IFormFile moclicensefile, IFormFile civilidASfile, IFormFile salarycertificatefile, IFormFile companysignfile, IFormFile staffemployeefile, IFormFile companyguaranteefile, IFormFile lcfile)
        {
            if(newLCModel.LcInfo != null)
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    int noofapt = 0;
                    var recordcount = connection.Query<int>($"select count(*) from renewallc where leaseno='{newLCModel.LcInfo.LcNo}'").FirstOrDefault();
                    if (recordcount > 0)
                    {
                        var maxlease = connection.Query<string>("select leaseno from renewallc where myid=(select max(myid) from renewallc where id =(select id from renewallc where leaseno='{newLCModel.LcInfo.LcNo}'))").FirstOrDefault();
                        if (maxlease != newLCModel.LcInfo.LcNo)
                        {
                            ViewBag.Message = "There is a Renewed LC;cannot update this LC";
                            //return View();
                            goto nationalities;
                        }
                    }

                    await connection.ExecuteAsync($"update lcinfo set legal='{newLCModel.LcInfo.Legal}' where lc_no='{newLCModel.LcInfo.LcNo}'");

                    var oldtenant = connection.Query<int>("select count(*) from tenantshistory where lease_no='{newLCModel.LcInfo.LcNo}'").FirstOrDefault();

                    if(oldtenant > 0)
                    {
                        ViewBag.Message = "This is old LC,Tenant Moved out update not possible.";
                        //return View();
                        goto nationalities;
                    }

                    string prstatus = "", paymentmode = "";
                    if (newLCModel.LcInfo.Lctype == "Renewal LC")
                    {
                        prstatus = "LC Renewal";
                    }
                    else
                    {
                        prstatus = "LC Created";
                    }

                    if(newLCModel.LcInfo.Lctype == "LC Transfer")
                    {
                        prstatus = "LC Transfer";
                    }

                    if (newLCModel.PaymentMethod == "Annually")
                        paymentmode = "AP";

                    if (newLCModel.PaymentMethod == "Semesterly")
                        paymentmode = "SP";

                    if (newLCModel.PaymentMethod == "Quarterly")
                        paymentmode = "QP";

                    if (newLCModel.PaymentMethod == "Monthly")
                        paymentmode = "MP";

                    if(newLCModel.UpdatedRenewalLC == "Yes")
                    {
                        if(!string.IsNullOrEmpty(newLCModel.filetxt))
                        {
                            ViewBag.Message = "Browse for the Updated LC";
                            //return View();
                            goto nationalities;
                        }
                    }

                    if(newLCModel.MultipleApartment)
                    {
                        if(newLCModel.AptRent == newLCModel.LcInfo.Rent)
                        {
                            ViewBag.Message = "Multiple Apartment is selected;Loi rent and apt rent cannot be same";
                            noofapt = newLCModel.NoOfApt;
                            //return View();
                            goto nationalities;
                        }
                    }
                    else
                    {
                        if (newLCModel.AptRent != newLCModel.LcInfo.Rent)
                        {
                            ViewBag.Message = "Multiple Apartment is not selected so the LOI rent and the Apartment rent should be same";
                            noofapt = 1;
                            //return View();
                            goto nationalities;
                        }
                    } 
                    
                    if(newLCModel.AptRent != newLCModel.LcInfo.Rent / noofapt)
                    {
                        ViewBag.Message = "No of Multiple Apartment is mismatching with the Apartment Rent";

                        //return View();
                        goto nationalities;
                    }

                    if(newLCModel.LcInfo.Payable > newLCModel.AptRent)
                    {
                        ViewBag.Message = "Payable rent can not be more than the apartment rent";

                        //return View();
                        goto nationalities;
                    }

                    var lcinfo = await connection.QueryAsync<Lcinfo>($"select id ,Remarks from lcinfo where lc_no='{newLCModel.LcInfo.LcNo}'");
                    var LOIRECORDCOUNT = connection.Query<int>($"select count(*) from lcinfo where lc_no='{newLCModel.LcInfo.LcNo}'' and id not IN({lcinfo.FirstOrDefault().Id})").FirstOrDefault();

                    if(lcinfo.FirstOrDefault().Remarks == "Cancelled")
                    {
                        ViewBag.Message = "Since the LC update modifies property master, the LC Update is not possible when the LC is Cancelled";

                        //return View();
                        goto nationalities;
                    }
                    
                    if (LOIRECORDCOUNT == 0)
                    {
                        int recordcount1 = 0;
                        if (newLCModel.MultipleApartment)
                        {
                            //recordcount1 = connection.Query<int>($"select count(*) from propertymaster where PropertyRef='" & Trim(ListBox1.Items(i)) & "'").FirstOrDefault();//have to do listing of propertyRef
                            List<int> countList = new();
                            foreach(var itemPref in newLCModel.GPrefList)
                            {
                                var count = connection.Query<int>($"select count(*) from propertymaster where PropertyRef='{itemPref}'").FirstOrDefault();
                                countList.Add(count);
                            }
                            foreach(var itemCount in countList)
                            {
                                if(itemCount == 1)
                                {
                                    recordcount1 = 1;
                                }
                                else
                                {
                                    recordcount1 = 0;
                                    //goto recordCountOneisZero;
                                    ViewBag.Message = "Check for the Property in the Property Master";
                                    //return View();
                                    goto nationalities;
                                }
                            }
                        }
                        else
                        {
                            recordcount1 = connection.Query<int>($"select count(*) as record from propertymaster where propertyref='{newLCModel.LcInfo.Pref}'").FirstOrDefault();
                        }

                        if(!newLCModel.Sublease)
                        {
                            var propertymaster = await connection.QueryAsync<Propertymaster>($"select moveout,vacatingstatus,cleaseno from propertymaster where propertyref='{newLCModel.LcInfo.Pref}' and moveout is not null ");
                            if(propertymaster.FirstOrDefault().Vacatingstatus == "Vacating" && propertymaster.FirstOrDefault().Cleaseno != newLCModel.LcInfo.LcNo)
                            {
                                if(propertymaster.FirstOrDefault().Moveout > newLCModel.LcInfo.Leasestart)
                                {
                                    ViewBag.Message = $"Lease cannot start , MoveOut date of existing tenant of this Property is {propertymaster.FirstOrDefault().Moveout}";

                                    //return View();
                                    goto nationalities;
                                }
                            }
                            int checkt = 0;
                            if (newLCModel.LcInfo.Leasestart <= DateTime.Today)
                            {
                                if(!newLCModel.MultipleApartment)
                                {
                                    checkt = connection.Query<int>($"select case when LEN(CNAME) is null then 0 else len(cname) end AS REC from PROPERTYMASTER WHERE propertyref='{newLCModel.LcInfo.Pref}'").FirstOrDefault();
                                    if(checkt == 0)
                                    {
                                        ViewBag.Message = "There is no tenant in Property Master.Tenant might have moved out.Check TLC";

                                        //return View();
                                        goto nationalities;
                                    }

                                    if(newLCModel.LcInfo.Lctype == "Renewal LC")
                                    {
                                        checkt = connection.Query<int>($"select count(*) as rec from PROPERTYMASTER WHERE propertyref='{newLCModel.LcInfo.Pref}' and len(cname)>0 AND  cname <> '{newLCModel.LcInfo.Tenname}' ").FirstOrDefault();
                                        if(checkt > 0)
                                        {
                                            ViewBag.Message = "There is already a Tenant.Do Vacating or quick fix it if it is same tenant";

                                            //return View();
                                            goto nationalities;
                                        }
                                    }
                                }
                            }
                        }

                        if(recordcount1 > 0)
                        {
                            if (passportfile != null)
                            {
                                string pppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/pp");
                                if (!Directory.Exists(pppath))
                                    Directory.CreateDirectory(pppath);

                                string ppextension = Path.GetExtension(passportfile.FileName);
                                string ppfileName = newLCModel.LcInfo.LcNo + ppextension;
                                string ppfileNameWithPath = Path.Combine(pppath, ppfileName);
                                if (System.IO.File.Exists(ppfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = ppfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var ppstream = new FileStream(ppfileNameWithPath, FileMode.Create);
                                passportfile.CopyTo(ppstream);
                                ppstream.Close();
                                newLCModel.LcInfo.Pp = ppfileNameWithPath;
                            }


                            if (civilidfile != null)
                            {
                                string cidpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cid");
                                if (!Directory.Exists(cidpath))
                                    Directory.CreateDirectory(cidpath);

                                string cidextension = Path.GetExtension(civilidfile.FileName);
                                string cidfileName = newLCModel.LcInfo.LcNo + cidextension;
                                string cidfileNameWithPath = Path.Combine(cidpath, cidfileName);
                                if (System.IO.File.Exists(cidfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cidfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cidstream = new FileStream(cidfileNameWithPath, FileMode.Create);
                                civilidfile.CopyTo(cidstream);
                                cidstream.Close();
                                newLCModel.LcInfo.Cid = cidfileNameWithPath;
                            }


                            if (nocvisafile != null)
                            {
                                string nocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/noc");
                                if (!Directory.Exists(nocpath))
                                    Directory.CreateDirectory(nocpath);

                                string nocextension = Path.GetExtension(nocvisafile.FileName);
                                string nocfileName = newLCModel.LcInfo.LcNo + nocextension;
                                string nocfileNameWithPath = Path.Combine(nocpath, nocfileName);
                                if (System.IO.File.Exists(nocfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = nocfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var nocstream = new FileStream(nocfileNameWithPath, FileMode.Create);
                                nocvisafile.CopyTo(nocstream);
                                nocstream.Close();
                                newLCModel.LcInfo.Noc = nocfileNameWithPath;
                            }


                            if (marriagecontractfile != null)
                            {
                                string mcpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/mc");
                                if (!Directory.Exists(mcpath))
                                    Directory.CreateDirectory(mcpath);

                                string mcextension = Path.GetExtension(marriagecontractfile.FileName);
                                string mcfileName = newLCModel.LcInfo.LcNo + mcextension;
                                string mcfileNameWithPath = Path.Combine(mcpath, mcfileName);
                                if (System.IO.File.Exists(mcfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = mcfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var mcstream = new FileStream(mcfileNameWithPath, FileMode.Create);
                                marriagecontractfile.CopyTo(mcstream);
                                mcstream.Close();
                                newLCModel.LcInfo.Mc = mcfileNameWithPath;
                            }


                            if (moclicensefile != null)
                            {
                                string mocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/moc");
                                if (!Directory.Exists(mocpath))
                                    Directory.CreateDirectory(mocpath);

                                string mocextension = Path.GetExtension(moclicensefile.FileName);
                                string mocfileName = newLCModel.LcInfo.LcNo + mocextension;
                                string mocfileNameWithPath = Path.Combine(mocpath, mocfileName);
                                if (System.IO.File.Exists(mocfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = mocfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var mocstream = new FileStream(mocfileNameWithPath, FileMode.Create);
                                moclicensefile.CopyTo(mocstream);
                                mocstream.Close();
                                newLCModel.LcInfo.Moc = mocfileNameWithPath;
                            }


                            if (civilidASfile != null)
                            {
                                string caspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cas");
                                if (!Directory.Exists(caspath))
                                    Directory.CreateDirectory(caspath);

                                string casextension = Path.GetExtension(civilidASfile.FileName);
                                string casfileName = newLCModel.LcInfo.LcNo + casextension;
                                string casfileNameWithPath = Path.Combine(caspath, casfileName);
                                if (System.IO.File.Exists(casfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = casfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var casstream = new FileStream(casfileNameWithPath, FileMode.Create);
                                civilidASfile.CopyTo(casstream);
                                casstream.Close();
                                newLCModel.LcInfo.Cas = casfileNameWithPath;
                            }


                            if (salarycertificatefile != null)
                            {
                                string coepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/coe");
                                if (!Directory.Exists(coepath))
                                    Directory.CreateDirectory(coepath);

                                string coeextension = Path.GetExtension(salarycertificatefile.FileName);
                                string coefileName = newLCModel.LcInfo.LcNo + coeextension;
                                string coefileNameWithPath = Path.Combine(coepath, coefileName);
                                if (System.IO.File.Exists(coefileNameWithPath))
                                {
                                    string fileNameWithPathTemp = coefileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var coestream = new FileStream(coefileNameWithPath, FileMode.Create);
                                salarycertificatefile.CopyTo(coestream);
                                coestream.Close();
                                newLCModel.LcInfo.Coe = coefileNameWithPath;
                            }


                            if (companysignfile != null)
                            {
                                string cosignpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cosign");
                                if (!Directory.Exists(cosignpath))
                                    Directory.CreateDirectory(cosignpath);

                                string cosignextension = Path.GetExtension(companysignfile.FileName);
                                string cosignfileName = newLCModel.LcInfo.LcNo + cosignextension;
                                string cosignfileNameWithPath = Path.Combine(cosignpath, cosignfileName);
                                if (System.IO.File.Exists(cosignfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cosignfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cosignstream = new FileStream(cosignfileNameWithPath, FileMode.Create);
                                companysignfile.CopyTo(cosignstream);
                                cosignstream.Close();
                                newLCModel.LcInfo.Cosign = cosignfileNameWithPath;
                            }


                            if (staffemployeefile != null)
                            {
                                string cidpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cidpp");
                                if (!Directory.Exists(cidpppath))
                                    Directory.CreateDirectory(cidpppath);

                                string cidppextension = Path.GetExtension(staffemployeefile.FileName);
                                string cidppfileName = newLCModel.LcInfo.LcNo + cidppextension;
                                string cidppfileNameWithPath = Path.Combine(cidpppath, cidppfileName);
                                if (System.IO.File.Exists(cidppfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cidppfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cidppstream = new FileStream(cidppfileNameWithPath, FileMode.Create);
                                staffemployeefile.CopyTo(cidppstream);
                                cidppstream.Close();
                                newLCModel.LcInfo.Cidpp = cidppfileNameWithPath;
                            }


                            if (companyguaranteefile != null)
                            {
                                string cgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cg");
                                if (!Directory.Exists(cgpath))
                                    Directory.CreateDirectory(cgpath);

                                string cgextension = Path.GetExtension(companyguaranteefile.FileName);
                                string cgfileName = newLCModel.LcInfo.LcNo + cgextension;
                                string cgfileNameWithPath = Path.Combine(cgpath, cgfileName);
                                if (System.IO.File.Exists(cgfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cgfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cgstream = new FileStream(cgfileNameWithPath, FileMode.Create);
                                companyguaranteefile.CopyTo(cgstream);
                                cgstream.Close();
                                newLCModel.LcInfo.Cg = cgfileNameWithPath;
                            }


                            if (lcfile != null)
                            {
                                string LCpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/lc");
                                if (!Directory.Exists(LCpath))
                                    Directory.CreateDirectory(LCpath);

                                string lcextension = Path.GetExtension(lcfile.FileName);
                                string lcfileName = newLCModel.LcInfo.LcNo + lcextension;
                                string lcfileNameWithPath = Path.Combine(LCpath, lcfileName);
                                if (System.IO.File.Exists(lcfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = lcfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var loistream = new FileStream(lcfileNameWithPath, FileMode.Create);
                                lcfile.CopyTo(loistream);
                                loistream.Close();
                                newLCModel.LcInfo.Lcpath = lcfileNameWithPath;
                            }

                            if (Convert.ToString(newLCModel.LcInfo.Orginallcsent).Length == 0 && Convert.ToString(newLCModel.LcInfo.Rvsent).Length > 1)
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.GroupPref}', Aptno = '{newLCModel.GroupAptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = null, rvsent = '{Convert.ToDateTime(newLCModel.LcInfo.Rvsent).ToString("yyyy-MM-dd")}', 103), dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103)  CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        //foreach(var itemPref in newLCModel.GPrefList)
                                        //{
                                        //    var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef='{itemPref}' ");
                                        //}
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef in ('{newLCModel.GroupPref}') ");
                                    }

                                }
                                else
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.LcInfo.Pref}', Aptno = '{newLCModel.LcInfo.Aptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = null, rvsent = '{Convert.ToDateTime(newLCModel.LcInfo.Rvsent).ToString("yyyy-MM-dd")}', dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef = '{newLCModel.LcInfo.Pref}' ");
                                    }

                                }
                            }
                            else if (Convert.ToString(newLCModel.LcInfo.Orginallcsent).Length == 0 && Convert.ToString(newLCModel.LcInfo.Rvsent).Length == 0)
                            {
                                if(newLCModel.MultipleApartment)
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.GroupPref}', Aptno = '{newLCModel.GroupAptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = null, rvsent = null, dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        //foreach(var itemPref in newLCModel.GPrefList)
                                        //{
                                        //    var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef='{itemPref}' ");
                                        //}
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef in ('{newLCModel.GroupPref}') ");
                                    }
                                }
                                else
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.LcInfo.Pref}', Aptno = '{newLCModel.LcInfo.Aptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = null, rvsent = null, dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef = '{newLCModel.LcInfo.Pref}' ");
                                    }
                                }
                            }
                            else if (Convert.ToString(newLCModel.LcInfo.Orginallcsent).Length > 1 && Convert.ToString(newLCModel.LcInfo.Rvsent).Length == 0)
                            {
                                if(newLCModel.MultipleApartment)
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.GroupPref}', Aptno = '{newLCModel.GroupAptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = '{Convert.ToDateTime(newLCModel.LcInfo.Orginallcsent).ToString("yyyy-MM-dd")}', rvsent = null, dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        //foreach(var itemPref in newLCModel.GPrefList)
                                        //{
                                        //    var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef='{itemPref}' ");
                                        //}
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef in ('{newLCModel.GroupPref}') ");
                                    }
                                }
                                else
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.LcInfo.Pref}', Aptno = '{newLCModel.LcInfo.Aptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = '{Convert.ToDateTime(newLCModel.LcInfo.Orginallcsent).ToString("yyyy-MM-dd")}', rvsent = null, dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103), lcsenttollon = CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103),
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef = '{newLCModel.LcInfo.Pref}' ");
                                    }
                                }
                            }

                            else
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.GroupPref}', Aptno = '{newLCModel.GroupAptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = '{Convert.ToDateTime(newLCModel.LcInfo.Orginallcsent).ToString("yyyy-MM-dd")}', rvsent = '{Convert.ToDateTime(newLCModel.LcInfo.Rvsent).ToString("yyyy-MM-dd")}', dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        //foreach(var itemPref in newLCModel.GPrefList)
                                        //{
                                        //    var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef='{itemPref}' ");
                                        //}
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef in ('{newLCModel.GroupPref}') ");
                                    }
                                }
                                else
                                {
                                    var updateLc = await connection.ExecuteAsync(@$"update lcinfo 
                                    set pmode='{paymentmode}',otherreason='{newLCModel.LcInfo.Otherreason}',renewreason='{newLCModel.LcInfo.Renewreason}',lc_no='{newLCModel.LcInfo.LcNo}',loi_no='{newLCModel.LcInfo.LoiNo}' ,
                                    lct = '{newLCModel.LcInfo.Lct}', lctype = '{newLCModel.LcInfo.Lctype}', pname = '{newLCModel.LcInfo.Pname}', pref = '{newLCModel.LcInfo.Pref}', Aptno = '{newLCModel.LcInfo.Aptno}',
                                    fstatus = '{newLCModel.LcInfo.Fstatus}', btype = '{newLCModel.LcInfo.Btype}', Tenname = '{newLCModel.LcInfo.Tenname}', lcmob = '{newLCModel.LcInfo.Lcmob}',
                                    Nationality = '{newLCModel.LcInfo.Nationality}', CIVILID = '{newLCModel.LcInfo.Civilid}', PPNO = '{newLCModel.LcInfo.Ppno}', COMPADDRESS = '{newLCModel.LcInfo.Compaddress}',
                                    Leasestart = '{Convert.ToDateTime(newLCModel.LcInfo.Leasestart).ToString("yyyy-MM-dd")}', Leaseend = '{Convert.ToDateTime(newLCModel.LcInfo.Leaseend).ToString("yyyy-MM-dd")}', type = '{newLCModel.LcInfo.Type}', period = '{newLCModel.LcInfo.Period}',
                                    deposit = '{newLCModel.LcInfo.Deposit}', payable = '{newLCModel.LcInfo.Payable}', Rent = '{newLCModel.LcInfo.Rent}', deppaid = '{newLCModel.LcInfo.Deppaid}', rentpaid = '{newLCModel.LcInfo.Rentpaid}',
                                    RESF = '{newLCModel.LcInfo.Resf}', LCMadeon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcmadeon).ToString("yyyy-MM-dd")}', ownremarks = '{newLCModel.LcInfo.Ownremarks}', Source = '{newLCModel.LcInfo.Source}',
                                    Orginallcsent = '{Convert.ToDateTime(newLCModel.LcInfo.Orginallcsent).ToString("yyyy-MM-dd")}', rvsent = '{Convert.ToDateTime(newLCModel.LcInfo.Rvsent).ToString("yyyy-MM-dd")}', dou = getdate(), enqtype = '{newLCModel.LcInfo.Enqtype}', ptype = '{newLCModel.LcInfo.Ptype}',
                                    lcreceivedfromtenanton = '{Convert.ToDateTime(newLCModel.LcInfo.Lcreceivedfromtenanton).ToString("yyyy-MM-dd")}', lcsenttollon = '{Convert.ToDateTime(newLCModel.LcInfo.Lcsenttollon).ToString("yyyy-MM-dd")}',
                                    moc='{newLCModel.LcInfo.Moc}',cidpp='{newLCModel.LcInfo.Cidpp}',cg='{newLCModel.LcInfo.Cg}',pp='{newLCModel.LcInfo.Pp}',cid='{newLCModel.LcInfo.Cid}',noc='{newLCModel.LcInfo.Noc}',mc='{newLCModel.LcInfo.Mc}',coe='{newLCModel.LcInfo.Coe}',cosign='{newLCModel.LcInfo.Cosign}',cas='{newLCModel.LcInfo.Cas}',lcpath='{newLCModel.LcInfo.Lcpath}'
                                    where lc_no = '{newLCModel.LCNo}'");//CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103) CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103) CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcreceivedfromtenanton}', 103) CONVERT(date, '{newLCModel.LcInfo.Lcsenttollon}', 103)

                                    if (!newLCModel.Sublease)
                                    {
                                        var updatePM = await connection.ExecuteAsync($"update propertymaster set status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart='{newLCModel.LcInfo.Leasestart}',rlend='{newLCModel.LcInfo.Leaseend}',pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='LCupdate' where PropertySource='ManagedProperty' and PropertyRef = '{newLCModel.LcInfo.Pref}' ");
                                    }
                                }
                            }

                            //if(Convert.ToString(newLCModel.LcInfo.Lcreceivedfromtenanton).Length > 0)
                            //{
                            //    await connection.ExecuteAsync($"update lcinfo set lcreceivedfromtenanton='{newLCModel.LcInfo.Lcreceivedfromtenanton}' where lc_no='{}'");
                            //}
                            //if (Convert.ToString(newLCModel.LcInfo.Lcsenttollon).Length > 0)
                            //{
                            //    await connection.ExecuteAsync($"update lcinfo set lcsenttollon='{newLCModel.LcInfo.Lcsenttollon}' where lc_no='{}'");
                            //}

                            if(newLCModel.LcInfo.Lctype == "Renewal LC")
                            {
                                var reccount = connection.Query<int>($"select count(*) from renewallc where leaseno='{newLCModel.LcInfo.LcNo}'").FirstOrDefault();
                                if(reccount > 0)
                                {
                                    var updateRenewalLc = await connection.ExecuteAsync($"update renewallc set leasestart='{newLCModel.LcInfo.Leasestart}',leaseend='{newLCModel.LcInfo.Leaseend}' where leaseno='{newLCModel.LcInfo.LcNo}'");
                                }
                                //else//insertion into renewalLC table
                                //{
                                //    var updateLcinfo = await connection.ExecuteAsync($"update lcinfo set renewalid=1 where lc_no='{newLCModel.LcInfo.LcNo}'");
                                //    var renewrecordcount = connection.Query<int>($"select count(*) from renewallc where leaseno='{newLCModel.LcInfo.LcNo}'").FirstOrDefault();
                                //    if(renewrecordcount == 0)
                                //    {
                                //        var lcid = connection.Query<int>($"select id from lcinfo where lc_no='{newLCModel.LcInfo.LcNo}'").FirstOrDefault();
                                //    }
                                //}
                            }

                            if (newLCModel.UpdatedRenewalLC == "Yes")
                            {
                                int lenoflist = 0;
                                await connection.ExecuteAsync($"update lcinfo set updateandrenewed='YES' where lc_no='{newLCModel.LCNo}'");
                                if(newLCModel.NoOfApt == 0)
                                {
                                    lenoflist = 1;
                                }
                                else
                                {
                                    lenoflist = newLCModel.NoOfApt + 1;
                                }

                                //saving of updated or renewed LC file
                            }

                            string tidupdate = "";
                            if (newLCModel.MultipleApartment)
                            {
                                
                                foreach (var itemPref in newLCModel.GPrefList)
                                {
                                    var tid = connection.Query<int>($"select tid from propertymaster where propertyref='{itemPref}'").FirstOrDefault();
                                    if(tid > 0)
                                    {
                                        tidupdate = "true";
                                    }
                                    else
                                    {
                                        tidupdate = "false";
                                    }

                                    if(tidupdate == "true")
                                    {
                                        var tdetails = await connection.QueryAsync<Tdetail>($"select * from tdetails where id={tid}");

                                        if (newLCModel.PaymentMethod == "Annually")
                                            paymentmode = "Yearly";

                                        if (newLCModel.PaymentMethod == "Semesterly")
                                            paymentmode = "Semi-Annually";

                                        if (newLCModel.PaymentMethod == "Quarterly")
                                            paymentmode = "Quaterly";

                                        if (newLCModel.PaymentMethod == "Monthly")
                                            paymentmode = "Monthly";

                                        var updateTdetails = await connection.ExecuteAsync($"update tdetails set lastupdatedtname='{newLCModel.LcInfo.Tenname}',lastupdatedtrent='{newLCModel.LcInfo.Rent}',lastupdatedtprent='{newLCModel.LcInfo.Payable}',lastupdatednat='{newLCModel.LcInfo.Nationality}',lastupdatedttype='{newLCModel.LcInfo.Fstatus}',lastupdatedtbed='{newLCModel.LcInfo.Btype}',lastupdatedtmode='{paymentmode}',lastupdatedtctpe='{newLCModel.LcInfo.Enqtype}' where id={tid}");
                                    }
                                }
                            }
                            else
                            {
                                var tid = connection.Query<int>($"select tid from propertymaster where propertyref='{newLCModel.LcInfo.Pref}'").FirstOrDefault();
                                if (tid > 0)
                                {
                                    tidupdate = "true";
                                }
                                else
                                {
                                    tidupdate = "false";
                                }

                                if (tidupdate == "true")
                                {
                                    var tdetails = await connection.QueryAsync<Tdetail>($"select * from tdetails where id={tid}");

                                    if (newLCModel.PaymentMethod == "Annually")
                                        paymentmode = "Yearly";

                                    if (newLCModel.PaymentMethod == "Semesterly")
                                        paymentmode = "Semi-Annually";

                                    if (newLCModel.PaymentMethod == "Quarterly")
                                        paymentmode = "Quaterly";

                                    if (newLCModel.PaymentMethod == "Monthly")
                                        paymentmode = "Monthly";

                                    var updateTdetails = await connection.ExecuteAsync($"update tdetails set lastupdatedtname='{newLCModel.LcInfo.Tenname}',lastupdatedtrent='{newLCModel.LcInfo.Rent}',lastupdatedtprent='{newLCModel.LcInfo.Payable}',lastupdatednat='{newLCModel.LcInfo.Nationality}',lastupdatedttype='{newLCModel.LcInfo.Fstatus}',lastupdatedtbed='{newLCModel.LcInfo.Btype}',lastupdatedtmode='{paymentmode}',lastupdatedtctpe='{newLCModel.LcInfo.Enqtype}' where id={tid}");
                                }
                            }

                            var loidept = connection.Query<string>($"select dept from loiinformation where loi_no='{newLCModel.LcInfo.LoiNo}'").FirstOrDefault();
                            if(newLCModel.LcInfo.LoiNo.Length > 0)
                            {
                                if (loidept == "Admin")
                                {
                                    var updateLOI = await connection.ExecuteAsync($"update loiinformation set payremarks='PAYMENTS UPDATED IN LOI AFTER LC',rent='{newLCModel.LcInfo.Rent}' ,deposit='{newLCModel.LcInfo.Deposit}' ,clientresf='{newLCModel.LcInfo.Resf}' where loi_no='{newLCModel.LcInfo.LoiNo}'");
                                }
                                if(newLCModel.MultipleApartment)
                                {
                                    await connection.ExecuteAsync($"UPDATE [dbo].[LOIInformation] SET clientname='{newLCModel.LcInfo.Tenname}', [Leasedate] ='{Convert.ToDateTime(newLCModel.LcInfo.Leasestart)}' ,Leaseenddate='{Convert.ToDateTime(newLCModel.LcInfo.Leaseend)}' WHERE loi_no='{newLCModel.LcInfo.LoiNo}'");
                                }
                                else
                                {
                                    await connection.ExecuteAsync($"UPDATE [dbo].[LOIInformation] SET clientname='{newLCModel.LcInfo.Tenname}' , APTNO='{newLCModel.LcInfo.Aptno}',propertyrefno='{newLCModel.LcInfo.Pref}' ,[Leasedate] ='{Convert.ToDateTime(newLCModel.LcInfo.Leasestart)}' ,Leaseenddate='{Convert.ToDateTime(newLCModel.LcInfo.Leaseend)}' WHERE loi_no='{newLCModel.LcInfo.LoiNo}'");
                                }
                            }

                            if(newLCModel.oldCivilId != null)
                            {
                                await connection.ExecuteAsync($"update tenantentry set te_cid='{newLCModel.LcInfo.Civilid}' where te_cid='{newLCModel.oldCivilId}'");
                            }

                            ViewBag.Message = "LC is Updated";

                            if(newLCModel.Sublease)
                            {
                                if (newLCModel.PaymentMethod == "Annually")
                                    paymentmode = "AP";

                                if (newLCModel.PaymentMethod == "Semesterly")
                                    paymentmode = "SP";

                                if (newLCModel.PaymentMethod == "Quarterly")
                                    paymentmode = "QP";

                                if (newLCModel.PaymentMethod == "Monthly")
                                    paymentmode = "MP";

                                await connection.ExecuteAsync($"UPDATE SUBLEASES SET name='{newLCModel.LcInfo.Tenname}',lstart='{newLCModel.LcInfo.Leasestart}',lend='{newLCModel.LcInfo.Leaseend}',LCRENT='{newLCModel.LcInfo.Rent}',ACTUALRENT='{newLCModel.LcInfo.Payable}',NATIONALITY='{newLCModel.LcInfo.Nationality}',PAYMODE='{paymentmode}',BED='{newLCModel.LcInfo.Btype}',TTYPE='{newLCModel.LcInfo.Enqtype}',FTYPE='{newLCModel.LcInfo.Fstatus}' WHERE lno='{newLCModel.LcInfo.LcNo}' and pname='{newLCModel.LcInfo.Pname}'");
                                ViewBag.Message = "Sublease updated, Property database not updated";
                            }
                        }
                        else
                        {
                            //recordCountOneisZero:
                            ViewBag.Message = "Check for the Property in the Property Master";
                            //return View();
                            goto nationalities;
                        }
                    }
                    else
                    {
                        ViewBag.Message = $"LC NO Duplication for the LC {newLCModel.LcInfo.LcNo}";
                        //return View();
                        goto nationalities;
                    }


                    //Lcinfo lcinfo = new();
                    //lcinfo = newLCModel.LcInfo;
                    //context.Lcinfos.Update(lcinfo);
                    //await context.SaveChangesAsync();
                    //{
                    //    connection.Execute($"update propertymaster set cname='{newLCModel.LcInfo.Tenname}',cmob='{newLCModel.LcInfo.Lcmob}',cnat='{newLCModel.LcInfo.Nationality}',cleaseno='{newLCModel.LcInfo.LcNo}',crent='{newLCModel.LcInfo.Rent}',Status='NotAvailable',leasestart='CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103)',leaseend='CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103)' where PropertyRef='{newLCModel.LcInfo.Pref}'");
                    //}

                    ModelState.Clear();
                    
                    connection.Close();
                }
            }
            else
            {
                newLCModel.LcInfo = context.Lcinfos.Find(id);
                if (newLCModel.LcInfo.Pref.Contains(","))
                {
                    newLCModel.MultipleApartment = true;
                    newLCModel.GroupAptno = newLCModel.LcInfo.Aptno;
                    newLCModel.GroupPref = newLCModel.LcInfo.Pref;
                    newLCModel.LCNo = newLCModel.LcInfo.LcNo;
                    string[] noofapt = newLCModel.LcInfo.Pref.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    newLCModel.NoOfApt = noofapt.Count();
                    //newLCModel.GPrefList = new();
                    newLCModel.GPrefList = newLCModel.LcInfo.Pref.Split(',').ToList();
                }
                else
                {
                    newLCModel.AptRent = newLCModel.LcInfo.Rent;
                }
            }

            nationalities:
            //newLCModel.Nationalities = context.Nationalities.ToList();
            return View(newLCModel);
        }

        public async Task<IActionResult> NewLC(NewLCModel newLCModel, IFormFile passportfile, IFormFile civilidfile, IFormFile nocvisafile, IFormFile marriagecontractfile, IFormFile moclicensefile, IFormFile civilidASfile, IFormFile salarycertificatefile, IFormFile companysignfile, IFormFile staffemployeefile, IFormFile companyguaranteefile, IFormFile lcfile, IFormFile loifile)
        {
            if (newLCModel.LcInfo != null)
            {
                try
                {
                    string prstatus = "";
                    if(newLCModel.LcInfo.Id <= 0)
                    {
                        if(newLCModel.LcInfo.Lctype == "New LC")
                        {
                            prstatus = "LC Created";
                        }
                        if(newLCModel.LcInfo.Lctype == "LC Transfer")
                        {
                            prstatus = "LC Transfer";
                        }

                        using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                        {
                            connection.Open();

                            var loirecordCount = connection.Query<int>($"select count(*) from lcinfo where lc_no='{newLCModel.LcInfo.LcNo}' ").FirstOrDefault();
                            if(loirecordCount > 0)
                            {
                                ViewBag.Message = $"LC NO Duplication for the LC {newLCModel.LcInfo.LcNo}";
                                //return View();
                                goto nationalities;
                            }

                            var recordCount = connection.Query<int>($"select count(*) from propertymaster where propertyref='{newLCModel.LcInfo.Pref}'").FirstOrDefault();
                            
                            if(!newLCModel.Sublease)
                            {
                                var propertymaster = await connection.QueryAsync<Propertymaster>($"select moveout,vacatingstatus,reservation from propertymaster where propertyref='{newLCModel.LcInfo.Pref}' and moveout is not null ");
                                var checkr = connection.Query<int>($"SELECT COUNT(*) FROM PROPERTYMASTER WHERE propertyref='{newLCModel.LcInfo.Pref}' AND RESERVATION='yes' AND leaseno not in('{newLCModel.LcInfo.LoiNo}')").FirstOrDefault();
                                int checkt=0;
                                if (Convert.ToDateTime(newLCModel.LcInfo.Leasestart) <= DateTime.Today)
                                {
                                    if(newLCModel.LcInfo.Lctype != "Renewal LC")
                                    {
                                        checkt = connection.Query<int>($"select count(*) from PROPERTYMASTER WHERE propertyref='{newLCModel.LcInfo.Pref}' AND len(cname)>1 ").FirstOrDefault();
                                    }
                                }
                                if(checkr > 0)
                                {
                                    ViewBag.Message = "There is already a Reservation. Pls Clear it first";
                                    //return View();
                                    goto nationalities;
                                }
                                if(checkt > 0)
                                {
                                    ViewBag.Message = "There is already a Tenant. Do Vacating";
                                    //return View();
                                    goto nationalities;
                                }

                                if(propertymaster.Count() != 0)
                                {
                                    if (propertymaster.FirstOrDefault().Vacatingstatus == "Vacating")
                                    {
                                        if (Convert.ToDateTime(propertymaster.FirstOrDefault().Moveout) > Convert.ToDateTime(newLCModel.LcInfo.Leasestart))
                                        {
                                            ViewBag.Message = $"Lease cannot start. MoveOut date of existing tenant of this Property is {propertymaster.FirstOrDefault().Moveout}";
                                            //return View();
                                            goto nationalities;
                                        }
                                    }
                                }
                            }

                            string LOIpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/pp");
                            if (!Directory.Exists(LOIpppath))
                                Directory.CreateDirectory(LOIpppath);
                            if (passportfile == null)
                            {
                                newLCModel.LcInfo.Pp = "";
                            }
                            else
                            {
                                string ppextension = Path.GetExtension(passportfile.FileName);
                                string ppfileName = newLCModel.LcInfo.LcNo + ppextension;
                                string ppfileNameWithPath = Path.Combine(LOIpppath, ppfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var ppstream = new FileStream(ppfileNameWithPath, FileMode.Create);
                                passportfile.CopyTo(ppstream);
                                ppstream.Close();
                                newLCModel.LcInfo.Pp = ppfileNameWithPath;
                            }

                            string LOIcidpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cid");
                            if (!Directory.Exists(LOIcidpath))
                                Directory.CreateDirectory(LOIcidpath);
                            if (civilidfile == null)
                            {
                                newLCModel.LcInfo.Cid = "";
                            }
                            else
                            {
                                string cidextension = Path.GetExtension(civilidfile.FileName);
                                string cidfileName = newLCModel.LcInfo.LcNo + cidextension;
                                string cidfileNameWithPath = Path.Combine(LOIcidpath, cidfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var cidstream = new FileStream(cidfileNameWithPath, FileMode.Create);
                                civilidfile.CopyTo(cidstream);
                                cidstream.Close();
                                newLCModel.LcInfo.Cid = cidfileNameWithPath;
                            }

                            string LOInocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/noc");
                            if (!Directory.Exists(LOInocpath))
                                Directory.CreateDirectory(LOInocpath);
                            if (nocvisafile == null)
                            {
                                newLCModel.LcInfo.Noc = "";
                            }
                            else
                            {
                                string nocextension = Path.GetExtension(nocvisafile.FileName);
                                string nocfileName = newLCModel.LcInfo.LcNo + nocextension;
                                string nocfileNameWithPath = Path.Combine(LOInocpath, nocfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var nocstream = new FileStream(nocfileNameWithPath, FileMode.Create);
                                nocvisafile.CopyTo(nocstream);
                                nocstream.Close();
                                newLCModel.LcInfo.Noc = nocfileNameWithPath;
                            }

                            string LOImcpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/mc");
                            if (!Directory.Exists(LOImcpath))
                                Directory.CreateDirectory(LOImcpath);
                            if (marriagecontractfile == null)
                            {
                                newLCModel.LcInfo.Mc = "";
                            }
                            else
                            {
                                string mcextension = Path.GetExtension(marriagecontractfile.FileName);
                                string mcfileName = newLCModel.LcInfo.LcNo + mcextension;
                                string mcfileNameWithPath = Path.Combine(LOImcpath, mcfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var mcstream = new FileStream(mcfileNameWithPath, FileMode.Create);
                                marriagecontractfile.CopyTo(mcstream);
                                mcstream.Close();
                                newLCModel.LcInfo.Mc = mcfileNameWithPath;
                            }

                            string LOImocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/moc");
                            if (!Directory.Exists(LOImocpath))
                                Directory.CreateDirectory(LOImocpath);
                            if (moclicensefile == null)
                            {
                                newLCModel.LcInfo.Moc = "";
                            }
                            else
                            {
                                string mocextension = Path.GetExtension(moclicensefile.FileName);
                                string mocfileName = newLCModel.LcInfo.LcNo + mocextension;
                                string mocfileNameWithPath = Path.Combine(LOImocpath, mocfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var mocstream = new FileStream(mocfileNameWithPath, FileMode.Create);
                                moclicensefile.CopyTo(mocstream);
                                mocstream.Close();
                                newLCModel.LcInfo.Moc = mocfileNameWithPath;
                            }

                            string LOIcaspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cas");
                            if (!Directory.Exists(LOIcaspath))
                                Directory.CreateDirectory(LOIcaspath);
                            if (civilidASfile == null)
                            {
                                newLCModel.LcInfo.Cas = "";
                            }
                            else
                            {
                                string casextension = Path.GetExtension(civilidASfile.FileName);
                                string casfileName = newLCModel.LcInfo.LcNo + casextension;
                                string casfileNameWithPath = Path.Combine(LOIcaspath, casfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var casstream = new FileStream(casfileNameWithPath, FileMode.Create);
                                civilidASfile.CopyTo(casstream);
                                casstream.Close();
                                newLCModel.LcInfo.Cas = casfileNameWithPath;
                            }

                            string LOIcoepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/coe");
                            if (!Directory.Exists(LOIcoepath))
                                Directory.CreateDirectory(LOIcoepath);
                            if (salarycertificatefile == null)
                            {
                                newLCModel.LcInfo.Coe = "";
                            }
                            else
                            {
                                string coeextension = Path.GetExtension(salarycertificatefile.FileName);
                                string coefileName = newLCModel.LcInfo.LcNo + coeextension;
                                string coefileNameWithPath = Path.Combine(LOIcoepath, coefileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var coestream = new FileStream(coefileNameWithPath, FileMode.Create);
                                salarycertificatefile.CopyTo(coestream);
                                coestream.Close();
                                newLCModel.LcInfo.Coe = coefileNameWithPath;
                            }

                            string LOIcosignpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cosign");
                            if (!Directory.Exists(LOIcosignpath))
                                Directory.CreateDirectory(LOIcosignpath);
                            if (companysignfile == null)
                            {
                                newLCModel.LcInfo.Cosign = "";
                            }
                            else
                            {
                                string cosignextension = Path.GetExtension(companysignfile.FileName);
                                string cosignfileName = newLCModel.LcInfo.LcNo + cosignextension;
                                string cosignfileNameWithPath = Path.Combine(LOIcosignpath, cosignfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var cosignstream = new FileStream(cosignfileNameWithPath, FileMode.Create);
                                companysignfile.CopyTo(cosignstream);
                                cosignstream.Close();
                                newLCModel.LcInfo.Cosign = cosignfileNameWithPath;
                            }

                            string LOIcidpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cidpp");
                            if (!Directory.Exists(LOIcidpppath))
                                Directory.CreateDirectory(LOIcidpppath);
                            if (staffemployeefile == null)
                            {
                                newLCModel.LcInfo.Cidpp = "";
                            }
                            else
                            {
                                string cidppextension = Path.GetExtension(staffemployeefile.FileName);
                                string cidppfileName = newLCModel.LcInfo.LcNo + cidppextension;
                                string cidppfileNameWithPath = Path.Combine(LOIcidpppath, cidppfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var cidppstream = new FileStream(cidppfileNameWithPath, FileMode.Create);
                                staffemployeefile.CopyTo(cidppstream);
                                cidppstream.Close();
                                newLCModel.LcInfo.Cidpp = cidppfileNameWithPath;
                            }

                            string LOIcgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cg");
                            if (!Directory.Exists(LOIcgpath))
                                Directory.CreateDirectory(LOIcgpath);
                            if (companyguaranteefile == null)
                            {
                                newLCModel.LcInfo.Cg = "";
                            }
                            else
                            {
                                string cgextension = Path.GetExtension(companyguaranteefile.FileName);
                                string cgfileName = newLCModel.LcInfo.LcNo + cgextension;
                                string cgfileNameWithPath = Path.Combine(LOIcgpath, cgfileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var cgstream = new FileStream(cgfileNameWithPath, FileMode.Create);
                                companyguaranteefile.CopyTo(cgstream);
                                cgstream.Close();
                                newLCModel.LcInfo.Cg = cgfileNameWithPath;
                            }

                            string LCpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/lc");
                            if (!Directory.Exists(LCpath))
                                Directory.CreateDirectory(LCpath);
                            if (lcfile == null)
                            {
                                newLCModel.LcInfo.Lcpath = "";
                            }
                            else
                            {
                                string loiextension = Path.GetExtension(lcfile.FileName);
                                string loifileName = newLCModel.LcInfo.LcNo + loiextension;
                                string loifileNameWithPath = Path.Combine(LCpath, loifileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var loistream = new FileStream(loifileNameWithPath, FileMode.Create);
                                lcfile.CopyTo(loistream);
                                loistream.Close();
                                newLCModel.LcInfo.Lcpath = loifileNameWithPath;
                            }

                            string LOIpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/loi");
                            if (!Directory.Exists(LOIpath))
                                Directory.CreateDirectory(LOIpath);
                            if (loifile == null)
                            {
                                newLCModel.LcInfo.Loipath = "";
                            }
                            else
                            {
                                string loiextension = Path.GetExtension(loifile.FileName);
                                string loifileName = newLCModel.LcInfo.LoiNo + loiextension;
                                string loifileNameWithPath = Path.Combine(LOIpath, loifileName);
                                //if (System.IO.File.Exists(fileNameWithPath))
                                //{
                                //    string fileNameWithPathTemp = fileNameWithPath;
                                //    System.IO.File.Delete(fileNameWithPath);
                                //    fileNameWithPath = fileNameWithPathTemp;
                                //}
                                var loistream = new FileStream(loifileNameWithPath, FileMode.Create);
                                loifile.CopyTo(loistream);
                                loistream.Close();
                                newLCModel.LcInfo.Loipath = loifileNameWithPath;
                            }

                            if (newLCModel.LcInfo.Orginallcsent == null && newLCModel.LcInfo.Rvsent != null)
                            {
                                if(newLCModel.MultipleApartment)
                                {
                                    var insert = await connection.ExecuteAsync(@$"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.GroupPref}', '{newLCModel.GroupAptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    null, CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103), getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");//multiple apartment insertion. have to change pref and aptno
                                }
                                else
                                {
                                    var insert = await connection.ExecuteAsync(@$"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.LcInfo.Pref}', '{newLCModel.LcInfo.Aptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    null, CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103), getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");//single apartment insertion
                                }

                                //Lcinfo lcinfo = new();
                                //lcinfo = newLCModel.LcInfo;
                                //context.Lcinfos.Add(lcinfo);
                                //await context.SaveChangesAsync();

                                if (!string.IsNullOrEmpty(newLCModel.LcInfo.LoiNo))
                                    connection.Execute($"update LOIInformation set lccreate='lc-created' where loi_no='{newLCModel.LcInfo.LoiNo}'");

                                if(!newLCModel.Sublease)
                                {
                                    if(newLCModel.MultipleApartment)
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO',status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef in ({newLCModel.GroupPref})");//group pref
                                    }
                                    else
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO'status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef='{newLCModel.LcInfo.Pref}' ");
                                    }
                                }

                                ModelState.Clear();
                                ViewBag.Message = "New LC is created";
                            }

                            else if(newLCModel.LcInfo.Orginallcsent == null && newLCModel.LcInfo.Rvsent == null)
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var insert = await connection.ExecuteAsync(@$"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.GroupPref}', '{newLCModel.GroupAptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    null, null, getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");//pref and aptno to be changed according to multiple apt
                                }
                                else
                                {
                                    var insert = await connection.ExecuteAsync($@"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.LcInfo.Pref}', '{newLCModel.LcInfo.Aptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    null, null, getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");
                                }

                                if (!string.IsNullOrEmpty(newLCModel.LcInfo.LoiNo))
                                    connection.Execute($"update LOIInformation set lccreate='lc-created' where loi_no='{newLCModel.LcInfo.LoiNo}'");

                                if (!newLCModel.Sublease)
                                {
                                    if (newLCModel.MultipleApartment)
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO',status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef in ({newLCModel.GroupPref})");//group pref
                                    }
                                    else
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO'status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef='{newLCModel.LcInfo.Pref}' ");
                                    }
                                }

                                ModelState.Clear();
                                ViewBag.Message = "New LC is created";
                            }

                            else if (newLCModel.LcInfo.Orginallcsent != null && newLCModel.LcInfo.Rvsent == null)
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var insert = await connection.ExecuteAsync($@"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.GroupPref}', '{newLCModel.GroupAptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103), null, getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");//group apt no and group pref
                                }
                                else
                                {
                                    var insert = await connection.ExecuteAsync($@"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.LcInfo.Pref}', '{newLCModel.LcInfo.Aptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103), null, getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}','{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");
                                }

                                if (!string.IsNullOrEmpty(newLCModel.LcInfo.LoiNo))
                                    connection.Execute($"update LOIInformation set lccreate='lc-created' where loi_no='{newLCModel.LcInfo.LoiNo}'");

                                if (!newLCModel.Sublease)
                                {
                                    if (newLCModel.MultipleApartment)
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO',status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef in ({newLCModel.GroupPref})");//group pref
                                    }
                                    else
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO'status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef='{newLCModel.LcInfo.Pref}' ");
                                    }
                                }

                                ModelState.Clear();
                                ViewBag.Message = "New LC is created";
                            }
                            else
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var insert = await connection.ExecuteAsync($@"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,legal,Datefrom,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.GroupPref}', '{newLCModel.GroupAptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103), CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103), getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}',
                                    '{newLCModel.LcInfo.Legal}', CONVERT(date, '{newLCModel.LcInfo.DateFrom}', 103),'{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");//group apt and pref
                                }
                                else
                                {
                                    var insert = await connection.ExecuteAsync($@"insert into lcinfo(enqtype,lctype,renewreason,lct,renewalof,renewalid,LC_no,loi_no,
                                    ptype,Pname,pref,Aptno,fstatus,btype,Tenname,lcmob,Nationality,CIVILID,PPNO,COMPADDRESS,
                                    Leasestart,Leaseend,type,period,deposit,Rent,payable,RESF,rentpaid,deppaid,LCMadeon,ownremarks,
                                    Source,Orginallcsent,rvsent,doc,otherreason,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc,pmode,legal,Datefrom,lcpath,loipath)
                                    values('{newLCModel.LcInfo.Enqtype}','{newLCModel.LcInfo.Lctype}','{newLCModel.LcInfo.Renewreason}','{newLCModel.LcInfo.Lct}',
                                    '{newLCModel.LcInfo.Renewalof}', 0, '{newLCModel.LcInfo.LcNo}', '{newLCModel.LcInfo.LoiNo}', '{newLCModel.LcInfo.Ptype}',
                                    '{newLCModel.LcInfo.Pname}', '{newLCModel.LcInfo.Pref}', '{newLCModel.LcInfo.Aptno}', '{newLCModel.LcInfo.Fstatus}',
                                    '{newLCModel.LcInfo.Btype}', '{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Lcmob}', '{newLCModel.LcInfo.Nationality}',
                                    '{newLCModel.LcInfo.Civilid}', '{newLCModel.LcInfo.Ppno}', '{newLCModel.LcInfo.Compaddress}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),
                                    CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103), '{newLCModel.LcInfo.Type}', '{newLCModel.LcInfo.Period}', '{newLCModel.LcInfo.Deposit}',
                                    '{newLCModel.LcInfo.Rent}', '{newLCModel.LcInfo.Payable}', '{newLCModel.LcInfo.Resf}', '{newLCModel.LcInfo.Rentpaid}',
                                    '{newLCModel.LcInfo.Deppaid}', CONVERT(date, '{newLCModel.LcInfo.Lcmadeon}', 103), '{newLCModel.LcInfo.Ownremarks}', '{newLCModel.LcInfo.Source}',
                                    CONVERT(date, '{newLCModel.LcInfo.Orginallcsent}', 103), CONVERT(date, '{newLCModel.LcInfo.Rvsent}', 103), getdate(), '{newLCModel.LcInfo.Otherreason}',
                                    '{newLCModel.LcInfo.Pp}', '{newLCModel.LcInfo.Cid}', '{newLCModel.LcInfo.Noc}', '{newLCModel.LcInfo.Mc}', '{newLCModel.LcInfo.Coe}',
                                    '{newLCModel.LcInfo.Cosign}', '{newLCModel.LcInfo.Cas}', '{newLCModel.LcInfo.Cidpp}', '{newLCModel.LcInfo.Cg}', '{newLCModel.LcInfo.Moc}', '{newLCModel.LcInfo.Pmode}',
                                    '{newLCModel.LcInfo.Legal}', CONVERT(date, '{newLCModel.LcInfo.DateFrom}', 103),'{newLCModel.LcInfo.Lcpath}','{newLCModel.LcInfo.Loipath}')");
                                }

                                if (!string.IsNullOrEmpty(newLCModel.LcInfo.LoiNo))
                                    connection.Execute($"update LOIInformation set lccreate='lc-created' where loi_no='{newLCModel.LcInfo.LoiNo}'");

                                if (!newLCModel.Sublease)
                                {
                                    if (newLCModel.MultipleApartment)
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO',status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef in ({newLCModel.GroupPref})");//group pref
                                    }
                                    else
                                    {
                                        var update = await connection.ExecuteAsync($"update propertymaster set leasetype='{newLCModel.LcInfo.Lctype}',paid='NO',status='NotAvailable',rstatus='{prstatus}',reservation='yes',leaseno='{newLCModel.LcInfo.LcNo}',reservedfor='{newLCModel.LcInfo.Tenname}',rnat='{newLCModel.LcInfo.Nationality}',rmob='{newLCModel.LcInfo.Lcmob}',reservedrent='{newLCModel.AptRent}',rftype='{newLCModel.LcInfo.Fstatus}',rbtype='{newLCModel.LcInfo.Btype}',rlstart=CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),rlend=CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),pupdatetime=getdate(),pupdateby='{Environment.MachineName}',pmode='NewLC' where PropertySource='ManagedProperty' and PropertyRef='{newLCModel.LcInfo.Pref}' ");
                                    }
                                }

                                ModelState.Clear();
                                ViewBag.Message = "New LC is created";
                            }

                            if(newLCModel.Sublease)
                            {
                                var insertLcchanges = await connection.ExecuteAsync($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('New SUBLEASE',' {newLCModel.LcInfo.Pname}  Apt- {newLCModel.LcInfo.Aptno}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','{newLCModel.LcInfo.LcNo}')");
                            }
                            else
                            {
                                if (newLCModel.MultipleApartment)
                                {
                                    var insertLcchanges = await connection.ExecuteAsync($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('New LC','{newLCModel.LcInfo.Pname} Apt- {newLCModel.GroupAptno}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','{newLCModel.LcInfo.LcNo}')");
                                }
                                else
                                {
                                    var insertLcchanges = await connection.ExecuteAsync($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('New LC','{newLCModel.LcInfo.Pname} Apt- {newLCModel.LcInfo.Aptno}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','{newLCModel.LcInfo.LcNo}')");
                                }
                            }

                            if (newLCModel.Sublease)
                            {
                                string paymentmode = "", loitype = "";
                                if(newLCModel.PaymentMethod == "Annually")
                                    paymentmode = "AP";
                                
                                if (newLCModel.PaymentMethod == "Semesterly")
                                    paymentmode = "SP";
                                
                                if (newLCModel.PaymentMethod == "Quarterly")
                                    paymentmode = "QP";
                                
                                if (newLCModel.PaymentMethod == "Monthly")
                                    paymentmode = "MP";
                                
                                if(newLCModel.LcInfo.Enqtype == "Individual Enquiry")
                                {
                                    loitype = "Individual Enquiry";
                                }
                                else
                                {
                                    loitype = "Corporate Enquiry";
                                }
                                var chksublease = connection.Query<int>($"select count(*) from subleases where PNAME='{newLCModel.LcInfo.Pname}' AND APTNO='{newLCModel.LcInfo.Aptno}'").FirstOrDefault();
                                if(chksublease > 0)
                                {
                                    var updateSublease = await connection.ExecuteAsync($"update subleases set active='0' where pname='{newLCModel.LcInfo.Pname}' and aptno='{newLCModel.LcInfo.Aptno}'");
                                }
                                var insertSublease = await connection.ExecuteAsync($@"insert into subleases(name,pname,aptno,pref,lno,slno,doc,active,NATIONALITY,LCRENT,ACTUALRENT,PAYMODE,FTYPE,TTYPE,BED,lstart,lend)
                                values('{newLCModel.LcInfo.Tenname}', '{newLCModel.LcInfo.Pname}', '{newLCModel.LcInfo.Aptno}', '{newLCModel.LcInfo.Pref}',
                                '{newLCModel.LcInfo.LcNo}', null, getdate(), 1, '{newLCModel.LcInfo.Nationality}', '{newLCModel.LcInfo.Rent}',
                                '{newLCModel.LcInfo.Payable}', '{paymentmode}', '{newLCModel.LcInfo.Fstatus}', '{loitype}',
                                '{newLCModel.LcInfo.Btype}', CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103), CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103))");
                                ViewBag.Message1 = "Sublease also created";
                            }

                            if(newLCModel.renewlcno != null)
                            {
                                await connection.ExecuteAsync($"update lcinfo set renewalid=1 where lc_no='{newLCModel.renewlcno}'");

                                var renewrecordcount = connection.Query<int>($"select count(*) from renewallc where leaseno='{newLCModel.renewlcno}'").FirstOrDefault();
                                if(renewrecordcount == 0)
                                {
                                    var lcid = connection.Query<int>($"select id from lcinfo where lc_no='{newLCModel.renewlcno}'").FirstOrDefault();
                                    var insert1 = await connection.ExecuteAsync($"insert into renewallc(id,leaseno,leasestart,leaseend,doc)values({lcid},'{newLCModel.renewlcno}',CONVERT(date, '{newLCModel.renewls}', 103),CONVERT(date, '{newLCModel.renewle}', 103),getdate())");
                                    var insert2 = await connection.ExecuteAsync($"insert into renewallc(id,leaseno,leasestart,leaseend,doc)values({lcid},'{newLCModel.LcInfo.LcNo}',CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),getdate())");
                                }
                                else
                                {
                                    var lcid = connection.Query<int>($"select id from renewallc where LEASENO='{newLCModel.renewlcno}'").FirstOrDefault();
                                    var insert = await connection.ExecuteAsync($"insert into renewallc(id,leaseno,leasestart,leaseend,doc)values({lcid},'{newLCModel.LcInfo.LcNo}',CONVERT(date, '{newLCModel.LcInfo.Leasestart}', 103),CONVERT(date, '{newLCModel.LcInfo.Leaseend}', 103),getdate())");
                                }
                            }
                            
                            connection.Close();
                        }
                        //have to do some more functions same as in application
                        
                    }
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Unknown Error";
                }
            }

            nationalities:
            newLCModel.LcInfo = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var count = connection.Query<int>(@"select count(*) from lcinfo where year(getdate())=year(lcmadeon)").FirstOrDefault();

                if (count == 0)
                {
                    newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0001";
                }
                else
                {
                    var newlcno = connection.Query<int>(@"select right(lc_no,4)+1 from lcinfo where id=(select max(id) from lcinfo where year(getdate())=year(lcmadeon))").FirstOrDefault();

                    if(newlcno <= 9)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-"+ "000" + newlcno;
                    else if(newlcno > 9 && newlcno <= 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "00" + newlcno;
                    else if(newlcno > 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0" + newlcno;
                    else
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "000" + newlcno;
                }
            }
            //newLCModel.Nationalities = context.Nationalities.ToList();
            
            return View(newLCModel);
        }

        public async Task<IActionResult> ListLC(/*int page = 1, int pageSize = 10*/)/*, string year = ""*/
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
                                                                            CONVERT(VARCHAR(11),Orginallcsent,106) as Orginallcsent,
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

        public ActionResult ViewLC(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select Lcpath from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                if (file != "")
                {
                    byte[] abc = System.IO.File.ReadAllBytes(file);
                    System.IO.File.WriteAllBytes(file, abc);
                    MemoryStream ms = new MemoryStream(abc);
                    return new FileStreamResult(ms, "application/pdf");
                }
                else
                {
                    return RedirectToAction("ListLC", "LC");
                }
            }
        }

        public ActionResult ViewPassport(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select pp from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCid(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select cid from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewNoc(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select noc from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewMc(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select mc from LOIInformation where ID={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCoe(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select coe from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCosign(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select cosign from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCas(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select cas from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCidpp(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select cidpp from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewCg(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select cg from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public ActionResult ViewMoc(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select moc from lcinfo where id={id}").FirstOrDefault();
                connection.Close();
                byte[] abc = System.IO.File.ReadAllBytes(file);
                System.IO.File.WriteAllBytes(file, abc);
                MemoryStream ms = new MemoryStream(abc);
                return new FileStreamResult(ms, "application/pdf");
                //if (file != "")
                //{

                //}
                //else
                //{
                //    return RedirectToAction("ListLOI", "LOI");
                //}
            }
        }

        public string DeleteLC(int Id)
        {
            try
            {
                var deleteLC = context.Lcinfos.Find(Id);
                context.Lcinfos.Remove(deleteLC);
                context.SaveChanges();
                ViewBag.Message = "Deleted Successfully";
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }
            return ViewBag.Message;
        }

        public IActionResult ExpiringContracts(/*int page = 1, int pageSize = 10*/)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                List<PMS_Admin_Web.Models.Lcinfo> lcinfosExpiring = connection.Query<Lcinfo>($@"select id,renewalid,leasestart,leaseend,lc_no as LcNo,lct,pname,aptno 
                                                                                    from(select id,renewalid,LC_no,loi_no,Pname,lct,Aptno,Tenname,Nationality,
                                                                                    CONVERT(VARCHAR(11), Leasestart,106) as Leasestart,
                                                                                    CONVERT(VARCHAR(11),Leaseend,106) as  Leaseend,
                                                                                    cast(deposit as decimal(34,3)) as deposit,
                                                                                    cast(Rent as decimal(34,3)) Rent,
                                                                                    cast(RESF as decimal(34,3)) RESF,
                                                                                    CONVERT(VARCHAR(11),LCMadeon,106) as LCMadeon,ownRemarks,Source,
                                                                                    CONVERT(VARCHAR(11),Orginallcsent,106) as originalllcsent,
                                                                                    CONVERT(VARCHAR(11),rvsent,106) as rvsent from lcinfo where renewalid <>1 or renewalid is null)src 
                                                                                    where leaseend between getdate() and DATEADD(day,90, GETDATE()) AND lct='Not Renewable' and renewalid=0 order by convert(datetime, leaseend, 103) ASC").ToList();
                //PagedList<PMS_Admin_Web.Models.Lcinfo> model = new PagedList<PMS_Admin_Web.Models.Lcinfo>(lcinfosExpiring, page, pageSize);
                connection.Close();
                return View(lcinfosExpiring);
            }
        }

        public IActionResult MailIdBox(MailIdBoxModel mailIdBoxModel)
        {
            if(mailIdBoxModel.emailuser != null)//insertion into emailuser table
            {
                try
                {
                    Emailuser emailuser = new();
                    emailuser.Mailid = mailIdBoxModel.emailuser.Mailid;
                    emailuser.Dept = HttpContext.Session.GetString("CurrentUserDepartment");
                    emailuser.Mailstatus = mailIdBoxModel.emailuser.Mailstatus;
                    context.Emailusers.Add(emailuser);
                    context.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = "Successfully Added";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Unknown Error";
                }
            }
            mailIdBoxModel.emailuser = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                if(HttpContext.Session.GetString("CurrentUserRole") == "PA")
                {
                    mailIdBoxModel.TO = connection.Query<Emailuser>(@$"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserRole")}' and mailstatus='to'").ToList();
                    mailIdBoxModel.CC = connection.Query<Emailuser>($@"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserRole")}' and  mailstatus='cc'").ToList();
                }
                else
                {
                    mailIdBoxModel.TO = connection.Query<Emailuser>(@$"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and mailstatus='to'").ToList();
                    mailIdBoxModel.CC = connection.Query<Emailuser>($@"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and  mailstatus='cc'").ToList();
                }
            }
            return View(mailIdBoxModel);
        }

        public IActionResult DeleteMailID(int id)
        {
            if(id != 0)
            { 
                try
                {
                    var deleteMailId = context.Emailusers.Find(id);
                    context.Remove(deleteMailId);
                    context.SaveChanges();
                    //ViewBag.Message = "Successfully Deleted";
                }
                catch(Exception ex)
                {
                    ViewBag.Message = "Unknown Error";
                }
            }
            MailIdBoxModel mailIdBoxModel = new();
            mailIdBoxModel.emailuser = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                mailIdBoxModel.TO = connection.Query<Emailuser>(@$"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and mailstatus='to'").ToList();
                mailIdBoxModel.CC = connection.Query<Emailuser>($@"select mailid,id 
                                                                from emailusers 
                                                                where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' and  mailstatus='cc'").ToList();
            }
            return View("MailIdBox", mailIdBoxModel);
        }

        public async Task<string> UpdateLCNo(string oldlcno, string newlcno)
        {
            if (oldlcno != null && newlcno != null)
            {
                try
                {
                    using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                    {
                        connection.Open();
                        var isExist = await connection.QueryAsync<Lcinfo>($"select * from lcinfo where LC_no = '{oldlcno}'");
                        var Exist = await connection.QueryAsync<int>($"select count(1) from lcinfo where LC_no = '{newlcno}'");
                        if(Exist.FirstOrDefault() != 1)
                        {
                            Lcinfo lcinfo = new();
                            lcinfo = isExist.FirstOrDefault();
                            lcinfo.LcNo = newlcno;
                            context.Update(lcinfo);
                            context.SaveChanges();
                            var ExistLCinPM = connection.Query<int>($"select COUNT(*) from propertymaster where leaseno='{oldlcno}'").FirstOrDefault();
                            if(ExistLCinPM == 1)
                            {
                                var updateLCinPM = await connection.ExecuteAsync($"update propertymaster set leaseno='{newlcno}' where PropertyRef='{lcinfo.Pref}'");
                            }
                                

                            ViewBag.Message = "Updated";
                        }
                        else
                        {
                            ViewBag.Message = "New LC No already exist";
                        }
                        
                        ModelState.Clear();

                        
                        //lCNoFixingModel.OldLcNo = null;
                        //lCNoFixingModel.NewLcNo = null;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Unknown Error";
                }
                
            }
            return ViewBag.Message;
        }
        
        public IActionResult RentChange(RentChangeModel rentChangeModel, IFormFile approvednoticefile)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (rentChangeModel.rentchange != null)
                {
                    try
                    {
                        if (!rentChangeModel.NoLC)
                        {
                            if (rentChangeModel.rentchange.Lcno == null)
                            {
                                ViewBag.Message = "Select the Lease NO";
                                goto gotoreturn;
                            }
                        }
                        if (approvednoticefile == null)
                        {
                            ViewBag.Message = "Attach the Approved Notice";
                            goto gotoreturn;
                        }
                        if (rentChangeModel.rentchange.Newrent == null)
                        {
                            ViewBag.Message = "Enter the New Rent";
                            goto gotoreturn;
                        }
                        if (rentChangeModel.ChangeInLC != "LC Changes" && rentChangeModel.ChangeInLC != "LC stays as it is")
                        {
                            ViewBag.Message = "Mention the LC stays as it is or change in LC";
                            goto gotoreturn;
                        }

                        var recordcount = connection.Query<int>($"SELECT COUNT(*) AS REC FROM RENTCHANGE WHERE PREF='{rentChangeModel.rentchange.Pref}' AND EFFECTFROM='{rentChangeModel.rentchange.Effectfrom}' AND NEWRENT='{rentChangeModel.rentchange.Newrent}'").FirstOrDefault();
                        if (recordcount > 0)
                        {
                            ViewBag.Message = "Seems like already rentchange is done for this property for this particular time";
                            goto gotoreturn;
                        }

                        //var fileNameWithPath = commonController.UploadFile("rentchange", rentChangeModel.postedFile, rentChangeModel.postedFile.FileName, rentChangeModel.rentchange.Approvednotice, rentChangeModel.rentchange.Lcno);
                        ////file saving
                        //string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/rentchange");
                        //if (!Directory.Exists(path))
                        //    Directory.CreateDirectory(path);
                        //FileInfo fileInfo = new FileInfo(rentChangeModel.postedFile.FileName);
                        //string fileName = rentChangeModel.rentchange.Approvednotice + rentChangeModel.rentchange.Lcno + fileInfo.Extension;
                        //string fileNameWithPath = Path.Combine(path, fileName);
                        //using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                        //{
                        //    rentChangeModel.postedFile.CopyTo(stream);
                        //}


                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/rentchange");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        string extension = Path.GetExtension(approvednoticefile.FileName);
                        string fileName = rentChangeModel.rentchange.Approvednotice + rentChangeModel.rentchange.Lcno + extension;
                        string fileNameWithPath = Path.Combine(path, fileName);

                        //if (System.IO.File.Exists(fileNameWithPath))
                        //{
                        //    string fileNameWithPathTemp = fileNameWithPath;
                        //    System.IO.File.Delete(fileNameWithPath);
                        //    fileNameWithPath = fileNameWithPathTemp;
                        //}

                        var stream = new FileStream(fileNameWithPath, FileMode.Create);
                        approvednoticefile.CopyTo(stream);
                        rentChangeModel.rentchange.Approvednotice = fileNameWithPath;

                        if (!rentChangeModel.NoLC)
                        {
                            connection.Execute(@$"insert into rentchange (lcno,pref,oldrent,newrent,effectfrom,remarks,approvednotice,doc,rentchangeid)values('{rentChangeModel.rentchange.Lcno}','{rentChangeModel.rentchange.Pref}','{rentChangeModel.rentchange.Oldrent}','{rentChangeModel.rentchange.Newrent}',CONVERT(date,'{rentChangeModel.rentchange.Effectfrom}', 103),'{rentChangeModel.rentchange.Remarks}','{rentChangeModel.rentchange.Approvednotice}',getdate(),0)");
                            connection.Execute($@"update propertymaster set cleaseno='{rentChangeModel.rentchange.Lcno}' where propertyref='{rentChangeModel.rentchange.Pref}'");
                            connection.Execute($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('RENT CHANGE','{rentChangeModel.PropertyName} Apt - {rentChangeModel.AptNo}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','{rentChangeModel.rentchange.Lcno}')");
                        }
                        else
                        {
                            connection.Execute(@$"insert into rentchange (lcno,pref,oldrent,newrent,effectfrom,remarks,approvednotice,doc,rentchangeid)values('NO-LC-NO','{rentChangeModel.rentchange.Pref}','{rentChangeModel.rentchange.Oldrent}','{rentChangeModel.rentchange.Newrent}',CONVERT(date,'{rentChangeModel.rentchange.Effectfrom}', 103),'{rentChangeModel.rentchange.Remarks}','{rentChangeModel.rentchange.Approvednotice}',getdate(),0)");
                            connection.Execute($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('RENT CHANGE','{rentChangeModel.PropertyName} Apt - {rentChangeModel.AptNo}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','NO-LC-NO')");
                        }

                        ViewBag.Message = "Successfully Updated";//have to do mailing also

                        //RentChangeModel NewrentChangeModel = new();
                        //rentChangeModel = NewrentChangeModel;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Unknown Error";
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    //javascript
                    //rentChangeModel.propertynames = connection.Query<Propertymaster>(@$"select distinct(BldgName) from propertymaster where PropertySource='ManagedProperty'").ToList();
                    rentChangeModel.MailToList = connection.Query<string>($"select mailid from emailusers where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id").ToList();
                    foreach (var tomail in rentChangeModel.MailToList)
                    {
                        if (rentChangeModel.MailTo == null)
                        {
                            rentChangeModel.MailTo = tomail.Trim();
                        }
                        else
                        {
                            rentChangeModel.MailTo = rentChangeModel.MailTo + ";" + tomail.Trim();
                        }
                    }
                }
                
                connection.Close();
            }
            
            gotoreturn:
            return View(rentChangeModel);
        }

        public IActionResult EditRentChange(int id, RentChangeModel rentChangeModel, IFormFile approvednoticefile)
        {
            //RentChangeModel rentChangeModel = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if(rentChangeModel.rentchange == null)
                {
                    var rentchangedetails = connection.Query<Rentchange>($"select id,approvednotice,remarks,lcno,pref,newrent,CONVERT(VARCHAR(11),effectfrom,106) as effectfrom from rentchange where id='{id}'").FirstOrDefault();

                    rentChangeModel.rentchange = rentchangedetails;

                    //rentChangeModel.propertynames = connection.Query<Propertymaster>(@$"select distinct(BldgName) from propertymaster where PropertySource='ManagedProperty'").ToList();
                }
                else
                {
                    try
                    {
                        if(approvednoticefile == null)
                        {
                            if (rentChangeModel.rentchange.Approvednotice == null)
                            {
                                ViewBag.Message = "Attach the Approved Notice";
                                goto gotoreturn;
                            }
                        }
                        
                        if (rentChangeModel.rentchange.Newrent == null)
                        {
                            ViewBag.Message = "Enter the New Rent";
                            goto gotoreturn;
                        }

                        if(approvednoticefile != null)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/rentchange");
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            string fileName = rentChangeModel.rentchange.Approvednotice + rentChangeModel.rentchange.Lcno /*+ fileInfo.Extension*/;
                            string fileNameWithPath = Path.Combine(path, fileName);
                            var stream = new FileStream(fileNameWithPath, FileMode.Create);
                            approvednoticefile.CopyTo(stream);
                            rentChangeModel.rentchange.Approvednotice = fileNameWithPath;
                        }
                        
                        var update = connection.Execute(@$"update rentchange set effectfrom=CONVERT(date,'{rentChangeModel.rentchange.Effectfrom}', 103),pref='{rentChangeModel.rentchange.Pref}',lcno='{rentChangeModel.rentchange.Lcno}',newrent='{rentChangeModel.rentchange.Newrent}',remarks='{rentChangeModel.rentchange.Remarks}',approvednotice='{rentChangeModel.rentchange.Approvednotice}' where id={rentChangeModel.rentchange.Id}");
                        var updatePM = connection.Execute($@"update propertymaster set cleaseno='{rentChangeModel.rentchange.Lcno}' where propertyref='{rentChangeModel.rentchange.Pref}'");
                        var insertLCC = connection.Execute($@"INSERT INTO LCCHANGES(CATEGORY,FEATURE,REASON,Change_DateTime,status,changeby,lcno)values('RENT CHANGE - MODIFIED','{rentChangeModel.PropertyName} Apt - {rentChangeModel.AptNo}','',getdate(),'ACTIVE','{HttpContext.Session.GetString("CurrentUsername")}','{rentChangeModel.rentchange.Lcno}')");
                        ViewBag.Message = "Successfully Updated";//have to do mailing also  
                    }
                    catch(Exception ex)
                    {
                        ViewBag.Message = "Unknown Error";
                        Console.WriteLine(ex);
                    }
                }

                connection.Close();
            }
            //return View("RentChange", rentChangeModel);
            gotoreturn:
            return View(rentChangeModel);
        }

        public string RentChangeEmail(string changeinrent, string propertyname, string aptno, string tenantname, decimal? oldrent, decimal? newrent, DateTime? effectfrom, string remarks, string mailto, string mailcc, string attachment)
        {
            string rentmode1 = "";
            string message = "";
            if (changeinrent == "Rent Increase")
            {
                rentmode1 = "Increased";
            }
            else
            {
                rentmode1 = "Decreased";
            }
            var MailTo = "";
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var MailToList = connection.Query<string>($"select mailid from emailusers where dept='{HttpContext.Session.GetString("CurrentUserDepartment")}' order by id").ToList();
                foreach (var tomail in MailToList)
                {
                    if (MailTo == null)
                    {
                        MailTo = tomail.Trim();
                    }
                    else
                    {
                        MailTo = MailTo + ";" + tomail.Trim();
                    }
                }
                connection.Close();
            }
                

            Microsoft.Office.Interop.Outlook.Application Outlook = new Microsoft.Office.Interop.Outlook.Application();

            string strMsg1 = "";
            strMsg1 = strMsg1 + @$"<html>
                                    <body>
                                        <p style=""font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; font-size: 12px"">
                                            <b>Dear ALL:</b>
                                            <br />
                                            <br />
                                            Please find attached<u><b>Approval of {changeinrent}</b></u> for the following:
                                            <br />
                                            <ul>
                                                <li>
                                                    <label style=""color: red;""><u><b>{propertyname} - Apt # {aptno}</b></u></label>
                                                    rented by <u><b><label style=""color: red;""> {tenantname}</label></b></u>
                                                    at KD {oldrent} {rentmode1} to KD {newrent} w.e.f <b>{Convert.ToDateTime(effectfrom).Day} {Convert.ToDateTime(effectfrom).Month} {Convert.ToDateTime(effectfrom).Year}</b>
                                                </li>
                                            </ul>
                                            <br />
                                            <br />
                                            Note: {remarks}
                                            <br />
                                            <br />
                                            This is for your information and records.
                                            <br />
                                            <br />
                                            <br />
                                            <label style=""color:blue""><b>THIS EMAIL IS GENERATED THROUGH PMS</b></label>
                                        </p>
                                    </body>
                                  </html>";
            var Mail = Outlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
            Mail.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
            Mail.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            Mail.Subject = $"Approval for {changeinrent} - {propertyname} - Apt# {aptno}";
            Mail.To = MailTo;
            Mail.CC = "business@q8realtor.com; operations@q8realtor.com";
            Mail.HTMLBody = strMsg1;
            message = "Mail Generated";
            ((Microsoft.Office.Interop.Outlook._MailItem)Mail).Display();

            //MailMessage message = new MailMessage();
            //message.Subject = $"Approval for {changeinrent} - {propertyname} - Apt# {aptno}";
            //message.HtmlBody = @$"<html>
            //                        <body>
            //                            <p style=""font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; font-size: 12px"">
            //                                <b>Dear ALL:</b>
            //                                <br />
            //                                <br />
            //                                Please find attached<u><b>Approval of {changeinrent}</b></u> for the following:
            //                                <br />
            //                                <ul>
            //                                    <li>
            //                                        <label style=""color: red;""><u><b>{propertyname} - Apt # {aptno}</b></u></label>
            //                                        rented by <u><b><label style=""color: red;""> {tenantname}</label></b></u>
            //                                        at KD {oldrent} {rentmode1} to KD {newrent} w.e.f <b>{Convert.ToDateTime(effectfrom).Day} {Convert.ToDateTime(effectfrom).Month} {Convert.ToDateTime(effectfrom).Year}</b>
            //                                    </li>
            //                                </ul>
            //                                <br />
            //                                <br />
            //                                Note: {remarks}
            //                                <br />
            //                                <br />
            //                                This is for your information and records.
            //                                <br />
            //                                <br />
            //                                <br />
            //                                <label style=""color:blue""><b>THIS EMAIL IS GENERATED THROUGH PMS</b></label>
            //                            </p>
            //                        </body>
            //                      </html>";
            //message.From = new MailAddress("narutouzumaki95415@gmail.com", "Tester", false);
            //message.To.Add(new MailAddress(mailto, "Recipient 1", false));
            //message.CC.Add(new MailAddress(mailcc, "Recipient 3", false));
            //if (attachment != null)
            //    message.Attachments.Add(new Attachment(attachment));
            //message.Save("EmailMessage.eml", SaveOptions.DefaultEml);

            //MailMessage msg = MailMessage.Load("EmailMessage.eml");
            //SmtpClient client = new SmtpClient();
            //client.Host = "mail.server.com";
            //client.Username = "username";
            //client.Password = "password";
            //client.Port = 587;
            //client.SecurityOptions = SecurityOptions.SSLExplicit;

            //client.SendAsync(msg);

            //Console.WriteLine("Sending message...");

            //string response = "Mail Sent";
            //msg.Dispose();

            return message;
        }

        public IActionResult ListRentChange(/*int page = 1, int pageSize = 10*/)
        {
            List<ListRentChangeModel> listRentChanges = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                listRentChanges = connection.Query<ListRentChangeModel>($@"select id,pref,(select bldgname from propertymaster where PropertyRef =pref) as PropertyName,
                                                                        (select aptno from propertymaster where PropertyRef =pref) as AptNo,lcno as LCNo,
                                                                        cast(oldrent as decimal(34,3)) as OldRent,
                                                                        cast(newrent as decimal(34,3)) as NewRent,
                                                                        CONVERT(VARCHAR(11),effectfrom,106) as EffectFrom,
                                                                        CONVERT(VARCHAR(11),doc,106) as Created 
                                                                        from rentchange 
                                                                        order by id desc").ToList();
                //PagedList<PMS_Admin_Web.Models.LC.ListRentChangeModel> model = new PagedList<PMS_Admin_Web.Models.LC.ListRentChangeModel>(listRentChanges, page, pageSize);
                connection.Close();
                return View(listRentChanges);
            }

        }

        public IActionResult ListSublease(/*int page = 1, int pageSize = 10*/)
        {
            List<Sublease> subleases = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                subleases = connection.Query<Sublease>($@"select nationality,lcrent,actualrent,paymode,ftype,ttype,
                                                    bed,ID, pname,aptno,lno,slno,name,
                                                    (select rent from lcinfo where lc_no=lno) as rent ,active 
                                                    from subleases 
                                                    where active=1 
                                                    order by id desc").ToList();
                //PagedList<PMS_Admin_Web.Models.Sublease> model = new PagedList<PMS_Admin_Web.Models.Sublease>(subleases, page, pageSize);
                connection.Close();
                return View(subleases);
            }
        }

        public IActionResult LcChanges(/*int page = 1, int pageSize = 10*/)
        {
            List<Lcchange> lcchanges = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                lcchanges = connection.Query<Lcchange>($@"select id,Category,Feature,Reason,Change_DateTime as ChangeDateTime,status,changeby,oldupdate,lcno 
                                                    from lcchanges 
                                                    where status='ACTIVE' 
                                                    order by id desc").ToList();
                //PagedList<PMS_Admin_Web.Models.Lcchange> model = new PagedList<PMS_Admin_Web.Models.Lcchange>(lcchanges, page, pageSize);
                connection.Close();
                return View(lcchanges);
            }
        }

        public async Task<IActionResult> VacatingAndTlc()
        {
            VacatingsAndTlcModel vacatingsAndTlcModel = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var vacatingList = await connection.QueryAsync<VacatingsAndTlcGridModel>(@$"select propertyref Pref,BldgName as PropertyName,aptno as AptNo,cftype as Type,cbtype as Bed,cnat as Nationality,cname as TenantName,cmob as ContactNo,
                                                                                            CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
                                                                                            CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
                                                                                            CONVERT(VARCHAR(11),moveout,106) as MoveOutOn,vacatingstatus,
                                                                                            case when len((select bmapproval from moveout where pref =propertyref and id=moveoutid))<1 then 0 when len((select bmapproval from moveout where pref =propertyref and id=moveoutid)) is null then '0' else 1 end as record,
                                                                                            case when convert(datetime, moveout, 103)<=convert(datetime, getdate(), 103) then 'yes' else 'no' end as todayv 
                                                                                            from propertymaster 
                                                                                            where vacatingstatus='Vacating' 
                                                                                            order by convert(datetime, moveout, 103) ASC");//id as Id,
                vacatingsAndTlcModel.vacatingList = vacatingList.ToList();

                var pendingVacating = await connection.QueryAsync<VacatingsAndTlcGridModel>(@$"select pref Pref,id,pname PropertyName,aptno AptNo,ftype Type,btype Bed,tenantname,nationality,contact ContactNo,
                                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
                                                                                                CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
                                                                                                CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
                                                                                                case when len((select moi  from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,
                                                                                                case when len((select bmapproval  from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord 
                                                                                                from tenantshistory 
                                                                                                WHERE STATUS='PENDING' 
                                                                                                order by id desc");
                vacatingsAndTlcModel.pendingVacating = pendingVacating.ToList();

                //var tlc = await connection.QueryAsync<VacatingsAndTlcGridModel>(@$"select pref,id,pname as PropertyName,aptno AptNo,ftype Type,btype Bed,tenantname TenantName,nationality Nationality,contact ContactNo,
                //                                                                        CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
                //                                                                        CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
                //                                                                        CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
                //                                                                        case when len((select moi from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,
                //                                                                        case when len((select bmapproval from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord 
                //                                                                        from tenantshistory 
                //                                                                        WHERE STATUS='CLOSED' 
                //                                                                        order by convert(datetime, closedate, 103) desc");
                //vacatingsAndTlcModel.tlc = tlc.ToList();

                connection.Close();
            }

            vacatingsAndTlcModel.tlc = new();
            var query = await context.Tenantshistories.Where(s => s.Status == "CLOSED").OrderByDescending(s => s.Closedate).ToListAsync();
            foreach (var item in query)
            {
                VacatingsAndTlcGridModel modelgrid = new();
                modelgrid.Id = item.Id;
                modelgrid.PropertyName = item.Pname;
                modelgrid.AptNo = item.Aptno;
                modelgrid.Type = item.Ftype;
                modelgrid.Bed = item.Btype;
                modelgrid.TenantName = item.Tenantname;
                modelgrid.LeaseStartDate = Convert.ToDateTime(item.Leasestart);
                modelgrid.LeaseEndDate = Convert.ToDateTime(item.Leaseend);
                modelgrid.ContactNo = item.Contact;
                modelgrid.Nationality = item.Nationality;
                modelgrid.MoveOutOn = Convert.ToDateTime(item.Movedate);
                modelgrid.Pref = item.Pref;

                vacatingsAndTlcModel.tlc.Add(modelgrid);
            }
            return View(vacatingsAndTlcModel);
        }

        public IActionResult VacatingsAndTlc(VacatingsAndTlcModel vm)
        {
            if(vm == null)
            {
                vm = new VacatingsAndTlcModel()
                {
                    ActiveTab = VacatingsAndTlcTab.VacatingList
                };
            }
            
            return PartialView(vm);
        }

        

        public IActionResult VacatingList(int page = 1, int pageSize = 10)
        {
            //VacatingsAndTlcModel vacatingsAndTlcModel = new();
            List<VacatingsAndTlcGridModel> gridmodel = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                gridmodel = connection.Query<VacatingsAndTlcGridModel>(@$"select id as Id,propertyref,BldgName as PropertyName,aptno as AptNo,cftype as Type,cbtype as Bed,cnat as Nationality,cname as TenantName,cmob as ContactNo,
                                                                                                CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
                                                                                                CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
                                                                                                CONVERT(VARCHAR(11),moveout,106) as MoveOutOn,vacatingstatus,
                                                                                                case when len((select bmapproval from moveout where pref =propertyref and id=moveoutid))<1 then 0 when len((select bmapproval from moveout where pref =propertyref and id=moveoutid)) is null then '0' else 1 end as record,
                                                                                                case when convert(datetime, moveout, 103)<=convert(datetime, getdate(), 103) then 'yes' else 'no' end as todayv 
                                                                                                from propertymaster 
                                                                                                where vacatingstatus='Vacating' 
                                                                                                order by convert(datetime, moveout, 103) ASC").ToList();
                PagedList<VacatingsAndTlcGridModel> model = new PagedList<VacatingsAndTlcGridModel>(gridmodel, page, pageSize);
                connection.Close();
                return View(model);
            }
        }

        public IActionResult PendingVacating(int page = 1, int pageSize = 10)
        {
            //VacatingsAndTlcModel vacatingsAndTlcModel = new();
            List<VacatingsAndTlcGridModel> gridmodel = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                gridmodel = connection.Query<VacatingsAndTlcGridModel>(@$"select pref,id,pname PropertyName,aptno AptNo,ftype Type,btype Bed,tenantname,nationality,contact ContactNo,
                                                                        CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
                                                                        CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
                                                                        CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
                                                                        case when len((select moi  from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,
                                                                        case when len((select bmapproval  from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord 
                                                                        from tenantshistory 
                                                                        WHERE STATUS='PENDING' 
                                                                        order by id desc").ToList();
                PagedList<VacatingsAndTlcGridModel> model = new PagedList<VacatingsAndTlcGridModel>(gridmodel, page, pageSize);
                connection.Close();
                return View(model);
            }
        }

        public async Task<IActionResult> TLC(int page = 1, int pageSize = 10)
        {
            //VacatingsAndTlcModel vacatingsAndTlcModel = new();
            List<VacatingsAndTlcGridModel> gridmodel = new();
            var query = await context.Tenantshistories.Where(s => s.Status == "CLOSED").OrderByDescending(s=>s.Closedate).ToListAsync();
            
            foreach(var item in query)
            {
                VacatingsAndTlcGridModel modelgrid = new();
                modelgrid.Id = item.Id;
                modelgrid.PropertyName = item.Pname;
                modelgrid.AptNo = item.Aptno;
                modelgrid.Type = item.Ftype;
                modelgrid.Bed = item.Btype;
                modelgrid.TenantName = item.Tenantname;
                modelgrid.LeaseStartDate = Convert.ToDateTime(item.Leasestart);
                modelgrid.LeaseEndDate = Convert.ToDateTime(item.Leaseend);
                modelgrid.ContactNo = item.Contact;
                modelgrid.Nationality = item.Nationality;
                modelgrid.MoveOutOn = Convert.ToDateTime(item.Movedate);

                gridmodel.Add(modelgrid);
            }
            PagedList<VacatingsAndTlcGridModel> model = new PagedList<VacatingsAndTlcGridModel>(gridmodel, page, pageSize);

            //using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            //{
            //    gridmodel = connection.Query<VacatingsAndTlcGridModel>(@$"select pref,id,pname as PropertyName,aptno AptNo,ftype Type,btype Bed,tenantname TenantName,nationality Nationality,contact ContactNo,
            //                                                            CONVERT(VARCHAR(11),leasestart,106) as LeaseStartDate,
            //                                                            CONVERT(VARCHAR(11),leaseend,106) as LeaseEndDate,
            //                                                            CONVERT(VARCHAR(11),movedate,106) as MoveOutOn,
            //                                                            case when len((select moi from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as moirecord,
            //                                                            case when len((select bmapproval from moveout m where m.pref =pref and id=moveoutid)) is null then 0 else 1 end as apprecord 
            //                                                            from tenantshistory 
            //                                                            WHERE STATUS='CLOSED' 
            //                                                            order by convert(datetime, closedate, 103) desc").ToList();
            //    PagedList<VacatingsAndTlcGridModel> model = new PagedList<VacatingsAndTlcGridModel>(gridmodel, page, pageSize);
            //    connection.Close();
            //    return View("VacatingsAndTlc", model);
            //}
            return View(model);
        }

        //[HttpPost]
        public async Task<IActionResult> VacatingProcess(int id, string name, string pref)
        {
            VacatingProcessModel model = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                if (name == "TLC") 
                {
                    var tenantHistory = await connection.QueryAsync<Tenantshistory>($@"select CONVERT(VARCHAR(11),keyreturndate,106) as keyreturndate,
                                                                                    pref,pname,aptno,ftype,btype,tenantname,nationality,contact,remarks,
                                                                                    CONVERT(VARCHAR(11),leasestart,106) as leasestart,
                                                                                    CONVERT(VARCHAR(11),leaseend,106) as leaseend,
                                                                                    CONVERT(VARCHAR(11),movedate,106) as movedate,moveoutid,status,forceclose 
                                                                                    from tenantshistory 
                                                                                    where pref='{pref}' and id={id}");
                    model.AptNo = tenantHistory.FirstOrDefault().Aptno;
                    model.PropertyRef = tenantHistory.FirstOrDefault().Pref;
                    model.PropertyName = tenantHistory.FirstOrDefault().Pname;
                    model.MoveOutDate = Convert.ToDateTime(tenantHistory.FirstOrDefault().Movedate);
                    model.TenantName = tenantHistory.FirstOrDefault().Tenantname;
                    model.Remarks = tenantHistory.FirstOrDefault().Remarks;//textbox1 in old code
                    model.KeyReturnDate = Convert.ToDateTime(tenantHistory.FirstOrDefault().Keyreturndate);
                    if(!string.IsNullOrEmpty(tenantHistory.FirstOrDefault().Forceclose))
                        model.ForceClose = tenantHistory.FirstOrDefault().Forceclose.Trim();

                    int varmoveid;
                    if(DBNull.Value.Equals(tenantHistory.FirstOrDefault().Moveoutid))
                    {
                        var moveId = await connection.QueryAsync<int>(@$"insert into moveout(pref,pname,aptno,tenantname,doc) values('{tenantHistory.FirstOrDefault().Pref}','{tenantHistory.FirstOrDefault().Pname}','{tenantHistory.FirstOrDefault().Aptno}','{tenantHistory.FirstOrDefault().Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int) ");
                        varmoveid = moveId.FirstOrDefault();
                        var update=await connection.ExecuteAsync($"update tenantshistory set moveoutid='{varmoveid}' where  pref='{pref}' and id={id}");
                    }
                    else
                    {
                        var tenantHis=await connection.QueryAsync<Tenantshistory>($"select pref,pname,aptno,ftype,btype,tenantname,nationality,contact,remarks,CONVERT(VARCHAR(11),leasestart,106) as leasestart,CONVERT(VARCHAR(11),leaseend,106) as leaseend,CONVERT(VARCHAR(11),movedate,106) as movedate,moveoutid from tenantshistory where pref='{pref}' and id={id}");
                        varmoveid = Convert.ToInt32(tenantHis.FirstOrDefault().Moveoutid);
                    }

                    if(varmoveid == 0)
                    {
                        var moveId = await connection.QueryAsync<int>(@$"insert into moveout(pref,pname,aptno,tenantname,doc) values('{tenantHistory.FirstOrDefault().Pref}','{tenantHistory.FirstOrDefault().Pname}','{tenantHistory.FirstOrDefault().Aptno}','{tenantHistory.FirstOrDefault().Tenantname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int) ");
                        varmoveid = moveId.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update tenantshistory set moveoutid='{varmoveid}' where  pref='{pref}' and id={id}");

                    }

                    var moveout = await connection.QueryAsync<Moveout>(@$"select SDFROM,PMODEOFSD,
                                                    CASE WHEN SDAMT IS NULL THEN 0 ELSE SDAMT END AS SDAMT,
                                                    CONVERT(VARCHAR(11),ARDATE,106) as ARDATE,
                                                    CONVERT(VARCHAR(11),moidate,106) as moidate,vn,
                                                    case when RMAT is null then 0 else RMAT end as RMAT,
                                                    case when ARMAT is null then 0 else ARMAT end as ARMAT,
                                                    REDUCTION,WAVED,bmapproval,bmreason,bmapproved,keys,
                                                    acards,moi,moilist,refurbbrkdown,moidate,lc,sd,sat,
                                                    rentpaid,moistatus,sdendorsement,tenantrefurb,sparemarks 
                                                    from moveout 
                                                    where id={varmoveid}");
                    model.moveout = moveout.FirstOrDefault();
                }
                else
                {
                    var propertymaster = await connection.QueryAsync<Propertymaster>($"select aptno,propertyref,bldgname,CONVERT(VARCHAR(11),moveout,106) as moveout,cname,moveoutremarks,moveoutid from propertymaster where propertyref='{pref}'");
                    model.AptNo = propertymaster.FirstOrDefault().AptNo;
                    model.PropertyRef = propertymaster.FirstOrDefault().PropertyRef;
                    model.PropertyName = propertymaster.FirstOrDefault().BldgName;
                    model.MoveOutDate = Convert.ToDateTime(propertymaster.FirstOrDefault().Moveout);
                    model.TenantName = propertymaster.FirstOrDefault().Cname;
                    model.Remarks = propertymaster.FirstOrDefault().Moveoutremarks;//textbox1 of old code
                    
                    int varmoveid;
                    if(DBNull.Value.Equals(propertymaster.FirstOrDefault().Moveoutid))
                    {
                        var moveid = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{propertymaster.FirstOrDefault().PropertyRef}','{propertymaster.FirstOrDefault().BldgName}','{propertymaster.FirstOrDefault().AptNo}','{propertymaster.FirstOrDefault().Cname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");
                        varmoveid = moveid.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update propertymaster set moveoutid={varmoveid} where  propertyref='{pref}' ");
                    }
                    else
                    {
                        var proMaster = await connection.QueryAsync<Propertymaster>($"select aptno,propertyref,bldgname,CONVERT(VARCHAR(11),moveout,106) as moveout,cname,moveoutremarks,moveoutid from propertymaster where propertyref='{pref}'");
                        varmoveid = Convert.ToInt32(proMaster.FirstOrDefault().Moveoutid);
                    }

                    if(varmoveid == 0)
                    {
                        //if(model.TenantName == "")
                        //{
                        //    var updateProperty = await connection.ExecuteAsync($"");
                        //}
                        var movevid = await connection.QueryAsync<int>($"insert into moveout(pref,pname,aptno,tenantname,doc) values('{propertymaster.FirstOrDefault().PropertyRef}','{propertymaster.FirstOrDefault().BldgName}','{propertymaster.FirstOrDefault().AptNo}','{propertymaster.FirstOrDefault().Cname}',getdate()); SELECT CAST(SCOPE_IDENTITY() as int )");
                        varmoveid = movevid.FirstOrDefault();
                        var update = await connection.ExecuteAsync($"update propertymaster set moveoutid={varmoveid} where  propertyref='{pref}' ");
                    }

                    var moveout = await connection.QueryAsync<Moveout>(@$"select SDFROM,PMODEOFSD,
                                                                        CASE WHEN SDAMT IS NULL THEN 0 ELSE SDAMT END AS SDAMT,
                                                                        CONVERT(VARCHAR(11),ARDATE,106) as ARDATE,
                                                                        CONVERT(VARCHAR(11),moidate,106) as moidate,vn,
                                                                        case when RMAT is null then 0 else RMAT end as RMAT,
                                                                        case when ARMAT is null then 0 else ARMAT end as ARMAT,
                                                                        REDUCTION,WAVED,bmapproval,bmreason,bmapproved,
                                                                        keys,acards,moi,moilist,refurbbrkdown,moidate,lc,sd,sat,
                                                                        rentpaid,moistatus,sdendorsement,tenantrefurb,sparemarks 
                                                                        from moveout 
                                                                        where id={varmoveid}");
                    model.moveout = moveout.FirstOrDefault();
                }
            }
                
            return View(model);
        }

        public string ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                if(oldPassword != null && newPassword != null)
                {
                    using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
                    {
                        connection.Open();
                        var exist = connection.Query<User>($"select * from users where Password='{oldPassword}' and Usrname='{HttpContext.Session.GetString("CurrentUsername")}'").FirstOrDefault();
                        if(exist != null)
                        {
                            connection.Execute($"update users set Password='{newPassword}' where Usrname='{HttpContext.Session.GetString("CurrentUsername")}'");
                            ViewBag.Message = "Updated";
                        }
                        else
                        {
                            ViewBag.Message = "Account does not exist";
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Fields are empty";
                }
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }

            return ViewBag.Message;
        }

        public string LoadLeaseEndDate(DateTime startDate, int count, string periodName)
        {
            DateTime today = startDate;
            string endDate="";
            if (periodName=="Month")
            {
                int noofDays = count * 30;
                DateTime answer = today.AddDays(noofDays);
                endDate = answer.ToString("yyyy-MM-dd");
                return endDate;
            }
            else if(periodName=="Year")
            {
                int noofDays = count * 365;
                DateTime answer = today.AddDays(noofDays);
                endDate = answer.ToString("yyyy-MM-dd");
                return endDate;
            }
            else//day
            {
                DateTime answer = today.AddDays(count);
                endDate = answer.ToString("yyyy-MM-dd");
                return endDate;
            }
            
            //return endDate;
        }

        //public JsonResult PaymentViewLC(string loiNo)
        //{
        //    string LoiNo;
        //    if (loiNo == null)
        //        LoiNo = "";
        //    else
        //        LoiNo = loiNo;

        //    using (var connection=new SqlConnection(sqlConnectionString.ConnectionString))
        //    {
        //        ViewPaymentModel viewPaymentModel = new();
        //        connection.Open();

        //        var loiinfo = connection.Query<Loiinformation>($"select payremarks,PROPERTYSOURCE,LLRESF,ClientRESF,lename,LoiNo,inqno,loipath,CONVERT(VARCHAR(11),leasedate,106) as Leasestart,CONVERT(VARCHAR(11),Leaseenddate,106) as Leaseend,RENT,DEPOSIT,loi_no,clientname,aptno,propertyname from loiinformation where loi_no='{LoiNo}'").FirstOrDefault();
        //        var rentamt = loiinfo.Rent;
        //        var depositamt = loiinfo.Deposit;
        //        var clresf = loiinfo.ClientResf;
        //        var LLRESF = loiinfo.Llresf;

        //        var recordcount = connection.Query<int>($"select count(*) from PAYMENTVOUCHER where loi_no='{LoiNo}'").FirstOrDefault();

        //        if(recordcount > 0)
        //        {
        //            viewPaymentModel.RentPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RENT'").FirstOrDefault();
        //            viewPaymentModel.DepositPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='DEPOSIT'").FirstOrDefault();
        //            viewPaymentModel.ClResfPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RESF'").FirstOrDefault();
        //            viewPaymentModel.LlResfPaid = connection.Query<string>(@$"select case when sum(amt) is null then '0' else sum(amt) end amt from paymentvoucher where loi_no='{LoiNo}' and [amt-type]='RESF-LL'").FirstOrDefault();
        //            var paymentvoucherList = connection.Query<ViewPaymentListModel>(@$"select pname,aptno,tname,lc_no lcno,loi_no,[amt-type] as VoucherType, 
        //                                                                                        cast(amt as decimal(34,3)) AS Amt,
        //                                                                                        cast(cash as decimal(34,3)) as Cash, 
        //                                                                                        cast(knet as decimal(34,3)) as Knet, 
        //                                                                                        cast(cheque as decimal(34,3)) as Cheque,
        //                                                                                        cast(BT as decimal(34,3)) as BT,
        //                                                                                        CONVERT(VARCHAR(11),datefrom,106) as DateFrom,
        //                                                                                        CONVERT(VARCHAR(11),dateto,106) as DateTo,comments Remarks
        //                                                                                        from paymentvoucher 
        //                                                                                        where loi_no='{LoiNo}'").ToList();
        //            viewPaymentModel.viewPaymentListModels = new();
        //            foreach (var item in paymentvoucherList)
        //            {
        //                DateTime fromdate = Convert.ToDateTime(item.DateFrom);
        //                DateTime todate = Convert.ToDateTime(item.DateTo);

        //                ViewPaymentListModel model = new();
        //                model.pname = item.pname;
        //                model.aptno = item.aptno;
        //                model.tname = item.tname;
        //                model.lcno = item.lcno;
        //                model.VoucherType = item.VoucherType;
        //                model.Amt = item.Amt;
        //                model.Cash = item.Cash;
        //                model.Knet = item.Knet;
        //                model.Cheque = item.Cheque;
        //                model.BT = item.BT;
        //                model.DateFrom = fromdate.ToString("dd-MM-yyyy");
        //                model.DateTo = todate.ToString("dd-MM-yyyy");
        //                model.Remarks = item.Remarks;

        //                viewPaymentModel.viewPaymentListModels.Add(model);
        //            }

        //            viewPaymentModel.RentDue = (rentamt - Convert.ToDecimal(viewPaymentModel.RentPaid)).ToString();
        //            viewPaymentModel.DepositDue = (depositamt - Convert.ToDecimal(viewPaymentModel.DepositPaid)).ToString();
        //            viewPaymentModel.ClResfDue = (clresf - Convert.ToDecimal(viewPaymentModel.ClResfPaid)).ToString();
        //            viewPaymentModel.LlResfDue = (LLRESF - Convert.ToDecimal(viewPaymentModel.LlResfPaid)).ToString();
        //        }
                
        //        connection.Close();
        //        return Json(viewPaymentModel);
        //    }
            
        //}

        public IActionResult CreateLC(int id)
        {
            NewLCModel newLCModel = new();

            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();


                    var fetch = connection.Query<Loiinformation>($"select *, loi_no LoiNo1 from LOIInformation where ID={id}").FirstOrDefault();
                    if (fetch != null)
                    {
                        newLCModel.LcInfo = new();
                        newLCModel.LcInfo.Tenname = fetch.ClientName;//
                        newLCModel.LcInfo.Compaddress = fetch.ClientCompany;
                        newLCModel.LcInfo.Nationality = fetch.Cnationality;//
                        newLCModel.LcInfo.Source = fetch.ClientSource;//
                        newLCModel.LcInfo.Lcmob = fetch.Cmob;//
                        newLCModel.LcInfo.Ptype = fetch.PropertySource;//
                        newLCModel.LcInfo.Pname = fetch.PropertyName;//
                        newLCModel.LcInfo.Aptno = fetch.Aptno;//
                        newLCModel.LcInfo.Pref = fetch.PropertyRefNo;//
                        newLCModel.LcInfo.Btype = fetch.Req;//
                        newLCModel.LcInfo.Fstatus = fetch.Fur;//
                        newLCModel.LcInfo.LoiNo = fetch.LoiNo1;//
                        newLCModel.LcInfo.Leasestart = fetch.Leasedate;//
                        newLCModel.LcInfo.Leaseend = fetch.Leaseenddate;//
                        newLCModel.LcInfo.Period = Convert.ToInt32(fetch.Ap);//
                        newLCModel.LcInfo.Type = fetch.Aptype;//
                        newLCModel.LcInfo.Rent = fetch.Rent;//
                        newLCModel.LcInfo.Deposit = fetch.Deposit;//
                        newLCModel.LcInfo.Resf = fetch.ClientResf;//
                        newLCModel.LcInfo.Enqtype = fetch.EnquiryType;//

                        newLCModel.LcInfo.Pp = fetch.Pp;
                        newLCModel.LcInfo.Cid = fetch.Cid;
                        newLCModel.LcInfo.Noc = fetch.Noc;
                        newLCModel.LcInfo.Mc = fetch.Mc;
                        newLCModel.LcInfo.Coe = fetch.Coe;
                        newLCModel.LcInfo.Cosign = fetch.Cosign;
                        newLCModel.LcInfo.Cas = fetch.Cas;
                        newLCModel.LcInfo.Cidpp = fetch.Cidpp;
                        newLCModel.LcInfo.Cg = fetch.Cg;
                        newLCModel.LcInfo.Moc = fetch.Moc;
                        newLCModel.LcInfo.Loipath = fetch.Loipath;

                        if (newLCModel.LcInfo.Pref.Contains(","))
                        {
                            newLCModel.MultipleApartment = true;
                            newLCModel.GroupAptno = newLCModel.LcInfo.Aptno;
                            newLCModel.GroupPref = newLCModel.LcInfo.Pref;
                            newLCModel.LCNo = newLCModel.LcInfo.LcNo;
                            string[] noofapt = newLCModel.LcInfo.Pref.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            newLCModel.NoOfApt = noofapt.Count();
                            //newLCModel.GPrefList = new();
                            newLCModel.GPrefList = newLCModel.LcInfo.Pref.Split(',').ToList();
                        }
                        else
                        {
                            newLCModel.AptRent = fetch.Rent;
                        }

                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }


            //newLCModel.LcInfo = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var count = connection.Query<int>(@"select count(*) from lcinfo where year(getdate())=year(lcmadeon)").FirstOrDefault();

                if (count == 0)
                {
                    newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0001";
                }
                else
                {
                    var newlcno = connection.Query<int>(@"select right(lc_no,4)+1 from lcinfo where id=(select max(id) from lcinfo where year(getdate())=year(lcmadeon))").FirstOrDefault();

                    if (newlcno <= 9)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "000" + newlcno;
                    else if (newlcno > 9 && newlcno <= 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "00" + newlcno;
                    else if (newlcno > 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0" + newlcno;
                    else
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "000" + newlcno;
                }
            }
            //newLCModel.Nationalities = context.Nationalities.ToList();

            return View("NewLC", newLCModel);
        }

        public IActionResult RenewLC(string currentLcNo)
        {
            NewLCModel newLCModel = new();

            try
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    var fetch = connection.Query<Lcinfo>($"select *, loi_no LoiNo from lcinfo where LC_no='{currentLcNo}'").FirstOrDefault();
                    if (fetch != null)
                    {
                        newLCModel.LcInfo = new();
                        newLCModel.renewlcno = currentLcNo;
                        newLCModel.renewls = fetch.Leasestart;
                        newLCModel.renewle = fetch.Leaseend;
                        newLCModel.LcInfo.Tenname = fetch.Tenname;
                        newLCModel.LcInfo.Compaddress = fetch.Compaddress;
                        newLCModel.LcInfo.Nationality = fetch.Nationality;
                        newLCModel.LcInfo.Ppno = fetch.Ppno;
                        newLCModel.LcInfo.Civilid = fetch.Civilid;
                        newLCModel.LcInfo.Source = fetch.Source;
                        newLCModel.LcInfo.Lcmob = fetch.Lcmob;
                        newLCModel.LcInfo.Ptype = fetch.Ptype;
                        newLCModel.LcInfo.Pname = fetch.Pname;
                        newLCModel.LcInfo.Aptno = fetch.Aptno;
                        newLCModel.LcInfo.Pref = fetch.Pref;
                        newLCModel.LcInfo.Btype = fetch.Btype;
                        newLCModel.LcInfo.Fstatus = fetch.Fstatus;
                        newLCModel.LcInfo.LoiNo = fetch.LoiNo;
                        newLCModel.LcInfo.Leasestart = fetch.Leaseend;
                        newLCModel.LcInfo.Leaseend = null;
                        newLCModel.LcInfo.Period = fetch.Period;
                        newLCModel.LcInfo.Type = fetch.Type;
                        newLCModel.LcInfo.Rent = fetch.Rent;
                        newLCModel.LcInfo.Deposit = fetch.Deposit;
                        newLCModel.LcInfo.Resf = fetch.Resf;
                        newLCModel.LcInfo.Enqtype = fetch.Enqtype;
                        if (newLCModel.LcInfo.Pref.Contains(","))
                        {
                            newLCModel.MultipleApartment = true;
                            newLCModel.GroupAptno = newLCModel.LcInfo.Aptno;
                            newLCModel.GroupPref = newLCModel.LcInfo.Pref;
                            newLCModel.LCNo = newLCModel.LcInfo.LcNo;
                            string[] noofapt = newLCModel.LcInfo.Pref.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            newLCModel.NoOfApt = noofapt.Count();
                            //newLCModel.GPrefList = new();
                            newLCModel.GPrefList = newLCModel.LcInfo.Pref.Split(',').ToList();
                        }
                        else
                        {
                            newLCModel.AptRent = fetch.Rent;
                        }

                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Unknown Error";
            }


            //newLCModel.LcInfo = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                var count = connection.Query<int>(@"select count(*) from lcinfo where year(getdate())=year(lcmadeon)").FirstOrDefault();

                if (count == 0)
                {
                    newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0001";
                }
                else
                {
                    var newlcno = connection.Query<int>(@"select right(lc_no,4)+1 from lcinfo where id=(select max(id) from lcinfo where year(getdate())=year(lcmadeon))").FirstOrDefault();

                    if (newlcno <= 9)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "000" + newlcno;
                    else if (newlcno > 9 && newlcno <= 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "00" + newlcno;
                    else if (newlcno > 99)
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "0" + newlcno;
                    else
                        newLCModel.LcInfo.LcNo = "LC-" + DateTime.Today.Year + "-" + "000" + newlcno;
                }
            }
            //newLCModel.Nationalities = context.Nationalities.ToList();

            return View("NewLC", newLCModel);
        }

        public async Task<List<Lcinfo>> SearchLC(string lcnoKeyword)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var lcResult = await connection.QueryAsync<Lcinfo>($"select *, LC_no lcNo, LOI_no loiNo from lcinfo where LC_no like '%{lcnoKeyword}%' order by LC_no desc"); /*and DATEPART(yyyy, lcmadeon)= datepart(yyyy, getdate())*/
                connection.Close();
                return lcResult.ToList();
            }
        }

        public async Task<List<Lcinfo>> Sort1LC(string sortKeyword)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var lcResult = await connection.QueryAsync<Lcinfo>($"select *, LC_no lcNo,LOI_no loiNo from lcinfo where lctype = '{sortKeyword}'  order by LC_no desc"); /*and DATEPART(yyyy, lcmadeon)= datepart(yyyy, getdate())*/
                connection.Close();
                return lcResult.ToList();
            }
        }

        
        public async Task<List<Lcinfo>> Sort2LC(string sortKeyword)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var lcResult = await connection.QueryAsync<Lcinfo>($"select *, LC_no lcNo, LOI_no loiNo from lcinfo where lct = '{sortKeyword}'  order by LC_no desc"); /*and DATEPART(yyyy, lcmadeon)= datepart(yyyy, getdate())*/
                connection.Close();
                return lcResult.ToList();
            }
        }

        
    }

    public class RefNoLoadModel
    {
        public string propertyRef { get; set; }
        public string cname { get; set; }
        public string crent { get; set; }
        public string tenname { get; set; }
        public string rent { get; set; }
        public string lcno { get; set; }
        public DateTime leasestart { get; set; }
        public DateTime leaseend { get; set; }
        public string leasestart1 { get; set; }
        public string leaseend1 { get; set; }
    }

    public class LcNoLoadNameRentModel
    {
        public string tenname { get; set; }
        public string rent { get; set; }
    }

    public class ViewPaymentModel
    {
        public string RentPaid { get; set; }
        public string DepositPaid { get; set; }
        public string ClResfPaid { get; set; }
        public string LlResfPaid { get; set; }
        public string RentDue { get; set; }
        public string DepositDue { get; set; }
        public string ClResfDue { get; set; }
        public string LlResfDue { get; set; }
        public List<ViewPaymentListModel> viewPaymentListModels { get; set; }
    }

    public class ViewPaymentListModel
    {
        public string pname { get; set; }
        public string aptno { get; set; }
        public string tname { get; set; }
        public string lcno { get; set; }
        public string VoucherType { get; set; }
        public string Amt { get; set; }
        public string Cash { get; set; }
        public string Knet { get; set; }
        public string Cheque { get; set; }
        public string BT { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Remarks { get; set; }
    }
}
