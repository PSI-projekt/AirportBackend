﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Airport.Domain.Models;
using Airport.Domain.DTOs;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Airport.Infrastructure.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;
        public AirportRepository(AirportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
        public async Task<List<AirportForListDto>> GetAirports() 
        {
            try
            {
                return await _context.Airports
                    .OrderBy(x => x.Name)
                    .ProjectTo<AirportForListDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> Edit(AirportForEditDto airportForEdit)
        {
            var airport = await GetById(airportForEdit.Id);
            try
            {
                _mapper.Map(airportForEdit, airport);
                return await Update(airport);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        private async Task<bool> Update(AirportEntity airport)
        {
            try
            {
                _context.Update(airport);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<AirportEntity> GetById(int airportId)
        {
            try
            {
                return await _context.Airports.FirstOrDefaultAsync(x => x.Id == airportId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
