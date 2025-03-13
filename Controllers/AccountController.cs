using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is missing.");
        }

        var user = new User
        {
            Email = userDto.Email,
            Name = userDto.Name,
            PhoneNumber = userDto.PhoneNumber
        };
        if (userDto.Role == UserRole.Doctor)
        {
            user.Role = UserRole.Doctor;
        }
        else if (userDto.Role == UserRole.Patient)
        {
            user.Role = UserRole.Patient;
        }
        else
        {
            return BadRequest(new { Message = "Invalid User!! Use Doctor or Patient" });
        }

        var result = await _userService.RegisterUser(user, userDto.Password);
        if (result)
        {
            return Ok(new { Message = "User registered successfully. Please Login!!" });
        }

        return BadRequest(new { Message = "User registration failed" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var token = await _userService.AuthenticateUser(loginDto.Email, loginDto.Password);
        if (token == null)
        {
            return Unauthorized(new { Message = "Invalid login attempt. Check Email Id and Password!!" });
        }

        return Ok(new { JwtToken = token });
    }
}