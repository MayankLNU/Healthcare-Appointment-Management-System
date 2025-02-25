namespace AppointmentManagement.Models
{
    // Created a UserRole Datatype for Role
    public enum UserRole
    {
        Doctor, Patient
    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public UserRole Role {  get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //Navigation
        public List<Appointment> PatientAppointments { get; set; } = new List<Appointment>();
        public List<Appointment> DoctorAppointments { get; set; } = new List<Appointment>();
    }
}
