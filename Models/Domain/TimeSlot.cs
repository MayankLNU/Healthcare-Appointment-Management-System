using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Models.Domain
{
    public class TimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public Availability? Availability { get; set; }

        // Foreign keys
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
    }
}