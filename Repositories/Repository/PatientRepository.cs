using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Repo
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppointmentManagementDbContext _context;

        public PatientRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddPatientAsync(Patient user)
        {
            await _context.Patients.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Read
        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int userId)
        {
            return await _context.Patients.FindAsync(userId);
        }

        public async Task<Patient> GetPatientByEmailAsync(string email)
        {
            return await _context.Patients.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdatePatientAsync(Patient user)
        {
            _context.Patients.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeletePatientAsync(int userId)
        {
            var user = await _context.Patients.FindAsync(userId);
            if (user != null)
            {
                _context.Patients.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}