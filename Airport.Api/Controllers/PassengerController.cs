using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerRepository _passengerRepository;

        public PassengerController(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PassengerForListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPassengersForUser()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();
            
            var result = await _passengerRepository.GetPassengersForUser(userId);

            return result != null ? Ok(result) : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpGet("{bookingId}")]
        [ProducesResponseType(typeof(IEnumerable<PassengerForListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPassengersForBooking(int bookingId)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();
            
            var result = await _passengerRepository.GetPassengersForBooking(bookingId, userId);

            return result != null && result.Any() ? Ok(result) : BadRequest();
        }
    }
}