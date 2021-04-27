using System;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;

namespace Airport.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AirportDbContext _context;
        public EmployeeRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(User employee, string password)
        {
            try
            {
                await _context.Users.AddAsync(employee);
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