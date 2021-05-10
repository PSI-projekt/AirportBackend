using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Repositories;
using AirportBackend.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AirportBackend.Enums;
using System.Collections.Generic;

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
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository flightRepository, IBookingRepository bookingRepository, 
            IAirplaneRepository airplaneRepository, IAuthRepository authRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _airplaneRepository = airplaneRepository;
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(FlightForAddDto flightForAdd)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() {(int)UserPrivileges.Administrator, (int)UserPrivileges.Employee};

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

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
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _flightRepository.Edit(flightForEdit);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpPatch("status")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateStatus(FlightForStatusChangeDto flightForStatusChange)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _flightRepository.UpdateStatus(flightForStatusChange);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpDelete("{flightId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int flightId)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _flightRepository.Delete(flightId);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpGet ("details/{flightId}")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetDetailsById(int flightId)
        {
            var result = await _flightRepository.GetDetailsById(flightId);

            if (result == null) return BadRequest("Could not find any flight with this ID");

            return result != null ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
        }

    }
}
