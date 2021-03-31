using System;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class AirportEntityRepository : IAirportEntityRepository
    {
        private readonly AirportDbContext _context;
        public AirportEntityRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<AirportEntity> Add(AirportEntity airport)
        {
            if (airport == null) return null;

            try
            {
                await _context.Airports.AddAsync(airport);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return airport;
        }
    }
}
