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
    }
}