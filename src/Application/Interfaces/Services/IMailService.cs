using DCMS.Application.Requests.Mail;
using System.Threading.Tasks;

namespace DCMS.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}