using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AirportBackend.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
        }
    }
}