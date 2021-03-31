using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IAirportEntityRepository
    {
        Task<AirportEntity> Add(AirportEntity airport);
    }
}
