using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DCMS.Application.Interfaces.Repositories;
using DCMS.Application.Interfaces.Services.Storage;
using DCMS.Application.Interfaces.Services.Storage.Provider;
using DCMS.Application.Interfaces.Serialization.Serializers;
using DCMS.Application.Serialization.JsonConverters;
using DCMS.Infrastructure.Repositories;
using DCMS.Infrastructure.Services.Storage;
using DCMS.Application.Serialization.Options;
using DCMS.Infrastructure.Services.Storage.Provider;
using DCMS.Application.Serialization.Serializers;

namespace DCMS.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                //.AddTransient<IProductRepository, ProductRepository>()
                //.AddTransient<IBrandRepository, BrandRepository>()
                //.AddTransient<IDocumentRepository, DocumentRepository>()
                //.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddExtendedAttributesUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IExtendedAttributeUnitOfWork<,,>), typeof(ExtendedAttributeUnitOfWork<,,>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}