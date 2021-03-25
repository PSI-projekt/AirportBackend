using System.Threading.Tasks;
using Airport.Domain.Email;

namespace Airport.Infrastructure.Interfaces
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(EmailMessage message);
    }
}