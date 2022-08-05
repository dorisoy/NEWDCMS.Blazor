using DCMS.Application.Interfaces.Services;
using DCMS.Infrastructure.Mapping.CSMS;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DCMS.Domain;



namespace DCMS.Infrastructure.Contexts
{
	public class CSMSContext : AuditableBaseContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public CSMSContext(DbContextOptions<CSMSContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
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


			#region Map

			modelBuilder.ApplyConfiguration(new TerminalSignReportMap());
			modelBuilder.ApplyConfiguration(new OrderDetailMap());

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}