using System;
using System.Threading.Tasks;
using DCMS.Infrastructure.Contexts;
using DCMS.Server.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DCMS.Application.Configurations;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace DCMS.Server
{
	public class Program
	{

		public async static Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
				var configuration = serviceProvider.GetRequiredService<IConfiguration>();
				var section = configuration.GetSection(nameof(DataSettings));
				var options = new DataSettings();
				section.Bind(options);

				try
				{
					//是否启用迁移
					if (options.ApplyDbMigrationsOnStartup)
					{
						//迁移主数据
						var config = serviceProvider.GetRequiredService<IConfiguration>();
						var context = serviceProvider.GetRequiredService<MainContext>();
						if (context.Database.IsMySql())
						{
							context.Database.Migrate();
						}

						//批量迁移主业务(主库)
						var conns = options.DbConnections;
						if (conns.Any())
						{
							foreach (var conn in conns)
							{
								string connStr = conn.ConnectionString;
								var soucrce = conn.Source.ToUpper();
								switch (soucrce)
								{
									case "AUTH_RW":
									{
										//迁移核心业务数据
										var _context = scope.ServiceProvider.GetRequiredService<AUTHContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "DCMS_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<DCMSContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "CENSUS_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<CensusContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "SKD_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<SKDContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "TSS_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<TSSContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "CRM_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<CRMContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "CSMS_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<CSMSContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
									case "OCMS_RW":
									{
										var _context = scope.ServiceProvider.GetRequiredService<OCMSContext>();
										await MigrateAsync(connStr, _context, logger);
									}
									break;
								}

							}
						}
					}
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "迁移或设定数据库种子时出错!");
					throw;
				}
			}

			//异步启动
			await host.RunAsync();
		}

		private static async Task MigrateAsync(string conn, DbContext context, ILogger<Program> logger)
		{
			var db = context.Database;
			db.SetConnectionString(conn);
			logger.LogInformation($"来自：{conn}");
			logger.LogInformation($"开始迁移：{context}");
			if (db.IsMySql())
			{
				if (db.GetMigrations().Count() == 0)
				{
					logger.LogInformation($"创建中...");
					await db.EnsureCreatedAsync();
				}
				else
				{
					logger.LogInformation($"迁移中...");
					await db.MigrateAsync();
				}
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStaticWebAssets();
					webBuilder.UseStartup<Startup>();
				});
	}
}