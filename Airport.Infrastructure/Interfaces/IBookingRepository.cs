using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;

namespace Airport.Infrastructure.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> GetNumberOfPassengersForFlight(int flightId);
        Task<Booking> Add(Booking booking);
        Task<Booking> GetByPaymentReference(string paymentReference);
        Task<bool> Cancel(int bookingId);
        Task<Booking> GetById(int bookingId);
        Task<PagedList<BookingForListDto>> GetBookingsByUserId(int userId, BookingParameters parameters);
    }
}