using System.ComponentModel.DataAnnotations;
using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }

    public class UserResponseDTO
    {
        public string Email { get; set; }

        public string Role { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email must end with @gmail.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class JWTTokenResponse
    {
        public string JwtToken { get; set; }
    }
}