using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Airport.Domain.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Airport.Infrastructure.Repositories
{
    public class AirplaneRepository : IAirplaneRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;
        public AirplaneRepository(AirportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<AirplaneForListDto>> GetAirplanes()
        {
            try
            {
                return await _context.Airplanes
                    .OrderBy(x => x.Identifier)
                    .ProjectTo<AirplaneForListDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> Edit(AirplaneForEditDto airplaneForEdit)
        {
            var airplane = await GetById(airplaneForEdit.Id);
            try
            {
                _mapper.Map(airplaneForEdit, airplane);
                return await Update(airplane);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        private async Task<bool> Update(Airplane airplane)
        {
            try
            {
                _context.Update(airplane);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<Airplane> GetById(int airplaneId)
        {
            try
            {
                return await _context.Airplanes.FirstOrDefaultAsync(x => x.Id == airplaneId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<bool> Delete(int airplaneId)
        {
            var airplane = await GetById(airplaneId);
            try
            {
                airplane.IsRetired = true;
                return await Update(airplane);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}