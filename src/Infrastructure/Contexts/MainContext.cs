using DCMS.Application.Interfaces.Services;
using DCMS.Domain;
using DCMS.Infrastructure.Auth;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DCMS.Infrastructure.Contexts
{
	public class MainContext : AuditableContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public MainContext(DbContextOptions<MainContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
		}

		//public DbSet<ChatHistory<AppUser>> ChatHistories { get; set; }
		//public DbSet<Product> Products { get; set; }
		//public DbSet<Brand> Brands { get; set; }
		//public DbSet<Document> Documents { get; set; }
		//public DbSet<DocumentType> DocumentTypes { get; set; }
		//public DbSet<DocumentExtendedAttribute> DocumentExtendedAttributes { get; set; }


		#region 查询集


		#endregion


		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
		{
			foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						//entry.Entity.CreatedOn = _dateTimeService.NowUtc;
						//entry.Entity.CreatedBy = _currentUserService.UserId;
						break;

					case EntityState.Modified:
						//entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
						//entry.Entity.LastModifiedBy = _currentUserService.UserId;
						break;
				}
			}
			if (_currentUserService.UserId == null)
			{
				return await base.SaveChangesAsync(cancellationToken);
			}
			else
			{
				return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
			}
		}


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

			modelBuilder.ApplyConfiguration(new AppUserMap());
			modelBuilder.ApplyConfiguration(new AppRoleMap());
			modelBuilder.ApplyConfiguration(new UserRoleMap());
			modelBuilder.ApplyConfiguration(new UserClaimMap());
			modelBuilder.ApplyConfiguration(new UserLoginMap());
			modelBuilder.ApplyConfiguration(new AppRoleClaimMap());
			modelBuilder.ApplyConfiguration(new UserTokenMap());


			#region 映射


			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}