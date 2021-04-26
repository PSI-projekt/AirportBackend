using System.Threading.Tasks;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPassengersForUser(int userId)
        {
            var result = await _passengerRepository.GetPassengersForUser(userId);

            return Ok(result);
        }
    }
}