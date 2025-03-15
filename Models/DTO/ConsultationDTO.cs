namespace AppointmentManagement.Models.DTO
{
    public class ConsultationDTO
    {
        public int AppointmentId { get; set; }
        public string Prescription { get; set; }
        public string Notes { get; set; }
    }

    public class PatientConsultationDTO
    {
        public int AppointmentId { get; set; }
    }
}
