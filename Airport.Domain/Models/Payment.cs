namespace Airport.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsPaid { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}