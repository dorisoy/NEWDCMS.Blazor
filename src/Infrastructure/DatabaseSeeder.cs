using DCMS.Application.Interfaces.Services;
using DCMS.Infrastructure.Contexts;
using DCMS.Infrastructure.Helpers;
using DCMS.Infrastructure.Models.Identity;
using DCMS.Shared.Constants.Permission;
using DCMS.Shared.Constants.Role;
using DCMS.Shared.Constants.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DCMS.Infrastructure
{
	public class DatabaseSeeder : IDatabaseSeeder
	{
		private readonly ILogger<DatabaseSeeder> _logger;
		private readonly IStringLocalizer<DatabaseSeeder> _localizer;
		private readonly Contexts.MainContext _db;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;

		public DatabaseSeeder(
			UserManager<AppUser> userManager,
			RoleManager<AppRole> roleManager,
			Contexts.MainContext db,
			ILogger<DatabaseSeeder> logger,
			IStringLocalizer<DatabaseSeeder> localizer)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_db = db;
			_logger = logger;
			_localizer = localizer;
		}

		public void Initialize()
		{
			AddAdministrator();
			AddBasicUser();
			_db.SaveChanges();
		}

		private void AddAdministrator()
		{
			Task.Run(async () =>
			{
				//Check if Role Exists
				var adminRole = new AppRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
				var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
				if (adminRoleInDb == null)
				{
					await _roleManager.CreateAsync(adminRole);
					adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
					_logger.LogInformation(_localizer["Seeded Administrator Role."]);
				}
				//Check if User Exists
				var superUser = new AppUser
				{
					Email = "admin@jsdcms.com",
					UserName = "admin",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					CreatedOn = DateTime.Now,
					IsActive = true
				};
				var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
				if (superUserInDb == null)
				{
					await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
					var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
					if (result.Succeeded)
					{
						_logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
					}
					else
					{
						foreach (var error in result.Errors)
						{
							_logger.LogError(error.Description);
						}
					}
				}
				foreach (var permission in Permissions.GetRegisteredPermissions())
				{
					await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
				}
			}).GetAwaiter().GetResult();
		}

		private void AddBasicUser()
		{
			Task.Run(async () =>
			{
				//Check if Role Exists
				var basicRole = new AppRole(RoleConstants.BasicRole, _localizer["Basic role with default permissions"]);
				var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
				if (basicRoleInDb == null)
				{
					await _roleManager.CreateAsync(basicRole);
					_logger.LogInformation(_localizer["Seeded Basic Role."]);
				}
				//Check if User Exists
				var basicUser = new AppUser
				{
					Email = "john@jsdcms.com",
					UserName = "johndoe",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
					CreatedOn = DateTime.Now,
					IsActive = true
				};
				var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
				if (basicUserInDb == null)
				{
					await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
					await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
					_logger.LogInformation(_localizer["Seeded User with Basic Role."]);
				}
			}).GetAwaiter().GetResult();
		}
	}
}