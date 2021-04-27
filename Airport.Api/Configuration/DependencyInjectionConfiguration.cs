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
            services.AddScoped<IAirplaneRepository, AirplaneRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ISmtpClient, SmtpClient>();
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}