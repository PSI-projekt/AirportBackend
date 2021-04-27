using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
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
    public class AirportController : ControllerBase
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public AirportController(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(AirportForAddDto airportForAdd) 
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var airport = _mapper.Map<AirportEntity>(airportForAdd);

            var result = await _airportRepository.Add(airport);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("count")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AirportCountDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetNumberOfAirports()
        {
            var result = await _airportRepository.GetNumberOfAirports();

            return result switch
            {
                < 0 => StatusCode((int) HttpStatusCode.InternalServerError),
                0 => BadRequest("Could not find any airports"),
                _ => Ok(new AirportCountDto { NumberOfAirports = result })
            };
        }
    }
}
