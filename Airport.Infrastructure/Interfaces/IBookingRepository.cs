using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> GetNumberOfPassengersForFlight(int flightId);
        Task<Booking> Add(Booking booking);
        Task<bool> Cancel(int bookingId);
        Task<Booking> GetById(int bookingId);
        Task<bool> Edit(BookingForEditDto bookingForEdit);
    }
}