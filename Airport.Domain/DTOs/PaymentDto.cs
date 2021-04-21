namespace Airport.Domain.DTOs
{
    public class PaymentDto
    {
        public string Swift { get; set; }
        public string Iban { get; set; }
        public double Amount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}