using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Repositories.Interface
{
    public interface ITimeSlotRepository
    {
        Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDate(DateOnly date);
        Task<TimeSlot> GetAvailableTimeSlotsByDateTimeAndDrId(DateOnly date, TimeOnly startTime, int doctorId);
        Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsByDateAndDrId(DateOnly date, int doctorId);
        Task<bool> UpdateTimeSlotAsync(TimeSlot timeSlot);
        Task<bool> UpdateTimeSlotAvailabilityAsync(DateOnly dateOnly, TimeOnly startTime, int doctorId, bool isAvailable);
        Task DeleteTimeSlotAsync(int timeSlotId);
    }
}
