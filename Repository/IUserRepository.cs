using AppointmentManagement.Models;

namespace AppointmentManagement.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
