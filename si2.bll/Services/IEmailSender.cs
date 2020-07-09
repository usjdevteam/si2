using System.Threading.Tasks;

namespace si2.bll.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
