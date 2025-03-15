using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Repositories.Interface
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDate(DateOnly date);
        Task<TimeSlot> GetAvailableTimeSlotsByDateTimeAndDrId(DateOnly date, TimeOnly startTime, int doctorId);
        Task UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task UpdateTimeSlotAvailabilityAsync(DateOnly dateOnly, TimeOnly startTime, int doctorId, bool isAvailable);
    }
}
