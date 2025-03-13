using System.ComponentModel.DataAnnotations;

namespace AppointmentManagement.Models.DTO
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; } // Doctor or Patient
    }
}
