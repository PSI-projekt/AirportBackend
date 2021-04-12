using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly AirportDbContext _context;
        public AirplaneRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Airplane airplane)
        {
            try
            {
                await _context.Airplanes.AddAsync(airplane);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<int> GetNumberOfSeatsForFlight(int flightId)
        {
            try
            {
                return await _context.Flights
                    .Where(x => x.Id == flightId)
                    .AsNoTracking()
                    .Select(x => x.Airplane.NumberOfSeats)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}