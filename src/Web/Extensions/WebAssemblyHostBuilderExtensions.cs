using Blazored.LocalStorage;
using DCMS.Web.Infrastructure.Authentication;
using DCMS.Web.Infrastructure.Services;
using DCMS.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using DCMS.Web.Infrastructure.Services.ExtendedAttribute;
using DCMS.Domain.ExtendedAttributes;
using DCMS.Domain.Misc;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using ProLayout;
using DCMS.Shared.Services;

namespace DCMS.Web.Extensions
{
	public static class WebAssemblyHostBuilderExtensions
	{
		private const string ClientName = "DCMS.API";

		public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
		{
			builder.RootComponents.Add<App>("#app");
			return builder;
		}

		public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
		{

			var proSettings = builder.Configuration.GetSection("ProSettings");

			//身份认证
            builder.Services.AddAuthorizationCore(options =>
            {
				//JWT 鉴权
                RegisterPermissionClaims(options);
            });


            //本地存储
            builder.Services.AddBlazoredLocalStorage();

            //添加AntDesign
            builder.Services.AddAntDesign(configuration =>
            {
                //消息配置
                configuration.MessageConfig.Content = "";
                configuration.MessageConfig.Duration = 100;

                //全局配置
                configuration.MessageGlobalConfig.Top = 100;
                configuration.MessageGlobalConfig.Duration = 2;
                configuration.MessageGlobalConfig.MaxCount = 3;
                configuration.MessageGlobalConfig.Rtl = true;

            });

            //AutoMapper映射
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // 偏好设置
            //builder.Services.AddScoped<ClientPreferenceService>()

            //状态提供器
            builder.Services.AddScoped<AppStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, AppStateProvider>();

            //添加IService 服务
            builder.Services.AddServices();


            //扩展属性管理器
            //builder.Services.AddExtendedAttributeManagers()


            //认证处理
            builder.Services.AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);

                    //builder.HostEnvironment.BaseAddress
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();

            //配置动态布局
            builder.Services.Configure<ProSettings>(proSettings);

            //HTTPClient截取器
            builder.Services.AddHttpClientInterceptor();

            return builder;
		}

		/// <summary>
		/// 注入服务
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddServices(this IServiceCollection services)
		{

			var refServices = typeof(IService);

			var types = refServices
				.Assembly
				.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(t => new
				{
					Service = t.GetInterface($"I{t.Name}"),
					Implementation = t
				})
				.Where(t => t.Service != null);

			foreach (var type in types)
			{
				if (refServices.IsAssignableFrom(type.Service))
				{

					Console.WriteLine($"Reg ----------->: {type.Service.FullName}");
					services.AddTransient(type.Service, type.Implementation);
				}
			}

			return services;
		}


		//public static IServiceCollection AddExtendedAttributeManagers(this IServiceCollection services)
		//{
		//	//TODO - add managers with reflection!

		//	return services
		//		.AddTransient(typeof(IExtendedAttributeService<int, int, Document, DocumentExtendedAttribute>), typeof(ExtendedAttributeService<int, int, Document, DocumentExtendedAttribute>));
		//}

		private static void RegisterPermissionClaims(AuthorizationOptions options)
		{
			foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
			{
				var propertyValue = prop.GetValue(null);
				if (propertyValue is not null)
				{
					options.AddPolicy(propertyValue?.ToString() ?? "", policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue?.ToString() ?? ""));
				}
			}
		}
	}
}