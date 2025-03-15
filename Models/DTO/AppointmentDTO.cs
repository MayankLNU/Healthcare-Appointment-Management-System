using System.ComponentModel.DataAnnotations;
using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Models.DTO
{
    public class BookAppointmentDTO
    {
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public int DoctorId { get; set; }
        public string PatientEmail { get; set; }
    }

    public class BookAppointmentResponseDTO
    {
        public int AppointmentId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public string DoctorName { get; set; }
    }

    public class UpdateAppointmentDTO
    {
        public int AppointmentId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public int DoctorId { get; set; }
        public string PatientEmail { get; set; }
    }

    public class CancelAppointmentDTO
    {
        public int AppointmentId { get; set; }
        public string PatientEmail { get; set; }
    }

    public class CancelAppointmentResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
