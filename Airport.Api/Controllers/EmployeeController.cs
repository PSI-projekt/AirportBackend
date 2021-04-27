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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper, IAuthRepository authRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _authRepository = authRepository; 
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(EmployeeForAddDto employeeForAdd)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            if (await _authRepository.UserExists(employeeForAdd.Username)) return BadRequest("Username already exists");

            if (await _authRepository.EmailExists(employeeForAdd.Email)) return BadRequest("Email already registered");

            var employee = _mapper.Map<User>(employeeForAdd);

            employee.Privileges = (int)UserPrivileges.Employee;
            employee.IsConfirmed = true;

            var addedEmployee = await _authRepository.Register(employee, employeeForAdd.Password);

            return addedEmployee != null ? Ok(): StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}
