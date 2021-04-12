using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;

namespace Airport.Infrastructure.Interfaces
{
    public interface IFlightRepository
    {
        Task<bool> Add(Flight flight);
        Task<PagedList<FlightForListDto>> GetFlights(FlightParameters parameters);
        Task<bool> Edit(FlightForEditDto flightForEdit);
        Task<bool> UpdateStatus(FlightForStatusChangeDto flightForStatusChange);
        Task<bool> Delete(int flightId);
    }
}