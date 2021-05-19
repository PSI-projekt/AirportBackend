using System.Threading.Tasks;
using Airport.Domain.Models;

namespace Airport.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userId);
        Task<bool> Edit(UserForEditDto userForEdit);
        Task<bool> Delete(int userId);
    }
}