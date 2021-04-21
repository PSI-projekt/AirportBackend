using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> Add(double price, int passengerCount, int bookingId);
    }
}