using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BookingRepository : IBookingRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(AirportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<int> GetNumberOfPassengersForFlight(int flightId)
        {
            try
            {
                return await _context.Bookings
                    .Where(x => x.FlightId == flightId && x.IsCancelled != true)
                    .AsNoTracking()
                    .Select(x => x.NumberOfPassengers)
                    .SumAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public async Task<Booking> Add(Booking booking)
        {
            try
            {
                var result = await _context.Bookings.AddAsync(booking);

                await _context.SaveChangesAsync();

                return result.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<Booking> GetByPaymentReference(string paymentReference)
        {
            try
            {
                return await _context.Payments
                    .Where(x => x.ReferenceNumber == paymentReference)
                    .Include(x => x.Booking.Flight.Origin)
                    .Include(x => x.Booking.Flight.Destination)
                    .Select(x => x.Booking)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> Cancel(int bookingId) 
        {
            var booking = await GetById(bookingId);
            try
            {
                booking.IsCancelled = true;
                return await Update(booking);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public async Task<Booking> GetById(int bookingId)
        {
            try
            {
                return await _context.Bookings.FirstOrDefaultAsync(x => x.Id == bookingId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        private async Task<bool> Update(Booking booking)
        {
            try
            {
                _context.Update(booking);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<PagedList<BookingForListDto>> GetBookingsByUserId(int userId, BookingParameters parameters)
        {
            try
            {
                var bookings = _context.Bookings
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.DateOfBooking)
                    .ProjectTo<BookingForListDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();

                return await PagedList<BookingForListDto>.CreateAsync(bookings, parameters.PageNumber,
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