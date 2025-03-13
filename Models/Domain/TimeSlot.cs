namespace AppointmentManagement.Models.Domain
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date {  get; set; }
        public bool IsAvailable { get; set; } = true;

        // Navigation property
        public Availability? Availability { get; set; }

        // ForeignKeys
        public int DoctorId { get; set; }
    }
}
