using System.Collections.Generic;

namespace Airport.Domain.Models
{
    public class PassengerBooking
    {
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        public Passenger Passenger { get; set; }
        public Booking Booking { get; set; }
    }
}
