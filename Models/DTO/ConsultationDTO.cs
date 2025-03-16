using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class ConsultationDTO
    {
        [Required]
        public int AppointmentId { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
    }

    public class PatientConsultationDTO
    {
        [Required]
        public int AppointmentId { get; set; }
    }
}
