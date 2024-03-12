using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Entities.Partners;
using Domain.Entities.Payment;
using Domain.Entities.SO;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public partial class SmartDriveContext : DbContext
{
    public SmartDriveContext()
    {
    }

    public SmartDriveContext(DbContextOptions<SmartDriveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreaWorkgroup> AreaWorkgroups { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BatchEmployeeSalary> BatchEmployeeSalaries { get; set; }

    public virtual DbSet<BatchPartnerInvoice> BatchPartnerInvoices { get; set; }

    public virtual DbSet<BusinessEntity> BusinessEntities { get; set; }

    public virtual DbSet<CarBrand> CarBrands { get; set; }

    public virtual DbSet<CarModel> CarModels { get; set; }

    public virtual DbSet<CarSeries> CarSeries { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ClaimAssetEvidence> ClaimAssetEvidences { get; set; }

    public virtual DbSet<ClaimAssetSparepart> ClaimAssetSpareparts { get; set; }

    public virtual DbSet<CustomerClaim> CustomerClaims { get; set; }

    public virtual DbSet<CustomerInscAsset> CustomerInscAssets { get; set; }

    public virtual DbSet<CustomerInscDoc> CustomerInscDocs { get; set; }

    public virtual DbSet<CustomerInscExtend> CustomerInscExtends { get; set; }

    public virtual DbSet<CustomerRequest> CustomerRequests { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAreWorkgroup> EmployeeAreWorkgroups { get; set; }

    public virtual DbSet<EmployeeSalaryDetail> EmployeeSalaryDetails { get; set; }

    public virtual DbSet<Fintech> Finteches { get; set; }

    public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }

    public virtual DbSet<JobType> JobTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerAreaWorkgroup> PartnerAreaWorkgroups { get; set; }

    public virtual DbSet<PartnerContact> PartnerContacts { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Provinsi> Provinsis { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<RegionPlat> RegionPlats { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceOrder> ServiceOrders { get; set; }

    public virtual DbSet<ServiceOrderTask> ServiceOrderTasks { get; set; }

    public virtual DbSet<ServiceOrderWorkorder> ServiceOrderWorkorders { get; set; }

    public virtual DbSet<ServicePremi> ServicePremis { get; set; }

    public virtual DbSet<ServicePremiCredit> ServicePremiCredits { get; set; }

    public virtual DbSet<TemplateInsurancePremi> TemplateInsurancePremis { get; set; }

    public virtual DbSet<TemplateSalary> TemplateSalaries { get; set; }

    public virtual DbSet<TemplateServiceTask> TemplateServiceTasks { get; set; }

    public virtual DbSet<TemplateTaskWorkorder> TemplateTaskWorkorders { get; set; }

    public virtual DbSet<TemplateType> TemplateTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AreaWorkgroup>(entity =>
        {
            entity.HasKey(e => e.ArwgCode).HasName("PK__area_wor__B0CF95B363D450DB");

            entity.HasOne(d => d.ArwgCity).WithMany(p => p.AreaWorkgroups).HasConstraintName("FK__area_work__arwg___3493CFA7");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankEntityid).HasName("pk_bank_entityid");

            entity.Property(e => e.BankEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.BankEntity).WithOne(p => p.Bank).HasConstraintName("fk_bank_entityid");
        });

        modelBuilder.Entity<BatchEmployeeSalary>(entity =>
        {
            entity.HasKey(e => new { e.BesaEmpEntityId, e.BesaCreatedDate }).HasName("PK__batch_em__D2FFC7DC3B2819E6");
        });

        modelBuilder.Entity<BatchPartnerInvoice>(entity =>
        {
            entity.HasKey(e => e.BpinInvoiceNo).HasName("pk_bpin_invoiceNo");

            entity.HasOne(d => d.BpinPatrTrxnoNavigation).WithMany(p => p.BatchPartnerInvoices)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bpin_patr_trxno");

            entity.HasOne(d => d.BpinPatrnEntity).WithMany(p => p.BatchPartnerInvoices)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bpin_patrn_entityid");

            entity.HasOne(d => d.BpinSero).WithMany(p => p.BatchPartnerInvoices).HasConstraintName("fk_bpin_sero_id");
        });

        modelBuilder.Entity<BusinessEntity>(entity =>
        {
            entity.HasKey(e => e.Entityid).HasName("PK__business__DECC754018A2C1FF");
        });

        modelBuilder.Entity<CarBrand>(entity =>
        {
            entity.HasKey(e => e.CabrId).HasName("PK__car_bran__ED0C67C631D99CAB");
        });

        modelBuilder.Entity<CarModel>(entity =>
        {
            entity.HasKey(e => e.CarmId).HasName("PK__car_mode__C680A953F5FCDC08");

            entity.HasOne(d => d.CarmCabr).WithMany(p => p.CarModels).HasConstraintName("FK__car_model__carm___3587F3E0");
        });

        modelBuilder.Entity<CarSeries>(entity =>
        {
            entity.HasKey(e => e.CarsId).HasName("PK__car_seri__588724E487CFF182");

            entity.HasOne(d => d.CarsCarm).WithMany(p => p.CarSeries).HasConstraintName("FK__car_serie__cars___367C1819");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CateId).HasName("PK__category__34EAD173D4181A27");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__cities__031491A8741B4726");

            entity.HasOne(d => d.CityProv).WithMany(p => p.Cities).HasConstraintName("FK__cities__city_pro__37703C52");
        });

        modelBuilder.Entity<ClaimAssetEvidence>(entity =>
        {
            entity.HasKey(e => e.CaevId).HasName("pk_caev_id");

            entity.HasOne(d => d.CaevPartEntity).WithMany(p => p.ClaimAssetEvidences).HasConstraintName("fk_caev_part_entityid");

            entity.HasOne(d => d.CaevSero).WithMany(p => p.ClaimAssetEvidences).HasConstraintName("fk_caev_sero_id");
        });

        modelBuilder.Entity<ClaimAssetSparepart>(entity =>
        {
            entity.HasKey(e => e.CaspId).HasName("pk_casp_id");

            entity.HasOne(d => d.CaspPartEntity).WithMany(p => p.ClaimAssetSpareparts).HasConstraintName("fk_casp_part_entityid");

            entity.HasOne(d => d.CaspSero).WithMany(p => p.ClaimAssetSpareparts).HasConstraintName("fk_casp_sero_id");
        });

        modelBuilder.Entity<CustomerClaim>(entity =>
        {
            entity.HasKey(e => e.CuclCreqEntityid).HasName("PK__customer__268FDC39F2008167");

            entity.Property(e => e.CuclCreqEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.CuclCreqEntity).WithOne(p => p.CustomerClaim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUCLCREQ");
        });

        modelBuilder.Entity<CustomerInscAsset>(entity =>
        {
            entity.HasKey(e => e.CiasCreqEntityid).HasName("PK__customer__588FDDBBF4581C25");

            entity.Property(e => e.CiasCreqEntityid).ValueGeneratedNever();
            entity.Property(e => e.CiasIsNewChar).IsFixedLength();

            entity.HasOne(d => d.CiasCars).WithMany(p => p.CustomerInscAssets).HasConstraintName("FK_CIASCARS");

            entity.HasOne(d => d.CiasCity).WithMany(p => p.CustomerInscAssets).HasConstraintName("FK_CIASCITY");

            entity.HasOne(d => d.CiasCreqEntity).WithOne(p => p.CustomerInscAsset)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CIASCREQ");

            entity.HasOne(d => d.CiasIntyNameNavigation).WithMany(p => p.CustomerInscAssets).HasConstraintName("FK_CIASINTY");
        });

        modelBuilder.Entity<CustomerInscDoc>(entity =>
        {
            entity.HasKey(e => new { e.CadocId, e.CadocCreqEntityid }).HasName("PK_CADOC");

            entity.HasOne(d => d.CadocCreqEntity).WithMany(p => p.CustomerInscDocs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CADOCCREQ");
        });

        modelBuilder.Entity<CustomerInscExtend>(entity =>
        {
            entity.HasKey(e => new { e.CuexId, e.CuexCreqEntityid }).HasName("PK_CUEX");

            entity.HasOne(d => d.CuexCreqEntity).WithMany(p => p.CustomerInscExtends)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUEXCREQ");
        });

        modelBuilder.Entity<CustomerRequest>(entity =>
        {
            entity.HasKey(e => e.CreqEntityid).HasName("PK__customer__606B6AF199592E22");

            entity.Property(e => e.CreqEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.CreqAgenEntity).WithMany(p => p.CustomerRequests)
                .HasPrincipalKey(p => p.EawgId)
                .HasForeignKey(d => d.CreqAgenEntityid)
                .HasConstraintName("FK_CREQAGEN");

            entity.HasOne(d => d.CreqCustEntity).WithMany(p => p.CustomerRequests).HasConstraintName("FK_CREQENTITY");

            entity.HasOne(d => d.CreqEntity).WithOne(p => p.CustomerRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CREQCUST_ENTITY");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpEntityid).HasName("PK__employee__4B2A27D499752B65");

            entity.Property(e => e.EmpEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.EmpEntity).WithOne(p => p.Employee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employees__emp_e__32AB8735");

            entity.HasOne(d => d.EmpJobCodeNavigation).WithMany(p => p.Employees).HasConstraintName("FK__employees__emp_j__339FAB6E");
        });

        modelBuilder.Entity<EmployeeAreWorkgroup>(entity =>
        {
            entity.HasKey(e => new { e.EawgEntityid, e.EawgId }).HasName("PK__employee__0B04DA35C0E55D77");

            entity.HasOne(d => d.EawgArwgCodeNavigation).WithMany(p => p.EmployeeAreWorkgroups).HasConstraintName("FK__employee___eawg___30C33EC3");

            entity.HasOne(d => d.EawgEntity).WithMany(p => p.EmployeeAreWorkgroups)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee___eawg___2FCF1A8A");
        });

        modelBuilder.Entity<EmployeeSalaryDetail>(entity =>
        {
            entity.HasKey(e => new { e.EmsaId, e.EmsaEmpEntityid, e.EmsaCreateDate }).HasName("PK__employee__027F0DDCCF4FC361");

            entity.HasOne(d => d.EmsaEmpEntity).WithMany(p => p.EmployeeSalaryDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__employee___emsa___31B762FC");
        });

        modelBuilder.Entity<Fintech>(entity =>
        {
            entity.HasKey(e => e.FintEntityid).HasName("pk_fint_entityid");

            entity.Property(e => e.FintEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.FintEntity).WithOne(p => p.Fintech).HasConstraintName("fk_fint_entityid");
        });

        modelBuilder.Entity<InsuranceType>(entity =>
        {
            entity.HasKey(e => e.IntyName).HasName("PK__insuranc__38A54D40A5933419");
        });

        modelBuilder.Entity<JobType>(entity =>
        {
            entity.HasKey(e => e.JobCode).HasName("PK__job_type__FBB86DB2D17BB528");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartEntityid).HasName("pk_part_entityid");

            entity.Property(e => e.PartEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.PartCity).WithMany(p => p.Partners).HasConstraintName("fk_part_city_id");

            entity.HasOne(d => d.PartEntity).WithOne(p => p.Partner).HasConstraintName("fk_part_entityid");
        });

        modelBuilder.Entity<PartnerAreaWorkgroup>(entity =>
        {
            entity.HasKey(e => new { e.PawoPatrEntityid, e.PawoArwgCode, e.PawoUserEntityid }).HasName("pk_pawo_patr_arwg_user");

            entity.HasOne(d => d.PawoArwgCodeNavigation).WithMany(p => p.PartnerAreaWorkgroups).HasConstraintName("fk_pawo_arwg_code");

            entity.HasOne(d => d.Pawo).WithMany(p => p.PartnerAreaWorkgroups).HasConstraintName("fk_pawo_patr_user");
        });

        modelBuilder.Entity<PartnerContact>(entity =>
        {
            entity.HasKey(e => new { e.PacoPatrnEntityid, e.PacoUserEntityid }).HasName("pk_paco_patrn_user");

            entity.HasOne(d => d.PacoPatrnEntity).WithMany(p => p.PartnerContacts).HasConstraintName("fk_paco_patrn_entityid");

            entity.HasOne(d => d.PacoUserEntity).WithMany(p => p.PartnerContacts).HasConstraintName("fk_paco_user_entityid");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.PatrTrxno).HasName("pk_patr_trxno");

            entity.HasOne(d => d.PatrTrxnoRevNavigation).WithMany(p => p.InversePatrTrxnoRevNavigation).HasConstraintName("fk_patr_trxno_rev");
        });

        modelBuilder.Entity<Provinsi>(entity =>
        {
            entity.HasKey(e => e.ProvId).HasName("PK__provinsi__435F53266A2CE310");

            entity.HasOne(d => d.ProvZones).WithMany(p => p.Provinsis).HasConstraintName("FK__provinsi__prov_z__3864608B");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RetoId).HasName("pk_reto_id");

            entity.HasOne(d => d.RetoUser).WithMany(p => p.RefreshTokens).HasConstraintName("fk_reto_user_id");
        });

        modelBuilder.Entity<RegionPlat>(entity =>
        {
            entity.HasKey(e => e.RegpName).HasName("PK__region_p__187EAC814CC8BA3D");

            entity.HasOne(d => d.RegpProv).WithMany(p => p.RegionPlats).HasConstraintName("FK__region_pl__regp___395884C4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleName).HasName("pk_roles");

            entity.Property(e => e.RoleName).IsFixedLength();
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServId).HasName("pk_serv_id");

            entity.HasOne(d => d.ServCreqEntity).WithMany(p => p.Services).HasConstraintName("fk_serv_creq_entityid");

            entity.HasOne(d => d.ServCustEntity).WithMany(p => p.Services).HasConstraintName("fk_serv_cust_entityid");

            entity.HasOne(d => d.ServServ).WithMany(p => p.InverseServServ).HasConstraintName("fk_serv_serv_id");
        });

        modelBuilder.Entity<ServiceOrder>(entity =>
        {
            entity.HasKey(e => e.SeroId).HasName("pk_sero_id");

            entity.HasOne(d => d.SeroAgentEntity).WithMany(p => p.ServiceOrders)
                .HasPrincipalKey(p => p.EawgId)
                .HasForeignKey(d => d.SeroAgentEntityid)
                .HasConstraintName("fk_sero_eawg_code");

            entity.HasOne(d => d.SeroPart).WithMany(p => p.ServiceOrders).HasConstraintName("FK_SERO_PART_ID");

            entity.HasOne(d => d.SeroSero).WithMany(p => p.InverseSeroSero).HasConstraintName("fk_sero_sero_id");

            entity.HasOne(d => d.SeroServ).WithMany(p => p.ServiceOrders).HasConstraintName("fk_sero_serv_id");
        });

        modelBuilder.Entity<ServiceOrderTask>(entity =>
        {
            entity.HasKey(e => e.SeotId).HasName("pk_seot_id");

            entity.HasOne(d => d.SeotArwgCodeNavigation).WithMany(p => p.ServiceOrderTasks).HasConstraintName("fk_seot_arwg_code");

            entity.HasOne(d => d.SeotSero).WithMany(p => p.ServiceOrderTasks).HasConstraintName("fk_seot_sero_id");
        });

        modelBuilder.Entity<ServiceOrderWorkorder>(entity =>
        {
            entity.HasKey(e => e.SowoId).HasName("pk_sowo_id");

            entity.HasOne(d => d.SowoSeot).WithMany(p => p.ServiceOrderWorkorders).HasConstraintName("fk_sowo_seot_id");
        });

        modelBuilder.Entity<ServicePremi>(entity =>
        {
            entity.HasKey(e => e.SemiServId).HasName("pk_semi_serv_id");

            entity.Property(e => e.SemiServId).ValueGeneratedNever();

            entity.HasOne(d => d.SemiServ).WithOne(p => p.ServicePremi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_semi_serv_id");
        });

        modelBuilder.Entity<ServicePremiCredit>(entity =>
        {
            entity.HasKey(e => new { e.SecrId, e.SecrServId }).HasName("pk_secr");

            entity.Property(e => e.SecrId).HasColumnName("secr_id").ValueGeneratedOnAdd();

            entity.HasOne(d => d.SecrPatrTrxnoNavigation).WithMany(p => p.ServicePremiCredits).HasConstraintName("fk_secr_patr_trxno");

            entity.HasOne(d => d.SecrServ).WithMany(p => p.ServicePremiCredits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_secr_serv_id");
        });

        modelBuilder.Entity<TemplateInsurancePremi>(entity =>
        {
            entity.HasKey(e => e.TemiId).HasName("PK__template__3E6268652E62DDB8");

            entity.HasOne(d => d.TemiCate).WithMany(p => p.TemplateInsurancePremis).HasConstraintName("FK__template___temi___3C34F16F");

            entity.HasOne(d => d.TemiIntyNameNavigation).WithMany(p => p.TemplateInsurancePremis).HasConstraintName("FK__template___temi___3B40CD36");

            entity.HasOne(d => d.TemiZones).WithMany(p => p.TemplateInsurancePremis).HasConstraintName("FK__template___temi___3A4CA8FD");
        });

        modelBuilder.Entity<TemplateSalary>(entity =>
        {
            entity.HasKey(e => e.TesalId).HasName("PK__template__C273C168941EAA22");
        });

        modelBuilder.Entity<TemplateServiceTask>(entity =>
        {
            entity.HasKey(e => e.TestaId).HasName("PK__template__5FE71914BC81D9DE");

            entity.HasOne(d => d.TestaTety).WithMany(p => p.TemplateServiceTasks).HasConstraintName("FK__template___testa__3D2915A8");
        });

        modelBuilder.Entity<TemplateTaskWorkorder>(entity =>
        {
            entity.HasKey(e => e.TewoId).HasName("PK__template__5130424CBFFCF0CB");

            entity.HasOne(d => d.TewoTesta).WithMany(p => p.TemplateTaskWorkorders).HasConstraintName("FK__template___tewo___3E1D39E1");
        });

        modelBuilder.Entity<TemplateType>(entity =>
        {
            entity.HasKey(e => e.TetyId).HasName("PK__template__895AF6C9C93F17CD");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserEntityid).HasName("PK__users__32806B0622625187");

            entity.Property(e => e.UserEntityid).ValueGeneratedNever();

            entity.HasOne(d => d.UserEntity).WithOne(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__users__user_enti__634EBE90");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UsacId).HasName("pk_usac_id");

            entity.HasOne(d => d.UsacBankEntity).WithMany(p => p.UserAccounts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_usac_bank_entityid");

            entity.HasOne(d => d.UsacFintEntity).WithMany(p => p.UserAccounts).HasConstraintName("fk_usac_fint_entityid");

            entity.HasOne(d => d.UsacUserEntity).WithMany(p => p.UserAccounts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_user_entityid");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => new { e.UsdrId, e.UsdrEntityid }).HasName("pk_entity_address");

            entity.HasOne(d => d.UsdrCity).WithMany(p => p.UserAddresses).HasConstraintName("fk_address_cities");

            entity.HasOne(d => d.UsdrEntity).WithMany(p => p.UserAddresses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entity_address_users");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.HasKey(e => new { e.UsphEntityid, e.UsphPhoneNumber }).HasName("pk_entity_phone");

            entity.HasOne(d => d.UsphEntity).WithMany(p => p.UserPhones)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entityid_phone");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UsroEntityid, e.UsroRoleName }).HasName("pk_usro");

            entity.Property(e => e.UsroRoleName).IsFixedLength();

            entity.HasOne(d => d.UsroEntity).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entity_usro_users");

            entity.HasOne(d => d.UsroRoleNameNavigation).WithMany(p => p.UserRoles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__usro___6166761E");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.ZonesId).HasName("PK__zones__D409535DC3E081F9");
        });
        modelBuilder.HasSequence("besa_emp_entity_id", "hr").HasMin(1L);
        modelBuilder.HasSequence("cadoc_cuex_id");
        modelBuilder.HasSequence("employee_are_workgroup_seq", "hr").HasMin(1L);
        modelBuilder.HasSequence("emsa_id").HasMin(1L);
        modelBuilder.HasSequence("emsa_id", "hr").HasMin(1L);
        modelBuilder.HasSequence("serc_seq");
        modelBuilder.HasSequence("user_address_seq", "users")
            .StartsAt(2L)
            .HasMin(2L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}