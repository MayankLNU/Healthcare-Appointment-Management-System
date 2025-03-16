using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is missing.");
        }

        if (userDto.Role != "Doctor" && userDto.Role != "Patient")
        {
            return BadRequest("Invalid User!! Please use Doctor or Patient");
        }

        var result = await _userService.RegisterUser(userDto);
        if (result)
        {
            return Ok(new { Message = "User registered successfully. Please Login!!" });
        }
        return BadRequest(new { Message = "User registration failed" });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
    {
        var token = await _userService.AuthenticateUser(loginDto);
        if (token == null)
        {
            return Unauthorized(new { Message = "Invalid login attempt. Check Email Id and Password!!" });
        }

        return Ok(token);
    }
}