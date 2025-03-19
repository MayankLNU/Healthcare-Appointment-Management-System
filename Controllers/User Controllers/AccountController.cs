using System.Security.Authentication;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Services.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IUserService userService, ILogger<AccountController> logger)
    {
        _userService = userService;
        _logger = logger;
    }



    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserDTO userDTO)
    {
        try
        {
            var result = await _userService.RegisterUser(userDTO);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while registering user.");
            return StatusCode(500, "Internal server error. Please try again after some time.");
        }
    }



    [HttpPost("Login")]
    public async Task<IActionResult> AuthenticateUser([FromBody] LoginDTO loginDto)
    {
        try
        {
            var response = await _userService.AuthenticateUser(loginDto);
            if (response.UserId == null)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
            
        }
        catch (AuthenticationException ex)
        {
            _logger.LogWarning(ex, "Authentication failed: {Message}", ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during authentication.");
            return StatusCode(500, "Internal server error");
        }
    }
}