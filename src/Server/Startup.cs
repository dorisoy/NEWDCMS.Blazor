using DCMS.Application.Configurations;
using DCMS.Application.Extensions;
using DCMS.Infrastructure.Extensions;
using DCMS.Server.Extensions;
using DCMS.Server.Filters;
//using DCMS.Server.Services.Preferences;
using DCMS.Server.Middlewares;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System;
using System.IO;


namespace DCMS.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		private readonly IConfiguration _configuration;


		public void ConfigureServices(IServiceCollection services)
		{

			//获取数据配置
			services.Configure<DataSettings>(_configuration.GetSection(nameof(DataSettings)));
 
			services.AddCors();
			services.AddSignalR();
			services.AddLocalization(options =>
			{
				options.ResourcesPath = "Resources";
			});
			services.AddCurrentUserService();
			services.AddSerialization();

			//添加数据库上下文并迁移数据
			services.AddDefaultDatabase(_configuration);
			services.AddAndMigrateDatabase(_configuration);

			//TODO - 应该实现ServerStorageProvider才能正常工作!
			services.AddServerStorage(); 

			//services.AddScoped<ServerPreferenceManager>();
			//services.AddServerLocalization();

			services.AddIdentity();

			//JWT
			services.AddJwtAuthentication(services.GetApplicationSettings(_configuration));

			services.AddApplicationLayer();
			services.AddApplicationServices();
			services.AddRepositories();
			services.AddExtendedAttributesUnitOfWork();
			services.AddSharedInfrastructure(_configuration);
			services.RegisterSwagger();
			services.AddInfrastructureMappings();

			//services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
			//services.AddHangfireServer();

			//Hangfire后台作业
			var sContext = _configuration.GetConnectionString("DefaultConnection");
			services.AddHangfire(a => a.UseStorage(new MySqlStorage(sContext,
				new MySqlStorageOptions
				{
					TransactionIsolationLevel =  System.Data.IsolationLevel.ReadCommitted,
					QueuePollInterval = TimeSpan.FromSeconds(15),
					JobExpirationCheckInterval = TimeSpan.FromHours(1),
					CountersAggregateInterval = TimeSpan.FromMinutes(5),
					PrepareSchemaIfNecessary = true,
					DashboardJobListLimit = 50000,
					TransactionTimeout = TimeSpan.FromMinutes(1),
				})));
			services.AddHangfireServer();

			//使用Newtonsoft 替代 System.Text.Json ：
			//The JSON value could not be converted to System.Nullable
			services.AddControllers().AddNewtonsoftJson().AddValidators();
			services.AddExtendedAttributesValidators();
			services.AddExtendedAttributesHandlers();
			services.AddRazorPages();
			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 0);
				config.AssumeDefaultVersionWhenUnspecified = true;
				config.ReportApiVersions = true;
			});
			services.AddLazyCache();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> localizer)
		{
			app.UseCors();
			app.UseExceptionHandling(env);
			app.UseHttpsRedirection();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Files");
			if (!Directory.Exists(filePath))
				Directory.CreateDirectory(filePath);

			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(filePath),
				RequestPath = new PathString("/Files")
			});
			app.UseRequestLocalizationByCulture();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseHangfireDashboard("/jobs", new DashboardOptions
			{
				DashboardTitle = localizer["BlazorDCMS Jobs"],
				Authorization = new[] { new HangfireAuthorizationFilter() }
			});
			app.UseEndpoints();
			app.ConfigureSwagger();

			//初始化配置
			app.Initialize(_configuration);
		}
	}
}