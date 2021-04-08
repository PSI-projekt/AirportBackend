using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class FlightForAddDto
    {
        [Required]
        public DateTime DateOfDeparture { get; set; }
        [Required]
        public DateTime DateOfArrival { get; set; }
        [Required]
        public int OriginId { get; set; }
        [Required]
        public int DestinationId { get; set; }
        [Required]
        public string FlightNumber { get; set; }
        [Required]
        public string Gate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int AirplaneId { get; set; }
    }
}