using DCMS.Application.Interfaces.Services;
using DCMS.Data.Mapping.CRM;
using DCMS.Domain.CRM;
using DCMS.Infrastructure.Mapping.CRM;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DCMS.Domain;

namespace DCMS.Infrastructure.Contexts
{
	public class CRMContext : AuditableBaseContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public CRMContext(DbContextOptions<CRMContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }


        public DbSet<CRM_BP> CRM_Bps { get; set; }
        public DbSet<CRM_BUSTAT> CRM_Bustats { get; set; }
        public DbSet<CRM_HEIGHT_CONF> CRM_HeightConfs { get; set; }
        public DbSet<Terminal> CRM_NewTerminals { get; set; }
        public DbSet<CRM_ORG> CRM_Orgs { get; set; }
        public DbSet<CRM_RELATION> CRM_Relations { get; set; }
        public DbSet<CRM_RETURN> CRM_Returns { get; set; }
        public DbSet<Store> CRM_Stores { get; set; }
        public DbSet<Terminal> CRM_Terminals { get; set; }
        public DbSet<CRM_ZSNTM0040> CRM_Zsntm0040 { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //全局租户过滤器
            var tenantId = _currentUserService?.StoreId ?? 0;
            if (tenantId > 0)
            {
                ///modelBuilder.Entity<IAuditableEntity>().HasQueryFilter(a => a.StoreId == tenantId);
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                        modelBuilder.Entity(entityType.ClrType)
                            .AddQueryFilter<IAuditableEntity>(e => e.StoreId == tenantId);
                }
            }



            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }


            #region Map


            modelBuilder.ApplyConfiguration(new StoreMap());
            modelBuilder.ApplyConfiguration(new TerminalMap());
            modelBuilder.ApplyConfiguration(new CRM_RELATIONMap());
            modelBuilder.ApplyConfiguration(new CRM_RETURNMap());
            modelBuilder.ApplyConfiguration(new CRM_ORGMap());
            modelBuilder.ApplyConfiguration(new CRM_BPMap());
            modelBuilder.ApplyConfiguration(new CRM_ZSNTM0040Map());
            modelBuilder.ApplyConfiguration(new CRM_HEIGHT_CONFMap());
            modelBuilder.ApplyConfiguration(new CRM_BUSTATyMap());

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}