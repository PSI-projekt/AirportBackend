using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Email.Builders;
using Airport.Domain.Models;
using Airport.Domain.PDF.Builders;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Helpers.PaginationParameters;
using Airport.Infrastructure.Interfaces;
using AirportBackend.Configuration;
using AirportBackend.Enums;
using AirportBackend.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirportBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public BookingController(IBookingRepository bookingRepository, IPassengerRepository passengerRepository,
            IAirplaneRepository airplaneRepository, IPaymentRepository paymentRepository, 
            IFlightRepository flightRepository, IUserRepository userRepository, IMapper mapper, IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
            _airplaneRepository = airplaneRepository;
            _paymentRepository = paymentRepository;
            _flightRepository = flightRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentDto), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(BookingForAddDto bookingForAdd)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var numberOfPassengers = await _bookingRepository.GetNumberOfPassengersForFlight(bookingForAdd.FlightId);

            var availableSeats = 
                await _airplaneRepository.GetNumberOfSeatsForFlight(bookingForAdd.FlightId) - numberOfPassengers;

            if (bookingForAdd.Passengers.Count() > availableSeats)
                return BadRequest("There are no more seats available for this flight.");
                
            var bookingToAdd = new Booking
            {
                UserId = userId,
                FlightId = bookingForAdd.FlightId,
                NumberOfPassengers = bookingForAdd.Passengers.Count(),
                DateOfBooking = DateTime.UtcNow,
                IsCancelled = false
            };

            var bookingResult = await _bookingRepository.Add(bookingToAdd);

            if (bookingResult == null) return BadRequest("There was an error while processing Your request.");
            
            var newPassengers = new List<Passenger>();
            var passengersToUpdate = new List<Passenger>();

            foreach (var dto in bookingForAdd.Passengers)
            {
                var passengerResult = await _passengerRepository.GetPassenger(dto);

                if (passengerResult == null)
                {
                    newPassengers.Add(_mapper.Map<Passenger>(dto));
                    continue;
                }

                var passengerToUpdate = _mapper.Map<Passenger>(dto);
                passengerToUpdate.Id = passengerResult.Id;
                passengersToUpdate.Add(passengerToUpdate);
            }

            var added = await _passengerRepository.AddPassengers(newPassengers);
            var updated = await _passengerRepository.UpdatePassengers(passengersToUpdate);
            
            if (added == null || updated == null) return BadRequest("There was an error while processing Your request.");

            var passengerBookings = added.Select(passenger => new PassengerBooking
                {BookingId = bookingResult.Id, PassengerId = passenger.Id,}).ToList();
            passengerBookings.AddRange(updated.Select(passenger => new PassengerBooking
                {BookingId = bookingResult.Id, PassengerId = passenger.Id}));

            var passengerBookingResult = await _passengerRepository.AddPassengerBookings(passengerBookings);
            
            if (!passengerBookingResult) return BadRequest("There was an error while processing Your request.");

            var flight = await _flightRepository.GetById(bookingForAdd.FlightId);
            
            if (flight == null) return BadRequest("There was an error while processing Your request.");

            var paymentResult = await _paymentRepository
                .Add(flight.PricePerPassenger, bookingForAdd.Passengers.Count(), bookingResult.Id);
            
            if (paymentResult == null) return BadRequest("There was an error while processing Your request.");

            var paymentDto = _mapper.Map<PaymentDto>(paymentResult);

            paymentDto.Iban = Constants.Iban;
            paymentDto.Swift = Constants.Swift;

            var user = await _userRepository.GetUserById(userId);

            if (user == null) return Ok("There was an error while processing Your request.");

            var flightDetails = _flightRepository.GetDetailsById(bookingForAdd.FlightId);

            if (flightDetails == null) return Ok("There was an error while processing Your request.");

            var emailResult = await SendEmail(user.Email, user.Username,
                paymentDto.ReferenceNumber, paymentDto.Iban, paymentDto.Swift, paymentDto.Amount, flightDetails.Result);

            if (!emailResult) return Ok("There was an error while processing Your request.");

            return Ok(paymentDto);
        }
        
        [HttpPatch("cancel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Cancel(int bookingId)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var _))
                return Unauthorized();

            var privilages = new List<int>() { (int)UserPrivileges.Administrator, (int)UserPrivileges.Employee };

            int.TryParse(User.FindFirst(ClaimTypes.Role)?.Value, out var privilagesId);

            if (!privilages.Contains(privilagesId))
                return StatusCode((int)HttpStatusCode.Unauthorized);

            var result = await _bookingRepository.Cancel(bookingId);

            return result ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        [HttpPatch("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Edit(BookingForEditDto bookingForEdit)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var booking = await _bookingRepository.GetById(bookingForEdit.Id);

            if (booking == null) return BadRequest("There was an error while processing Your request.");

            if (booking.UserId != userId) return Unauthorized();

            var passengersToUpdate = _mapper.Map<List<Passenger>>(bookingForEdit.Passengers);

            var passengersForBooking = await _passengerRepository.GetPassengersForBooking(bookingForEdit.Id, userId);

            var passengersId = new List<int>();

            foreach (var passenger in passengersForBooking)
            {
                passengersId.Add(passenger.Id);
            }
            foreach (var passenger in passengersToUpdate)
            {
                if (!passengersId.Contains(passenger.Id))
                    return Unauthorized();
            }
            var updated = await _passengerRepository.UpdatePassengers(passengersToUpdate);
            
            return updated != null ? Ok() : BadRequest("There was an error while processing Your request.");
        }

        [HttpGet("pdf/{bookingId}")]
        [ProducesResponseType(typeof(File), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetTicket(int bookingId)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var booking = await _bookingRepository.GetById(bookingId);

            if (booking == null) return BadRequest("Booking does not exist.");

            if (booking.UserId != userId) return Unauthorized();

            var flight = await _flightRepository.GetDetailsById(booking.FlightId);
            
            if (flight == null) return BadRequest("There was an error while processing Your request.");

            var passengers = await _passengerRepository.GetPassengersForBooking(bookingId, userId);
            
            if (passengers == null) return BadRequest("There was an error while processing Your request.");

            var user = await _userRepository.GetUserById(userId);
            
            if (user == null) return BadRequest("There was an error while processing Your request.");

            var pdf = TicketPdfBuilder.BuildTicketPdf(passengers, booking, flight, user);

            return pdf != null && pdf.Length > 0
                ? File(pdf, "application/pdf", $"Ticket{booking.Id}.pdf")
                : StatusCode((int) HttpStatusCode.InternalServerError);
        }

        private async Task<bool> SendEmail(string receiver, string username,
            string referenceNumber, string iban, string bic, double amount, FlightDetailsDto flightDetails)
        {
            var message = PaymentInformationEmailBuilder
                .BuildPaymentInformationMessage(receiver, username, referenceNumber, iban, bic, amount, flightDetails);

            return await _emailService.SendEmail(message);
        }

        [HttpGet("bookings")]
        [ProducesResponseType(typeof(PagedList<BookingForListDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetBookingsByUserId([FromQuery] BookingParameters parameters)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty, out var userId))
                return Unauthorized();

            var bookings = await _bookingRepository.GetBookingsByUserId(userId, parameters);

            if (bookings == null) return BadRequest("Could not find any bookings for this user");

            foreach (var booking in bookings)
            {
                var flightDetails = await _flightRepository.GetDetailsById(booking.FlightId);

                if (flightDetails == null) return BadRequest("Could not find any flight for this booking");

                booking.FlightDetails = flightDetails;
                
                var passengers = await _passengerRepository.GetPassengersForBooking(booking.Id, userId);

                if (passengers == null) return BadRequest("Could not find any passengers for this booking");

                booking.Passengers = passengers;
            }

            Response.AddPaginationHeader(bookings.CurrentPage, bookings.PageSize, bookings.TotalCount,
                bookings.TotalPages);

            return Ok(bookings);
        }
    }
}