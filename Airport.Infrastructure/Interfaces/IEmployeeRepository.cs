using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<bool> Add(User employee);
        Task<int> GetUserRole(int userId);
    }
}