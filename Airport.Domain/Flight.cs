using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain
{
    class Flight
    {
        public int Id { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public DateTime DateOfArrival { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public string FlightNumber { get; set; }
        public string Gate { get; set; }
        public string Status { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
        public Airport Origin { get; set; }
        public Airport Destination { get; set; }

        public virtual IEnumerable<Booking> Bookings { get; set; } // tutaj też to dać? :)
    }
}
