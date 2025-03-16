using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class AvailabilityDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }
    }
}