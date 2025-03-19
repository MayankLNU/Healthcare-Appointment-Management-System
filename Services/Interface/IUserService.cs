using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IUserService
    {
        Task<UserNewAccountResponse> RegisterUser(UserDTO user);
        Task<AuthenticateUserResponse> AuthenticateUser(LoginDTO loginDto);
    }
}
