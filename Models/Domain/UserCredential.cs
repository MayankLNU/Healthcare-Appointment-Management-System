using System;

namespace AppointmentManagement.Models.Domain
{
    public class UserCredential
    {
        public int CredID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }

        // Navigation property
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

        // ForeignKeys
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }
    }
}
