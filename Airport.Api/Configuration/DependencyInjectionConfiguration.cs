using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Repositories;
using Airport.Infrastructure.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;

namespace AirportBackend.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();

            services.AddScoped<ISmtpClient, SmtpClient>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}