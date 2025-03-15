namespace AppointmentManagement.Models.Domain
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation properties
        public List<Appointment>? Appointments { get; set; } = [];
        public UserCredential? UserCredential { get; set; }
    }
}
