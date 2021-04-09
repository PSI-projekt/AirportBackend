using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


namespace Airport.Infrastructure.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;

        public FlightRepository(AirportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Add(Flight flight)
        { 
            try
            {
                await _context.Flights.AddAsync(flight);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<PagedList<FlightForListDto>> GetFlights(FlightParameters parameters)
        {
            try
            {
                var flights = _context.Flights.Where(x => x.DateOfArrival > DateTime.UtcNow)
                    .OrderBy(x => x.DateOfDeparture)
                    .Include(x => x.Origin)
                    .Include(x => x.Destination)
                    .ProjectTo<FlightForListDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();

                return await PagedList<FlightForListDto>.CreateAsync(flights, parameters.PageNumber,
                    parameters.PageSize);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}