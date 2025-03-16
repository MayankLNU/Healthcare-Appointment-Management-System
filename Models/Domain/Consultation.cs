using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Models.Domain
{
    public class Consultation
    {
        [Key]
        public int ConsultationId { get; set; }

        public string? Notes { get; set; }
        public string? Prescription { get; set; }

        // Navigation properties
        public Appointment? Appointment { get; set; }

        // Foreign keys
        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
    }
}