using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.LOI;
using X.PagedList;
using System.Data.SqlClient;
using Dapper;
using PMS_Admin_Web.Repository;
using PMS_Admin_Web.Enum;
using PMS_Admin_Web.Models.LC;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace PMS_Admin_Web.Controllers
{
    public class LOIController : Controller
    {
        private RealtorContext context = new();
        private Connection sqlConnectionString = new();

        public LOIController(RealtorContext _context)
        {
            context = _context;
        }

        public List<string> LoadProperties(string propertysource)
        {
            List<string> properties;
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
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
                aptno = connection.Query<string>(@$"select AptNo from propertymaster where BldgName='{propertyname.Trim()}'").ToList();
                connection.Close();
                return aptno;
            }
        }

        public JsonResult LoadRefNo(string propertyname,string aptno)
        {
            //List<string> refno;
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var refno = connection.Query<string>(@$"select PropertyRef from propertymaster where BldgName='{propertyname}' and AptNo='{aptno}' ").ToList();
                connection.Close();
                return Json(refno);
            }
        }

        public async Task<IActionResult> EditLOI(int id,NewLOIModel newLOIModel, IFormFile passportfile, IFormFile civilidfile, IFormFile nocvisafile, IFormFile marriagecontractfile, IFormFile moclicensefile, IFormFile civilidASfile, IFormFile salarycertificatefile, IFormFile companysignfile, IFormFile staffemployeefile, IFormFile companyguaranteefile, IFormFile loifile)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            //NewLOIModel newLOIModel = new();
            if (newLOIModel.LOI != null)
            {
                int noofapt = 0;
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    try
                    {
                        if (newLOIModel.LOI.LoiStatus != "Cancelled")
                        {
                            if (HttpContext.Session.GetString("CurrentUserDepartment") == "MARKETING")
                            {
                                ViewBag.Message = "LOI is created from the marketing department; cannot be edited create LC";
                                //return View();
                                goto nationalities;
                            }
                        }

                        if (newLOIModel.MultipleApartment)
                        {
                            if (newLOIModel.AptRent == newLOIModel.LOI.Rent)
                            {
                                ViewBag.Message = "Multiple Apartment is selected;Loi rent and apt rent cannot be same";
                                //return View();
                                goto nationalities;
                            }
                            noofapt = newLOIModel.NoOfApt;
                        }
                        else
                        {
                            if (newLOIModel.AptRent != newLOIModel.LOI.Rent)
                            {
                                ViewBag.Message = "Multiple Apartment is not selected so the LOI rent and the Apartment rent should be same";
                                //return View();
                                goto nationalities;
                            }
                            noofapt = 1;
                        }

                        if (newLOIModel.AptRent != newLOIModel.LOI.Rent / noofapt)
                        {
                            ViewBag.Message = "No of Multiple Apartment is mismatching with the Apartment Rent";
                            //return View();
                            goto nationalities;
                        }
                        int recordcount = 0;
                        if (newLOIModel.MultipleApartment)
                        {
                            List<int> countList = new();
                            foreach (var itemPref in newLOIModel.GPrefList)
                            {
                                var count = connection.Query<int>($"select count(*) from propertymaster where PropertyRef='{itemPref}'").FirstOrDefault();
                                countList.Add(count);
                            }
                            foreach (var itemCount in countList)
                            {
                                if (itemCount == 1)
                                {
                                    recordcount = 1;
                                }
                                else
                                {
                                    recordcount = 0;
                                    //goto recordCountOneisZero;
                                    ViewBag.Message = "Check for the Property in the Property Master";
                                    //return View();
                                    goto nationalities;
                                }
                            }

                        }
                        else
                        {
                            recordcount = connection.Query<int>($"select count(*) from propertymaster where propertyref='{newLOIModel.LOI.PropertyRefNo}'").FirstOrDefault();
                        }

                        var propertymaster = await connection.QueryAsync<Propertymaster>($"select moveout,vacatingstatus from propertymaster where propertyref='{newLOIModel.LOI.PropertyRefNo}' and moveout is not null ");
                        if(propertymaster.Count() != 0)
                        {
                            if (propertymaster.FirstOrDefault().Vacatingstatus == "Vacating")
                            {
                                if (Convert.ToDateTime(propertymaster.FirstOrDefault().Moveout) > Convert.ToDateTime(newLOIModel.LOI.Leasedate))
                                {
                                    ViewBag.Message = $"Lease cannot start ,MoveOut date of existing tenant of this Property is {propertymaster.FirstOrDefault().Moveout}";
                                    //return View();
                                    goto nationalities;
                                }
                            }
                        }
                        

                        if (recordcount > 0)
                        {
                            
                            if (passportfile != null)
                            {
                                string LOIpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/pp");
                                if (!Directory.Exists(LOIpppath))
                                    Directory.CreateDirectory(LOIpppath);

                                string ppextension = Path.GetExtension(passportfile.FileName);
                                string ppfileName = newLOIModel.LOI.LoiNo1 + ppextension;
                                string ppfileNameWithPath = Path.Combine(LOIpppath, ppfileName);
                                if (System.IO.File.Exists(ppfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = ppfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var ppstream = new FileStream(ppfileNameWithPath, FileMode.Create);
                                passportfile.CopyTo(ppstream);
                                ppstream.Close();
                                newLOIModel.LOI.Pp = ppfileNameWithPath;
                            }

                            
                            if (civilidfile != null)
                            {
                                string LOIcidpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cid");
                                if (!Directory.Exists(LOIcidpath))
                                    Directory.CreateDirectory(LOIcidpath);

                                string cidextension = Path.GetExtension(civilidfile.FileName);
                                string cidfileName = newLOIModel.LOI.LoiNo1 + cidextension;
                                string cidfileNameWithPath = Path.Combine(LOIcidpath, cidfileName);
                                if (System.IO.File.Exists(cidfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cidfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cidstream = new FileStream(cidfileNameWithPath, FileMode.Create);
                                civilidfile.CopyTo(cidstream);
                                cidstream.Close();
                                newLOIModel.LOI.Cid = cidfileNameWithPath;
                            }

                            
                            if (nocvisafile != null)
                            {
                                string LOInocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/noc");
                                if (!Directory.Exists(LOInocpath))
                                    Directory.CreateDirectory(LOInocpath);

                                string nocextension = Path.GetExtension(nocvisafile.FileName);
                                string nocfileName = newLOIModel.LOI.LoiNo1 + nocextension;
                                string nocfileNameWithPath = Path.Combine(LOInocpath, nocfileName);
                                if (System.IO.File.Exists(nocfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = nocfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var nocstream = new FileStream(nocfileNameWithPath, FileMode.Create);
                                nocvisafile.CopyTo(nocstream);
                                nocstream.Close();
                                newLOIModel.LOI.Noc = nocfileNameWithPath;
                            }

                            
                            if (marriagecontractfile != null)
                            {
                                string LOImcpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/mc");
                                if (!Directory.Exists(LOImcpath))
                                    Directory.CreateDirectory(LOImcpath);

                                string mcextension = Path.GetExtension(marriagecontractfile.FileName);
                                string mcfileName = newLOIModel.LOI.LoiNo1 + mcextension;
                                string mcfileNameWithPath = Path.Combine(LOImcpath, mcfileName);
                                if (System.IO.File.Exists(mcfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = mcfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var mcstream = new FileStream(mcfileNameWithPath, FileMode.Create);
                                marriagecontractfile.CopyTo(mcstream);
                                mcstream.Close();
                                newLOIModel.LOI.Mc = mcfileNameWithPath;
                            }

                            
                            if (moclicensefile != null)
                            {
                                string LOImocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/moc");
                                if (!Directory.Exists(LOImocpath))
                                    Directory.CreateDirectory(LOImocpath);

                                string mocextension = Path.GetExtension(moclicensefile.FileName);
                                string mocfileName = newLOIModel.LOI.LoiNo1 + mocextension;
                                string mocfileNameWithPath = Path.Combine(LOImocpath, mocfileName);
                                if (System.IO.File.Exists(mocfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = mocfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var mocstream = new FileStream(mocfileNameWithPath, FileMode.Create);
                                moclicensefile.CopyTo(mocstream);
                                mocstream.Close();
                                newLOIModel.LOI.Moc = mocfileNameWithPath;
                            }

                            
                            if (civilidASfile != null)
                            {
                                string LOIcaspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cas");
                                if (!Directory.Exists(LOIcaspath))
                                    Directory.CreateDirectory(LOIcaspath);

                                string casextension = Path.GetExtension(civilidASfile.FileName);
                                string casfileName = newLOIModel.LOI.LoiNo1 + casextension;
                                string casfileNameWithPath = Path.Combine(LOIcaspath, casfileName);
                                if (System.IO.File.Exists(casfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = casfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var casstream = new FileStream(casfileNameWithPath, FileMode.Create);
                                civilidASfile.CopyTo(casstream);
                                casstream.Close();
                                newLOIModel.LOI.Cas = casfileNameWithPath;
                            }

                            
                            if (salarycertificatefile != null)
                            {
                                string LOIcoepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/coe");
                                if (!Directory.Exists(LOIcoepath))
                                    Directory.CreateDirectory(LOIcoepath);

                                string coeextension = Path.GetExtension(salarycertificatefile.FileName);
                                string coefileName = newLOIModel.LOI.LoiNo1 + coeextension;
                                string coefileNameWithPath = Path.Combine(LOIcoepath, coefileName);
                                if (System.IO.File.Exists(coefileNameWithPath))
                                {
                                    string fileNameWithPathTemp = coefileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var coestream = new FileStream(coefileNameWithPath, FileMode.Create);
                                salarycertificatefile.CopyTo(coestream);
                                coestream.Close();
                                newLOIModel.LOI.Coe = coefileNameWithPath;
                            }

                            
                            if (companysignfile != null)
                            {
                                string LOIcosignpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cosign");
                                if (!Directory.Exists(LOIcosignpath))
                                    Directory.CreateDirectory(LOIcosignpath);

                                string cosignextension = Path.GetExtension(companysignfile.FileName);
                                string cosignfileName = newLOIModel.LOI.LoiNo1 + cosignextension;
                                string cosignfileNameWithPath = Path.Combine(LOIcosignpath, cosignfileName);
                                if (System.IO.File.Exists(cosignfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cosignfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cosignstream = new FileStream(cosignfileNameWithPath, FileMode.Create);
                                companysignfile.CopyTo(cosignstream);
                                cosignstream.Close();
                                newLOIModel.LOI.Cosign = cosignfileNameWithPath;
                            }

                            
                            if (staffemployeefile != null)
                            {
                                string LOIcidpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cidpp");
                                if (!Directory.Exists(LOIcidpppath))
                                    Directory.CreateDirectory(LOIcidpppath);

                                string cidppextension = Path.GetExtension(staffemployeefile.FileName);
                                string cidppfileName = newLOIModel.LOI.LoiNo1 + cidppextension;
                                string cidppfileNameWithPath = Path.Combine(LOIcidpppath, cidppfileName);
                                if (System.IO.File.Exists(cidppfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cidppfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cidppstream = new FileStream(cidppfileNameWithPath, FileMode.Create);
                                staffemployeefile.CopyTo(cidppstream);
                                cidppstream.Close();
                                newLOIModel.LOI.Cidpp = cidppfileNameWithPath;
                            }

                            
                            if (companyguaranteefile != null)
                            {
                                string LOIcgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cg");
                                if (!Directory.Exists(LOIcgpath))
                                    Directory.CreateDirectory(LOIcgpath);

                                string cgextension = Path.GetExtension(companyguaranteefile.FileName);
                                string cgfileName = newLOIModel.LOI.LoiNo1 + cgextension;
                                string cgfileNameWithPath = Path.Combine(LOIcgpath, cgfileName);
                                if (System.IO.File.Exists(cgfileNameWithPath))
                                {
                                    string fileNameWithPathTemp = cgfileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var cgstream = new FileStream(cgfileNameWithPath, FileMode.Create);
                                companyguaranteefile.CopyTo(cgstream);
                                cgstream.Close();
                                newLOIModel.LOI.Cg = cgfileNameWithPath;
                            }

                            
                            if (loifile != null)
                            {
                                string LOIpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/loi");
                                if (!Directory.Exists(LOIpath))
                                    Directory.CreateDirectory(LOIpath);

                                string loiextension = Path.GetExtension(loifile.FileName);
                                string loifileName = newLOIModel.LOI.LoiNo1 + loiextension;
                                string loifileNameWithPath = Path.Combine(LOIpath, loifileName);
                                if (System.IO.File.Exists(loifileNameWithPath))
                                {
                                    string fileNameWithPathTemp = loifileNameWithPath;
                                    System.IO.File.Delete(fileNameWithPathTemp);
                                }
                                var loistream = new FileStream(loifileNameWithPath, FileMode.Create);
                                loifile.CopyTo(loistream);
                                loistream.Close();
                                newLOIModel.LOI.Loipath = loifileNameWithPath;
                            }

                            if (newLOIModel.MultipleApartment)
                            {
                                var updateLoi = await connection.ExecuteAsync(@$"UPDATE LOIInformation   
                                    SET EnquiryType='{newLOIModel.LOI.EnquiryType}',loi_no='{newLOIModel.LOI.LoiNo1}',clientname='{newLOIModel.LOI.ClientName}',cmob='{newLOIModel.LOI.Cmob}',
                                    clientcompany = '{newLOIModel.LOI.ClientCompany}',[ClientSource] = '{newLOIModel.LOI.ClientSource}', cnationality = '{newLOIModel.LOI.Cnationality}',
                                    req = '{newLOIModel.LOI.Req}', fur = '{newLOIModel.LOI.Fur}', ap = '{newLOIModel.LOI.Ap}', aptype = '{newLOIModel.LOI.Aptype}',
                                    [PropertySource] = '{newLOIModel.LOI.PropertySource}',[PropertyName] = '{newLOIModel.LOI.PropertyName}',[PropertyRefNo] = '{newLOIModel.GroupPref}',
                                    [Aptno] = '{newLOIModel.GroupAptno}',[Leasedate] = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}',[Renttobepaidby] = '{newLOIModel.LOI.Renttobepaidby}',
                                    [rent] = '{newLOIModel.LOI.Rent}',[Securitydepositpaidby] = '{newLOIModel.LOI.Securitydepositpaidby}',[deposit] = '{newLOIModel.LOI.Deposit}',
                                    [feetobepaidby] = '{newLOIModel.LOI.Feetobepaidby}',[clientRESF] = '{newLOIModel.LOI.ClientResf}',[LLRESF] = '{newLOIModel.LOI.Llresf}',
                                    [TotClients] = '{newLOIModel.LOI.TotClients}',[SearchedProperty] = '{newLOIModel.LOI.SearchedProperty}', updateddate = getdate(),
                                    Leaseenddate = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}',loisigndate = '{Convert.ToDateTime(newLOIModel.LOI.Loisigndate).ToString("yyyy-MM-dd")}', DOCSUBMITTED = '{newLOIModel.LOI.Docsubmitted}',
                                    loiremarks = '{newLOIModel.LOI.Loiremarks}', movingindate = '{Convert.ToDateTime(newLOIModel.LOI.Movingindate).ToString("yyyy-MM-dd")}',[LoiStatus] = 'Approved',
                                    loipath='{newLOIModel.LOI.Loipath}',pp='{newLOIModel.LOI.Pp}',cid='{newLOIModel.LOI.Cid}',noc='{newLOIModel.LOI.Noc}',mc='{newLOIModel.LOI.Mc}',coe='{newLOIModel.LOI.Coe}',cosign='{newLOIModel.LOI.Cosign}',cas='{newLOIModel.LOI.Cas}',cidpp='{newLOIModel.LOI.Cidpp}',cg='{newLOIModel.LOI.Cg}',moc='{newLOIModel.LOI.Moc}'
                                    WHERE LoiNo = '{newLOIModel.LOI.LoiNo}' ");//CONVERT(date, '{newLOIModel.LOI.Loisigndate}', 103) CONVERT(date, '{newLOIModel.LOI.Movingindate}', 103)

                                var updatePropM = await connection.ExecuteAsync($@"update propertymaster set status='NotAvailable',leasetype='New LC',
                                    rstatus='LOI Created',reservation='yes',leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                    rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                    rlstart = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}', rlend = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'Loiupdate'
                                    where PropertySource = 'ManagedProperty'
                                    and PropertyRef in ('{newLOIModel.GroupPref}') ");
                            }
                            else
                            {
                                var updateLoi = await connection.ExecuteAsync(@$"UPDATE LOIInformation   
                                    SET EnquiryType='{newLOIModel.LOI.EnquiryType}',loi_no='{newLOIModel.LOI.LoiNo1}',clientname='{newLOIModel.LOI.ClientName}',cmob='{newLOIModel.LOI.Cmob}',
                                    clientcompany = '{newLOIModel.LOI.ClientCompany}',[ClientSource] = '{newLOIModel.LOI.ClientSource}', cnationality = '{newLOIModel.LOI.Cnationality}',
                                    req = '{newLOIModel.LOI.Req}', fur = '{newLOIModel.LOI.Fur}', ap = '{newLOIModel.LOI.Ap}', aptype = '{newLOIModel.LOI.Aptype}',
                                    [PropertySource] = '{newLOIModel.LOI.PropertySource}',[PropertyName] = '{newLOIModel.LOI.PropertyName}',[PropertyRefNo] = '{newLOIModel.LOI.PropertyRefNo}',
                                    [Aptno] = '{newLOIModel.LOI.Aptno}',[Leasedate] = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}',[Renttobepaidby] = '{newLOIModel.LOI.Renttobepaidby}',
                                    [rent] = '{newLOIModel.LOI.Rent}',[Securitydepositpaidby] = '{newLOIModel.LOI.Securitydepositpaidby}',[deposit] = '{newLOIModel.LOI.Deposit}',
                                    [feetobepaidby] = '{newLOIModel.LOI.Feetobepaidby}',[clientRESF] = '{newLOIModel.LOI.ClientResf}',[LLRESF] = '{newLOIModel.LOI.Llresf}',
                                    [TotClients] = '{newLOIModel.LOI.TotClients}',[SearchedProperty] = '{newLOIModel.LOI.SearchedProperty}', updateddate = getdate(),
                                    Leaseenddate = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}',loisigndate = '{Convert.ToDateTime(newLOIModel.LOI.Loisigndate).ToString("yyyy-MM-dd")}', DOCSUBMITTED = '{newLOIModel.LOI.Docsubmitted}',
                                    loiremarks = '{newLOIModel.LOI.Loiremarks}', movingindate = '{Convert.ToDateTime(newLOIModel.LOI.Movingindate).ToString("yyyy-MM-dd")}',[LoiStatus] = 'Approved',
                                    loipath='{newLOIModel.LOI.Loipath}',pp='{newLOIModel.LOI.Pp}',cid='{newLOIModel.LOI.Cid}',noc='{newLOIModel.LOI.Noc}',mc='{newLOIModel.LOI.Mc}',coe='{newLOIModel.LOI.Coe}',cosign='{newLOIModel.LOI.Cosign}',cas='{newLOIModel.LOI.Cas}',cidpp='{newLOIModel.LOI.Cidpp}',cg='{newLOIModel.LOI.Cg}',moc='{newLOIModel.LOI.Moc}'
                                    WHERE LoiNo = '{newLOIModel.LOI.LoiNo}' ");//CONVERT(date, '{newLOIModel.LOI.Loisigndate}', 103) CONVERT(date, '{newLOIModel.LOI.Movingindate}', 103)

                                var updatePropM = await connection.ExecuteAsync($@"update propertymaster set status='NotAvailable',leasetype='New LC',
                                    rstatus='LOI Created',reservation='yes',leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                    rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                    rlstart = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}', rlend = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'Loiupdate'
                                    where PropertySource = 'ManagedProperty'
                                    and PropertyRef = '{newLOIModel.LOI.PropertyRefNo}'");
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Search Property Master for the Property LOI made";
                            //return View();
                            goto nationalities;
                        }


                        ModelState.Clear();
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "LOI Updated");
                        ViewBag.Message = "LOI is updated successfully";
                    }
                    catch (Exception ex)
                    {
                        //ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Unknown Error");
                        logger.Info(ex);
                        ViewBag.Message = "Unknown Error";
                    }
                }
            }
            else
            {
                newLOIModel.LOI = context.Loiinformations.Find(id);
                newLOIModel.LOI.LoiDate = Convert.ToDateTime(newLOIModel.LOI.LoiDate.ToString("yyyy-MM-dd"));
                if (newLOIModel.LOI.PropertyRefNo.Contains(","))
                {
                    newLOIModel.MultipleApartment = true;
                    newLOIModel.GroupAptno = newLOIModel.LOI.Aptno;
                    newLOIModel.GroupPref = newLOIModel.LOI.PropertyRefNo;
                    string[] noofapt = newLOIModel.LOI.PropertyRefNo.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    newLOIModel.NoOfApt = noofapt.Count();
                    newLOIModel.GPrefList = newLOIModel.LOI.PropertyRefNo.Split(',').ToList();
                }
                else
                {
                    newLOIModel.AptRent = newLOIModel.LOI.Rent;
                }
            }

            nationalities:
            //newLOIModel.Nationalities = context.Nationalities.ToList();
            return View(newLOIModel);
        }

        public async Task<IActionResult> NewLOI(NewLOIModel newLOIModel, IFormFile passportfile, IFormFile civilidfile, IFormFile nocvisafile, IFormFile marriagecontractfile, IFormFile moclicensefile, IFormFile civilidASfile, IFormFile salarycertificatefile, IFormFile companysignfile, IFormFile staffemployeefile, IFormFile companyguaranteefile, IFormFile loifile)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            if (newLOIModel.LOI != null)
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    //using (var trans = connection.BeginTransaction())
                    {
                        int noofapt = 0;
                        //if (newLOIModel.LOI.Id <= 0)
                        {
                            try
                            {
                                if (newLOIModel.MultipleApartment)
                                {
                                    if (!string.IsNullOrEmpty(newLOIModel.GroupAptno))
                                    {
                                        ViewBag.Message = "Apartments are not selected";
                                        //return View();
                                        goto nationalities;
                                    }
                                    if (newLOIModel.GroupAptno.Contains(",") == false)
                                    {
                                        ViewBag.Message = "There are no multiple apartments are selected so uncheck the multiple apartments option";
                                        //return View();
                                        goto nationalities;
                                    }
                                }

                                if (newLOIModel.MultipleApartment)
                                {
                                    if (newLOIModel.LOI.Rent == newLOIModel.AptRent)
                                    {
                                        ViewBag.Message = "Multiple Apartment is selected;Loi rent and apt rent cannot be same";
                                        //return View();
                                        goto nationalities;
                                    }
                                    if (newLOIModel.GroupAptno.Contains(","))
                                    {
                                        var words = newLOIModel.GroupAptno.Split(",");
                                        List<string> aptno = new();
                                        foreach (var word in words)
                                        {
                                            aptno.Add(word);
                                        }
                                        noofapt = aptno.Count;
                                    }
                                }
                                else
                                {
                                    if (newLOIModel.LOI.Rent != newLOIModel.AptRent)
                                    {
                                        ViewBag.Message = "Multiple Apartment is not selected so the LOI rent and the Apartment rent should be same";
                                        //return View();
                                        goto nationalities;
                                    }
                                    noofapt = 1;
                                }
                                var val = newLOIModel.LOI.Rent / noofapt;
                                if (newLOIModel.AptRent != val)
                                {
                                    ViewBag.Message = "No of Multiple Apartment is mismatching with the Apartment Rent";
                                    //return View();
                                    goto nationalities;
                                }

                                var LOIRECORDCOUNT = connection.Query<int>($"select count(*) from loiinformation where loi_no='{newLOIModel.LOI.LoiNo1}'").FirstOrDefault();//,null, trans
                                if (LOIRECORDCOUNT > 0)
                                {
                                    ViewBag.Message = "This LOI No already Exist";
                                    //return View();
                                    goto nationalities;
                                }

                                if (newLOIModel.LOI.LoiNo1.Length > 19)
                                {
                                    ViewBag.Message = "Check the LOI NO format , the format must be like LOI-YEAR-ddmm-0000";
                                    //return View();
                                    goto nationalities;
                                }

                                var recordcount = connection.Query<int>($"select count(*) as record from propertymaster where propertyref='{newLOIModel.LOI.PropertyRefNo}'").FirstOrDefault();//, null, trans
                                var propertymaster = await connection.QueryAsync<Propertymaster>($"select CONVERT(VARCHAR(11),moveout,106) as moveout,vacatingstatus, reservation,reservedfor,leaseno from propertymaster where propertyref='{newLOIModel.LOI.PropertyRefNo}' and moveout is not null ");//, null, trans
                                if (propertymaster.Count() != 0)
                                {
                                    if (propertymaster.FirstOrDefault().Vacatingstatus.ToString() == "Vacating")
                                    {
                                        if (propertymaster.FirstOrDefault().Moveout > newLOIModel.LOI.Leasedate)
                                        {
                                            ViewBag.Message = $"Lease cannot start, MoveOut date of existing tenant of this Property is {propertymaster.FirstOrDefault().Moveout}";
                                            //return View();
                                            goto nationalities;
                                        }
                                    }

                                    if (propertymaster.FirstOrDefault().Reservation == "yes")
                                    {
                                        ViewBag.Message = $"This Property is already reserved by the Client {propertymaster.FirstOrDefault().Reservedfor} and the Lease No is {propertymaster.FirstOrDefault().Leaseno}";
                                        //return View();
                                        goto nationalities;
                                    }
                                }
                                
                                var recordcount1 = connection.Query<int>($"select count(*) rec from propertymaster where '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}' between leasestart and leaseend and propertyref='{newLOIModel.LOI.PropertyRefNo}' and vacatingstatus not in('Vacating')").FirstOrDefault();//, null, trans
                                if (recordcount1 > 0)
                                {
                                    ViewBag.Message = "This property is Occupied by this time.Check the Property master";
                                    //return View();
                                    goto nationalities;
                                }

                                if (recordcount > 0)
                                {
                                    string LOIpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/pp");
                                    if (!Directory.Exists(LOIpppath))
                                        Directory.CreateDirectory(LOIpppath);
                                    if(passportfile == null)
                                    {
                                        newLOIModel.LOI.Pp = "";
                                    }
                                    else
                                    {
                                        string ppextension = Path.GetExtension(passportfile.FileName);
                                        string ppfileName = newLOIModel.LOI.LoiNo1 + ppextension;
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
                                        newLOIModel.LOI.Pp = ppfileNameWithPath;
                                    }

                                    string LOIcidpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cid");
                                    if (!Directory.Exists(LOIcidpath))
                                        Directory.CreateDirectory(LOIcidpath);
                                    if (civilidfile == null)
                                    {
                                        newLOIModel.LOI.Cid = "";
                                    }
                                    else
                                    {
                                        string cidextension = Path.GetExtension(civilidfile.FileName);
                                        string cidfileName = newLOIModel.LOI.LoiNo1 + cidextension;
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
                                        newLOIModel.LOI.Cid = cidfileNameWithPath;
                                    }

                                    string LOInocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/noc");
                                    if (!Directory.Exists(LOInocpath))
                                        Directory.CreateDirectory(LOInocpath);
                                    if (nocvisafile == null)
                                    {
                                        newLOIModel.LOI.Noc = "";
                                    }
                                    else
                                    {
                                        string nocextension = Path.GetExtension(nocvisafile.FileName);
                                        string nocfileName = newLOIModel.LOI.LoiNo1 + nocextension;
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
                                        newLOIModel.LOI.Noc = nocfileNameWithPath;
                                    }

                                    string LOImcpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/mc");
                                    if (!Directory.Exists(LOImcpath))
                                        Directory.CreateDirectory(LOImcpath);
                                    if (marriagecontractfile == null)
                                    {
                                        newLOIModel.LOI.Mc = "";
                                    }
                                    else
                                    {
                                        string mcextension = Path.GetExtension(marriagecontractfile.FileName);
                                        string mcfileName = newLOIModel.LOI.LoiNo1 + mcextension;
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
                                        newLOIModel.LOI.Mc = mcfileNameWithPath;
                                    }

                                    string LOImocpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/moc");
                                    if (!Directory.Exists(LOImocpath))
                                        Directory.CreateDirectory(LOImocpath);
                                    if (moclicensefile == null)
                                    {
                                        newLOIModel.LOI.Moc = "";
                                    }
                                    else
                                    {
                                        string mocextension = Path.GetExtension(moclicensefile.FileName);
                                        string mocfileName = newLOIModel.LOI.LoiNo1 + mocextension;
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
                                        newLOIModel.LOI.Moc = mocfileNameWithPath;
                                    }

                                    string LOIcaspath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cas");
                                    if (!Directory.Exists(LOIcaspath))
                                        Directory.CreateDirectory(LOIcaspath);
                                    if (civilidASfile == null)
                                    {
                                        newLOIModel.LOI.Cas = "";
                                    }
                                    else
                                    {
                                        string casextension = Path.GetExtension(civilidASfile.FileName);
                                        string casfileName = newLOIModel.LOI.LoiNo1 + casextension;
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
                                        newLOIModel.LOI.Cas = casfileNameWithPath;
                                    }

                                    string LOIcoepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/coe");
                                    if (!Directory.Exists(LOIcoepath))
                                        Directory.CreateDirectory(LOIcoepath);
                                    if (salarycertificatefile == null)
                                    {
                                        newLOIModel.LOI.Coe = "";
                                    }
                                    else
                                    {
                                        string coeextension = Path.GetExtension(salarycertificatefile.FileName);
                                        string coefileName = newLOIModel.LOI.LoiNo1 + coeextension;
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
                                        newLOIModel.LOI.Coe = coefileNameWithPath;
                                    }

                                    string LOIcosignpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cosign");
                                    if (!Directory.Exists(LOIcosignpath))
                                        Directory.CreateDirectory(LOIcosignpath);
                                    if (companysignfile == null)
                                    {
                                        newLOIModel.LOI.Cosign = "";
                                    }
                                    else
                                    {
                                        string cosignextension = Path.GetExtension(companysignfile.FileName);
                                        string cosignfileName = newLOIModel.LOI.LoiNo1 + cosignextension;
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
                                        newLOIModel.LOI.Cosign = cosignfileNameWithPath;
                                    }

                                    string LOIcidpppath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cidpp");
                                    if (!Directory.Exists(LOIcidpppath))
                                        Directory.CreateDirectory(LOIcidpppath);
                                    if (staffemployeefile == null)
                                    {
                                        newLOIModel.LOI.Cidpp = "";
                                    }
                                    else
                                    {
                                        string cidppextension = Path.GetExtension(staffemployeefile.FileName);
                                        string cidppfileName = newLOIModel.LOI.LoiNo1 + cidppextension;
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
                                        newLOIModel.LOI.Cidpp = cidppfileNameWithPath;
                                    }

                                    string LOIcgpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/cg");
                                    if (!Directory.Exists(LOIcgpath))
                                        Directory.CreateDirectory(LOIcgpath);
                                    if (companyguaranteefile == null)
                                    {
                                        newLOIModel.LOI.Cg = "";
                                    }
                                    else
                                    {
                                        string cgextension = Path.GetExtension(companyguaranteefile.FileName);
                                        string cgfileName = newLOIModel.LOI.LoiNo1 + cgextension;
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
                                        newLOIModel.LOI.Cg = cgfileNameWithPath;
                                    }

                                    string LOIpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/supportingdocx/loi");
                                    if (!Directory.Exists(LOIpath))
                                        Directory.CreateDirectory(LOIpath);
                                    if(loifile == null)
                                    {
                                        newLOIModel.LOI.Loipath = "";
                                    }
                                    else
                                    {
                                        string loiextension = Path.GetExtension(loifile.FileName);
                                        string loifileName = newLOIModel.LOI.LoiNo1 + loiextension;
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
                                        newLOIModel.LOI.Loipath = loifileNameWithPath;
                                    }
                                    

                                    if (newLOIModel.MultipleApartment)
                                    {
                                        //var insert = await connection.ExecuteAsync($@"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,
                                        //        PropertyName,PropertyRefNo,aptno,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,
                                        //        clientRESF,LLRESF,docsubmitted,TotClients,SearchedProperty,LoiDate,LoiStatus,doc,dept,deptusr,loi_no,loipath,LOIREMARKS,movingindate)
                                        //        values('{newLOIModel.LOI.EnquiryType}','0000','-','{newLOIModel.LOI.ClientName}','{newLOIModel.LOI.Cmob}','{newLOIModel.LOI.Cnationality}',
                                        //        '{newLOIModel.LOI.ClientCompany}', '{newLOIModel.LOI.ClientSource}', '{newLOIModel.LOI.Req}', '{newLOIModel.LOI.Fur}', '{newLOIModel.LOI.PropertySource}',
                                        //        '{newLOIModel.LOI.PropertyName}', '{newLOIModel.GroupPref}', '{newLOIModel.GroupAptno}', CONVERT(date, '{newLOIModel.LOI.Leasedate}', 103),
                                        //        CONVERT(date, '{newLOIModel.LOI.Leaseenddate}', 103), CONVERT(date, '{newLOIModel.LOI.Loisigndate}', 103), '{newLOIModel.LOI.Ap}',
                                        //        '{newLOIModel.LOI.Aptype}', '{newLOIModel.LOI.Renttobepaidby}', '{newLOIModel.LOI.Rent}', '{newLOIModel.LOI.Securitydepositpaidby}',
                                        //        '{newLOIModel.LOI.Deposit}', '{newLOIModel.LOI.Feetobepaidby}', '{newLOIModel.LOI.ClientResf}', '{newLOIModel.LOI.Llresf}', '{newLOIModel.LOI.Docsubmitted}',
                                        //        '{newLOIModel.LOI.TotClients}', '{newLOIModel.LOI.SearchedProperty}', getdate(), '{newLOIModel.LOI.LoiStatus}', getdate(), 'Admin', '{HttpContext.Session.GetString("CurrentUsername")}',
                                        //        '{newLOIModel.LOI.LoiNo1}', '{newLOIModel.LOI.Loipath}', '{newLOIModel.LOI.Loiremarks}', CONVERT(date, '{newLOIModel.LOI.Movingindate}', 103))");//, null, trans

                                        var insert = await connection.ExecuteAsync($@"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,
                                                PropertyName,PropertyRefNo,aptno,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,
                                                clientRESF,LLRESF,docsubmitted,TotClients,SearchedProperty,LoiDate,LoiStatus,doc,dept,deptusr,loi_no,loipath,LOIREMARKS,movingindate,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc)
                                                values('{newLOIModel.LOI.EnquiryType}','0000','-','{newLOIModel.LOI.ClientName}','{newLOIModel.LOI.Cmob}','{newLOIModel.LOI.Cnationality}',
                                                '{newLOIModel.LOI.ClientCompany}', '{newLOIModel.LOI.ClientSource}', '{newLOIModel.LOI.Req}', '{newLOIModel.LOI.Fur}', '{newLOIModel.LOI.PropertySource}',
                                                '{newLOIModel.LOI.PropertyName}', '{newLOIModel.GroupPref}', '{newLOIModel.GroupAptno}', '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}',
                                                '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', '{Convert.ToDateTime(newLOIModel.LOI.Loisigndate).ToString("yyyy-MM-dd")}', '{newLOIModel.LOI.Ap}',
                                                '{newLOIModel.LOI.Aptype}', '{newLOIModel.LOI.Renttobepaidby}', '{newLOIModel.LOI.Rent}', '{newLOIModel.LOI.Securitydepositpaidby}',
                                                '{newLOIModel.LOI.Deposit}', '{newLOIModel.LOI.Feetobepaidby}', '{newLOIModel.LOI.ClientResf}', '{newLOIModel.LOI.Llresf}', '{newLOIModel.LOI.Docsubmitted}',
                                                '{newLOIModel.LOI.TotClients}', '{newLOIModel.LOI.SearchedProperty}', getdate(), '{newLOIModel.LOI.LoiStatus}', getdate(), 'Admin', '{HttpContext.Session.GetString("CurrentUsername")}',
                                                '{newLOIModel.LOI.LoiNo1}', '{newLOIModel.LOI.Loipath}', '{newLOIModel.LOI.Loiremarks}', '{Convert.ToDateTime(newLOIModel.LOI.Movingindate).ToString("yyyy-MM-dd")}','{newLOIModel.LOI.Pp}','{newLOIModel.LOI.Cid}','{newLOIModel.LOI.Noc}','{newLOIModel.LOI.Mc}','{newLOIModel.LOI.Coe}','{newLOIModel.LOI.Cosign}','{newLOIModel.LOI.Cas}','{newLOIModel.LOI.Cidpp}','{newLOIModel.LOI.Cg}','{newLOIModel.LOI.Moc}')");//, null, trans

                                        //var update = await connection.ExecuteAsync($@"update propertymaster set RENTED='NO',updated='YES',leasetype='New LC',paid='NO',status='NotAvailable',rstatus='LOI Created',reservation='yes',
                                        //        leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                        //        rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                        //        rlstart = CONVERT(date, '{newLOIModel.LOI.Leasedate}', 103), rlend = CONVERT(date, '{newLOIModel.LOI.Leaseenddate}', 103), pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'NewLoi'
                                        //        where PropertySource = 'ManagedProperty'
                                        //        and PropertyRef in ({newLOIModel.GroupPref})");//, null, trans

                                        var update = await connection.ExecuteAsync($@"update propertymaster set RENTED='NO',updated='YES',leasetype='New LC',paid='NO',status='NotAvailable',rstatus='LOI Created',reservation='yes',
                                                leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                                rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                                rlstart = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}', rlend = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'NewLoi'
                                                where PropertySource = 'ManagedProperty'
                                                and PropertyRef in ({newLOIModel.GroupPref})");//, null, trans
                                    }
                                    else
                                    {
                                        //var insert = await connection.ExecuteAsync($@"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,
                                        //        PropertyName,PropertyRefNo,aptno,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,
                                        //        clientRESF,LLRESF,docsubmitted,TotClients,SearchedProperty,LoiDate,LoiStatus,doc,dept,deptusr,loi_no,loipath,LOIREMARKS,movingindate)
                                        //        values('{newLOIModel.LOI.EnquiryType}','0000','-','{newLOIModel.LOI.ClientName}','{newLOIModel.LOI.Cmob}','{newLOIModel.LOI.Cnationality}',
                                        //        '{newLOIModel.LOI.ClientCompany}', '{newLOIModel.LOI.ClientSource}', '{newLOIModel.LOI.Req}', '{newLOIModel.LOI.Fur}', '{newLOIModel.LOI.PropertySource}',
                                        //        '{newLOIModel.LOI.PropertyName}', '{newLOIModel.LOI.PropertyRefNo}', '{newLOIModel.LOI.Aptno}', CONVERT(date, '{newLOIModel.LOI.Leasedate}', 103),
                                        //        CONVERT(date, '{newLOIModel.LOI.Leaseenddate}', 103), CONVERT(date, '{newLOIModel.LOI.Loisigndate}', 103), '{newLOIModel.LOI.Ap}',
                                        //        '{newLOIModel.LOI.Aptype}', '{newLOIModel.LOI.Renttobepaidby}', '{newLOIModel.LOI.Rent}', '{newLOIModel.LOI.Securitydepositpaidby}',
                                        //        '{newLOIModel.LOI.Deposit}', '{newLOIModel.LOI.Feetobepaidby}', '{newLOIModel.LOI.ClientResf}', '{newLOIModel.LOI.Llresf}', '{newLOIModel.LOI.Docsubmitted}',
                                        //        '{newLOIModel.LOI.TotClients}', '{newLOIModel.LOI.SearchedProperty}', getdate(), '{newLOIModel.LOI.LoiStatus}', getdate(), 'Admin', '{HttpContext.Session.GetString("CurrentUsername")}',
                                        //        '{newLOIModel.LOI.LoiNo1}', '{newLOIModel.LOI.Loipath}', '{newLOIModel.LOI.Loiremarks}', CONVERT(date, '{newLOIModel.LOI.Movingindate}', 103))");//, null, trans

                                        var insert = await connection.ExecuteAsync($@"insert into LOIInformation(EnquiryType,inqno,LEName,ClientName,cmob,CNationality,ClientCompany,ClientSource,req,fur,PropertySource,
                                                PropertyName,PropertyRefNo,aptno,leasedate,Leaseenddate,loisigndate,Ap,aptype,Renttobepaidby,Rent,Securitydepositpaidby,deposit,feetobepaidby,
                                                clientRESF,LLRESF,docsubmitted,TotClients,SearchedProperty,LoiDate,LoiStatus,doc,dept,deptusr,loi_no,loipath,LOIREMARKS,movingindate,pp,cid,noc,mc,coe,cosign,cas,cidpp,cg,moc)
                                                values('{newLOIModel.LOI.EnquiryType}','0000','-','{newLOIModel.LOI.ClientName}','{newLOIModel.LOI.Cmob}','{newLOIModel.LOI.Cnationality}',
                                                '{newLOIModel.LOI.ClientCompany}', '{newLOIModel.LOI.ClientSource}', '{newLOIModel.LOI.Req}', '{newLOIModel.LOI.Fur}', '{newLOIModel.LOI.PropertySource}',
                                                '{newLOIModel.LOI.PropertyName}', '{newLOIModel.LOI.PropertyRefNo}', '{newLOIModel.LOI.Aptno}', '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}',
                                                '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', '{Convert.ToDateTime(newLOIModel.LOI.Loisigndate).ToString("yyyy-MM-dd")}', '{newLOIModel.LOI.Ap}',
                                                '{newLOIModel.LOI.Aptype}', '{newLOIModel.LOI.Renttobepaidby}', '{newLOIModel.LOI.Rent}', '{newLOIModel.LOI.Securitydepositpaidby}',
                                                '{newLOIModel.LOI.Deposit}', '{newLOIModel.LOI.Feetobepaidby}', '{newLOIModel.LOI.ClientResf}', '{newLOIModel.LOI.Llresf}', '{newLOIModel.LOI.Docsubmitted}',
                                                '{newLOIModel.LOI.TotClients}', '{newLOIModel.LOI.SearchedProperty}', getdate(), '{newLOIModel.LOI.LoiStatus}', getdate(), 'Admin', '{HttpContext.Session.GetString("CurrentUsername")}',
                                                '{newLOIModel.LOI.LoiNo1}', '{newLOIModel.LOI.Loipath}', '{newLOIModel.LOI.Loiremarks}', '{Convert.ToDateTime(newLOIModel.LOI.Movingindate).ToString("yyyy-MM-dd")}','{newLOIModel.LOI.Pp}','{newLOIModel.LOI.Cid}','{newLOIModel.LOI.Noc}','{newLOIModel.LOI.Mc}','{newLOIModel.LOI.Coe}','{newLOIModel.LOI.Cosign}','{newLOIModel.LOI.Cas}','{newLOIModel.LOI.Cidpp}','{newLOIModel.LOI.Cg}','{newLOIModel.LOI.Moc}')");//, null, trans

                                        //var update = await connection.ExecuteAsync($@"update propertymaster set RENTED='NO',updated='YES',leasetype='New LC',paid='NO',status='NotAvailable',rstatus='LOI Created',reservation='yes',
                                        //        leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                        //        rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                        //        rlstart = CONVERT(date, '{newLOIModel.LOI.Leasedate}', 103), rlend = CONVERT(date, '{newLOIModel.LOI.Leaseenddate}', 103), pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'NewLoi'
                                        //        where PropertySource = 'ManagedProperty'
                                        //        and PropertyRef = '{newLOIModel.LOI.PropertyRefNo}'");//, null, trans

                                        var update = await connection.ExecuteAsync($@"update propertymaster set RENTED='NO',updated='YES',leasetype='New LC',paid='NO',status='NotAvailable',rstatus='LOI Created',reservation='yes',
                                                leaseno='{newLOIModel.LOI.LoiNo1}',reservedfor='{newLOIModel.LOI.ClientName}',rnat='{newLOIModel.LOI.Cnationality}',
                                                rmob = '{newLOIModel.LOI.Cmob}', reservedrent = '{newLOIModel.AptRent}', rftype = '{newLOIModel.LOI.Fur}', rbtype = '{newLOIModel.LOI.Req}',
                                                rlstart = '{Convert.ToDateTime(newLOIModel.LOI.Leasedate).ToString("yyyy-MM-dd")}', rlend = '{Convert.ToDateTime(newLOIModel.LOI.Leaseenddate).ToString("yyyy-MM-dd")}', pupdatetime = getdate(), pupdateby = '{Environment.MachineName}', pmode = 'NewLoi'
                                                where PropertySource = 'ManagedProperty'
                                                and PropertyRef = '{newLOIModel.LOI.PropertyRefNo}'");//, null, trans
                                    }
                                }
                                else
                                {
                                    ViewBag.Message = "Property Not Found,create the Property";
                                    goto nationalities;
                                }


                                ModelState.Clear();
                                ViewBag.Message = "LOI is generated Successfully";
                            }
                            catch (Exception ex)
                            {
                                logger.Info(ex);
                                //#if DEBUG
                                //    ViewBag.Message = ex;
                                //#else
                                //    ViewBag.Message = "Unknown Error";
                                //#endif


                                ViewBag.Message = "Unknown Error";
                                //Console.WriteLine(ex);
                            }
                        }
                    }
                    connection.Close();
                }
            }

            newLOIModel.LOI = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var count = connection.Query<int>(@"select count(*) from LOIInformation where year(getdate())=year(LoiDate)").FirstOrDefault();

                var day = "";
                var month = "";

                if (DateTime.Now.Day.ToString().Length == 1)
                    day = "0" + DateTime.Now.Day.ToString();
                else
                    day = DateTime.Now.Day.ToString();

                if (DateTime.Now.Month.ToString().Length == 1)
                    month = "0" + DateTime.Now.Month.ToString();
                else
                    month = DateTime.Now.Month.ToString();

                if (count == 0)
                {
                    newLOIModel.LOI.LoiNo1 = "LOI-" + DateTime.Today.Year + "-" + day + month + "-" + "0000";
                }
                else
                {
                    var newloino = connection.Query<int>(@"select right(loi_no,4)+1 from LOIInformation where id=(select max(id) from LOIInformation where year(getdate())=year(LoiDate))").FirstOrDefault();

                    var loino = "";
                    if (newloino.ToString().Length == 1)
                        loino = "000" + newloino;
                    else if (newloino.ToString().Length == 2)
                        loino = "00" + newloino;
                    else if (newloino.ToString().Length == 3)
                        loino = "000" + newloino;

                    newLOIModel.LOI.LoiNo1 = "LOI-" + DateTime.Today.Year + "-" + day + month + "-" + loino;

                }
                connection.Close();
            }

            nationalities:
            //newLOIModel.Nationalities = context.Nationalities.ToList();
            return View(newLOIModel);
        }

        public async Task<IActionResult> ListLOI(/*int page = 1, int pageSize = 10*/)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                ListLOIModel listLOIModel = new();
                var loiinformations = await connection.QueryAsync<Loiinformation>($"select top 50 id,LoiNo,loi_no LoiNo1,inqno,LEName,ClientName,ClientSource,PropertySource,PropertyName,PropertyRefNo,CONVERT(VARCHAR(11),Leasedate ,106) as Leasedate,LoiStatus,loimailstatus,Lccreate from loiInformation order by id desc");
                listLOIModel.loiinformations = loiinformations.ToList();
                //PagedList<PMS_Admin_Web.Models.Loiinformation> model = new PagedList<PMS_Admin_Web.Models.Loiinformation>(loi, page, pageSize);
                listLOIModel.nocancelled = connection.Query<int>($"select count(*) from loiInformation where id in(select top 50 id from loiInformation order by id desc) and LoiStatus ='Cancelled' ").FirstOrDefault();
                listLOIModel.noprogress = connection.Query<int>($"select count(*) from loiInformation where id in(select top 50 id from loiInformation order by id desc) and LoiStatus not in('Cancelled') ").FirstOrDefault();
                connection.Close();
                return View(listLOIModel);
            }
            
            //List<PMS_Admin_Web.Models.Loiinformation> loiinformations = context.Loiinformations.OrderByDescending(x=>x.Id).ToList();
            ////var results = q.OrderByDescending(x => x.StatusId == 3 ? x.ReserveDate : x.LastUpdateDate);
            //PagedList<PMS_Admin_Web.Models.Loiinformation> model = new PagedList<PMS_Admin_Web.Models.Loiinformation>(loiinformations, page, pageSize);
            //return View(model);
            
        }

        public ActionResult ViewLOI(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select loipath from LOIInformation where ID={id}").FirstOrDefault();
                connection.Close();
                if(file != "")
                {
                    byte[] abc = System.IO.File.ReadAllBytes(file);
                    System.IO.File.WriteAllBytes(file, abc);
                    MemoryStream ms = new MemoryStream(abc);
                    return new FileStreamResult(ms, "application/pdf");
                }
                else
                {
                    return RedirectToAction("ListLOI", "LOI");
                }
            }
        }

        public ActionResult ViewPassport(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var file = connection.Query<string>($"select pp from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select cid from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select noc from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select coe from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select cosign from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select cas from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select cidpp from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select cg from LOIInformation where ID={id}").FirstOrDefault();
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
                var file = connection.Query<string>($"select moc from LOIInformation where ID={id}").FirstOrDefault();
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

        public int loirecordcount(int id)
        {
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var mainloino = connection.Query<string>($"select loi_no from LOIInformation where ID={id}").FirstOrDefault();
                var LOIRECORDCOUNT = connection.Query<int>($"select count(*) from lcinfo where loi_no='{mainloino}' and len(LOI_no)>1").FirstOrDefault();
                connection.Close();
                return LOIRECORDCOUNT;
            }
        }
        
        public string DeleteLOI(int Id)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            try
            {
                var deleteLC = context.Loiinformations.Find(Id);
                context.Loiinformations.Remove(deleteLC);
                context.SaveChanges();
                ViewBag.Message = "Deleted Successfully";
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                ViewBag.Message = "Unknown Error";
            }
            return ViewBag.Message;
        }

        //public IActionResult LOINoFixing(LOINoFixingModel lOINoFixingModel)
        //{
        //    lOINoFixingModel.SelectLOINo = context.Loiinformations.ToList();
        //    return View("LOI", lOINoFixingModel);
        //}

        public async Task<string> UpdateLOINo(string oldloino, string newloino)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("NLog.config").GetCurrentClassLogger();
            if (oldloino != null && newloino != null)
            {
                using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        var IsExist = await connection.QueryAsync<Loiinformation>($"select * from LOIInformation where loi_no='{oldloino}'");
                        var Exist = await connection.QueryAsync<int>($"select count(1) from LOIInformation where loi_no='{newloino}'");
                        if (Exist.FirstOrDefault() == 0)
                        {
                            Loiinformation loiinformation = new();
                            loiinformation = IsExist.FirstOrDefault();
                            loiinformation.LoiNo1 = newloino;
                            context.Update(loiinformation);
                            context.SaveChanges();
                            var ExistLOIinPM = connection.Query<int>($"select COUNT(*) from propertymaster where leaseno='{oldloino}'").FirstOrDefault();
                            var ExistLOIinLC = connection.Query<int>($"select COUNT(*) from lcinfo where LOI_no='{oldloino}'").FirstOrDefault();
                            if (ExistLOIinPM == 1)
                            {
                                var updateLOIofPM = await connection.ExecuteAsync($"update propertymaster set leaseno='{newloino}' where PropertyRef='{loiinformation.PropertyRefNo}'");
                            }
                            if (ExistLOIinLC == 1)
                            {
                                var updateLOIofLC = await connection.ExecuteAsync($"update lcinfo set LOI_no='{newloino}' where pref='{loiinformation.PropertyRefNo}'");
                            }

                            ModelState.Clear();

                            ViewBag.Message = "Updated";
                        }
                        else
                        {
                            ViewBag.Message = "New LOI No already exist";
                        }
                    }
                    catch(Exception ex)
                    {
                        logger.Info(ex);
                        ViewBag.Message = "Unknown Error";
                    }
                    
                    connection.Close();
                }
            }
            return ViewBag.Message;
        }

        public List<Loiinformation> SearchLOI(string loinoKeyword)
        {
            using(var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                var loiResult = connection.Query<Loiinformation>($"select *,loi_no loiNo1 from LOIInformation where loi_no like '%{loinoKeyword}%'").ToList();
                connection.Close();
                return loiResult;
            }
        }

        
    }
}
