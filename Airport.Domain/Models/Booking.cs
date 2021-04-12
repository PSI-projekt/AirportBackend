using System;
using System.Collections.Generic;

namespace Airport.Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime DateOfBooking { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public virtual IEnumerable<UserBooking> UserBookings { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
    }
}
