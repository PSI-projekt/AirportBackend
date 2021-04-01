using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Persistence.DbSeed
{
    public class Seed
    {
        private readonly AirportDbContext _context;
        public Seed(AirportDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            SeedUsers();
            SeedAirplanes();
        }
        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private void SeedUsers()
        {
            if (!_context.Users.Any())
            {
                var userData = File
                    .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/UserData.json");
                var users = JsonSerializer.Deserialize<List<User>>(userData);
                foreach (var user in users)
                {
                    CreatePasswordHash("password", out var passwordHash, out var passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.IsConfirmed = true;
                    _context.Users.Add(user);
                }
                _context.SaveChanges();
            }
        }
    }

    private void SeedAirplanes()
    {
        if (!_context.Airplanes.Any())
        {
            var airplaneData = File
                .ReadAllText("../Airport.Infrastructure/Persistence/DbSeed/AirplaneData.json");
            var airplanes = JsonSerializer.Deserialize<List<Airplane>>(airportData);
            foreach (var airplane in airplanes)
            {
                airplane.IsInRepair = false;
                _context.Airplanes.Add(airplane);
            }
            _context.SaveChanges();
        }
    }
}