using InstitutoEducativo.Models;
using System.Threading.Tasks;

namespace InstitutoEducativo.Repository
{
    public interface IAccountRepository
    {
        Task GenerateForgotPasswordTokenAsync(Persona user);
        Task<Persona> GetUserByEmailAsync(string email);
  
    }
}