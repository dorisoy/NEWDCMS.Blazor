using DCMS.Application.Configurations;
using DCMS.Application.Interfaces.Serialization.Options;
using DCMS.Application.Interfaces.Serialization.Serializers;
using DCMS.Application.Interfaces.Serialization.Settings;
using DCMS.Application.Interfaces.Services;
using DCMS.Application.Interfaces.Services.Account;
using DCMS.Application.Interfaces.Services.Identity;
using DCMS.Application.Serialization.JsonConverters;
using DCMS.Application.Serialization.Options;
using DCMS.Application.Serialization.Serializers;
using DCMS.Application.Serialization.Settings;
using DCMS.Infrastructure;
using DCMS.Infrastructure.Contexts;
using DCMS.Infrastructure.Models.Identity;
using DCMS.Infrastructure.Services;
using DCMS.Infrastructure.Services.Identity;
using DCMS.Infrastructure.Shared.Services;
using DCMS.Server.Localization;
//using DCMS.Server.Services.Preferences;
using DCMS.Server.Services;
using DCMS.Server.Settings;
using DCMS.Shared.Constants.Application;
//using DCMS.Shared.Constants.Localization;
using DCMS.Shared.Constants.Permission;
using DCMS.Shared.Wrapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DCMS.Server.Extensions
{
	internal static class ServiceCollectionExtensions
	{
		//internal static async Task<IStringLocalizer> GetRegisteredServerLocalizerAsync<T>(this IServiceCollection services) where T : class
		//{
		//	var serviceProvider = services.BuildServiceProvider();

		//	await SetCultureFromServerPreferenceAsync(serviceProvider);
		//	var localizer = serviceProvider.GetService<IStringLocalizer<T>>();
		//	await serviceProvider.DisposeAsync();
		//	return localizer;
		//}

		//private static async Task SetCultureFromServerPreferenceAsync(IServiceProvider serviceProvider)
		//{
		//	var storageService = serviceProvider.GetService<ServerPreferenceManager>();
		//	if (storageService != null)
		//	{
		//		CultureInfo culture;
		//		var preference = await storageService.GetPreference() as ServerPreference;
		//		if (preference != null)
		//			culture = new CultureInfo(preference.LanguageCode);
		//		else
		//			culture = new CultureInfo(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US");
		//		CultureInfo.DefaultThreadCurrentCulture = culture;
		//		CultureInfo.DefaultThreadCurrentUICulture = culture;
		//		CultureInfo.CurrentCulture = culture;
		//		CultureInfo.CurrentUICulture = culture;
		//	}
		//}

		//internal static IServiceCollection AddServerLocalization(this IServiceCollection services)
		//{
		//	services.TryAddTransient(typeof(IStringLocalizer<>), typeof(ServerLocalizer<>));
		//	return services;
		//}

		internal static AppConfiguration GetApplicationSettings(
		   this IServiceCollection services,
		   IConfiguration configuration)
		{
			var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
			services.Configure<AppConfiguration>(applicationSettingsConfiguration);
			return applicationSettingsConfiguration.Get<AppConfiguration>();
		}

		internal static void RegisterSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen( c =>
			{
				//TODO - Lowercase Swagger Documents
				//c.DocumentFilter<LowercaseDocumentFilter>();
				//Refer - https://gist.github.com/rafalkasa/01d5e3b265e5aa075678e0adfd54e23f

				// include all project's xml comments
				var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
				foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (!assembly.IsDynamic)
					{
						var xmlFile = $"{assembly.GetName().Name}.xml";
						var xmlPath = Path.Combine(baseDirectory, xmlFile);
						if (File.Exists(xmlPath))
						{
							c.IncludeXmlComments(xmlPath);
						}
					}
				}

				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "BlazorDCMS",
				});

				//var localizer = await GetRegisteredServerLocalizerAsync<ServerCommonResources>(services);

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer",
							},
							Scheme = "Bearer",
							Name = "Bearer",
							In = ParameterLocation.Header,
						}, new List<string>()
					},
				});
			});
		}

		internal static IServiceCollection AddSerialization(this IServiceCollection services)
		{
			services
				.AddScoped<IJsonSerializerOptions, SystemTextJsonOptions>()
				.Configure<SystemTextJsonOptions>(configureOptions =>
				{
					if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
						configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
				});
			services.AddScoped<IJsonSerializerSettings, NewtonsoftJsonSettings>();

			services.AddScoped<IJsonSerializer, SystemTextJsonSerializer>(); // you can change it
			return services;
		}


		internal static IServiceCollection AddDefaultDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MainContext>(options =>
			{
				var connectionString = configuration.GetConnectionString("DefaultConnection");
				//MSSQL
				//options.UseSqlServer(connectionString);
				//MySQL
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

			}).AddTransient<IDatabaseSeeder, DatabaseSeeder>();

			return services;
		}


		/// <summary>
		/// 添加数据库上下文并迁移数据
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		internal static IServiceCollection AddAndMigrateDatabase(this IServiceCollection services, IConfiguration configuration)
		{
		
			using var scope = services.BuildServiceProvider().CreateScope();
			try
			{
				//批量添加核心业务数据库上下文
				var options = services.GetOptions<DataSettings>(nameof(DataSettings));
				var conns = options.DbConnections;
				if (conns.Any())
				{
					//AddDbContext 生命周期 ServiceLifetime.Transient
					foreach (var conn in conns)
					{
						string connStr = conn.ConnectionString;
						switch (conn.Source.ToUpper())
						{
							case "AUTH_RO":
							case "AUTH_RW":
							{
								services.AddDbContext<AUTHContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "DCMS_RO":
							case "DCMS_RW":
							{
								services.AddDbContext<DCMSContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "CENSUS_RO":
							case "CENSUS_RW":
							{
								services.AddDbContext<CensusContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "SKD_RO":
							case "SKD_RW":
							{
								services.AddDbContext<SKDContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "TSS_RO":
							case "TSS_RW":
							{
								services.AddDbContext<TSSContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "CRM_RO":
							case "CRM_RW":
							{
								services.AddDbContext<CRMContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "CSMS_RO":
							case "CSMS_RW":
							{
								services.AddDbContext<CSMSContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
							case "OCMS_RO":
							case "OCMS_RW":
							{
								services.AddDbContext<OCMSContext>(options =>
								{
									options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
								}, ServiceLifetime.Transient);
							}
							break;
						}

					}

				}
			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "迁移或设定数据库种子时出错!");
				throw;
			}

			return services;
		}


		private static void Migrate(string conn, AuditableContext context)
		{
			context.Database.SetConnectionString(conn);
			if (context.Database.GetMigrations().Count() > 0)
			{
				context.Database.Migrate();
			}
		}

		public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
		{
			using var serviceProvider = services.BuildServiceProvider();
			var configuration = serviceProvider.GetRequiredService<IConfiguration>();
			var section = configuration.GetSection(sectionName);
			var options = new T();
			section.Bind(options);
			return options;
		}


		//private void SetupDatabaseContext(IServiceCollection services)
		//{
		//    var connectionString = Configuration.GetConnectionString("dbConnection");

		//    //生命周期ServiceLifetime.Transient
		//    services.AddDbContext<EmployeeContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);
		//}


		internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<ICurrentUserService, CurrentUserService>();
			return services;
		}

		internal static IServiceCollection AddIdentity(this IServiceCollection services)
		{
			services
				.AddIdentity<AppUser, AppRole>(options =>
				{
					options.Password.RequiredLength = 6;
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.User.RequireUniqueEmail = true;
				})
				.AddEntityFrameworkStores<MainContext>()
				.AddDefaultTokenProviders();

			return services;
		}

		internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient<IDateTimeService, SystemDateTimeService>();
			services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
			services.AddTransient<IMailService, SMTPMailService>();
			return services;
		}

		internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddTransient<IRoleClaimService, RoleClaimService>();
			services.AddTransient<ITokenService, IdentityService>();
			services.AddTransient<IRoleService, RoleService>();
			services.AddTransient<IAccountService, AccountService>();
			services.AddTransient<IUserService, UserService>();
			//services.AddTransient<IChatService, ChatService>();
			services.AddTransient<IUploadService, UploadService>();
			services.AddTransient<IAuditService, AuditService>();
			services.AddScoped<IExcelService, ExcelService>();
			return services;
		}

		internal static IServiceCollection AddJwtAuthentication(
			this IServiceCollection services, AppConfiguration config)
		{
			var key = Encoding.ASCII.GetBytes(config.Secret);
			services
				.AddAuthentication(authentication =>
				{
					authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer( bearer =>
				{
					bearer.RequireHttpsMetadata = false;
					bearer.SaveToken = true;
					bearer.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false,
						RoleClaimType = ClaimTypes.Role,
						ClockSkew = TimeSpan.Zero
					};

					//var localizer = await GetRegisteredServerLocalizerAsync<ServerCommonResources>(services);

					bearer.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							var accessToken = context.Request.Query["access_token"];

							// If the request is for our hub...
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrEmpty(accessToken) &&
								(path.StartsWithSegments(ApplicationConstants.SignalR.HubUrl)))
							{
								// Read the token out of the query string
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						},
						OnAuthenticationFailed = c =>
						{
							if (c.Exception is SecurityTokenExpiredException)
							{
								c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
								c.Response.ContentType = "application/json";
								var result = JsonConvert.SerializeObject(Result.Fail("The Token is expired."));
								return c.Response.WriteAsync(result);
							}
							else
							{
#if DEBUG
								c.NoResult();
								c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
								c.Response.ContentType = "text/plain";
								return c.Response.WriteAsync(c.Exception.ToString());
#else
								c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
								c.Response.ContentType = "application/json";
								var result = JsonConvert.SerializeObject(Result.Fail("发生了一个未经处理的错误"));
								return c.Response.WriteAsync(result);
#endif
							}
						},
						OnChallenge = context =>
						{
							context.HandleResponse();
							if (!context.Response.HasStarted)
							{
								context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
								context.Response.ContentType = "application/json";
								var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized."));
								return context.Response.WriteAsync(result);
							}

							return Task.CompletedTask;
						},
						OnForbidden = context =>
						{
							context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
							context.Response.ContentType = "application/json";
							var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource."));
							return context.Response.WriteAsync(result);
						},
					};
				});
			services.AddAuthorization(options =>
			{
				// Here I stored necessary permissions/roles in a constant
				foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
				{
					var propertyValue = prop.GetValue(null);
					if (propertyValue is not null)
					{
						options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
					}
				}
			});
			return services;
		}
	}
}