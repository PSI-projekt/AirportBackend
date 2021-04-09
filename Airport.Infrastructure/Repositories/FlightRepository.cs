using System;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirportDbContext _context;
        public FlightRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Flight flight)
        {
            if (flight == null) return false;

            try
            {
                await _context.Flights.AddAsync(flight);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}