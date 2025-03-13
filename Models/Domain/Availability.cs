namespace AppointmentManagement.Models.Domain
{
    public class Availability
    {
        public int DoctorId { get; set; }
        public DateOnly Date { get; set; }

        //Navigation
        public List<Appointment>? DoctorAppointment { get; set; } = [];
        public List<TimeSlot> TimeSlots { get; set; } = [];

        // Method to generate time slots
        public void GenerateTimeSlots(TimeOnly startTime, TimeOnly endTime)
        {
            TimeOnly currentTime = startTime;

            while (currentTime < endTime)
            {
                TimeOnly slotEndTime = currentTime.AddMinutes(30);
                if (slotEndTime > endTime)
                {
                    break;
                }
                TimeSlots.Add(new TimeSlot
                {
                    StartTime = currentTime,
                    EndTime = slotEndTime,
                    DoctorId = this.DoctorId,
                    Date = this.Date
                });
                currentTime = slotEndTime;
            }
        }
    }
}
