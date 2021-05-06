using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> GetNumberOfPassengersForFlight(int flightId);
        Task<Booking> Add(Booking booking);
        Task<Booking> GetByPaymentReference(string paymentReference);
        Task<bool> Cancel(int bookingId);
        Task<Booking> GetById(int bookingId);
    }
}