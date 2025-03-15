namespace AppointmentManagement.Models.Domain
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly TimeSlot { get; set; }
        public string Status { get; set; }

        //Navigation
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Availability? Availability { get; set; }
        public Consultation? Consultation { get; set; }

        // ForeignKeys
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
