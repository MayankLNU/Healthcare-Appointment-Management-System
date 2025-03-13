using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repository
{
    public interface IAvailabilityRepository
    {
        Task AddAvailability(Availability availability);
        Task<Availability> GetAvailabilityByIdAsync(int availabilityId);
        Task<IEnumerable<Availability>> GetAllAvailabilityAsync();
        Task UpdateAvailabilityAsync(Availability availability);
        Task DeleteAvailabilityAsync(int availabilityId);
    }
}
