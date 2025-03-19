using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class AvailableTimeSlotResponseDTO
    {
        public string Message { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }   

    }

    public class BookedTimeSlotResponseDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Message { get; set; }

    }

    public class BookedTimeSlotsDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string DoctorEmail { get; set; }

        [Required]
        public DateOnly Date { get; set; }
    }

    public class RemoveTimeSlotDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string DoctorEmail { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }
    }

    public class RemoveTimeSlotResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
