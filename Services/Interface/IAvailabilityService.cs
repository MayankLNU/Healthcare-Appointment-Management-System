using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IAvailabilityService
    {
        Task<AvailabilityResponseDTO> AddAvailabilityAsync(AvailabilityDTO availabilityDTO);
        Task<RemoveTimeSlotResponseDTO> RemoveTimeSlotAsync(RemoveTimeSlotDTO removeTimeSlotDTO);
        Task<IEnumerable<AvailableTimeSlotResponseDTO>> GetAvailableTimeSlots(DateOnly date);
        Task<IEnumerable<BookedTimeSlotResponseDTO>> GetBookedTimeSlots(BookedTimeSlotsDTO bookedSlotsDTO);
    }
}
