using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class ConsultationDTO
    {
        public int ConsultationId { get; set; }

        public string Notes { get; set; }

        public string Prescription { get; set; }

        [Required]
        public int AppointmentId { get; set; }
    }
}
