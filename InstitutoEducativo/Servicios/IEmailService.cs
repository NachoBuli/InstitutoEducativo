using InstitutoEducativo.Models;
using System.Threading.Tasks;

namespace InstitutoEducativo.Servicios
{
    public interface IEmailService
    {
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}