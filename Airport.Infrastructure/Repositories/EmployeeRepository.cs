using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AirportDbContext _context;
        public EmployeeRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Employee employee)
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
        public async Task<int> GetUserRole(int userId)
        {
            try
            {
                return await _context.Users
                    .Where(x => x.Id == userId)
                    .AsNoTracking()
                    .Select(x => x.Privileges)                    
                    .FirstOrDefaultAsync(x => x.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }               
    }
}