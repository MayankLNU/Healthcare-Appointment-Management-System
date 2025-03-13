using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class AvailabilityDTO
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public List<TimeSlotDTO> TimeSlots { get; set; }
    }
}
