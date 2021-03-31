using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class AirportForAddDto
    {
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
