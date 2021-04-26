using System;
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

            var bookingToAdd = new Booking
            {
                UserId = userId,
                FlightId = bookingForAdd.FlightId,
                NumberOfPassengers = bookingForAdd.Passengers.Count(),
                DateOfBooking = DateTime.UtcNow,
                IsAccepted = false
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

            return Ok(paymentDto);
        }
    }
}