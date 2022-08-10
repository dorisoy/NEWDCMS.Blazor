using System.Globalization;
using System.Text.Encodings.Web;
using AntDesign;
using AntDesign.JsInterop;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;



namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntDesign(this IServiceCollection services, Action<AntDesignConfiguration> configuration)
        {

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new AntDesignConfiguration();
            configuration(options);


            services.TryAddScoped<DomEventService>();
            services.TryAddTransient((sp) =>
            {
                var domEventService = sp.GetRequiredService<DomEventService>();
                return domEventService.CreateDomEventListerner();
            });

            services.TryAddScoped(sp => new HtmlRenderService(new HtmlRenderer(sp, sp.GetRequiredService<ILoggerFactory>(),
                        s => HtmlEncoder.Default.Encode(s)))
            );

            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            services.TryAddScoped<IconService>();
            services.TryAddScoped<InteropService>();
            services.TryAddScoped<NotificationService>();

            services.AddAntDesignMessage(options);

            //services.TryAddScoped<MessageService>();

            services.TryAddScoped<ModalService>();
            services.TryAddScoped<DrawerService>();
            services.TryAddScoped<ConfirmService>();
            services.TryAddScoped<ImageService>();
            services.TryAddScoped<ConfigService>();


            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

            return services;
        }


        public static IServiceCollection AddAntDesignMessage(this IServiceCollection services, AntDesignConfiguration configuration = null)
        {
            configuration ??= new AntDesignConfiguration();

            services.TryAddScoped<MessageService>();
            services.TryAddScoped<IMessage>(builder =>
                new MessageService(builder.GetService<NavigationManager>(), configuration));

            return services;
        }
    }
}
