using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class AvailableTimeSlotResponseDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }   

    }
}
