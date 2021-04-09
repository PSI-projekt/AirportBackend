using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
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
        private readonly IMapper _mapper;

        public FlightController(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(FlightForAddDto flightForAdd)
        {
            var flight = _mapper.Map<Flight>(flightForAdd);

            var result = await _flightRepository.Add(flight);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int) HttpStatusCode.OK)]
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
    }
}
