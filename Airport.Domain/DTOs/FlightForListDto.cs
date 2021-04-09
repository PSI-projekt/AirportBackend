using System;
using Airport.Domain.Models;

namespace Airport.Domain.DTOs
{
    public class FlightForListDto
    {
        public int Id { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string FlightNumber { get; set; }
        public string Status { get; set; }
        public AirportForFlightDto Origin { get; set; }
        public AirportForFlightDto Destination { get; set; }
    }
}