using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repositories.Repository
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly AppointmentManagementDbContext _context;

        public UserCredentialRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddUserCredentialAsync(UserCredential userCredential)
        {
            await _context.userCredentials.AddAsync(userCredential);
            await _context.SaveChangesAsync();
        }

        public async Task<UserCredential> GetCredByEmailAsync(string email)
        {
            return await _context.userCredentials.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
