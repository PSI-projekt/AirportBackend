using Airport.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class BookingForEditDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public IEnumerable<PassengerForEditDto> Passengers { get; set; }
    }
}