namespace AppointmentManagement.Models
{
    public class Availability
    {
        public int DoctorId { get; set; }
        public DateTime Date {  get; set; }

        //Navigation
        public List<Appointment> DoctorAppointment { get; set; } = new List<Appointment> ();
        public List<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();

    }
}
