namespace Airport.Domain.DTOs
{
    public class AirplaneForListDto
    {
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Identifier { get; set; }
        public string Airline { get; set; }
        public bool IsInRepair { get; set; }
        public int LocationId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
