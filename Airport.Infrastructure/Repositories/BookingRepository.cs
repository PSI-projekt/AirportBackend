using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AirportDbContext _context;

        public BookingRepository(AirportDbContext context)
        {
            _context = context;
        }
        
        public async Task<int> GetNumberOfPassengersForFlight(int flightId)
        {
            try
            {
                return await _context.Bookings
                    .Where(x => x.FlightId == flightId)
                    .AsNoTracking()
                    .Select(x => x.NumberOfPassengers)
                    .SumAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public async Task<Booking> Add(Booking booking)
        {
            try
            {
                var result = await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}