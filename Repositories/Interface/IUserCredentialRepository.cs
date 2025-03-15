using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repositories.Interface
{
    public interface IUserCredentialRepository
    {
        Task AddUserCredentialAsync(UserCredential userCredential);
        Task<UserCredential> GetCredByEmailAsync(string email);
    }
}
