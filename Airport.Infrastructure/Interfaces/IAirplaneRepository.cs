using System.Threading.Tasks;
using Airport.Domain.Models;
using System.Collections.Generic;
using Airport.Domain.DTOs;

namespace Airport.Infrastructure.Interfaces
{
    public interface IAirplaneRepository
    {
        Task<bool> Add(Airplane airplane);
        Task<int> GetNumberOfSeatsForFlight(int flightId);
        Task<List<AirplaneForListDto>> GetAirplanes();

    }
}