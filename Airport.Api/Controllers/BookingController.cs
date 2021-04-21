using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Airport.Domain.DTOs;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using AirportBackend.Configuration;
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
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IPassengerRepository passengerRepository,
            IAirplaneRepository airplaneRepository, IPaymentRepository paymentRepository, 
            IFlightRepository flightRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
            _airplaneRepository = airplaneRepository;
            _paymentRepository = paymentRepository;
            _flightRepository = flightRepository;
            _mapper = mapper;
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

            var result = await _bookingRepository.Add(userId, bookingForAdd.FlightId, bookingForAdd.Passengers.Count());

            if (result == null) return BadRequest("There was an error while processing Your request.");

            var passengers = _mapper.Map<IEnumerable<Passenger>>(bookingForAdd.Passengers).ToList();

            foreach (var passenger in passengers)
            {
                passenger.UserBookingId = result.Id;
            }

            var resultPassengers = await _passengerRepository.AddPassengers(passengers);
            
            if (!resultPassengers) return BadRequest("There was an error while processing Your request.");

            var flight = await _flightRepository.GetById(bookingForAdd.FlightId);
            
            if (flight == null) return BadRequest("There was an error while processing Your request.");

            var paymentResult = await _paymentRepository
                .Add(flight.PricePerPassenger, bookingForAdd.Passengers.Count(), result.Booking.Id);
            
            if (paymentResult == null) return BadRequest("There was an error while processing Your request.");

            var paymentDto = _mapper.Map<PaymentDto>(paymentResult);

            paymentDto.Iban = Constants.Iban;
            paymentDto.Swift = Constants.Swift;

            return Ok(paymentDto);
        }
    }
}