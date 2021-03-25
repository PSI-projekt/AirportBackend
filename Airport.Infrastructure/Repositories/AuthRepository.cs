using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AirportDbContext _context;

        public AuthRepository(AirportDbContext context)
        {
            _context = context;
        }
        
        public async Task<User> Register(User user, string password)
        {
            if (user == null || password == null || password == string.Empty) return null;
            
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            User user;
            
            try
            {
                user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            if (user == null) return null;

            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? user : null;
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _context.Users.AnyAsync(x => x.Username == username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return true;
            }
        }

        public async Task<bool> EmailExists(string email)
        {
            try
            {
                return await _context.Users.AnyAsync(x => x.Email == email);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return true;
            }
        }
        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return !computedHash.Where((t, i) => t != passwordHash[i]).Any();
        }
    }
}