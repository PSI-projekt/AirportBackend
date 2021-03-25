using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Domain.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        [Encrypted] public string FirstName { get; set; }
        [Encrypted] public string LastName { get; set; }
        public string Country { get; set; }
        [Encrypted] public string City { get; set; }
        [Encrypted] public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostCode { get; set; }
        [Encrypted] public string IDNumber { get; set; }
        public string UserBookingId { get; set; }
        public UserBooking UserBooking { get; set; }
    }
}
