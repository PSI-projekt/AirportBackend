using System.ComponentModel.DataAnnotations;

namespace Airport.Domain.DTOs
{
    public class AirplaneForAddDto
    {
        [Required]
        public string Maker { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Identifier { get; set; }
        [Required]
        public string Airline { get; set; }
        [Required]
        public bool IsInRepair { get; set; }
        [Required]
        public int LocationId { get; set; }        
    }
}
