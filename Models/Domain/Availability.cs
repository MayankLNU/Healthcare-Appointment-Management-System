namespace AppointmentManagement.Models.Domain
{
    public class Availability
    {
        public int DoctorId { get; set; }
        public DateOnly Date { get; set; }

        //Navigation
        public List<Appointment>? DoctorAppointment { get; set; } = [];
        public List<TimeSlot> TimeSlots { get; set; } = [];
    }
}
