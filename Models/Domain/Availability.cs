using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Models.Domain
{
    public class Availability
    {
        [Key]
        public int DoctorId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        // Navigation properties
        public List<Appointment>? DoctorAppointment { get; set; } = [];
        public List<TimeSlot> TimeSlots { get; set; } = [];
    }
}