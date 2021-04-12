using System;
using System.Collections.Generic;

namespace Airport.Domain.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public string FlightNumber { get; set; }
        public string Gate { get; set; }
        public string Status { get; set; }
        public double PricePerPassenger { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
        public AirportEntity Origin { get; set; }
        public AirportEntity Destination { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; }
    }
}
