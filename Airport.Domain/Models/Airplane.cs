using System.Collections.Generic;

namespace Airport.Domain.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Identifier { get; set; }
        public string Airline { get; set; }
        public bool IsInRepair { get; set; }
        public int LocationId { get; set; }      
        public AirportEntity Location { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsRetired { get; set; }

        public virtual IEnumerable<Flight> Flights { get; set; }
    }
}
