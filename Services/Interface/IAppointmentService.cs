using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AvailableTimeSlotResponseDTO>> GetAvailableTimeSlots(DateOnly date);
        Task<IEnumerable<BookedTimeSlotResponseDTO>> GetBookedTimeSlots(BookedTimeSlotsDTO bookedSlotsDTO);
        Task<BookAppointmentResponseDTO> BookAppointment(BookAppointmentDTO bookAppointmentDTO);
        Task<BookAppointmentResponseDTO> UpdateAppointment(UpdateAppointmentDTO updateAppointmentDTO);
        Task<CancelAppointmentResponseDTO> CancelAppointmentAsync(CancelAppointmentDTO cancelAppointmentDTO);
    }
}