using System;
using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class AirportForEditDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string CodeIATA { get; set; }
    }
}