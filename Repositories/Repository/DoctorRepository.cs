using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Repo
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppointmentManagementDbContext _context;

        public DoctorRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddDoctorAsync(Doctor user)
        {
            await _context.Doctors.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _context.Doctors.ToListAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int userId)
        {
            return await _context.Doctors.FindAsync(userId);
        }

        public async Task<Doctor> GetDoctorByEmailAsync(string email)
        {
            return await _context.Doctors.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateDoctorAsync(Doctor user)
        {
            _context.Doctors.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteDoctorAsync(int userId)
        {
            var user = await _context.Doctors.FindAsync(userId);
            if (user != null)
            {
                _context.Doctors.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}