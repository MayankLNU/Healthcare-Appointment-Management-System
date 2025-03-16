using System.ComponentModel.DataAnnotations;
using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Models.DTO
{
    public class BookAppointmentDTO
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string PatientEmail { get; set; }
    }

    public class BookAppointmentResponseDTO
    {
        public int AppointmentId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public string DoctorName { get; set; }
    }

    public class UpdateAppointmentDTO
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string PatientEmail { get; set; }
    }

    public class CancelAppointmentDTO
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string PatientEmail { get; set; }
    }

    public class CancelAppointmentResponseDTO
    {
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}