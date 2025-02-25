namespace AppointmentManagement.Models
{
    public enum Status
    {
        Booked, Cancelled, Completed
    }
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime TimeSlot { get; set; }
        public Status Status { get; set; }

        //Navigation
        public User Patient { get; set; }
        public User Doctor { get; set; }
        public Availability Availability { get; set; }
        public Consultation Consultation { get; set; }

        // ForeignKeys
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
