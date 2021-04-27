using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IBookingRepository
    {
        Task<int> GetNumberOfPassengersForFlight(int flightId);
        Task<Booking> Add(Booking booking);
    }
}