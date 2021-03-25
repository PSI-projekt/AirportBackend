using Microsoft.EntityFrameworkCore;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Persistence
{
    public class AirportDbContext : DbContext
    {
        public AirportDbContext(DbContextOptions<AirportDbContext> options) : base(options) {}

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<AirportEntity> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBooking> UserBookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                .HasOne(x => x.Origin)
                .WithMany(x => x.Origins)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(x => x.Destination)
                .WithMany(x => x.Destinations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserBooking>()
                .HasKey(x => new {x.UserId, x.BookingId});
        }
    }
}