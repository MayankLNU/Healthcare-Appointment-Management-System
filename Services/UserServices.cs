using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;

    public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
    }

    public async Task<bool> RegisterUser(User user, string password)
    {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        await _userRepository.AddUserAsync(user);
        return true;
    }

    public async Task<string> AuthenticateUser(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null;
        }

        var roles = new List<string> { user.Role.ToString() };
        return _tokenRepository.CreateJWTToken(new IdentityUser { Email = user.Email }, roles);
    }
}