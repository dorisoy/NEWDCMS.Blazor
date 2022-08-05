using DCMS.Application.Interfaces.Services;
using DCMS.Domain;
using DCMS.Domain.Auth;
using DCMS.Infrastructure.Mapping.Auth;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace DCMS.Infrastructure.Contexts
{
	public class AUTHContext : AuditableContext
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IDateTimeService _dateTimeService;



		public AUTHContext(DbContextOptions<AUTHContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
			: base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
		}


		#region 查询集

		//public DbSet<AclRecord> AclRecord { get; set; }
		//public DbSet<ActivityLog> ActivityLog { get; set; }
		//public DbSet<ActivityLogType> ActivityLogType { get; set; }
		//public DbSet<Branch> Branch { get; set; }
		//public DbSet<DataChannelPermission> DataChannelPermissions { get; set; }
		//public DbSet<Log> Log { get; set; }
		//public DbSet<Module> Module { get; set; }
		//public DbSet<ModuleRoleMap> Module_Role_Mapping { get; set; }
		//public DbSet<PermissionRecord> PermissionRecord { get; set; }
		//public DbSet<PermissionRecordRoles> PermissionRecord_Role_Mapping { get; set; }
		//public DbSet<PrivateMessage> PrivateMessage { get; set; }
		//public DbSet<RefreshToken> RefreshToken { get; set; }
		//public DbSet<UserDistricts> UserDistricts { get; set; }
		//public DbSet<UserGroup> UserGroup { get; set; }
		//public DbSet<UserGroupUserRole> UserGroup_UserRoles_Mapping { get; set; }
		//public DbSet<UserGroupUser> UserGroup_Users_Mapping { get; set; }
		//public DbSet<UserPassword> UserPassword { get; set; }
		//public DbSet<UserRole> UserRoles { get; set; }
		//public DbSet<UserUserRole> User_UserRole_Mapping { get; set; }
		//public DbSet<User> Users { get; set; }


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

			#region 查询视图


			#endregion


			#region 映射

			modelBuilder.ApplyConfiguration(new AclRecordMap());
			modelBuilder.ApplyConfiguration(new ActivityLogMap());
			modelBuilder.ApplyConfiguration(new ActivityLogTypeMap());
			modelBuilder.ApplyConfiguration(new BranchMap());
			modelBuilder.ApplyConfiguration(new DataChannelPermissionMap());
			modelBuilder.ApplyConfiguration(new LogMap());
			modelBuilder.ApplyConfiguration(new ModuleMap());
			modelBuilder.ApplyConfiguration(new ModuleRoleMap());
			modelBuilder.ApplyConfiguration(new PermissionRecordMap());
			modelBuilder.ApplyConfiguration(new PermissionRecordRolesMap());
			modelBuilder.ApplyConfiguration(new PrivateMessageMap());
			modelBuilder.ApplyConfiguration(new RefreshTokenMap());
			modelBuilder.ApplyConfiguration(new UserDistrictsMap());
			modelBuilder.ApplyConfiguration(new UserGroupMap());
			modelBuilder.ApplyConfiguration(new UserGroupUserMap());
			modelBuilder.ApplyConfiguration(new UserGroupUserRoleMap());
			modelBuilder.ApplyConfiguration(new UserPasswordMap());
			modelBuilder.ApplyConfiguration(new UserRoleMapV3());
			modelBuilder.ApplyConfiguration(new UserUserRoleMap());
			modelBuilder.ApplyConfiguration(new UserMapV3());

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}