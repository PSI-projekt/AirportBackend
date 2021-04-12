using System.Collections.Generic;

namespace Airport.Domain.Models
{
    public class UserBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public int NumberOfPassengers { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }

        public virtual IEnumerable<Passenger> Passengers { get; set; }
    }
}
