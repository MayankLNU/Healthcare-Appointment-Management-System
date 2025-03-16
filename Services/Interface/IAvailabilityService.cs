using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IAvailabilityService
    {
        Task<bool> AddAvailabilityAsync(AvailabilityDTO availabilityDTO);
        Task<bool> RemoveTimeSlotAsync(RemoveTimeSlotDTO removeTimeSlotDTO);
    }
}
