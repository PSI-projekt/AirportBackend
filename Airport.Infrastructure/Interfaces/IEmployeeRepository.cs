using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<User> Add(User employee, string password);
    }
}