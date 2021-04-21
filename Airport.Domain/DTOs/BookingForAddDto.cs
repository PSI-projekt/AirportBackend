using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class BookingForAddDto
    {
        [Required]
        public int FlightId { get; set; }
        [Required]
        public IEnumerable<PassengerForBookingDto> Passengers { get; set; }
    }
}