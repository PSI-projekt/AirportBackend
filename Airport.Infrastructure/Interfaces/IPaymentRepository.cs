using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> Add(double price, int passengerCount, int bookingId);
        Task<bool> Confirm(string referenceNumber);
        Task<Payment> GetByReferenceNumber(string referenceNumber);
    }
}