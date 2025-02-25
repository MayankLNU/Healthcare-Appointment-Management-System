namespace AppointmentManagement.Models
{
    public class Consultation
    {
        public int ConsultationId {  get; set; }
        public string? Notes { get; set; }
        public string? Prescription { get; set; }

        //Navigation
        public Appointment Appointment { get; set; }

        // ForeignKeys
        public int AppointmentId { get; set; }
    }
}
