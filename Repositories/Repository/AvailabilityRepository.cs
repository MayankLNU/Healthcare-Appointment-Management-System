using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repositories.Repository
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly AppointmentManagementDbContext _context;

        public AvailabilityRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<bool> AddAvailabilityAsync(Availability availability)
        {
            if (availability != null)
            {
                await _context.Availabilities.AddAsync(availability);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Read
        public async Task<IEnumerable<Availability>> GetAllAvailabilityAsync()
        {
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<Availability> GetAvailabilityByIdAsync(int availabilityId)
        {
            return await _context.Availabilities.FirstOrDefaultAsync(q => q.DoctorId == availabilityId);
        }

        // Update
        public async Task<bool> UpdateAvailabilityAsync(Availability availability)
        {
            if (availability != null)
            {
                _context.Availabilities.Update(availability);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Delete
        public async Task DeleteAvailabilityAsync(int availabilityId)
        {
            var availability = await _context.Availabilities.FindAsync(availabilityId);
            if (availability != null)
            {
                _context.Availabilities.Remove(availability);
                await _context.SaveChangesAsync();
            }
        }
    }
}