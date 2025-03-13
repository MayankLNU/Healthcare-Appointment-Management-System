using System.ComponentModel.DataAnnotations;
using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Models.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }

        [Required]
        public DateTime TimeSlot { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }
    }
}
