using System;
using System.Collections.Generic;

namespace Airport.Domain.DTOs
{
    public class BookingForListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBooking { get; set; }
        public bool IsCancelled { get; set; }
        public int NumberOfPassengers { get; set; }
        public FlightDetailsDto FlightDetails { get; set; }
        public IEnumerable<PassengerForEditDto> Passengers { get; set; }
    }
}
