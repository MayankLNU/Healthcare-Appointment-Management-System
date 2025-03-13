using AppointmentManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repository
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly AppointmentManagementDbContext _context;
        public AvailabilityRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        //Create
        public async Task AddAvailability(Availability availability)
        {
            await _context.Availabilities.AddAsync(availability);
            await _context.SaveChangesAsync();
        }

        //Read
        public async Task<IEnumerable<Availability>> GetAllAvailabilityAsync()
        {
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<Availability> GetAvailabilityByIdAsync(int availabilityId)
        {
            return await _context.Availabilities.FindAsync(availabilityId);
        }

        //Update
        public async Task UpdateAvailabilityAsync(Availability availability)
        {
            _context.Availabilities.Update(availability);
            await _context.SaveChangesAsync();
        }

        //Delete
        public async Task DeleteAvailabilityAsync(int availabilityId)
        {
            var availablity = await _context.Availabilities.FindAsync(availabilityId);
            if (availablity != null)
            {
                _context.Availabilities.Remove(availablity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
