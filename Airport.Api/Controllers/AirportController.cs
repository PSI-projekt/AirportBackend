using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Email.Builders;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AirportBackend.Controllers
{
    [AllowAnonymous] // narazie dałem allow all bo chciałem przetestować ale nie działa i tak
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportEntityRepository _airportEntityRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AirportController(IAirportEntityRepository airportEntityRepository, IConfiguration configuration, IMapper mapper,
    IEmailService emailService)
        {
            _airportEntityRepository = airportEntityRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(AirportForAddDto airportForAdd) 
        {
            var airport = _mapper.Map<AirportEntity>(airportForAdd);

            var newAirport = await _airportEntityRepository.Add(airport);

            if (newAirport == null) return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok();
        }
    }
}
