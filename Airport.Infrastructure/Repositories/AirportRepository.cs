﻿using System;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportDbContext _context;
        public AirportRepository(AirportDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(AirportEntity airport)
        {
            try
            {
                await _context.Airports.AddAsync(airport);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<int> GetNumberOfAirports()
        {
            try
            {
                return await _context.Airports.CountAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }
    }
}
