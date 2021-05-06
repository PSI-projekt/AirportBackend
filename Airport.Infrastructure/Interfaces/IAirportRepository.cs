using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Domain.DTOs;

namespace Airport.Infrastructure.Interfaces
{
    public interface IAirportRepository
    {
        Task<bool> Add(AirportEntity airport);
        Task<int> GetNumberOfAirports();
        Task<List<AirportForListDto>> GetAirports();
        Task<bool> Edit(AirportForEditDto airportForEdit);
    }
}
