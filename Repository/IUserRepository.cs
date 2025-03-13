using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}