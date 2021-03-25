﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.Models
{
    class UserBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }

        public virtual IEnumerable<Passenger> Passengers { get; set; }
    }
}
