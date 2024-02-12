using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PMS_Admin_Web.Models
{
    public partial class RealtorContext : DbContext
    {
        public RealtorContext()
        {
        }

        public RealtorContext(DbContextOptions<RealtorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accamt> Accamts { get; set; }
        public virtual DbSet<AccountStname> AccountStnames { get; set; }
        public virtual DbSet<AccountsCategoryExpense> AccountsCategoryExpenses { get; set; }
        public virtual DbSet<AccountsPm> AccountsPms { get; set; }
        public virtual DbSet<Actiondetail> Actiondetails { get; set; }
        public virtual DbSet<Adminactivity> Adminactivities { get; set; }
        public virtual DbSet<Advpayment> Advpayments { get; set; }
        public virtual DbSet<Amal> Amals { get; set; }
        public virtual DbSet<Amal1> Amal1s { get; set; }
        public virtual DbSet<AmalResidence> AmalResidences { get; set; }
        public virtual DbSet<AmarillaResidence> AmarillaResidences { get; set; }
        public virtual DbSet<ArcadiaTowerA> ArcadiaTowerAs { get; set; }
        public virtual DbSet<ArcadiaTowerB> ArcadiaTowerBs { get; set; }
        public virtual DbSet<ArcadiaTowerC> ArcadiaTowerCs { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Asrchange> Asrchanges { get; set; }
        public virtual DbSet<Asrnote> Asrnotes { get; set; }
        public virtual DbSet<Attender> Attenders { get; set; }
        public virtual DbSet<Autologout> Autologouts { get; set; }
        public virtual DbSet<BBuilding> BBuildings { get; set; }
        public virtual DbSet<Basaeer2> Basaeer2s { get; set; }
        public virtual DbSet<Basaeer5> Basaeer5s { get; set; }
        public virtual DbSet<Basaeer6> Basaeer6s { get; set; }
        public virtual DbSet<Basaeer7> Basaeer7s { get; set; }
        public virtual DbSet<Basaeer8> Basaeer8s { get; set; }
        public virtual DbSet<Basaeer9> Basaeer9s { get; set; }
        public virtual DbSet<Bcode> Bcodes { get; set; }
        public virtual DbSet<Building15> Building15s { get; set; }
        public virtual DbSet<Bulkprinting> Bulkprintings { get; set; }
        public virtual DbSet<Bulkprintingtemp> Bulkprintingtemps { get; set; }
        public virtual DbSet<Cancelledreceipt> Cancelledreceipts { get; set; }
        public virtual DbSet<Cancellrequest> Cancellrequests { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cc> Ccs { get; set; }
        public virtual DbSet<Ccat> Ccats { get; set; }
        public virtual DbSet<Cgl> Cgls { get; set; }
        public virtual DbSet<ChangeinApt> ChangeinApts { get; set; }
        public virtual DbSet<Checking> Checkings { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Clientquestion> Clientquestions { get; set; }
        public virtual DbSet<Combineapartment> Combineapartments { get; set; }
        public virtual DbSet<Cremark> Cremarks { get; set; }
        public virtual DbSet<DailyTask> DailyTasks { get; set; }
        public virtual DbSet<DaliaTower> DaliaTowers { get; set; }
        public virtual DbSet<Deletedlogin> Deletedlogins { get; set; }
        public virtual DbSet<Deletedtvoucher> Deletedtvouchers { get; set; }
        public virtual DbSet<Deltedinquiry> Deltedinquiries { get; set; }
        public virtual DbSet<Dhc> Dhcs { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<DriverScheduel> DriverScheduels { get; set; }
        public virtual DbSet<Driverformat> Driverformats { get; set; }
        public virtual DbSet<Dummy> Dummies { get; set; }
        public virtual DbSet<Dummy1> Dummy1s { get; set; }
        public virtual DbSet<Dummyfileupload> Dummyfileuploads { get; set; }
        public virtual DbSet<Emailuser> Emailusers { get; set; }
        public virtual DbSet<FaisalTower> FaisalTowers { get; set; }
        public virtual DbSet<Fcacategory> Fcacategories { get; set; }
        public virtual DbSet<Forty1Residence> Forty1Residences { get; set; }
        public virtual DbSet<Fschedule> Fschedules { get; set; }
        public virtual DbSet<Fscheduleattender> Fscheduleattenders { get; set; }
        public virtual DbSet<Fscheduleattender1> Fscheduleattenders1 { get; set; }
        public virtual DbSet<Fscheduleattendershistory> Fscheduleattendershistories { get; set; }
        public virtual DbSet<Fschedulehistory> Fschedulehistories { get; set; }
        public virtual DbSet<Governarate> Governarates { get; set; }
        public virtual DbSet<HawallyResidence> HawallyResidences { get; set; }
        public virtual DbSet<Haya1> Haya1s { get; set; }
        public virtual DbSet<Haya2> Haya2s { get; set; }
        public virtual DbSet<Haya4> Haya4s { get; set; }
        public virtual DbSet<HessaBuilding> HessaBuildings { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Img> Imgs { get; set; }
        public virtual DbSet<Issueitem> Issueitems { get; set; }
        public virtual DbSet<ItemDetail> ItemDetails { get; set; }
        public virtual DbSet<Itempurchased> Itempurchaseds { get; set; }
        public virtual DbSet<Jobverification> Jobverifications { get; set; }
        public virtual DbSet<LavanResidence1> LavanResidence1s { get; set; }
        public virtual DbSet<LavanResidenceMarina> LavanResidenceMarinas { get; set; }
        public virtual DbSet<Lavaranda2> Lavaranda2s { get; set; }
        public virtual DbSet<Lcchange> Lcchanges { get; set; }
        public virtual DbSet<Lcinfo> Lcinfos { get; set; }
        public virtual DbSet<Leasehistory> Leasehistories { get; set; }
        public virtual DbSet<Leinfo> Leinfos { get; set; }
        public virtual DbSet<Lldetail> Lldetails { get; set; }
        public virtual DbSet<Llreport> Llreports { get; set; }
        public virtual DbSet<Locationgroup> Locationgroups { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Loiinformation> Loiinformations { get; set; }
        public virtual DbSet<LulwahAlMullahComplex> LulwahAlMullahComplices { get; set; }
        public virtual DbSet<Madmin> Madmins { get; set; }
        public virtual DbSet<Maintenacecat> Maintenacecats { get; set; }
        public virtual DbSet<Maintenancecontractor> Maintenancecontractors { get; set; }
        public virtual DbSet<MaintenceBlock> MaintenceBlocks { get; set; }
        public virtual DbSet<Manalysis> Manalyses { get; set; }
        public virtual DbSet<Manalysis1> Manalysis1s { get; set; }
        public virtual DbSet<MariamResidence> MariamResidences { get; set; }
        public virtual DbSet<MarinaCrownResidence> MarinaCrownResidences { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<Maxvoucherno> Maxvouchernos { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Missue> Missues { get; set; }
        public virtual DbSet<MissueFollowup> MissueFollowups { get; set; }
        public virtual DbSet<Missuecancel> Missuecancels { get; set; }
        public virtual DbSet<Moveout> Moveouts { get; set; }
        public virtual DbSet<Movingin> Movingins { get; set; }
        public virtual DbSet<Mrent> Mrents { get; set; }
        public virtual DbSet<Mrequest> Mrequests { get; set; }
        public virtual DbSet<Mstatus> Mstatuses { get; set; }
        public virtual DbSet<MstrFaMachine> MstrFaMachines { get; set; }
        public virtual DbSet<MstrFaTreatment> MstrFaTreatments { get; set; }
        public virtual DbSet<MstrStockItemQty> MstrStockItemQties { get; set; }
        public virtual DbSet<Myno> Mynos { get; set; }
        public virtual DbSet<Nationality> Nationalities { get; set; }
        public virtual DbSet<Nationalitydetail> Nationalitydetails { get; set; }
        public virtual DbSet<NoorBuilding> NoorBuildings { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Ot> Ots { get; set; }
        public virtual DbSet<Othernote> Othernotes { get; set; }
        public virtual DbSet<Otherpaytype> Otherpaytypes { get; set; }
        public virtual DbSet<Otherproperty> Otherproperties { get; set; }
        public virtual DbSet<Othersource> Othersources { get; set; }
        public virtual DbSet<P> Ps { get; set; }
        public virtual DbSet<P1> P1s { get; set; }
        public virtual DbSet<Padocx> Padocxes { get; set; }
        public virtual DbSet<Paemail> Paemails { get; set; }
        public virtual DbSet<Palist> Palists { get; set; }
        public virtual DbSet<Palogin> Palogins { get; set; }
        public virtual DbSet<Pamailsending> Pamailsendings { get; set; }
        public virtual DbSet<Papayment> Papayments { get; set; }
        public virtual DbSet<Passwordaccess> Passwordaccesses { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Paymentsdummy> Paymentsdummies { get; set; }
        public virtual DbSet<Paymentsstatus> Paymentsstatuses { get; set; }
        public virtual DbSet<Paymentstemp> Paymentstemps { get; set; }
        public virtual DbSet<Paymentvoucher> Paymentvouchers { get; set; }
        public virtual DbSet<Periodexpense> Periodexpenses { get; set; }
        public virtual DbSet<Periodexpensesentry> Periodexpensesentries { get; set; }
        public virtual DbSet<Pgl> Pgls { get; set; }
        public virtual DbSet<Pgldetail> Pgldetails { get; set; }
        public virtual DbSet<Pme> Pmes { get; set; }
        public virtual DbSet<Pme1> Pmes1 { get; set; }
        public virtual DbSet<Pmesaccount> Pmesaccounts { get; set; }
        public virtual DbSet<Pmesreq> Pmesreqs { get; set; }
        public virtual DbSet<Pmesst> Pmessts { get; set; }
        public virtual DbSet<Pmetype> Pmetypes { get; set; }
        public virtual DbSet<Pmf> Pmfs { get; set; }
        public virtual DbSet<Po> Pos { get; set; }
        public virtual DbSet<Privatemileage> Privatemileages { get; set; }
        public virtual DbSet<PropMaster> PropMasters { get; set; }
        public virtual DbSet<PropertyChange> PropertyChanges { get; set; }
        public virtual DbSet<PropertyFeature> PropertyFeatures { get; set; }
        public virtual DbSet<PropertyList> PropertyLists { get; set; }
        public virtual DbSet<PropertyPicture> PropertyPictures { get; set; }
        public virtual DbSet<Propertyfloor> Propertyfloors { get; set; }
        public virtual DbSet<Propertymaster> Propertymasters { get; set; }
        public virtual DbSet<Propertytype> Propertytypes { get; set; }
        public virtual DbSet<Ptv> Ptvs { get; set; }
        public virtual DbSet<Pwd> Pwds { get; set; }
        public virtual DbSet<Quicfix> Quicfixes { get; set; }
        public virtual DbSet<Quickfix> Quickfixes { get; set; }
        public virtual DbSet<RawaseaResidence> RawaseaResidences { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<Removeapt> Removeapts { get; set; }
        public virtual DbSet<Removing> Removings { get; set; }
        public virtual DbSet<Renewallc> Renewallcs { get; set; }
        public virtual DbSet<Renewalreason> Renewalreasons { get; set; }
        public virtual DbSet<Rentchange> Rentchanges { get; set; }
        public virtual DbSet<Rented> Renteds { get; set; }
        public virtual DbSet<Rentedapartment> Rentedapartments { get; set; }
        public virtual DbSet<Rentstatement> Rentstatements { get; set; }
        public virtual DbSet<Reportingremark> Reportingremarks { get; set; }
        public virtual DbSet<Requirement> Requirements { get; set; }
        public virtual DbSet<Rissue> Rissues { get; set; }
        public virtual DbSet<SafiTower> SafiTowers { get; set; }
        public virtual DbSet<ShaikhaComplex> ShaikhaComplices { get; set; }
        public virtual DbSet<ShaikhaTower> ShaikhaTowers { get; set; }
        public virtual DbSet<SharifaAlMullaBuilding> SharifaAlMullaBuildings { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Stinv> Stinvs { get; set; }
        public virtual DbSet<Sublease> Subleases { get; set; }
        public virtual DbSet<Subleasename> Subleasenames { get; set; }
        public virtual DbSet<Tdetail> Tdetails { get; set; }
        public virtual DbSet<Templogin> Templogins { get; set; }
        public virtual DbSet<Temppayment> Temppayments { get; set; }
        public virtual DbSet<TenantEntry> TenantEntries { get; set; }
        public virtual DbSet<Tenantshistory> Tenantshistories { get; set; }
        public virtual DbSet<Tenantstransfer> Tenantstransfers { get; set; }
        public virtual DbSet<Tourdetail> Tourdetails { get; set; }
        public virtual DbSet<Tt> Tts { get; set; }
        public virtual DbSet<Updatedlc> Updatedlcs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vacatingcancel> Vacatingcancels { get; set; }
        public virtual DbSet<Vacatingnote> Vacatingnotes { get; set; }
        public virtual DbSet<Validchardm> Validchardms { get; set; }
        public virtual DbSet<Vehicleno> Vehiclenos { get; set; }
        public virtual DbSet<Vme> Vmes { get; set; }
        public virtual DbSet<Walkininquiry> Walkininquiries { get; set; }
        public virtual DbSet<WarbaBeachResort> WarbaBeachResorts { get; set; }
        public virtual DbSet<Wmrfeature> Wmrfeatures { get; set; }
        public virtual DbSet<Workstatus> Workstatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(local)\\sqlexpress;Initial Catalog=Realtor;User Id=sa;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Accamt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("accamt");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Dept)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<AccountStname>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AccountSTname");

                entity.Property(e => e.AccName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ACC_NAME");

                entity.Property(e => e.CompName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("comp_name");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<AccountsCategoryExpense>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Accounts_category_expenses");

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<AccountsPm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("accountsPM");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Combine)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COMBINE");

                entity.Property(e => e.Csremarks)
                    .IsUnicode(false)
                    .HasColumnName("csremarks");

                entity.Property(e => e.Csstatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("csstatus");

                entity.Property(e => e.Cstime)
                    .HasColumnType("datetime")
                    .HasColumnName("cstime");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("date")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Leaseno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("leaseno");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Pname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PREF");

                entity.Property(e => e.Prent)
                    .HasColumnType("money")
                    .HasColumnName("prent");

                entity.Property(e => e.Ptid).HasColumnName("ptid");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("tname");
            });

            modelBuilder.Entity<Actiondetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Actions)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Adminremarks).IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LENAME");

                entity.Property(e => e.Refno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Adminactivity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("adminactivities");

                entity.Property(e => e.Advertising).IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fromtime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fromtime");

                entity.Property(e => e.Ft).HasColumnName("ft");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.OtherReason).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.Property(e => e.Totime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("totime");

                entity.Property(e => e.Tt).HasColumnName("tt");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Advpayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("advpayments");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Banked)
                    .HasColumnType("date")
                    .HasColumnName("banked");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("bt");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Collectionmonth).HasColumnName("collectionmonth");

                entity.Property(e => e.Collectionyear).HasColumnName("collectionyear");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Discamt)
                    .HasColumnType("money")
                    .HasColumnName("discamt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Entrymode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entrymode");

                entity.Property(e => e.Entryuser)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("entryuser");

                entity.Property(e => e.Exclude)
                    .HasMaxLength(10)
                    .HasColumnName("exclude")
                    .IsFixedLength(true);

                entity.Property(e => e.Highlight)
                    .HasMaxLength(10)
                    .HasColumnName("highlight")
                    .IsFixedLength(true);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Myid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("myid");

                entity.Property(e => e.Payid).HasColumnName("payid");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paymenttype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remstatus)
                    .HasMaxLength(10)
                    .HasColumnName("remstatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Rno)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rno");

                entity.Property(e => e.Sortid).HasColumnName("sortid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Amal>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("amal");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<Amal1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Amal1");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");
            });

            modelBuilder.Entity<AmalResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Amal Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<AmarillaResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Amarilla Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<ArcadiaTowerA>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Arcadia Tower-A");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<ArcadiaTowerB>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Arcadia Tower-B");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<ArcadiaTowerC>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Arcadia Tower-C");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Area");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Asrchange>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ASRchanges");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Changeby)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("changeby");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Feature)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("feature");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.New)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("new");

                entity.Property(e => e.Old)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("old");

                entity.Property(e => e.Pname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.ViewByAdmin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("viewByAdmin");

                entity.Property(e => e.ViewBySpa)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("viewBySpa");
            });

            modelBuilder.Entity<Asrnote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ASRNOTE");

                entity.Property(e => e.Pname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Remark)
                    .IsUnicode(false)
                    .HasColumnName("REMARK");
            });

            modelBuilder.Entity<Attender>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("attenders");

                entity.Property(e => e.Attender1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("attender");

                entity.Property(e => e.Attgrp)
                    .HasMaxLength(10)
                    .HasColumnName("attgrp")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mobile");
            });

            modelBuilder.Entity<Autologout>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("autologout");

                entity.Property(e => e.Dept)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Role)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<BBuilding>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("B Building");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-2");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer5>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-5");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer6>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-6");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer7>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-7");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer8>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-8");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Basaeer9>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Basaeer-9");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Bcode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bcode");

                entity.Property(e => e.Bcpde)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bcpde");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<Building15>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Building-15");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Bulkprinting>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Bulkprinting");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Fromperiod)
                    .HasColumnType("date")
                    .HasColumnName("fromperiod");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Mrentinwords)
                    .IsUnicode(false)
                    .HasColumnName("mrentinwords");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Prent)
                    .HasColumnType("money")
                    .HasColumnName("prent");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ptype");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Tname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Toperiod)
                    .HasColumnType("date")
                    .HasColumnName("toperiod");
            });

            modelBuilder.Entity<Bulkprintingtemp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Bulkprintingtemp");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Fromperiod)
                    .HasColumnType("date")
                    .HasColumnName("fromperiod");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Mrentinwords)
                    .IsUnicode(false)
                    .HasColumnName("mrentinwords");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Prent)
                    .HasColumnType("money")
                    .HasColumnName("prent");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ptype");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Tname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Toperiod)
                    .HasColumnType("date")
                    .HasColumnName("toperiod");
            });

            modelBuilder.Entity<Cancelledreceipt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cancelledreceipts");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Pname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("STATUS")
                    .IsFixedLength(true);

                entity.Property(e => e.Voucherno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("voucherno");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Cancellrequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cancellrequest");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Cdate)
                    .HasColumnType("datetime")
                    .HasColumnName("cdate");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Issue)
                    .IsUnicode(false)
                    .HasColumnName("issue");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasColumnName("reason");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("category");

                entity.Property(e => e.Categories)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("categories");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Cc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cc");

                entity.Property(e => e.Appdate)
                    .HasColumnType("date")
                    .HasColumnName("appdate");

                entity.Property(e => e.Appointment)
                    .HasMaxLength(10)
                    .HasColumnName("appointment")
                    .IsFixedLength(true);

                entity.Property(e => e.Ccname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ccname");

                entity.Property(e => e.Cdate)
                    .HasColumnType("date")
                    .HasColumnName("cdate");

                entity.Property(e => e.Cdesg)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cdesg");

                entity.Property(e => e.Cmobile)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cmobile");

                entity.Property(e => e.Cname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cname");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fdat)
                    .HasColumnType("date")
                    .HasColumnName("fdat");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lename)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("lename");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<Ccat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ccat");

                entity.Property(e => e.Ccategory)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ccategory");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Cgl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CGL");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Balcony)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Bed)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Block).IsUnicode(false);

                entity.Property(e => e.Cancellationstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cancellationstatus");

                entity.Property(e => e.Cglrefno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CGLREFNO");

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Completedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateofCancel).HasColumnType("date");

                entity.Property(e => e.DateofCancelLe)
                    .HasColumnType("date")
                    .HasColumnName("DateofCancelLE");

                entity.Property(e => e.Doc).HasColumnType("date");

                entity.Property(e => e.DriverRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnquiryDate).HasColumnType("date");

                entity.Property(e => e.Enquirytype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("enquirytype");

                entity.Property(e => e.Furnished)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Garden)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Gym)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.InquiryStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Interest).IsUnicode(false);

                entity.Property(e => e.Internet)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Maidroom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Maxbudget).HasColumnType("money");

                entity.Property(e => e.MinBudget).HasColumnType("money");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Movingdate)
                    .HasColumnType("date")
                    .HasColumnName("movingdate");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtherInfo).IsUnicode(false);

                entity.Property(e => e.Othersource)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("othersource");

                entity.Property(e => e.Parking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Pool)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PropertySource)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Propertytype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonofCancellation).IsUnicode(false);

                entity.Property(e => e.ReasontocancelLe)
                    .IsUnicode(false)
                    .HasColumnName("ReasontocancelLE");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialInstructions).IsUnicode(false);

                entity.Property(e => e.Tv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Updatedon).HasColumnType("date");

                entity.Property(e => e.Wasinactive)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("wasinactive")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ChangeinApt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("changeinApt");

                entity.Property(e => e.Apt)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apt");

                entity.Property(e => e.Bldg)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("bldg");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Module)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("module");

                entity.Property(e => e.System)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("system");
            });

            modelBuilder.Entity<Checking>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CHECKING");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("city");

                entity.Property(e => e.Cityname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cityname");
            });

            modelBuilder.Entity<Clientquestion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("clientquestions");

                entity.Property(e => e.Bed)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("bed")
                    .IsFixedLength(true);

                entity.Property(e => e.Closeschool)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("closeschool");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("company");

                entity.Property(e => e.Contract)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("contract")
                    .IsFixedLength(true);

                entity.Property(e => e.Currentloc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("currentloc");

                entity.Property(e => e.Dimen)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("dimen");

                entity.Property(e => e.Distance)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("distance");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Facilities)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("facilities");

                entity.Property(e => e.Feedbacks)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("feedbacks");

                entity.Property(e => e.Final)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("final");

                entity.Property(e => e.Find)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("find");

                entity.Property(e => e.Findwhere)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("findwhere");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("fname");

                entity.Property(e => e.Howlong)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("howlong");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Idea)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("idea")
                    .IsFixedLength(true);

                entity.Property(e => e.Inkwt)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("inkwt");

                entity.Property(e => e.Kids)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("kids")
                    .IsFixedLength(true);

                entity.Property(e => e.Kind)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("kind");

                entity.Property(e => e.Kvisit)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("kvisit");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("lname");

                entity.Property(e => e.Maid)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("maid")
                    .IsFixedLength(true);

                entity.Property(e => e.Maritial)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("maritial")
                    .IsFixedLength(true);

                entity.Property(e => e.Newbuild)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("newbuild")
                    .IsFixedLength(true);

                entity.Property(e => e.Notice)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("notice");

                entity.Property(e => e.Number)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Otheragent)
                    .HasMaxLength(10)
                    .HasColumnName("otheragent")
                    .IsFixedLength(true);

                entity.Property(e => e.Pet)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("pet")
                    .IsFixedLength(true);

                entity.Property(e => e.Ploc)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("ploc");

                entity.Property(e => e.Refno)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("refno");

                entity.Property(e => e.School)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("school");

                entity.Property(e => e.Seen)
                    .HasMaxLength(10)
                    .HasColumnName("seen")
                    .IsFixedLength(true);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("type")
                    .IsFixedLength(true);

                entity.Property(e => e.Whenapt)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("whenapt");

                entity.Property(e => e.Wife)
                    .HasMaxLength(10)
                    .HasColumnName("wife")
                    .IsFixedLength(true);

                entity.Property(e => e.Wifereq)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("wifereq");
            });

            modelBuilder.Entity<Combineapartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("COMBINEAPARTMENTS");

                entity.Property(e => e.Accid).HasColumnName("accid");

                entity.Property(e => e.Apartments)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("apartments");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pref");
            });

            modelBuilder.Entity<Cremark>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cremark");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<DailyTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DailyTask");

                entity.Property(e => e.Actiontaken)
                    .IsUnicode(false)
                    .HasColumnName("actiontaken");

                entity.Property(e => e.Attendeddate).HasColumnType("date");

                entity.Property(e => e.ClientnameandNo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Inquiryno)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.List)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("list");

                entity.Property(e => e.Nextsteps).IsUnicode(false);

                entity.Property(e => e.Refno)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('DT_00'+CONVERT([varchar](20),[ID],(0)))", false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Req)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("req");
            });

            modelBuilder.Entity<DaliaTower>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Dalia Tower");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Deletedlogin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("deletedlogin");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Logmode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOGMODE");

                entity.Property(e => e.Logtime)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGTIME");

                entity.Property(e => e.Mode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MODE");

                entity.Property(e => e.Paname)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("PANAME");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Sysname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("sysname");
            });

            modelBuilder.Entity<Deletedtvoucher>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("deletedtvoucher");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.AmtType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("amt-type");

                entity.Property(e => e.Amtinwords)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("amtinwords");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APTNO");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("BT");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("CASH");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("CHEQUE");

                entity.Property(e => e.Comments)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("date")
                    .HasColumnName("datefrom");

                entity.Property(e => e.Dateto)
                    .HasColumnType("date")
                    .HasColumnName("dateto");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Flag)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Inqno)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("inqno");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("KNET");

                entity.Property(e => e.LcNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lc_no");

                entity.Property(e => e.LoiNo1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loi_no");

                entity.Property(e => e.Loino)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loino");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_date");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ptype");

                entity.Property(e => e.Tname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TNAME");

                entity.Property(e => e.User)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user");

                entity.Property(e => e.Voucherno)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("voucherno");
            });

            modelBuilder.Entity<Deltedinquiry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("deltedinquiries");

                entity.Property(e => e.Deletingdate)
                    .HasColumnType("datetime")
                    .HasColumnName("deletingdate");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Inqno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("inqno");

                entity.Property(e => e.Userinfo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("userinfo");
            });

            modelBuilder.Entity<Dhc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dhc");

                entity.Property(e => e.DhcSubmitted)
                    .HasColumnType("datetime")
                    .HasColumnName("dhc_submitted");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.DriverName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mob)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DriverScheduel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DriverScheduel");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.ClentName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.DriverName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FromTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ft).HasColumnName("ft");

                entity.Property(e => e.Grouple)
                    .IsUnicode(false)
                    .HasColumnName("grouple");

                entity.Property(e => e.Groupleyesno)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("groupleyesno");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.LEname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("L.EName");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Otherreason).IsUnicode(false);

                entity.Property(e => e.PropertyName).IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Refno)
                    .HasMaxLength(27)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('SCHD_00'+CONVERT([varchar](20),[ID],(0)))", false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Returntime).HasColumnName("returntime");

                entity.Property(e => e.Stype)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ToTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tt).HasColumnName("tt");
            });

            modelBuilder.Entity<Driverformat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("driverformat");

                entity.Property(e => e.Cname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cname");

                entity.Property(e => e.Dname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("dname");

                entity.Property(e => e.Ftime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ftime");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lename)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("lename");

                entity.Property(e => e.Pname)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Refno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("refno");

                entity.Property(e => e.Sdid).HasColumnName("sdid");

                entity.Property(e => e.Timings)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("timings");
            });

            modelBuilder.Entity<Dummy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dummy");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.No).HasColumnName("no");
            });

            modelBuilder.Entity<Dummy1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Dummy1");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Sortid)
                    .HasMaxLength(21)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dummyfileupload>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DUMMYFILEUPLOAD");

                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Emailuser>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("emailusers");

                entity.Property(e => e.Dept)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mailid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mailid");

                entity.Property(e => e.Mailstatus)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("mailstatus");
            });

            modelBuilder.Entity<FaisalTower>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Faisal Tower");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Fcacategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("fcacategory");

                entity.Property(e => e.Categoryname)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("categoryname");

                entity.Property(e => e.Catsection)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("catsection");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("date")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Orderno).HasColumnName("orderno");
            });

            modelBuilder.Entity<Forty1Residence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Forty - 1 Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Fschedule>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("fschedule");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Attender)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("attender");

                entity.Property(e => e.Cdate)
                    .HasColumnType("date")
                    .HasColumnName("cdate");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fromtime).HasColumnName("fromtime");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Issuedate)
                    .HasColumnType("date")
                    .HasColumnName("issuedate");

                entity.Property(e => e.Issueid).HasColumnName("issueid");

                entity.Property(e => e.Mid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mid");

                entity.Property(e => e.Multipleapt)
                    .HasMaxLength(10)
                    .HasColumnName("multipleapt")
                    .IsFixedLength(true);

                entity.Property(e => e.Otype)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("otype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Sdate)
                    .HasColumnType("date")
                    .HasColumnName("sdate");

                entity.Property(e => e.Seenbyfca)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("seenbyfca");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Tavailable)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tavailable");

                entity.Property(e => e.Totime).HasColumnName("totime");
            });

            modelBuilder.Entity<Fscheduleattender>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("fscheduleattender");

                entity.Property(e => e.Attender)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("attender");

                entity.Property(e => e.Fromtime).HasColumnName("fromtime");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Sid).HasColumnName("sid");

                entity.Property(e => e.Totime).HasColumnName("totime");
            });

            modelBuilder.Entity<Fscheduleattender1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Fscheduleattenders");

                entity.Property(e => e.Attid).HasColumnName("attid");

                entity.Property(e => e.Scdlid).HasColumnName("scdlid");
            });

            modelBuilder.Entity<Fscheduleattendershistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Fscheduleattendershistory");

                entity.Property(e => e.Attid).HasColumnName("attid");

                entity.Property(e => e.Fromt).HasColumnName("fromt");

                entity.Property(e => e.Historyid).HasColumnName("historyid");

                entity.Property(e => e.Scdlid).HasColumnName("scdlid");

                entity.Property(e => e.Tot).HasColumnName("tot");
            });

            modelBuilder.Entity<Fschedulehistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("fschedulehistory");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Attender)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("attender");

                entity.Property(e => e.Cdate)
                    .HasColumnType("date")
                    .HasColumnName("cdate");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fid).HasColumnName("fid");

                entity.Property(e => e.Fromtime).HasColumnName("fromtime");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Issuedate)
                    .HasColumnType("date")
                    .HasColumnName("issuedate");

                entity.Property(e => e.Issueid).HasColumnName("issueid");

                entity.Property(e => e.Mid)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mid");

                entity.Property(e => e.Mode)
                    .HasMaxLength(10)
                    .HasColumnName("mode")
                    .IsFixedLength(true);

                entity.Property(e => e.Multipleapt)
                    .HasMaxLength(10)
                    .HasColumnName("multipleapt")
                    .IsFixedLength(true);

                entity.Property(e => e.Newdate)
                    .HasColumnType("date")
                    .HasColumnName("newdate");

                entity.Property(e => e.Otype)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("otype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Sdate)
                    .HasColumnType("date")
                    .HasColumnName("sdate");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Tavailable)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("tavailable");

                entity.Property(e => e.Totime).HasColumnName("totime");
            });

            modelBuilder.Entity<Governarate>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("governarate");

                entity.Property(e => e.Governarate1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("governarate");
            });

            modelBuilder.Entity<HawallyResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Hawally Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Haya1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Haya-1");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Haya2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Haya-2");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Haya4>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Haya-4");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<HessaBuilding>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Hessa Building");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("HISTORY");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APTNO");

                entity.Property(e => e.Bed)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BED");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Lend)
                    .HasColumnType("date")
                    .HasColumnName("LEND");

                entity.Property(e => e.Lstart)
                    .HasColumnType("date")
                    .HasColumnName("LSTART");

                entity.Property(e => e.Mout)
                    .HasColumnType("date")
                    .HasColumnName("MOUT");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("RENT");

                entity.Property(e => e.Tname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TNAME");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Img>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("img");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img1)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("img");
            });

            modelBuilder.Entity<Issueitem>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("issueitems");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Issueid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("issueid");

                entity.Property(e => e.Items)
                    .IsRequired()
                    .HasMaxLength(450)
                    .IsUnicode(false)
                    .HasColumnName("items");
            });

            modelBuilder.Entity<ItemDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("item_details");

                entity.Property(e => e.Pic)
                    .HasColumnType("image")
                    .HasColumnName("pic");
            });

            modelBuilder.Entity<Itempurchased>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("itempurchased");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Iid).HasColumnName("iid");

                entity.Property(e => e.Itemdesc)
                    .IsUnicode(false)
                    .HasColumnName("itemdesc");

                entity.Property(e => e.Qty)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("qty");
            });

            modelBuilder.Entity<Jobverification>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("jobverification");

                entity.Property(e => e.Apt)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("apt");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fca)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FCA");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Jobdesc)
                    .IsUnicode(false)
                    .HasColumnName("jobdesc");

                entity.Property(e => e.Jobissueid).HasColumnName("jobissueid");

                entity.Property(e => e.Llreport)
                    .HasMaxLength(10)
                    .HasColumnName("llreport")
                    .IsFixedLength(true);

                entity.Property(e => e.Pname)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Rptdate)
                    .HasColumnType("date")
                    .HasColumnName("rptdate");
            });

            modelBuilder.Entity<LavanResidence1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lavan Residence - 1");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<LavanResidenceMarina>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LAVAN Residence Marina");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Lavaranda2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lavaranda-2");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Lcchange>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lcchanges");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ChangeDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Change_DateTime");

                entity.Property(e => e.Changeby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("changeby");

                entity.Property(e => e.Feature)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.Oldupdate)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("oldupdate");

                entity.Property(e => e.Reason)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Lcinfo>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("lcinfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Btype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("btype");

                entity.Property(e => e.Cas)
                    .IsUnicode(false)
                    .HasColumnName("cas");

                entity.Property(e => e.Cg)
                    .IsUnicode(false)
                    .HasColumnName("cg");

                entity.Property(e => e.Cid)
                    .IsUnicode(false)
                    .HasColumnName("cid");

                entity.Property(e => e.Cidpp)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("cidpp");

                entity.Property(e => e.Civilid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CIVILID");

                entity.Property(e => e.Coe)
                    .IsUnicode(false)
                    .HasColumnName("coe");

                entity.Property(e => e.Coguaranty)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("coguaranty")
                    .IsFixedLength(true);

                entity.Property(e => e.Compaddress)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("COMPADDRESS");

                entity.Property(e => e.Cosign)
                    .IsUnicode(false)
                    .HasColumnName("cosign");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.Deposit)
                    .HasColumnType("money")
                    .HasColumnName("deposit");

                entity.Property(e => e.Deppaid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("deppaid")
                    .IsFixedLength(true);

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Enqtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("enqtype");

                entity.Property(e => e.Fstatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fstatus");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.LcNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LC_no");

                entity.Property(e => e.Lcmadeon)
                    .HasColumnType("date")
                    .HasColumnName("LCMadeon");

                entity.Property(e => e.Lcmob)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lcmob");

                entity.Property(e => e.Lcpath)
                    .IsUnicode(false)
                    .HasColumnName("lcpath");

                entity.Property(e => e.Lcreceivedfromtenanton)
                    .HasColumnType("date")
                    .HasColumnName("lcreceivedfromtenanton");

                entity.Property(e => e.Lcsenttollon)
                    .HasColumnType("date")
                    .HasColumnName("lcsenttollon");

                entity.Property(e => e.Lcsignedbyclient)
                    .HasMaxLength(10)
                    .HasColumnName("lcsignedbyclient")
                    .IsFixedLength(true);

                entity.Property(e => e.Lcsignedbyoll)
                    .HasMaxLength(10)
                    .HasColumnName("lcsignedbyoll")
                    .IsFixedLength(true);

                entity.Property(e => e.Lct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lct");

                entity.Property(e => e.Lctype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lctype");

                entity.Property(e => e.Leaseend).HasColumnType("date");

                entity.Property(e => e.Leasestart).HasColumnType("date");

                entity.Property(e => e.Legal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Licensepermit)
                    .IsUnicode(false)
                    .HasColumnName("licensepermit");

                entity.Property(e => e.LoiNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOI_no");

                entity.Property(e => e.Loipath)
                    .IsUnicode(false)
                    .HasColumnName("loipath");

                entity.Property(e => e.Loistatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loistatus");

                entity.Property(e => e.Mc)
                    .IsUnicode(false)
                    .HasColumnName("mc");

                entity.Property(e => e.Moc)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("moc");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Noc)
                    .IsUnicode(false)
                    .HasColumnName("noc");

                entity.Property(e => e.Orginallcsent).HasColumnType("date");

                entity.Property(e => e.Otherreason)
                    .IsUnicode(false)
                    .HasColumnName("otherreason");

                entity.Property(e => e.Ownremarks)
                    .IsUnicode(false)
                    .HasColumnName("ownremarks");

                entity.Property(e => e.Payable)
                    .HasColumnType("money")
                    .HasColumnName("payable");

                entity.Property(e => e.Period).HasColumnName("period");

                entity.Property(e => e.Pmode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pmode");

                entity.Property(e => e.Pname)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Pp)
                    .IsUnicode(false)
                    .HasColumnName("pp");

                entity.Property(e => e.Ppno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PPNO");

                entity.Property(e => e.Pref)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ptype");

                entity.Property(e => e.Reason)
                    .IsUnicode(false)
                    .HasColumnName("reason");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Renewalid).HasColumnName("renewalid");

                entity.Property(e => e.Renewalof)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("renewalof");

                entity.Property(e => e.Renewreason)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("renewreason");

                entity.Property(e => e.Rent).HasColumnType("money");

                entity.Property(e => e.Rentpaid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("rentpaid")
                    .IsFixedLength(true);

                entity.Property(e => e.Resf)
                    .HasColumnType("money")
                    .HasColumnName("RESF");

                entity.Property(e => e.Resontocancel).IsUnicode(false);

                entity.Property(e => e.Rvsent)
                    .HasColumnType("date")
                    .HasColumnName("rvsent");

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Tenname)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Updateandrenewed)
                    .HasMaxLength(10)
                    .HasColumnName("updateandrenewed")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Leasehistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Leasehistory");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.Progress)
                    .IsUnicode(false)
                    .HasColumnName("progress");
            });

            modelBuilder.Entity<Leinfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LEInfo");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lemail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LEmail");

                entity.Property(e => e.Lemob)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("LEMob");

                entity.Property(e => e.Lename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LEName");

                entity.Property(e => e.Nametitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Refno)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('LE_00'+CONVERT([varchar](20),[ID],(0)))", false);

                entity.Property(e => e.Rightsassigned)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lldetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("lldetails");

                entity.Property(e => e.AccName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.AccNo)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("AccNO");

                entity.Property(e => e.Accbank)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Arabicaddress).HasColumnName("arabicaddress");

                entity.Property(e => e.Arabicllname).HasColumnName("arabicllname");

                entity.Property(e => e.Attdesg)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Attfax)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Attname)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Attph)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Attprop)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Bcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bcode");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Llname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("llname");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pnameinarabic).HasColumnName("pnameinarabic");

                entity.Property(e => e.R1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("r1");

                entity.Property(e => e.R2)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("r2");

                entity.Property(e => e.R3)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("r3");

                entity.Property(e => e.Reportname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("reportname");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .IsFixedLength(true);

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Llreport>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LLREPORT");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("DOC");

                entity.Property(e => e.Month).HasColumnName("MONTH");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Rptpath)
                    .IsUnicode(false)
                    .HasColumnName("RPTPATH");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            modelBuilder.Entity<Locationgroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOCATIONGROUP");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Locgroup)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LOCGROUP");

                entity.Property(e => e.Orderno).HasColumnName("ORDERNO");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("log");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("action");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Loginandout)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loginandout");

                entity.Property(e => e.Module)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("module");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<Loiinformation>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("LOIInformation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Ap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Aptype)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cas)
                    .IsUnicode(false)
                    .HasColumnName("cas");

                entity.Property(e => e.Cg)
                    .IsUnicode(false)
                    .HasColumnName("cg");

                entity.Property(e => e.Cid)
                    .IsUnicode(false)
                    .HasColumnName("cid");

                entity.Property(e => e.Cidpp)
                    .IsUnicode(false)
                    .HasColumnName("cidpp");

                entity.Property(e => e.ClientCompany)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ClientResf)
                    .HasColumnType("money")
                    .HasColumnName("clientRESF");

                entity.Property(e => e.ClientSource)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Cmob)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cnationality)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CNationality");

                entity.Property(e => e.Coe)
                    .IsUnicode(false)
                    .HasColumnName("coe");

                entity.Property(e => e.Cosign)
                    .IsUnicode(false)
                    .HasColumnName("cosign");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.Deposit)
                    .HasColumnType("money")
                    .HasColumnName("deposit");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DEPT");

                entity.Property(e => e.Deptusr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DEPTUSR");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Docsubmitted)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("docsubmitted");

                entity.Property(e => e.Duedeposit)
                    .HasColumnType("money")
                    .HasColumnName("duedeposit");

                entity.Property(e => e.Duerent)
                    .HasColumnType("money")
                    .HasColumnName("duerent");

                entity.Property(e => e.Dueresf)
                    .HasColumnType("money")
                    .HasColumnName("dueresf");

                entity.Property(e => e.EnquiryType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Feetobepaidby)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("feetobepaidby");

                entity.Property(e => e.Fur)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fur");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Inqno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("inqno");

                entity.Property(e => e.LcNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lc_no");

                entity.Property(e => e.Lccreate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lccreate");

                entity.Property(e => e.Lcsigned)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lcsigned");

                entity.Property(e => e.Leasedate).HasColumnType("date");

                entity.Property(e => e.Leaseenddate).HasColumnType("date");

                entity.Property(e => e.Legal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Lename)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LEName");

                entity.Property(e => e.Llresf)
                    .HasColumnType("money")
                    .HasColumnName("LLRESF");

                entity.Property(e => e.LoiDate).HasColumnType("date");

                entity.Property(e => e.LoiNo)
                    .HasMaxLength(26)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('LOI_00'+CONVERT([varchar](20),[ID],(0)))", false);

                entity.Property(e => e.LoiNo1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loi_no");

                entity.Property(e => e.LoiStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Loimailstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loimailstatus");

                entity.Property(e => e.Loinote)
                    .IsUnicode(false)
                    .HasColumnName("loinote");

                entity.Property(e => e.Loipath)
                    .IsUnicode(false)
                    .HasColumnName("loipath");

                entity.Property(e => e.Loiremarks)
                    .IsUnicode(false)
                    .HasColumnName("loiremarks");

                entity.Property(e => e.Loisigndate)
                    .HasColumnType("date")
                    .HasColumnName("loisigndate");

                entity.Property(e => e.Mc)
                    .IsUnicode(false)
                    .HasColumnName("mc");

                entity.Property(e => e.Moc)
                    .IsUnicode(false)
                    .HasColumnName("moc");

                entity.Property(e => e.Movingindate)
                    .HasColumnType("date")
                    .HasColumnName("movingindate");

                entity.Property(e => e.Noc)
                    .IsUnicode(false)
                    .HasColumnName("noc");

                entity.Property(e => e.Payment)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("payment");

                entity.Property(e => e.Paymentdate)
                    .HasColumnType("date")
                    .HasColumnName("paymentdate");

                entity.Property(e => e.Payremarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("payremarks");

                entity.Property(e => e.Paystart)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paystart");

                entity.Property(e => e.Paystatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paystatus");

                entity.Property(e => e.Pp)
                    .IsUnicode(false)
                    .HasColumnName("pp");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyRefNo)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PropertySource)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Renttobepaidby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Req)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SearchedProperty).HasDefaultValueSql("((0))");

                entity.Property(e => e.Securitydepositpaidby)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Updateddate).HasColumnType("datetime");
            });

            modelBuilder.Entity<LulwahAlMullahComplex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Lulwah Al Mullah Complex");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Madmin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("madmin");

                entity.Property(e => e.Admin)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("admin");

                entity.Property(e => e.Des)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("des");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Report)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("report");
            });

            modelBuilder.Entity<Maintenacecat>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("maintenacecat");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");
            });

            modelBuilder.Entity<Maintenancecontractor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("maintenancecontractors");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<MaintenceBlock>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MaintenceBlock");

                entity.Property(e => e.Aptno)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("reason");
            });

            modelBuilder.Entity<Manalysis>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MANALYSIS");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Locationgroup)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("locationgroup");

                entity.Property(e => e.Month).HasColumnName("MONTH");

                entity.Property(e => e.Orderno).HasColumnName("orderno");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            modelBuilder.Entity<Manalysis1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MANALYSIS1");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Locationgroup)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("locationgroup");

                entity.Property(e => e.Month).HasColumnName("MONTH");

                entity.Property(e => e.Orderno).HasColumnName("orderno");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            modelBuilder.Entity<MariamResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mariam Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<MarinaCrownResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Marina Crown Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("materials");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Doc)
                    .HasMaxLength(10)
                    .HasColumnName("doc")
                    .IsFixedLength(true);

                entity.Property(e => e.Material1)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("material");
            });

            modelBuilder.Entity<Maxvoucherno>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("maxvoucherno");

                entity.Property(e => e.No).HasColumnName("no");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Message");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.MDept)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("M_dept");

                entity.Property(e => e.MDoc)
                    .HasColumnType("datetime")
                    .HasColumnName("M_doc");

                entity.Property(e => e.MMode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("M_mode");

                entity.Property(e => e.MMsg)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("M_msg");

                entity.Property(e => e.MPname)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("M_pname");

                entity.Property(e => e.MTimeread)
                    .HasColumnType("datetime")
                    .HasColumnName("M_Timeread");
            });

            modelBuilder.Entity<Missue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MISSUE");

                entity.Property(e => e.Additionalwork)
                    .IsUnicode(false)
                    .HasColumnName("additionalwork");

                entity.Property(e => e.Aptlocation)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("APTLOCATION");

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Contactno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contactno");

                entity.Property(e => e.Contractorname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("contractorname");

                entity.Property(e => e.Contractortype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contractortype");

                entity.Property(e => e.Cwmr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cwmr");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fcaquestion)
                    .IsUnicode(false)
                    .HasColumnName("fcaquestion");

                entity.Property(e => e.Fcaseen)
                    .HasMaxLength(10)
                    .HasColumnName("fcaseen")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Issue)
                    .IsUnicode(false)
                    .HasColumnName("ISSUE");

                entity.Property(e => e.IssueNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IssueNO");

                entity.Property(e => e.Itempurchase)
                    .HasMaxLength(10)
                    .HasColumnName("itempurchase")
                    .IsFixedLength(true);

                entity.Property(e => e.Jobid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("JOBID");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.PaDoneOn)
                    .HasColumnType("datetime")
                    .HasColumnName("PA done ON");

                entity.Property(e => e.Paname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PANAME");

                entity.Property(e => e.Pareply)
                    .IsUnicode(false)
                    .HasColumnName("pareply");

                entity.Property(e => e.Pl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PL");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PREF");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Request)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REQUEST");

                entity.Property(e => e.Requestdate)
                    .HasColumnType("datetime")
                    .HasColumnName("requestdate");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Tavailability)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tavailability");

                entity.Property(e => e.Tname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Underobservationstart)
                    .HasColumnType("datetime")
                    .HasColumnName("underobservationstart");

                entity.Property(e => e.Underobservationstop)
                    .HasColumnType("date")
                    .HasColumnName("underobservationstop");

                entity.Property(e => e.Wcdate)
                    .HasColumnType("datetime")
                    .HasColumnName("wcdate");

                entity.Property(e => e.Wdstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wdstatus");

                entity.Property(e => e.Wmr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("WMR");

                entity.Property(e => e.Workdescription)
                    .IsUnicode(false)
                    .HasColumnName("workdescription");
            });

            modelBuilder.Entity<MissueFollowup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MissueFollowup");

                entity.Property(e => e.FollowupBy)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("followup_by");

                entity.Property(e => e.FollowupDoc)
                    .HasColumnType("datetime")
                    .HasColumnName("followup_doc");

                entity.Property(e => e.FollowupId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("followup_id");

                entity.Property(e => e.FollowupIssueid).HasColumnName("followup_issueid");

                entity.Property(e => e.FollowupRemarks)
                    .IsUnicode(false)
                    .HasColumnName("followup_remarks");
            });

            modelBuilder.Entity<Missuecancel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("missuecancel");

                entity.Property(e => e.Cid)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cid");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Iid).HasColumnName("iid");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");
            });

            modelBuilder.Entity<Moveout>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("moveout");

                entity.Property(e => e.Acards)
                    .IsUnicode(false)
                    .HasColumnName("acards");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Ardate)
                    .HasColumnType("date")
                    .HasColumnName("ardate");

                entity.Property(e => e.Armat)
                    .HasColumnType("money")
                    .HasColumnName("armat");

                entity.Property(e => e.Bmapproval)
                    .IsUnicode(false)
                    .HasColumnName("bmapproval");

                entity.Property(e => e.Bmapproved)
                    .HasMaxLength(10)
                    .HasColumnName("bmapproved")
                    .IsFixedLength(true);

                entity.Property(e => e.Bmreason)
                    .IsUnicode(false)
                    .HasColumnName("bmreason");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fileclosd)
                    .HasMaxLength(10)
                    .HasColumnName("fileclosd")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Keys)
                    .IsUnicode(false)
                    .HasColumnName("keys");

                entity.Property(e => e.Lc)
                    .HasMaxLength(10)
                    .HasColumnName("lc")
                    .IsFixedLength(true);

                entity.Property(e => e.Moi)
                    .IsUnicode(false)
                    .HasColumnName("MOI");

                entity.Property(e => e.Moidate)
                    .HasColumnType("date")
                    .HasColumnName("moidate");

                entity.Property(e => e.Moilist)
                    .IsUnicode(false)
                    .HasColumnName("moilist");

                entity.Property(e => e.Moistatus)
                    .HasMaxLength(10)
                    .HasColumnName("moistatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Pmodeofsd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pmodeofsd");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Reduction)
                    .HasMaxLength(10)
                    .HasColumnName("reduction")
                    .IsFixedLength(true);

                entity.Property(e => e.Refurbbrkdown)
                    .IsUnicode(false)
                    .HasColumnName("refurbbrkdown");

                entity.Property(e => e.Rentpaid)
                    .HasMaxLength(10)
                    .HasColumnName("rentpaid")
                    .IsFixedLength(true);

                entity.Property(e => e.Rmat)
                    .HasColumnType("money")
                    .HasColumnName("rmat");

                entity.Property(e => e.Sat)
                    .HasMaxLength(10)
                    .HasColumnName("sat")
                    .IsFixedLength(true);

                entity.Property(e => e.Sd)
                    .HasMaxLength(10)
                    .HasColumnName("sd")
                    .IsFixedLength(true);

                entity.Property(e => e.Sdamt)
                    .HasColumnType("money")
                    .HasColumnName("sdamt");

                entity.Property(e => e.Sdendorsement)
                    .HasMaxLength(10)
                    .HasColumnName("sdendorsement")
                    .IsFixedLength(true);

                entity.Property(e => e.Sdfrom)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sdfrom");

                entity.Property(e => e.Sparemarks)
                    .IsUnicode(false)
                    .HasColumnName("SPAremarks");

                entity.Property(e => e.Tenantname)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("tenantname");

                entity.Property(e => e.Tenantrefurb)
                    .HasMaxLength(10)
                    .HasColumnName("tenantrefurb")
                    .IsFixedLength(true);

                entity.Property(e => e.Updatedby)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("updatedby");

                entity.Property(e => e.Vn)
                    .IsUnicode(false)
                    .HasColumnName("vn");

                entity.Property(e => e.Waved)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("waved");
            });

            modelBuilder.Entity<Movingin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("movingin");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Inventorylist)
                    .IsUnicode(false)
                    .HasColumnName("inventorylist");

                entity.Property(e => e.Inventoryreport)
                    .IsUnicode(false)
                    .HasColumnName("inventoryreport");

                entity.Property(e => e.Keys)
                    .IsUnicode(false)
                    .HasColumnName("keys");

                entity.Property(e => e.Leaseid).HasColumnName("leaseid");

                entity.Property(e => e.Leaseno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("leaseno");

                entity.Property(e => e.Movingin1)
                    .HasMaxLength(10)
                    .HasColumnName("movingin")
                    .IsFixedLength(true);

                entity.Property(e => e.Movingindate)
                    .HasColumnType("date")
                    .HasColumnName("movingindate");

                entity.Property(e => e.Other)
                    .IsUnicode(false)
                    .HasColumnName("other");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Receipt)
                    .IsUnicode(false)
                    .HasColumnName("receipt");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rented)
                    .HasMaxLength(10)
                    .HasColumnName("RENTED")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Mrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MRENT");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Month).HasColumnName("MONTH");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("RENT");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            modelBuilder.Entity<Mrequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mrequest");

                entity.Property(e => e.Aptlocation)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("APTLOCATION");

                entity.Property(e => e.Availabletime)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("availabletime");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Issue)
                    .IsUnicode(false)
                    .HasColumnName("ISSUE");

                entity.Property(e => e.Paname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("paname");

                entity.Property(e => e.Pl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pl");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Statusseen)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUSSEEN");
            });

            modelBuilder.Entity<Mstatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MSTATUS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Sname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SNAME");
            });

            modelBuilder.Entity<MstrFaMachine>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mstr_FA-Machine");

                entity.Property(e => e.MacId).HasColumnName("Mac_Id");

                entity.Property(e => e.MacName)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("Mac_Name");

                entity.Property(e => e.MacNumber)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Mac_Number");

                entity.Property(e => e.MacValid)
                    .HasColumnType("date")
                    .HasColumnName("Mac_Valid");

                entity.Property(e => e.MacValue)
                    .HasColumnType("money")
                    .HasColumnName("Mac_Value");
            });

            modelBuilder.Entity<MstrFaTreatment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mstr_FA-Treatment");

                entity.Property(e => e.TreatArName)
                    .IsUnicode(false)
                    .HasColumnName("Treat_ArName");

                entity.Property(e => e.TreatCost)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Cost");

                entity.Property(e => e.TreatDisc1)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Disc1");

                entity.Property(e => e.TreatDisc2)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Disc2");

                entity.Property(e => e.TreatId).HasColumnName("Treat_id");

                entity.Property(e => e.TreatName)
                    .IsUnicode(false)
                    .HasColumnName("Treat_Name");

                entity.Property(e => e.TreatPr1)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Pr1");

                entity.Property(e => e.TreatPr2)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Pr2");

                entity.Property(e => e.TreatPr3)
                    .HasColumnType("money")
                    .HasColumnName("Treat_Pr3");

                entity.Property(e => e.TreatRemarks)
                    .IsUnicode(false)
                    .HasColumnName("Treat_Remarks");
            });

            modelBuilder.Entity<MstrStockItemQty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Mstr_StockItemQty");

                entity.Property(e => e.StockItemCode)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Stock_ItemCode");

                entity.Property(e => e.StockItemLoc).HasColumnName("Stock_ItemLoc");

                entity.Property(e => e.StockItemQty).HasColumnName("Stock_ItemQty");
            });

            modelBuilder.Entity<Myno>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("myno");

                entity.Property(e => e.No).HasColumnName("no");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("nationality");

                entity.Property(e => e.Nationalityname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nationalityname");
            });

            modelBuilder.Entity<Nationalitydetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Nationalitydetail");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Refno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NoorBuilding>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Noor Building");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NOTES");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Note1)
                    .IsUnicode(false)
                    .HasColumnName("NOTE");

                entity.Property(e => e.Sec)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SEC");

                entity.Property(e => e.Statement)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("statement");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Ot>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ot");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Ordertype)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ordertype");
            });

            modelBuilder.Entity<Othernote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("othernote");

                entity.Property(e => e.Category)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remark)
                    .IsUnicode(false)
                    .HasColumnName("remark");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Otherpaytype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OTHERPAYTYPE");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Paytype)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PAYTYPE");
            });

            modelBuilder.Entity<Otherproperty>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OTHERPROPERTY");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Pname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");
            });

            modelBuilder.Entity<Othersource>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("othersource");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<P>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ps");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Prosource)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("prosource");
            });

            modelBuilder.Entity<P1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("p1");

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");
            });

            modelBuilder.Entity<Padocx>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PAdocx");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.DocPath)
                    .IsUnicode(false)
                    .HasColumnName("doc_path");

                entity.Property(e => e.DocType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("doc_type");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Paemail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("paemail");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Palist>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("PAlist");

                //entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.PaEmpcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pa_empcode");

                entity.Property(e => e.PaName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pa_name");
            });

            modelBuilder.Entity<Palogin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PALOGIN");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Logmode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOGMODE");

                entity.Property(e => e.Logtime)
                    .HasColumnType("datetime")
                    .HasColumnName("LOGTIME");

                entity.Property(e => e.Mode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MODE");

                entity.Property(e => e.Paname)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("PANAME");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Sysname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("sysname");
            });

            modelBuilder.Entity<Pamailsending>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PAmailsending");

                entity.Property(e => e.Emailid)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("emailid");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mode");
            });

            modelBuilder.Entity<Papayment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("papayments");

                entity.Property(e => e.Advid).HasColumnName("advid");

                entity.Property(e => e.Amtinwords)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("amtinwords");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Banked)
                    .HasColumnType("date")
                    .HasColumnName("banked");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("bankname");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("bt");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Chqno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("chqno");

                entity.Property(e => e.Collectionmonth).HasColumnName("collectionmonth");

                entity.Property(e => e.Collectionyear).HasColumnName("collectionyear");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Discamt)
                    .HasColumnType("money")
                    .HasColumnName("discamt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Entrymode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entrymode");

                entity.Property(e => e.Exclude)
                    .HasMaxLength(10)
                    .HasColumnName("exclude")
                    .IsFixedLength(true);

                entity.Property(e => e.Highlight)
                    .HasMaxLength(10)
                    .HasColumnName("highlight")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paymenttype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pvno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PVNO");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remstatus)
                    .HasMaxLength(10)
                    .HasColumnName("remstatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Reprinted)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reprinted");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Sortid).HasColumnName("sortid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Passwordaccess>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("passwordaccess");

                entity.Property(e => e.Apppwd)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("apppwd");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("payments");

                entity.Property(e => e.Advid).HasColumnName("advid");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Banked)
                    .HasColumnType("date")
                    .HasColumnName("banked");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("bt");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Collectionmonth).HasColumnName("collectionmonth");

                entity.Property(e => e.Collectionyear).HasColumnName("collectionyear");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Discamt)
                    .HasColumnType("money")
                    .HasColumnName("discamt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Entrymode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entrymode");

                entity.Property(e => e.Entryuser)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("entryuser");

                entity.Property(e => e.Exclude)
                    .HasMaxLength(10)
                    .HasColumnName("exclude")
                    .IsFixedLength(true);

                entity.Property(e => e.Highlight)
                    .HasMaxLength(10)
                    .HasColumnName("highlight")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Oldsd).HasColumnName("oldsd");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paymenttype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pvno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PVNO");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remstatus)
                    .HasMaxLength(10)
                    .HasColumnName("remstatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Showpending)
                    .HasMaxLength(10)
                    .HasColumnName("showpending")
                    .IsFixedLength(true);

                entity.Property(e => e.Sortid).HasColumnName("sortid");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Paymentsdummy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("paymentsdummy");

                entity.Property(e => e.Advid).HasColumnName("advid");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Banked)
                    .HasColumnType("date")
                    .HasColumnName("banked");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("bt");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Collectionmonth).HasColumnName("collectionmonth");

                entity.Property(e => e.Collectionyear).HasColumnName("collectionyear");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Discamt)
                    .HasColumnType("money")
                    .HasColumnName("discamt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.EntryUser)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Entrymode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entrymode");

                entity.Property(e => e.Exclude)
                    .HasMaxLength(10)
                    .HasColumnName("exclude")
                    .IsFixedLength(true);

                entity.Property(e => e.Highlight)
                    .HasMaxLength(10)
                    .HasColumnName("highlight")
                    .IsFixedLength(true);

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paymenttype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pvno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PVNO");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remstatus)
                    .HasMaxLength(10)
                    .HasColumnName("remstatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Sortid).HasColumnName("sortid");

                entity.Property(e => e.Tid).HasColumnName("tid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Paymentsstatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PAYMENTSSTATUS");

                entity.Property(e => e.Month)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MONTH");

                entity.Property(e => e.Pname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("YEAR");
            });

            modelBuilder.Entity<Paymentstemp>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("paymentstemp");

                entity.Property(e => e.Additional)
                    .HasColumnType("money")
                    .HasColumnName("additional");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Internet)
                    .HasColumnType("money")
                    .HasColumnName("internet");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Lend)
                    .HasColumnType("date")
                    .HasColumnName("lend");

                entity.Property(e => e.Lstart)
                    .HasColumnType("date")
                    .HasColumnName("lstart");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Other)
                    .HasColumnType("money")
                    .HasColumnName("other");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Resf)
                    .HasColumnType("money")
                    .HasColumnName("resf");

                entity.Property(e => e.Rno)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rno");

                entity.Property(e => e.Sd)
                    .HasColumnType("money")
                    .HasColumnName("sd");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Paymentvoucher>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("paymentvoucher");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.AmtType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("amt-type");

                entity.Property(e => e.Amtinwords)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("amtinwords");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("APTNO");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("BT");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("CASH");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("CHEQUE");

                entity.Property(e => e.Comments)
                    .IsUnicode(false)
                    .HasColumnName("comments");

                entity.Property(e => e.Datefrom)
                    .HasColumnType("date")
                    .HasColumnName("datefrom");

                entity.Property(e => e.Dateto)
                    .HasColumnType("date")
                    .HasColumnName("dateto");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Flag)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("flag");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Inqno)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("inqno");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("KNET");

                entity.Property(e => e.LcNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lc_no");

                entity.Property(e => e.LoiNo1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loi_no");

                entity.Property(e => e.Loino)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loino");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("payment_date");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Ptype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ptype");

                entity.Property(e => e.Tname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TNAME");

                entity.Property(e => e.User)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("user");

                entity.Property(e => e.Voucherno)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("voucherno");
            });

            modelBuilder.Entity<Periodexpense>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("periodexpenses");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Gridnum).HasColumnName("gridnum");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("month");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Periodexpensesentry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("periodexpensesentry");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Gridnum).HasColumnName("gridnum");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("month");

                entity.Property(e => e.Pmeid).HasColumnName("pmeid");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Pgl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PGL");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Balcony)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Bed)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Block).IsUnicode(false);

                entity.Property(e => e.Cancellationstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cancellationstatus");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Completedby)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateofCancel).HasColumnType("date");

                entity.Property(e => e.DateofCancelLe)
                    .HasColumnType("date")
                    .HasColumnName("DateofCancelLE");

                entity.Property(e => e.Doc).HasColumnType("date");

                entity.Property(e => e.Driverroom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EnquiryDate).HasColumnType("date");

                entity.Property(e => e.Enquirytype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("enquirytype");

                entity.Property(e => e.Furnished)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Garden)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Gym)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.InquiryStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Interest).IsUnicode(false);

                entity.Property(e => e.Internet)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Maidroom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Maxbudget).HasColumnType("money");

                entity.Property(e => e.MinBudget).HasColumnType("money");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Movingdate)
                    .HasColumnType("date")
                    .HasColumnName("movingdate");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtherInfo).IsUnicode(false);

                entity.Property(e => e.Othersource)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("othersource");

                entity.Property(e => e.Parking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Pglrefno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PGLREFNO");

                entity.Property(e => e.Pool)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PropertySource)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Propertytype)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonofCancellation).IsUnicode(false);

                entity.Property(e => e.ReasontocancelLe)
                    .IsUnicode(false)
                    .HasColumnName("ReasontocancelLE");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialInstructions).IsUnicode(false);

                entity.Property(e => e.Tv)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Updatedon).HasColumnType("date");

                entity.Property(e => e.Wasinactive)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("wasinactive")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Pgldetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pgldetail");

                entity.Property(e => e.Agentfeeby)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pglno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Rent).HasColumnType("money");

                entity.Property(e => e.Rentby)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SecurityDepositby)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SpecialInstructions).IsUnicode(false);
            });

            modelBuilder.Entity<Pme>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PME");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Cat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cat");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Invdate)
                    .HasColumnType("date")
                    .HasColumnName("invdate");

                entity.Property(e => e.Invno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("invno");

                entity.Property(e => e.Mapt)
                    .HasMaxLength(10)
                    .HasColumnName("mapt")
                    .IsFixedLength(true);

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Staff)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("staff");

                entity.Property(e => e.Typeofpme)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("typeofpme");

                entity.Property(e => e.Workdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("workdesc");
            });

            modelBuilder.Entity<Pme1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PMES");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Cat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cat");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Exp)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("exp");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Invdate)
                    .HasColumnType("date")
                    .HasColumnName("invdate");

                entity.Property(e => e.Invno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("invno");

                entity.Property(e => e.Mapt)
                    .HasMaxLength(10)
                    .HasColumnName("mapt")
                    .IsFixedLength(true);

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.St)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ST");

                entity.Property(e => e.Staff)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("staff");

                entity.Property(e => e.Typeofpmes)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("typeofpmes");

                entity.Property(e => e.Workdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("workdesc");
            });

            modelBuilder.Entity<Pmesaccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pmesaccounts");

                entity.Property(e => e.Accstatus)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("accstatus");

                entity.Property(e => e.Accstname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ACCSTNAME");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Category)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Invdate)
                    .HasColumnType("date")
                    .HasColumnName("invdate");

                entity.Property(e => e.Invno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("invno");

                entity.Property(e => e.Month)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("month");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .HasColumnName("status")
                    .IsFixedLength(true);

                entity.Property(e => e.Stname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STNAME");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Pmesreq>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PMESREQ");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("AMT");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APTNO");

                entity.Property(e => e.Cat)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CAT");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Mapt)
                    .HasMaxLength(10)
                    .HasColumnName("MAPT")
                    .IsFixedLength(true);

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Pref)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PREF");

                entity.Property(e => e.Qty)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("QTY");

                entity.Property(e => e.Refno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REFNO");

                entity.Property(e => e.Uprice)
                    .HasColumnType("money")
                    .HasColumnName("UPRICE");
            });

            modelBuilder.Entity<Pmesst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PMESST");

                entity.Property(e => e.Accounts)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("accounts");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("AMT");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("APTNO");

                entity.Property(e => e.Cat)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("CAT");

                entity.Property(e => e.Category)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("DATE");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("DOC");

                entity.Property(e => e.Exp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("exp");

                entity.Property(e => e.Exptype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("exptype");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Invno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INVNO");

                entity.Property(e => e.Mapt)
                    .HasMaxLength(10)
                    .HasColumnName("MAPT")
                    .IsFixedLength(true);

                entity.Property(e => e.No)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NO");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PNAME");

                entity.Property(e => e.Pref)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PREF");

                entity.Property(e => e.Qty)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("QTY");

                entity.Property(e => e.Refno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REFNO");

                entity.Property(e => e.Smonth)
                    .HasColumnType("date")
                    .HasColumnName("SMONTH");

                entity.Property(e => e.Stid).HasColumnName("stid");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.Uprice)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPRICE");
            });

            modelBuilder.Entity<Pmetype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PMETYPE");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Typename)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TYPENAME");
            });

            modelBuilder.Entity<Pmf>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pmf");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Pmf1)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("pmf");

                entity.Property(e => e.Pmfamt)
                    .HasColumnType("money")
                    .HasColumnName("PMFAMT");

                entity.Property(e => e.Pmfdisc)
                    .HasColumnType("money")
                    .HasColumnName("PMFDISC");

                entity.Property(e => e.Pmfinv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pmfinv");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Po>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("po");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Podate)
                    .HasColumnType("date")
                    .HasColumnName("podate");

                entity.Property(e => e.Poid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("poid");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Staff)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("staff");
            });

            modelBuilder.Entity<Privatemileage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("privatemileage");

                entity.Property(e => e.Closemil).HasColumnName("closemil");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Drivername)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("drivername");

                entity.Property(e => e.Drivinghrs)
                    .HasMaxLength(10)
                    .HasColumnName("drivinghrs")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Openmil).HasColumnName("openmil");

                entity.Property(e => e.Privatemileage1).HasColumnName("privatemileage");

                entity.Property(e => e.Timein).HasColumnName("timein");

                entity.Property(e => e.Timeout).HasColumnName("timeout");

                entity.Property(e => e.Totmil).HasColumnName("totmil");
            });

            modelBuilder.Entity<PropMaster>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PropMaster");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Balcony)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Bed)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Block)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cctv)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Chargeinkd)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dateofavailability).HasColumnType("date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Eshot)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Facilities)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("facilities");

                entity.Property(e => e.Furnished)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Garden)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Gym)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasColumnType("image")
                    .HasColumnName("image");

                entity.Property(e => e.Internet)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Kitchen)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Osn)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Parking)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Poc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pool)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NO')")
                    .IsFixedLength(true);

                entity.Property(e => e.Ppersqmtr)
                    .HasColumnType("money")
                    .HasColumnName("ppersqmtr");

                entity.Property(e => e.PropertyRefNo)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComputedColumnSql("('PL_00'+CONVERT([varchar](20),[ID],(0)))", false);

                entity.Property(e => e.Propertytype)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Propname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ANY')");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.SecurityDeposit)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sqmtrs).HasColumnName("sqmtrs");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Units)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Updatedby)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Updatedon).HasColumnType("date");

                entity.Property(e => e.Visitedby)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyChange>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PropertyChange");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.PcPname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PC_PNAME");

                entity.Property(e => e.PcSubpname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PC_SUBPNAME");
            });

            modelBuilder.Entity<PropertyFeature>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.PfFeatures)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pf_features");

                entity.Property(e => e.PfMode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pf_mode");
            });

            modelBuilder.Entity<PropertyList>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PropertyList");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertyPicture>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasColumnType("image")
                    .HasColumnName("pic");

                entity.Property(e => e.RefNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Propertyfloor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("propertyfloors");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.PfFloors)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pf_floors");

                entity.Property(e => e.PfPname)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pf_pname");
            });

            modelBuilder.Entity<Propertymaster>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("propertymaster");
                
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ac)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Action)
                    .HasMaxLength(10)
                    .HasColumnName("action")
                    .IsFixedLength(true);

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.AptNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Areasize)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Availabledate)
                    .HasColumnType("date")
                    .HasColumnName("availabledate");

                entity.Property(e => e.Balcony)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Bath)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bed)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BldgName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BldgNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bldngid).HasColumnName("bldngid");

                entity.Property(e => e.BlockName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CabelTv)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Captno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("captno");

                entity.Property(e => e.Cbtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cbtype");

                entity.Property(e => e.Cctv)
                    .HasMaxLength(10)
                    .HasColumnName("CCTV")
                    .IsFixedLength(true);

                entity.Property(e => e.Cftype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cftype");

                entity.Property(e => e.Cleaseno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cleaseno");

                entity.Property(e => e.Cmob)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cmob");

                entity.Property(e => e.Cname)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("cname");

                entity.Property(e => e.Cnat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cnat");

                entity.Property(e => e.Crent)
                    .HasColumnType("money")
                    .HasColumnName("crent");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("date")
                    .HasColumnName("dou");

                entity.Property(e => e.Facilities)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("facilities");

                entity.Property(e => e.Floors)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Furnished)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Garden)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Generalremarks)
                    .IsUnicode(false)
                    .HasColumnName("generalremarks");

                entity.Property(e => e.Gym)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Highlighting)
                    .IsUnicode(false)
                    .HasColumnName("highlighting");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("imagepath");

                entity.Property(e => e.Internet)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Kitchen)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Kwtaccept)
                    .HasMaxLength(10)
                    .HasColumnName("kwtaccept")
                    .IsFixedLength(true);

                entity.Property(e => e.Leaseend)
                    .HasColumnType("date")
                    .HasColumnName("leaseend");

                entity.Property(e => e.Leaseno)
                    .HasMaxLength(90)
                    .IsUnicode(false)
                    .HasColumnName("leaseno");

                entity.Property(e => e.Leasestart)
                    .HasColumnType("date")
                    .HasColumnName("leasestart");

                entity.Property(e => e.Leasetype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("leasetype");

                entity.Property(e => e.LivingRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Lmonthrent)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lmonthrent");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaidRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Moveout)
                    .HasColumnType("date")
                    .HasColumnName("moveout");

                entity.Property(e => e.Moveoutid)
                    .HasColumnName("moveoutid")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Moveoutremarks)
                    .IsUnicode(false)
                    .HasColumnName("moveoutremarks");

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Orderid).HasColumnName("orderid");

                entity.Property(e => e.Osn)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.PaDoc)
                    .HasColumnType("datetime")
                    .HasColumnName("pa_doc");

                entity.Property(e => e.PaMode)
                    .HasMaxLength(10)
                    .HasColumnName("pa_mode")
                    .IsFixedLength(true);

                entity.Property(e => e.PaPainting)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pa_painting");

                entity.Property(e => e.PaPaintingdate)
                    .HasColumnType("date")
                    .HasColumnName("pa_paintingdate");

                entity.Property(e => e.PaRefub)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pa_refub");

                entity.Property(e => e.PaRefubdate)
                    .HasColumnType("date")
                    .HasColumnName("pa_refubdate");

                entity.Property(e => e.PaRemarks)
                    .IsUnicode(false)
                    .HasColumnName("pa_remarks");

                entity.Property(e => e.Paci)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("paci");

                entity.Property(e => e.Paid)
                    .HasMaxLength(10)
                    .HasColumnName("paid")
                    .IsFixedLength(true);

                entity.Property(e => e.Painting)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("painting");

                entity.Property(e => e.Paintingdate)
                    .HasColumnType("date")
                    .HasColumnName("paintingdate");

                entity.Property(e => e.Parking)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Pdate)
                    .HasColumnType("date")
                    .HasColumnName("pdate");

                entity.Property(e => e.PlayArea)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Pmode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pmode");

                entity.Property(e => e.Poc)
                    .IsUnicode(false)
                    .HasColumnName("poc");

                entity.Property(e => e.Pocnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pocnumber");

                entity.Property(e => e.Pool)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Ppersqmtr)
                    .HasColumnType("money")
                    .HasColumnName("ppersqmtr");

                entity.Property(e => e.PropertyRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertySource)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pupdateby)
                    .IsUnicode(false)
                    .HasColumnName("pupdateby");

                entity.Property(e => e.Pupdatetime)
                    .HasColumnType("datetime")
                    .HasColumnName("pupdatetime");

                entity.Property(e => e.Rbtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rbtype");

                entity.Property(e => e.Refub)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("refub");

                entity.Property(e => e.Refubdate)
                    .HasColumnType("date")
                    .HasColumnName("refubdate");

                entity.Property(e => e.Remarks).IsUnicode(false);

                entity.Property(e => e.Rented)
                    .HasMaxLength(10)
                    .HasColumnName("rented")
                    .IsFixedLength(true);

                entity.Property(e => e.Reservation)
                    .HasMaxLength(10)
                    .HasColumnName("reservation")
                    .IsFixedLength(true);

                entity.Property(e => e.Reservedfor)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("reservedfor");

                entity.Property(e => e.Reservedrent)
                    .HasColumnType("money")
                    .HasColumnName("reservedrent");

                entity.Property(e => e.Resf)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("resf");

                entity.Property(e => e.Rftype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rftype");

                entity.Property(e => e.Rlend)
                    .HasColumnType("date")
                    .HasColumnName("rlend");

                entity.Property(e => e.Rlstart)
                    .HasColumnType("date")
                    .HasColumnName("rlstart");

                entity.Property(e => e.Rmob)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rmob");

                entity.Property(e => e.Rnat)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rnat");

                entity.Property(e => e.Rstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rstatus");

                entity.Property(e => e.SalesType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Searchdate)
                    .HasColumnType("date")
                    .HasColumnName("searchdate");

                entity.Property(e => e.Seaview)
                    .HasMaxLength(10)
                    .HasColumnName("seaview")
                    .IsFixedLength(true);

                entity.Property(e => e.Security)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Showflat)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("showflat");

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("source");

                entity.Property(e => e.Sqmtrs)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sqmtrs");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StudyRoom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Tenantid).HasColumnName("tenantid");

                entity.Property(e => e.Tid)
                    .HasColumnName("TID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Units).IsUnicode(false);

                entity.Property(e => e.Updated)
                    .HasMaxLength(10)
                    .HasColumnName("updated")
                    .IsFixedLength(true);

                entity.Property(e => e.Updatedby)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("updatedby");

                entity.Property(e => e.Updatedon)
                    .HasColumnType("date")
                    .HasColumnName("updatedon");

                entity.Property(e => e.Vacatingstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vacatingstatus");

                entity.Property(e => e.Vistedby)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("vistedby");
            });

            modelBuilder.Entity<Propertytype>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("propertytype");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Ptv>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PTV");

                entity.Property(e => e.Location)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.Pdesc)
                    .IsUnicode(false)
                    .HasColumnName("pdesc");

                entity.Property(e => e.Poc)
                    .IsUnicode(false)
                    .HasColumnName("POC");

                entity.Property(e => e.Propertname)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("PROPERTNAME");

                entity.Property(e => e.Propertyref)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROPERTYREF");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("RENT");

                entity.Property(e => e.Sid).HasColumnName("SID");

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SOURCE");
            });

            modelBuilder.Entity<Pwd>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pwd");

                entity.Property(e => e.Dept)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dept");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Module)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("module");

                entity.Property(e => e.Pwd1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pwd");
            });

            modelBuilder.Entity<Quicfix>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("QUICFIX");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PWD");
            });

            modelBuilder.Entity<Quickfix>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("quickfixes");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("DOC");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Qaptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("qaptno");

                entity.Property(e => e.Qpname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("qpname");

                entity.Property(e => e.Qsysname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("qsysname");

                entity.Property(e => e.Qtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("qtype");
            });

            modelBuilder.Entity<RawaseaResidence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Rawasea Residence");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("receipt");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Mode)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("MODE");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Rno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rno");

                entity.Property(e => e.Sno).HasColumnName("SNO");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Year).HasColumnName("YEAR");
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("reminder");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Enqno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("enqno");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Reminderno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reminderno");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Usr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usr");
            });

            modelBuilder.Entity<Removeapt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("removeapt");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pref");
            });

            modelBuilder.Entity<Removing>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("removing");

                entity.Property(e => e.Removed)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("removed");
            });

            modelBuilder.Entity<Renewallc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("renewallc");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Leaseend)
                    .HasColumnType("date")
                    .HasColumnName("leaseend");

                entity.Property(e => e.Leaseno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("leaseno");

                entity.Property(e => e.Leasestart)
                    .HasColumnType("date")
                    .HasColumnName("leasestart");

                entity.Property(e => e.Myid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("myid");
            });

            modelBuilder.Entity<Renewalreason>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("renewalreasons");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Reasons)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("reasons");
            });

            modelBuilder.Entity<Rentchange>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rentchange");

                entity.Property(e => e.Approvednotice)
                    .IsUnicode(false)
                    .HasColumnName("approvednotice");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Effectfrom)
                    .HasColumnType("date")
                    .HasColumnName("effectfrom");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.Newrent)
                    .HasColumnType("money")
                    .HasColumnName("newrent");

                entity.Property(e => e.Oldrent)
                    .HasColumnType("money")
                    .HasColumnName("oldrent");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rentchangeid).HasColumnName("rentchangeid");
            });

            modelBuilder.Entity<Rented>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RENTED");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Highlighting)
                    .IsUnicode(false)
                    .HasColumnName("highlighting");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcdate)
                    .HasColumnType("date")
                    .HasColumnName("lcdate");

                entity.Property(e => e.Movein).HasColumnType("date");

                entity.Property(e => e.Newlyrented)
                    .HasMaxLength(10)
                    .HasColumnName("newlyrented")
                    .IsFixedLength(true);

                entity.Property(e => e.Note)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.Pdate)
                    .HasColumnType("date")
                    .HasColumnName("pdate");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Pstatus)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pstatus");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rstatus)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rstatus");

                entity.Property(e => e.Tid).HasColumnName("TID");
            });

            modelBuilder.Entity<Rentedapartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("rentedapartments");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Lcdate)
                    .HasColumnType("date")
                    .HasColumnName("lcdate");

                entity.Property(e => e.Movingindate)
                    .HasColumnType("date")
                    .HasColumnName("movingindate");

                entity.Property(e => e.Paintstatus)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("paintstatus");

                entity.Property(e => e.Pcompleted)
                    .HasColumnType("date")
                    .HasColumnName("pcompleted");

                entity.Property(e => e.Pname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Rcompleted)
                    .HasColumnType("date")
                    .HasColumnName("rcompleted");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rstatus)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("rstatus");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Rentstatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Rentstatement");

                entity.Property(e => e.Actualrent)
                    .HasColumnType("money")
                    .HasColumnName("actualrent");

                entity.Property(e => e.Aptno)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Asr)
                    .HasMaxLength(10)
                    .HasColumnName("ASR")
                    .IsFixedLength(true);

                entity.Property(e => e.Asrftype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("asrftype");

                entity.Property(e => e.Asrstatus)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("asrstatus");

                entity.Property(e => e.Bath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bath");

                entity.Property(e => e.Bed)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("bed");

                entity.Property(e => e.Close)
                    .HasMaxLength(10)
                    .HasColumnName("close")
                    .IsFixedLength(true);

                entity.Property(e => e.Corpinv)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("corpinv");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Ftype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ftype");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Leaseend)
                    .HasColumnType("date")
                    .HasColumnName("leaseend");

                entity.Property(e => e.Leasestart)
                    .HasColumnType("date")
                    .HasColumnName("leasestart");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mpay)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mpay");

                entity.Property(e => e.Nat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nat");

                entity.Property(e => e.Pname)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Tname)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Updated)
                    .HasMaxLength(10)
                    .HasColumnName("updated")
                    .IsFixedLength(true);

                entity.Property(e => e.Vtenant)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("vtenant");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<Reportingremark>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("reportingremarks");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("requirements");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Requirements)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("requirements");

                entity.Property(e => e.Typeofrequirements)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typeofrequirements");
            });

            modelBuilder.Entity<Rissue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RISSUE");

                entity.Property(e => e.Additionalwork)
                    .IsUnicode(false)
                    .HasColumnName("additionalwork");

                entity.Property(e => e.Aptlocation)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("APTLOCATION");

                entity.Property(e => e.Category)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Contactno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contactno");

                entity.Property(e => e.Contractorname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("contractorname");

                entity.Property(e => e.Contractortype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("contractortype");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Fcaquestion)
                    .IsUnicode(false)
                    .HasColumnName("fcaquestion");

                entity.Property(e => e.Fcaseen)
                    .HasMaxLength(10)
                    .HasColumnName("fcaseen")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Issue)
                    .IsUnicode(false)
                    .HasColumnName("ISSUE");

                entity.Property(e => e.IssueNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IssueNO");

                entity.Property(e => e.Itempurchase)
                    .HasMaxLength(10)
                    .HasColumnName("itempurchase")
                    .IsFixedLength(true);

                entity.Property(e => e.Jobid)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("JOBID");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.Paname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("PANAME");

                entity.Property(e => e.Pareply)
                    .IsUnicode(false)
                    .HasColumnName("pareply");

                entity.Property(e => e.Pl)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PL");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PREF");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("REMARKS");

                entity.Property(e => e.Request)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("REQUEST");

                entity.Property(e => e.Requestdate)
                    .HasColumnType("datetime")
                    .HasColumnName("requestdate");

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Tavailability)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tavailability");

                entity.Property(e => e.Tname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Underobservationstart)
                    .HasColumnType("datetime")
                    .HasColumnName("underobservationstart");

                entity.Property(e => e.Underobservationstop)
                    .HasColumnType("date")
                    .HasColumnName("underobservationstop");

                entity.Property(e => e.Wcdate)
                    .HasColumnType("datetime")
                    .HasColumnName("wcdate");

                entity.Property(e => e.Wdstatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wdstatus");

                entity.Property(e => e.Workdescription)
                    .IsUnicode(false)
                    .HasColumnName("workdescription");
            });

            modelBuilder.Entity<SafiTower>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Safi Tower");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<ShaikhaComplex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Shaikha Complex");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<ShaikhaTower>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Shaikha Tower");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<SharifaAlMullaBuilding>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Sharifa Al Mulla Building");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Source");

                entity.Property(e => e.Othername)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Source1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Source");
            });

            modelBuilder.Entity<Stinv>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stinv");

                entity.Property(e => e.Attstaff)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("attstaff");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Invno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("invno");

                entity.Property(e => e.No)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("no");
            });

            modelBuilder.Entity<Sublease>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("subleases");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Actualrent)
                    .HasColumnType("money")
                    .HasColumnName("actualrent");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Bed)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BED");

                entity.Property(e => e.Doc)
                    .HasColumnType("date")
                    .HasColumnName("doc");

                entity.Property(e => e.Ftype)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ftype");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcrent)
                    .HasColumnType("money")
                    .HasColumnName("lcrent");

                entity.Property(e => e.Lend)
                    .HasColumnType("date")
                    .HasColumnName("lend");

                entity.Property(e => e.Lno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("lno");

                entity.Property(e => e.Lstart)
                    .HasColumnType("date")
                    .HasColumnName("lstart");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Paymode)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("paymode");

                entity.Property(e => e.Pname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Slno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("slno");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Ttype)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ttype");
            });

            modelBuilder.Entity<Subleasename>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("subleasename");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Tdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TDETAILS");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Lastupdatednat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatednat");

                entity.Property(e => e.Lastupdatedtbath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedtbath");

                entity.Property(e => e.Lastupdatedtbed)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedtbed");

                entity.Property(e => e.Lastupdatedtctpe)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedtctpe");

                entity.Property(e => e.Lastupdatedtmode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedtmode");

                entity.Property(e => e.Lastupdatedtname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedtname");

                entity.Property(e => e.Lastupdatedtprent)
                    .HasColumnType("money")
                    .HasColumnName("lastupdatedtprent");

                entity.Property(e => e.Lastupdatedtrent)
                    .HasColumnType("money")
                    .HasColumnName("lastupdatedtrent");

                entity.Property(e => e.Lastupdatedttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastupdatedttype");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Tbath)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TBATH");

                entity.Property(e => e.Tbed)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TBED");

                entity.Property(e => e.Tctype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TCTYPE");

                entity.Property(e => e.Tmode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TMODE");

                entity.Property(e => e.Tname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("TNAME");

                entity.Property(e => e.Tnat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TNAT");

                entity.Property(e => e.Tprent)
                    .HasColumnType("money")
                    .HasColumnName("tprent");

                entity.Property(e => e.Trent)
                    .HasColumnType("money")
                    .HasColumnName("TRENT");

                entity.Property(e => e.Ttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TTYPE");
            });

            modelBuilder.Entity<Templogin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Templogin");

                entity.Property(e => e.TempDate)
                    .HasColumnType("date")
                    .HasColumnName("Temp_Date");

                entity.Property(e => e.TempEmpName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Temp_EmpName");

                entity.Property(e => e.TempEmpNr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Temp_EmpNr");

                entity.Property(e => e.TempIn).HasColumnName("Temp_in");

                entity.Property(e => e.TempLates).HasColumnName("Temp_lates");

                entity.Property(e => e.TempOut).HasColumnName("Temp_out");

                entity.Property(e => e.TempRemarks)
                    .IsUnicode(false)
                    .HasColumnName("Temp_remarks");

                entity.Property(e => e.TempStart).HasColumnName("Temp_start");
            });

            modelBuilder.Entity<Temppayment>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Advid).HasColumnName("advid");

                entity.Property(e => e.Amtinwords)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("amtinwords");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Banked)
                    .HasColumnType("date")
                    .HasColumnName("banked");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("bankname");

                entity.Property(e => e.Bt)
                    .HasColumnType("money")
                    .HasColumnName("bt");

                entity.Property(e => e.Cash)
                    .HasColumnType("money")
                    .HasColumnName("cash");

                entity.Property(e => e.Cheque)
                    .HasColumnType("money")
                    .HasColumnName("cheque");

                entity.Property(e => e.Chqno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("chqno");

                entity.Property(e => e.Collectionmonth).HasColumnName("collectionmonth");

                entity.Property(e => e.Collectionyear).HasColumnName("collectionyear");

                entity.Property(e => e.Description)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Discamt)
                    .HasColumnType("money")
                    .HasColumnName("discamt");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Entrymode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("entrymode");

                entity.Property(e => e.Exclude)
                    .HasMaxLength(10)
                    .HasColumnName("exclude")
                    .IsFixedLength(true);

                entity.Property(e => e.Highlight)
                    .HasMaxLength(10)
                    .HasColumnName("highlight")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Knet)
                    .HasColumnType("money")
                    .HasColumnName("knet");

                entity.Property(e => e.Month).HasColumnName("month");

                entity.Property(e => e.Mrent)
                    .HasColumnType("money")
                    .HasColumnName("mrent");

                entity.Property(e => e.Oldsd).HasColumnName("oldsd");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("paymenttype");

                entity.Property(e => e.Pname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pvno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PVNO");

                entity.Property(e => e.Rdate)
                    .HasColumnType("date")
                    .HasColumnName("rdate");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Remstatus)
                    .HasMaxLength(10)
                    .HasColumnName("remstatus")
                    .IsFixedLength(true);

                entity.Property(e => e.Rentdatefrom)
                    .HasColumnType("date")
                    .HasColumnName("rentdatefrom");

                entity.Property(e => e.Rentdateto)
                    .HasColumnType("date")
                    .HasColumnName("rentdateto");

                entity.Property(e => e.Rno).HasColumnName("rno");

                entity.Property(e => e.Sortid).HasColumnName("sortid");

                entity.Property(e => e.Tname)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Totamt)
                    .HasColumnType("money")
                    .HasColumnName("totamt");

                entity.Property(e => e.Year).HasColumnName("year");
            });

            modelBuilder.Entity<TenantEntry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TenantEntry");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Pname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.TeCid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("te_cid");

                entity.Property(e => e.TeDoc)
                    .HasColumnType("datetime")
                    .HasColumnName("te_doc");

                entity.Property(e => e.TeLcno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("te_lcno");

                entity.Property(e => e.TeTname)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("te_tname");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Tenantshistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tenantshistory");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Btype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("btype");

                entity.Property(e => e.Closedate)
                    .HasColumnType("datetime")
                    .HasColumnName("closedate");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Fcaseen)
                    .HasMaxLength(10)
                    .HasColumnName("fcaseen")
                    .IsFixedLength(true);

                entity.Property(e => e.Fcdocpath)
                    .IsUnicode(false)
                    .HasColumnName("fcdocpath");

                entity.Property(e => e.Fcreason)
                    .IsUnicode(false)
                    .HasColumnName("fcreason");

                entity.Property(e => e.Forceclose)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("forceclose");

                entity.Property(e => e.Ftype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ftype");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Keyreturndate)
                    .HasColumnType("date")
                    .HasColumnName("keyreturndate");

                entity.Property(e => e.LeaseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lease_no");

                entity.Property(e => e.Leaseend)
                    .HasColumnType("date")
                    .HasColumnName("leaseend");

                entity.Property(e => e.Leasestart)
                    .HasColumnType("date")
                    .HasColumnName("leasestart");

                entity.Property(e => e.Movedate)
                    .HasColumnType("date")
                    .HasColumnName("movedate");

                entity.Property(e => e.Moveoutid).HasColumnName("moveoutid");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Pname)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .IsFixedLength(true);

                entity.Property(e => e.Tenantname)
                    .IsUnicode(false)
                    .HasColumnName("tenantname");

                entity.Property(e => e.Tid).HasColumnName("tid");
            });

            modelBuilder.Entity<Tenantstransfer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tenantstransfer");

                entity.Property(e => e.Aptno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("aptno");

                entity.Property(e => e.Btype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("btype");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contact");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Ftype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ftype");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Keyreturndate)
                    .HasColumnType("date")
                    .HasColumnName("keyreturndate");

                entity.Property(e => e.LeaseNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lease_no");

                entity.Property(e => e.Leaseend)
                    .HasColumnType("date")
                    .HasColumnName("leaseend");

                entity.Property(e => e.Leasestart)
                    .HasColumnType("date")
                    .HasColumnName("leasestart");

                entity.Property(e => e.Movedate)
                    .HasColumnType("date")
                    .HasColumnName("movedate");

                entity.Property(e => e.Moveoutid).HasColumnName("moveoutid");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Pname)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Pref)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Rent)
                    .HasColumnType("money")
                    .HasColumnName("rent");

                entity.Property(e => e.Tenantname)
                    .IsUnicode(false)
                    .HasColumnName("tenantname");
            });

            modelBuilder.Entity<Tourdetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Tourdetail");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Propertytype)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Refno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("refno");
            });

            modelBuilder.Entity<Tt>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TT");
            });

            modelBuilder.Entity<Updatedlc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UPDATEDLC");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DATE");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LCNO");

                entity.Property(e => e.Path)
                    .IsUnicode(false)
                    .HasColumnName("PATH");
            });

            modelBuilder.Entity<User>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Designation)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                //entity.Property(e => e.Id)
                //    .ValueGeneratedOnAdd()
                //    .HasColumnName("id");

                entity.Property(e => e.Logmode).HasColumnName("logmode");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Usrname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vacatingcancel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vacatingcancel");

                entity.Property(e => e.Bmapprovalpath)
                    .IsUnicode(false)
                    .HasColumnName("bmapprovalpath");

                entity.Property(e => e.Canceldocpath)
                    .IsUnicode(false)
                    .HasColumnName("canceldocpath");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lcno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lcno");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Tname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("tname");

                entity.Property(e => e.Vnpath)
                    .IsUnicode(false)
                    .HasColumnName("vnpath");
            });

            modelBuilder.Entity<Vacatingnote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vacatingnote");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Pname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remark)
                    .IsUnicode(false)
                    .HasColumnName("remark");
            });

            modelBuilder.Entity<Validchardm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VALIDCHARDMS");

                entity.Property(e => e.Char)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CHAR");
            });

            modelBuilder.Entity<Vehicleno>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("vehicleno");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Vno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vno");
            });

            modelBuilder.Entity<Vme>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VMES");

                entity.Property(e => e.Amt)
                    .HasColumnType("money")
                    .HasColumnName("amt");

                entity.Property(e => e.Attstaff)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("attstaff");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Drivername)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("drivername");

                entity.Property(e => e.Exp)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("exp");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Invdate)
                    .HasColumnType("date")
                    .HasColumnName("invdate");

                entity.Property(e => e.Invno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("invno");

                entity.Property(e => e.Odometer)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("odometer");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.Refno)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("refno");

                entity.Property(e => e.Smonth)
                    .HasColumnType("date")
                    .HasColumnName("smonth");

                entity.Property(e => e.St)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ST");

                entity.Property(e => e.Stid).HasColumnName("stid");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE");

                entity.Property(e => e.Vehiclename)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("vehiclename");

                entity.Property(e => e.Vno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vno");

                entity.Property(e => e.Workdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("workdesc");
            });

            modelBuilder.Entity<Walkininquiry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("walkininquiry");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Month)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("month");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nationality)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nationality");

                entity.Property(e => e.Pname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Seenbyfca)
                    .HasMaxLength(10)
                    .HasColumnName("seenbyfca")
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Year)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("year");
            });

            modelBuilder.Entity<WarbaBeachResort>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Warba Beach Resort");

                entity.Property(e => e.ACGrills)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("A/C Grills");

                entity.Property(e => e.AutoGateBarriers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Auto Gate Barriers");

                entity.Property(e => e.Corridors)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DrainageSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Drainage System");

                entity.Property(e => e.EquipmentGym)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Equipment / Gym");

                entity.Property(e => e.FireAlarmSecuritySystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Alarm / Security System");

                entity.Property(e => e.FireExits)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Exits");

                entity.Property(e => e.FireHoseReelsCabinetsExtinguishers)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Fire Hose Reels, Cabinets & Extinguishers");

                entity.Property(e => e.FloorLobby)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floor / Lobby");

                entity.Property(e => e.FlooringsWallsWindowsCeilingsGlassPanel)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Floorings-Walls- Windows-Ceilings-Glass Panel");

                entity.Property(e => e.Furnitures)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GarbageChutes)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Garbage Chutes");

                entity.Property(e => e.HvacRooms)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HVAC Rooms");

                entity.Property(e => e.LiftRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lift Room");

                entity.Property(e => e.LightingSystem)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Lighting System");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.ParkingArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Parking Area");

                entity.Property(e => e.PlantsGarden)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Plants & Garden");

                entity.Property(e => e.RollerShutters)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Roller Shutters");

                entity.Property(e => e.StaffAccomodation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Accomodation");

                entity.Property(e => e.StaffPresentation)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Staff Presentation");

                entity.Property(e => e.Staircases)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SwimmingPoolArea)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Swimming Pool / Area");

                entity.Property(e => e.TransformerIncomingSwitchRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Transformer & Incoming Switch Room");

                entity.Property(e => e.WaterTankPumpRoom)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Water Tank/ Pump Room");
            });

            modelBuilder.Entity<Wmrfeature>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("wmrfeatures");

                entity.Property(e => e.Features)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("features");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Pname)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("pname");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            modelBuilder.Entity<Workstatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("workstatus");

                entity.Property(e => e.Datecompp)
                    .HasColumnType("date")
                    .HasColumnName("datecompp");

                entity.Property(e => e.Datecompr)
                    .HasColumnType("date")
                    .HasColumnName("datecompr");

                entity.Property(e => e.Doc)
                    .HasColumnType("datetime")
                    .HasColumnName("doc");

                entity.Property(e => e.Dou)
                    .HasColumnType("datetime")
                    .HasColumnName("dou");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Painting)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("painting");

                entity.Property(e => e.Paintrem)
                    .IsUnicode(false)
                    .HasColumnName("paintrem");

                entity.Property(e => e.Pref)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("pref");

                entity.Property(e => e.Refurbishing)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("refurbishing");

                entity.Property(e => e.Refurbrem)
                    .IsUnicode(false)
                    .HasColumnName("refurbrem");

                entity.Property(e => e.Remarks)
                    .IsUnicode(false)
                    .HasColumnName("remarks");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
