using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Models.Domain
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly TimeSlot { get; set; }

        [Required]
        [StringLength(10)]
        public string Status { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Availability? Availability { get; set; }
        public Consultation? Consultation { get; set; }

        // Foreign keys
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
    }
}