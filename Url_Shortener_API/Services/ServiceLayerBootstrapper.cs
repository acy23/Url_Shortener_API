using Url_Shortener_API.Services.Abstractions;
using Url_Shortener_API.Services.Implementations;

namespace Url_Shortener_API.Services
{
    public static class ServiceLayerBootstrapper
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Service Layer Dependenceny Injections
            services.AddScoped<IUrlService, UrlService>();
            services.AddScoped<IMongoService, MongoService>();

            return services;
        }
    }
}
