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
        public int Origin { get; set; } // tutaj będzie id lotniska
        public int Destination { get; set; } // tutaj będzie id lotniska
        public string FlightNumber { get; set; }
        public string Gate { get; set; }
        public string Status { get; set; }
        public int AirplaneId { get; set; }

        public virtual Airport Airport { get; set; }
        public virtual Airplane Airplane { get; set; }
    }
}
