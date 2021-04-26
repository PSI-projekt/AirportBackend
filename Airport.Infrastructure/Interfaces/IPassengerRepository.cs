using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IPassengerRepository
    {
        Task<List<Passenger>> AddPassengers(IEnumerable<Passenger> passengers);
        Task<List<Passenger>> UpdatePassengers(IEnumerable<Passenger> passengers);
        Task<Passenger> GetPassenger(PassengerForBookingDto dto);
        Task<List<PassengerForListDto>> GetPassengersForUser(int userId);
        Task<bool> AddPassengerBookings(IEnumerable<PassengerBooking> passengerBookings);
    }
}