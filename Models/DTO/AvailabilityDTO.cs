using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class AvailabilityDTO
    {
        public string Email { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
