using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Airport.Infrastructure.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;

        public PassengerRepository(AirportDbContext context, IMapper _mapper)
        {
            _context = context;
            this._mapper = _mapper;
        }
        
        public async Task<List<Passenger>> AddPassengers(IEnumerable<Passenger> passengers)
        {
            try
            {
                var entities = passengers.Select(passenger => _context.Passengers.Add(passenger).Entity).ToList();
                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<List<Passenger>> UpdatePassengers(IEnumerable<Passenger> passengers)
        {
            try
            {
                var entities = passengers.Select(passenger => _context.Passengers.Update(passenger)).Select(result => result.Entity).ToList();
                
                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Passenger> GetPassenger(PassengerForBookingDto dto)
        {
            return await _context.Passengers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IDNumber == dto.IDNumber && x.Country == dto.Country);
        }

        public async Task<List<PassengerForListDto>> GetPassengersForUser(int userId)
        {
            try
            {
                return await _context.PassengerBookings
                    .Where(x => x.Booking.UserId == userId)
                    .Select(x => x.Passenger)
                    .ProjectTo<PassengerForListDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> AddPassengerBookings(IEnumerable<PassengerBooking> passengerBookings)
        {
            try
            {
                _context.PassengerBookings.AddRange(passengerBookings);
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