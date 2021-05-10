using Airport.Domain.Models;
using System;

namespace Airport.Domain.DTOs
{
    public class FlightDetailsDto
    {
        public int Id { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public AirportForListDto Origin { get; set; }
        public AirportForListDto Destination { get; set; }
        public string FlightNumber { get; set; }
        public Airplane Airplane { get; set; }
    }
}