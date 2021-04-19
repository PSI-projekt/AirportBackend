using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;

namespace Airport.Infrastructure.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly AirportDbContext _context;

        public PassengerRepository(AirportDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddPassengers(IEnumerable<Passenger> passengers)
        {
            try
            {
                _context.Passengers.AddRange(passengers);
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