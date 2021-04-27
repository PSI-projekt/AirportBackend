using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string IDNumber { get; set; }
        
        public IEnumerable<PassengerBooking> PassengerBookings { get; set; }
    }
}
