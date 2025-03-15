using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Repo
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly AppointmentManagementDbContext _context;

        public AvailabilityRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task AddAvailabilityAsync(Availability availability)
        {
            await _context.Availabilities.AddAsync(availability);
            await _context.SaveChangesAsync();
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
        public async Task UpdateAvailabilityAsync(Availability availability)
        {
            _context.Availabilities.Update(availability);
            await _context.SaveChangesAsync();
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