using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagement.Models.Domain
{
    public class UserCredential
    {
        [Key]
        public int CredID { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(10)]
        public string Role { get; set; }

        // Navigation properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }

        // Foreign keys
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }

        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
    }
}