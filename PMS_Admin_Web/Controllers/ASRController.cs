using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using PMS_Admin_Web.Models;
using PMS_Admin_Web.Models.ASR;

namespace PMS_Admin_Web.Controllers
{
    public class ASRController : Controller
    {
        private Connection sqlConnectionString = new();

        public IActionResult AmalResidence()
        {
            AmalResidenceASR model = new();
            model.CurrentDate = DateTime.Today.ToShortDateString();
            using (var connection=new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                model.Address = connection.Query<string>(@"select address from lldetails where pname='Amal Residence'").FirstOrDefault();

                model.Command = connection.Query<Propertymaster1>(@$"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS  CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='1'").FirstOrDefault();

                model.Command1 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='2'").FirstOrDefault();

                model.Command2 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='3'").FirstOrDefault();

                model.Command3 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='4'").FirstOrDefault();

                model.Command4 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='5'").FirstOrDefault();

                model.Command5 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='6'").FirstOrDefault();

                model.Command6 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='7'").FirstOrDefault();

                model.Command7 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='8'").FirstOrDefault();

                model.Command8 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='9'").FirstOrDefault();

                model.Command9 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='10'").FirstOrDefault();

                model.Command10 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='11'").FirstOrDefault();

                model.Command11 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='12'").FirstOrDefault();

                model.Command12 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='13'").FirstOrDefault();

                model.Command13 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='14'").FirstOrDefault();

                model.Command14 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='15'").FirstOrDefault();

                model.Command15 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='16'").FirstOrDefault();

                model.Command16 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='17'").FirstOrDefault();

                model.Command17 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='18'").FirstOrDefault();

                model.Command18 = connection.Query<Propertymaster1>($@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='19'").FirstOrDefault();

                model.Shop = connection.Query<Propertymaster1>(@"select status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='Amal Residence' and
                                                                propertysource='ManagedProperty' and aptno='Shop'").FirstOrDefault();

                connection.Close();
            }
            return View(model);
        }

        public IActionResult PropertyASR(string buildingName)
        {
            PropertyASRModel model = new();
            model.CurrentDate = DateTime.Today.ToShortDateString();
            model.PropertyName = buildingName;
            model.NoOfFlats = new();
            model.Command = new();
            model.BDR2 = new();
            model.BDR3 = new();
            model.SHOP = new();
            model.STORE1 = new();
            model.STORE2 = new();
            //model.BDR2.Reserved = new();
            //model.BDR2.Rented = new();
            //model.BDR2.Vacated = new();
            using (var connection = new SqlConnection(sqlConnectionString.ConnectionString))
            {
                connection.Open();
                //model.Apts = connection.Query<string>($"select AptNo from propertymaster where BldgName='{buildingName}'").ToList();
                //model.Apts = connection.Query<string>($"SELECT AptNo FROM propertymaster WHERE BldgName='{buildingName}' and AptNo NOT LIKE '%[A-Za-z]%' order by id desc").ToList();
                //model.Floors = connection.Query<string>($"select DISTINCT Floors from propertymaster where BldgName='{buildingName}'").ToList();
                //model.Floors = connection.Query<string>($"select DISTINCT Floors from propertymaster where BldgName='amal residence' and Floors NOT LIKE '%[A-Za-z]%' group by Floors order by Floors desc").ToList();

                //model.AlphaApt = connection.Query<string>($"SELECT AptNo FROM propertymaster WHERE BldgName='amal residence' and AptNo NOT LIKE '%[0-9]%' order by id desc").ToList();
                //model.AlphaFloor = connection.Query<string>($"select DISTINCT Floors from propertymaster where BldgName='amal residence' and Floors NOT LIKE '%[0-9]%' group by Floors order by Floors desc").ToList();
                
                model.Amenities= connection.Query<Propertymaster1>($"select * from propertymaster where BldgName='{buildingName}'").FirstOrDefault();
                model.NoOfFlats = connection.Query<int>($"select COUNT(AptNo) from propertymaster where BldgName='{buildingName}'").FirstOrDefault();//and AptNo NOT LIKE '%[A-Za-z]%'
                model.NoOfFloors = connection.Query<int>($"select COUNT(Distinct Floors) from propertymaster where BldgName='{buildingName}'").FirstOrDefault();
                model.Address = connection.Query<string>($"select address from lldetails where pname='{buildingName}'").FirstOrDefault();

                model.Command = connection.Query<Propertymaster1>($@"select floors,status,reservation,aptno,case when reservation='yes' and rstatus not in('LC Renewal') then 'Reserved for '+ reservedfor
                                                                when reservation='yes' and rstatus='LC Renewal' then 'Lc renewal for-'+reservedfor when Status ='Available' and  vacatingstatus='vacating' then 'Vacating on '+CONVERT(VARCHAR(11),moveout ,106)+''+cname 
                                                                when Status ='Available' and vacatingstatus is null then 'VACANT'
                                                                when Status ='Available' and len(vacatingstatus)<1 then 'VACANT'
                                                                else CNAME end as name,
                                                                case when reservation='yes'  then reservedrent when Status ='Available' and  vacatingstatus='vacating' then CRENT when Status ='Available' and vacatingstatus is null then AMOUNT
                                                                when Status ='Available' and len(vacatingstatus)<1 then AMOUNT else CRENT end as rent,CNAT,case when reservation='yes'  then CONVERT(VARCHAR(11),rlstart ,106)  when Status ='Available' 
                                                                and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),leasestart  ,106) 
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then '' else CONVERT(VARCHAR(11),leasestart  ,106) end as lstart ,
                                                                case when reservation='yes'  then CONVERT(VARCHAR(11),rlend  ,106)  when Status ='Available' and  vacatingstatus='vacating'  then CONVERT(VARCHAR(11),LEASEEND  ,106)
                                                                when Status ='Available' and vacatingstatus is null then ''
                                                                when Status ='Available' and len(vacatingstatus)<1 then ''
                                                                else CONVERT(VARCHAR(11),LEASEEND  ,106) end as  LEND, CASE WHEN CBTYPE IS NULL THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S'  when left(CBTYPE,1)='-' then '-' else LEFT(BED,1)+'BDR' end  WHEN LEN(CBTYPE)=0 THEN case when left(bed,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'   else LEFT(BED,1)+'BDR' end  ELSE  case when left(CBTYPE,1)='M' then 'M' when left(CBTYPE,1)='S' then 'S' when left(CBTYPE,1)='-' then '-'  else  left(CBTYPE,1)+'BDR' end END AS  CBTYPE,CASE WHEN CFTYPE='FF' THEN 'FF' WHEN CFTYPE='Fully-furnished' THEN 'FF' WHEN  CFTYPE='SF' THEN 'SF' WHEN CFTYPE='Semi Furnished' THEN 'SF'  WHEN  CFTYPE='UF' THEN 'UF' WHEN CFTYPE='Un Furnished' THEN 'UF' WHEN CFTYPE='' THEN Furnished  WHEN CFTYPE IS NULL THEN Furnished  END AS CFTYPE,vacatingstatus ,Status ,(select payable  from lcinfo where lc_no=cleaseno  ) AS PR,(select pmode from lcinfo where lc_no=cleaseno  ) AS PM  
                                                                from propertymaster
                                                                where bldgname='{buildingName}' and
                                                                propertysource='ManagedProperty' order by id asc").ToList();//aptno='{apt}'

                //model.Command.AddRange(command);

                model.BDR2.Reserved = connection.Query<PMstatistics>($@"Select Distinct case when reservation='yes' and status='NotAvailable' then 'RESERVED' END APTSTATUS,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%2%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%2%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%2%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where reservation='yes' and status='NotAvailable'").FirstOrDefault();
                model.BDR2.Rented = connection.Query<PMstatistics>($@"Select Distinct case when Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) then 'RENTED' END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%2%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%2%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%2%' and cftype='UF') as UF
                                                                    FROM propertymaster 
                                                                    where Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL)").FirstOrDefault();
                model.BDR2.Vacated = connection.Query<PMstatistics>($@"Select Distinct case WHEN Status ='Available' THEN 'VACANT' ELSE Status END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%2%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%2%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%2%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where Status ='Available'").FirstOrDefault();

                model.BDR3.Reserved = connection.Query<PMstatistics>($@"Select Distinct case when reservation='yes' and status='NotAvailable' then 'RESERVED' END APTSTATUS,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%3%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%3%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and Bed like '%3%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where reservation='yes' and status='NotAvailable'").FirstOrDefault();
                model.BDR3.Rented = connection.Query<PMstatistics>($@"Select Distinct case when Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) then 'RENTED' END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%3%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%3%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and Bed like '%3%' and cftype='UF') as UF
                                                                    FROM propertymaster 
                                                                    where Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL)").FirstOrDefault();
                model.BDR3.Vacated = connection.Query<PMstatistics>($@"Select Distinct case WHEN Status ='Available' THEN 'VACANT' ELSE Status END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%3%' and cftype='FF') as FF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%3%' and cftype='SF') as SF,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and Bed like '%3%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where Status ='Available'").FirstOrDefault();

                model.SHOP.Reserved = connection.Query<PMstatistics>($@"Select Distinct case when reservation='yes' and status='NotAvailable' then 'RESERVED' END APTSTATUS,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and AptNo like '%SHOP%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where reservation='yes' and status='NotAvailable'").FirstOrDefault();
                model.SHOP.Rented = connection.Query<PMstatistics>($@"Select Distinct case when Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) then 'RENTED' END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and AptNo like '%SHOP%' and cftype='UF') as UF
                                                                    FROM propertymaster 
                                                                    where Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL)").FirstOrDefault();
                model.SHOP.Vacated = connection.Query<PMstatistics>($@"Select Distinct case WHEN Status ='Available' THEN 'VACANT' ELSE Status END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and AptNo like '%SHOP%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where Status ='Available'").FirstOrDefault();

                model.STORE1.Reserved = connection.Query<PMstatistics>($@"Select Distinct case when reservation='yes' and status='NotAvailable' then 'RESERVED' END APTSTATUS,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and AptNo like '%Store1%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where reservation='yes' and status='NotAvailable'").FirstOrDefault();
                model.STORE1.Rented = connection.Query<PMstatistics>($@"Select Distinct case when Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) then 'RENTED' END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and AptNo like '%Store1%' and cftype='UF') as UF
                                                                    FROM propertymaster 
                                                                    where Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL)").FirstOrDefault();
                model.STORE1.Vacated = connection.Query<PMstatistics>($@"Select Distinct case WHEN Status ='Available' THEN 'VACANT' ELSE Status END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and AptNo like '%Store1%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where Status ='Available'").FirstOrDefault();

                model.STORE2.Reserved = connection.Query<PMstatistics>($@"Select Distinct case when reservation='yes' and status='NotAvailable' then 'RESERVED' END APTSTATUS,
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and reservation='yes' and status='NotAvailable' and AptNo like '%Store2%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where reservation='yes' and status='NotAvailable'").FirstOrDefault();
                model.STORE2.Rented = connection.Query<PMstatistics>($@"Select Distinct case when Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) then 'RENTED' END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL) and AptNo like '%Store2%' and cftype='UF') as UF
                                                                    FROM propertymaster 
                                                                    where Status ='NotAvailable' and (LEN(reservation)=0 or reservation IS NULL)").FirstOrDefault();
                model.STORE2.Vacated = connection.Query<PMstatistics>($@"Select Distinct case WHEN Status ='Available' THEN 'VACANT' ELSE Status END AS APTSTATUS, 
                                                                    (select count(*) from propertymaster where BldgName='{buildingName}' and Status ='Available' and AptNo like '%Store2%' and cftype='UF') as UF
                                                                    FROM propertymaster
                                                                    where Status ='Available'").FirstOrDefault();


                connection.Close();
            }
            return View(model);
        }
    }
}
