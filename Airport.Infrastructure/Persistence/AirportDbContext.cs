using System;
using Microsoft.EntityFrameworkCore;
using Airport.Domain.Models;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;

namespace Airport.Infrastructure.Persistence
{
    public class AirportDbContext : DbContext
    {
        private readonly IEncryptionProvider _provider;
        
        public AirportDbContext(DbContextOptions<AirportDbContext> options, IConfiguration configuration) :
            base(options)
        {
            var encryptionKey = Convert.FromBase64String(configuration.GetSection("AppSettings:EncryptionKey").Value);
            _provider = new AesProvider(encryptionKey);
        }

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<AirportEntity> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);
            
            modelBuilder.Entity<Flight>()
                .HasOne(x => x.Origin)
                .WithMany(x => x.Origins)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(x => x.Destination)
                .WithMany(x => x.Destinations)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}