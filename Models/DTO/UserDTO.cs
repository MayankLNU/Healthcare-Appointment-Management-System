using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Models.DTO
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
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
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string JwtToken { get; set; }
    }
}