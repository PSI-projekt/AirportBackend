using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Repositories;
using AirportBackend.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(UserForDetailsDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPassengersForUser()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();
            
            var result = await _userRepository.GetUserById(userId);

            return result != null
                ? Ok(_mapper.Map<UserForDetailsDto>(result))
                : StatusCode((int) HttpStatusCode.InternalServerError);
        }

        [HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update(UserForEditDto userForEdit)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var loggedUserId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if(loggedUserId != userForEdit.Id)
                if (!privilages.Contains(privilagesId))
                    return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _userRepository.Edit(userForEdit);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPatch("delete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int userId)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var loggedUserId))
                return Unauthorized();
        
            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };
        
            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (loggedUserId != userId)
                if (!privilages.Contains(privilagesId))
                    return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _userRepository.Delete(userId);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}