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
    public class AirplaneController : ControllerBase
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IMapper _mapper;

        public AirplaneController(IAirplaneRepository airplaneRepository, IMapper mapper)
        {
            _airplaneRepository = airplaneRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(AirplaneForAddDto airplaneForAdd)
        {
            var airplane = _mapper.Map<Airplane>(airplaneForAdd);

            var result = await _airplaneRepository.Add(airplane);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);                        
        }
    }
}
