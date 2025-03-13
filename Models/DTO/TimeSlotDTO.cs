using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class TimeSlotDTO
    {
        public int TimeSlotId { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int DoctorId { get; set; }
    }
}
