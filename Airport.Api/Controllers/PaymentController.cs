using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Email.Builders;
using Airport.Infrastructure.Interfaces;
using AirportBackend.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IEmailService _emailService;

        public PaymentController(IPaymentRepository paymentRepository, IBookingRepository bookingRepository,
            IUserRepository userRepository, IFlightRepository flightRepository, IEmailService emailService)
        {
            _paymentRepository = paymentRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _flightRepository = flightRepository;
            _emailService = emailService;
        }

        [HttpPatch]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Confirm(PaymentReferenceDto dto)
        {
            var privileges = new List<int> { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilegesId);

            if (!privileges.Contains(privilegesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var payment = await _paymentRepository.GetByReferenceNumber(dto.ReferenceNumber);

            if (payment == null) return BadRequest("This payment does not exist");

            if (payment.IsPaid == true) return BadRequest("This payment is already confirmed");

            var result = await _paymentRepository.Confirm(dto.ReferenceNumber);
            
            if (!result) return StatusCode((int)HttpStatusCode.InternalServerError);

            var booking = await _bookingRepository.GetByPaymentReference(dto.ReferenceNumber);
            
            if (booking == null) return Ok("Payment confirmed, but confirmation email not sent!");

            // var flight = await _flightRepository.GetById(booking.FlightId);

            var user = await _userRepository.GetUserById(booking.UserId);

            if (user == null) return Ok("Payment confirmed, but confirmation email not sent!");

            var emailResult = await SendEmail(user.Email, user.Username, booking.Flight.Origin.City,
                booking.Flight.Destination.City);
            
            if (!emailResult) return Ok("Payment confirmed, but confirmation email not sent!");

            return Ok();
        }
        
        
        private async Task<bool> SendEmail(string receiver, string username, string origin, string destination)
        {
            var message = PaymentConfirmationEmailBuilder.BuildPaymentConfirmationMessage(receiver, username, origin, destination);
            
            return await _emailService.SendEmail(message);
        }
    }
}