using System;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;

namespace Airport.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AirportDbContext _context;

        public PaymentRepository(AirportDbContext context)
        {
            _context = context;
        }
        
        public async Task<Payment> Add(double price, int passengerCount, int bookingId)
        {
            var payment = new Payment
            {
                Amount = price * passengerCount,
                BookingId = bookingId,
                ReferenceNumber = PaymentIdGenerator.GenerateId(bookingId)
            };

            try
            {
                var paymentResult = _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                
                return paymentResult.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}