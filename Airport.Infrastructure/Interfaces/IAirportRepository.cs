using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IAirportRepository
    {
        Task<bool> Add(AirportEntity airport);
    }
}
