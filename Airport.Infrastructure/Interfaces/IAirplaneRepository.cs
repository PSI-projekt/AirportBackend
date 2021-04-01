using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IAirplaneRepository
    {
        Task<bool> Add(Airplane airplane);
    }
}