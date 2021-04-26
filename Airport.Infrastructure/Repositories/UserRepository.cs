using System;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirportDbContext _context;

        public UserRepository(AirportDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}