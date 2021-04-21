using System;
using System.Linq;

namespace Airport.Infrastructure.Helpers
{
    public static class PaymentIdGenerator
    {
        private static readonly Random Random = new Random();
        public static string GenerateId(int bookingId)
        {
            var time = DateTime.UtcNow;

            return $"P/{time.Year}/{RandomString(10)}/{RandomString(3)}{bookingId}";
        }
        
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}