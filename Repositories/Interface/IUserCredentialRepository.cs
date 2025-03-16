using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repositories.Interface
{
    public interface IUserCredentialRepository
    {
        Task<bool> AddUserCredentialAsync(UserCredential userCredential);
        Task<UserCredential> GetCredByEmailAsync(string email);
    }
}
