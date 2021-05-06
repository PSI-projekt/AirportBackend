using System;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Helpers;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> Confirm(string referenceNumber)
        {
            var payment = await GetByReferenceNumber(referenceNumber);

            if (payment == null) return false;

            payment.IsPaid = true;

            return await Update(payment);
        }

        private async Task<Payment> GetByReferenceNumber(string referenceNumber)
        {
            try
            {
                return await _context.Payments.FirstOrDefaultAsync(x => x.ReferenceNumber == referenceNumber);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private async Task<bool> Update(Payment payment)
        {
            try
            {
                _context.Payments.Update(payment);

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}