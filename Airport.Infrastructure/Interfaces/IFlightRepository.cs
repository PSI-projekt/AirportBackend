using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IFlightRepository
    {
        Task<bool> Add(Flight flight);
    }
}