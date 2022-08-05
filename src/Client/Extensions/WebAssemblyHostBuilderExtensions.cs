using Blazored.LocalStorage;
using DCMS.Web.Infrastructure.Authentication;
using DCMS.Web.Infrastructure.Services;
//using DCMS.Web.Infrastructure.Services.Preferences;
using DCMS.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using DCMS.Web.Infrastructure.Services.ExtendedAttribute;
using DCMS.Domain.ExtendedAttributes;
using DCMS.Domain.Misc;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using DCMS.Shared.Services;
using System.Diagnostics;

namespace DCMS.Client.Extensions
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
            builder
                .Services
                .AddLocalization(options =>
                {
                    options.ResourcesPath = "Resources";
                })
                .AddAuthorizationCore(options =>
                {
                    RegisterPermissionClaims(options);
                })
                .AddBlazoredLocalStorage()
                .AddMudServices(configuration =>
                {
                    configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                    configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                    configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                    configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                    configuration.SnackbarConfiguration.ShowCloseIcon = false;
                })
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ClientPreferenceService>()
                .AddScoped<AppStateProvider>()
                .AddScoped<AuthenticationStateProvider, AppStateProvider>()
                .AddServices()
                .AddExtendedAttributeServices()
                .AddTransient<AuthenticationHeaderHandler>()
                .AddScoped(sp => sp
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient(ClientName).EnableIntercept(sp))
                .AddHttpClient(ClientName, client =>
                {
                    client.DefaultRequestHeaders.AcceptLanguage.Clear();
                    client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
                })
                .AddHttpMessageHandler<AuthenticationHeaderHandler>();
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

                    Debug.WriteLine($"Reg ----------->: {type.Service.FullName}");
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

        /*
         blazor.webassembly.js:1 System.AggregateException: One or more errors occurred. (Cannot provide a value for property '_userService' on type 'DCMS.Client.App'. There is no registered service of type 'DCMS.Web.Infrastructure.Services.Identity.Users.IUserService'.)
 ---> System.InvalidOperationException: Cannot provide a value for property '_userService' on type 'DCMS.Client.App'. There is no registered service of type 'DCMS.Web.Infrastructure.Services.Identity.Users.IUserService'.
   at Microsoft.AspNetCore.Components.ComponentFactory.<>c__DisplayClass7_0.<CreateInitializer>g__Initialize|1(IServiceProvider serviceProvider, IComponent component)
   at Microsoft.AspNetCore.Components.ComponentFactory.PerformPropertyInjection(IServiceProvider serviceProvider, IComponent instance)
   at Microsoft.AspNetCore.Components.ComponentFactory.InstantiateComponent(IServiceProvider serviceProvider, Type componentType)
   at Microsoft.AspNetCore.Components.RenderTree.Renderer.InstantiateComponent(Type componentType)
   at Microsoft.AspNetCore.Components.RenderTree.WebRenderer.AddRootComponent(Type componentType, String domElementSelector)
   at Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer.AddComponentAsync(Type componentType, ParameterView parameters, String domElementSelector)
   at Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHost.<>c.<<RunAsyncCore>b__15_1>d.MoveNext()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Components.WebAssembly.Hosting.WebAssemblyHost.RunAsyncCore(CancellationToken cancellationToken, WebAssemblyCultureProvider cultureProvider)
   at DCMS.Client.Program.Main(String[] args) in D:\Git\DCMS.Blazor\src\Client\Program.cs:line 35
   --- End of inner exception stack trace ---
         */

        public static IServiceCollection AddExtendedAttributeServices(this IServiceCollection services)
        {
            //TODO - add Services with reflection!

            return services
                .AddTransient(typeof(IExtendedAttributeService<int, int, Document, DocumentExtendedAttribute>), typeof(ExtendedAttributeService<int, int, Document, DocumentExtendedAttribute>));
        }

        private static void RegisterPermissionClaims(AuthorizationOptions options)
        {
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                {
                    options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
                }
            }
        }
    }
}