using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain
{
    class UserBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }

        public virtual User User { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
