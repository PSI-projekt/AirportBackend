using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IPassengerRepository
    {
        Task<bool> AddPassengers(IEnumerable<Passenger> passengers);
    }
}