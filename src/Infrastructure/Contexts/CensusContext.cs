using DCMS.Application.Interfaces.Services;
using DCMS.Domain.Census;
using DCMS.Infrastructure.Mapping;
using DCMS.Infrastructure.Mapping.Census;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DCMS.Domain;

namespace DCMS.Infrastructure.Contexts
{
	public class CensusContext : AuditableBaseContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public CensusContext(DbContextOptions<CensusContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
		}


		public DbSet<DisplayPhoto> DisplayPhoto { get; set; }
		public DbSet<DoorheadPhoto> DoorheadPhoto { get; set; }
		public DbSet<Tracking> Tracking { get; set; }

		#region 查询集

		public DbSet<ReachQuery> ReachQueries { get; set; }
		public DbSet<ReachOnlineQuery> ReachOnlineQueries { get; set; }

		#endregion

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


			#region 视图

			modelBuilder.Entity<ReachQuery>().HasNoKey().ToView("ReachQueries");
			modelBuilder.Entity<ReachOnlineQuery>().HasNoKey().ToView("ReachOnlineQueries");

			#endregion


			#region Map

			modelBuilder.ApplyConfiguration(new TrackingMap());
			modelBuilder.ApplyConfiguration(new DisplayPhotoMap());
			modelBuilder.ApplyConfiguration(new DoorheadPhotoMap());


			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}