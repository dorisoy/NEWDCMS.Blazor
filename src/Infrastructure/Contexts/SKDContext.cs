using DCMS.Application.Interfaces.Services;
using DCMS.Domain.SKD;
using DCMS.Infrastructure.Mapping.SKD;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DCMS.Domain;

namespace DCMS.Infrastructure.Contexts
{
	public class SKDContext : AuditableBaseContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;

		public SKDContext(DbContextOptions<SKDContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
		}


		public DbSet<EmailAccount> EmailAccount { get; set; }
		public DbSet<QueuedEmail> QueuedEmail { get; set; }
		public DbSet<QueuedMessage> QueuedMessage { get; set; }
		public DbSet<ScheduleTask> ScheduleTask { get; set; }


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


			#region 映射

			modelBuilder.ApplyConfiguration(new QueuedMessageMap());
			modelBuilder.ApplyConfiguration(new EmailAccountMap());
			modelBuilder.ApplyConfiguration(new QueuedEmailMap());
			modelBuilder.ApplyConfiguration(new ScheduleTaskMap());


			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}