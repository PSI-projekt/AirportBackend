using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain
{
    class Booking
    {
        public int Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public string IsPaid { get; set; } // string czy bool?
        public string FlightId { get; set; }

        public virtual Flight Flight { get; set; }
    }
}
