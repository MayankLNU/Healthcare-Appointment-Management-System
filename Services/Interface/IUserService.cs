using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserDTO user);
        Task<string> AuthenticateUser(LoginDTO loginDto);
    }
}
