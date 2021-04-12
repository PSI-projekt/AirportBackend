using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;
using Airport.Infrastructure.Interfaces;
using AirportBackend.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository flightRepository, IBookingRepository bookingRepository, 
            IAirplaneRepository airplaneRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _airplaneRepository = airplaneRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(FlightForAddDto flightForAdd)
        {
            // TODO: Check if user has privileges

            var flight = _mapper.Map<Flight>(flightForAdd);

            var result = await _flightRepository.Add(flight);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedList<FlightForListDto>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetFlights([FromQuery] FlightParameters parameters)
        {
            var flights = await _flightRepository.GetFlights(parameters);

            if (flights == null || !flights.Any()) return BadRequest("Could not find any flights");

            Response.AddPaginationHeader(flights.CurrentPage, flights.PageSize, flights.TotalCount, flights.TotalPages);

            return Ok(flights);
        }
        
        [HttpGet("seats/{flightId}")]
        [ProducesResponseType(typeof(SeatCountForFlightDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAvailableSeats(int flightId)
        {
            var passengerCount = await _bookingRepository.GetNumberOfPassengersForFlight(flightId);

            if (passengerCount < 0) return StatusCode((int) HttpStatusCode.InternalServerError);

            var seatCount = await _airplaneRepository.GetNumberOfSeatsForFlight(flightId);
            
            if (seatCount < 0) return StatusCode((int) HttpStatusCode.InternalServerError);

            var dto = new SeatCountForFlightDto
            {
                FlightId = flightId,
                SeatCount = seatCount - passengerCount
            };

            return Ok(dto);        
        }
        
        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(FlightForEditDto flightForEdit)
        {
            // TODO: Check if user has privileges

            var result = await _flightRepository.Edit(flightForEdit);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpPatch("status")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateStatus(FlightForStatusChangeDto flightForStatusChange)
        {
            // TODO: Check if user has privileges

            var result = await _flightRepository.UpdateStatus(flightForStatusChange);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpDelete("{flightId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int flightId)
        {
            // TODO: Check if user has privileges
            
            var result = await _flightRepository.Delete(flightId);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
