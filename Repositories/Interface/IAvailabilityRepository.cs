using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repository.Interface
{
    public interface IAvailabilityRepository
    {
        Task<bool> AddAvailabilityAsync(Availability availability);
        Task<IEnumerable<Availability>> GetAllAvailabilityAsync();
        Task<Availability> GetAvailabilityByIdAsync(int availabilityId);
        Task<bool> UpdateAvailabilityAsync(Availability availability);
        Task DeleteAvailabilityAsync(int availabilityId);
    }
}
