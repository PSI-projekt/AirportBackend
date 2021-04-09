using System.Net;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var airport = _mapper.Map<AirportEntity>(airportForAdd);

            var result = await _airportRepository.Add(airport);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("count")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(int), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetNumberOfAirports()
        {
            var result = await _airportRepository.GetNumberOfAirports();

            return result switch
            {
                < 0 => StatusCode((int) HttpStatusCode.InternalServerError),
                0 => BadRequest("Could not find any airports"),
                _ => Ok(result)
            };
        }
    }
}
