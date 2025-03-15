using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IAvailabilityRepository _availabilityRepository;
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly ITokenRepository _tokenRepository;

    public UserService(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IAvailabilityRepository availabilityRepository, IUserCredentialRepository userCredentialRepository, ITokenRepository tokenRepository)
    {
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _availabilityRepository = availabilityRepository;
        _userCredentialRepository = userCredentialRepository;
        _tokenRepository = tokenRepository;
    }

    public async Task<bool> RegisterUser(UserDTO userDTO)
    {
        if (userDTO.Role == "Doctor")
        {
            var user = new Doctor
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                PhoneNumber = userDTO.PhoneNumber,
                Role = userDTO.Role,
            };

            var availability = new Availability
            {
                Date = DateOnly.FromDateTime(DateTime.Now)
            };

            await _doctorRepository.AddDoctorAsync(user);
            await _availabilityRepository.AddAvailabilityAsync(availability);

            var doctor = await _doctorRepository.GetDoctorByEmailAsync(userDTO.Email);
            if (doctor == null)
            {
                throw new Exception("Doctor not found after insertion.");
            }

            var cred = new UserCredential
            {
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Role = userDTO.Role,
                DoctorId = doctor.DoctorId
            };
            await _userCredentialRepository.AddUserCredentialAsync(cred);
        }
        else if (userDTO.Role == "Patient")
        {
            var user = new Patient
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                PhoneNumber = userDTO.PhoneNumber,
                Role = userDTO.Role,
            };
            await _patientRepository.AddPatientAsync(user);

            var patient = await _patientRepository.GetPatientByEmailAsync(userDTO.Email);
            if (patient == null)
            {
                throw new Exception("Patient not found after insertion.");
            }

            var cred = new UserCredential
            {
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                Role = userDTO.Role,
                PatientId = patient.PatientId
            };
            await _userCredentialRepository.AddUserCredentialAsync(cred);
        }
        return true;
    }

    public async Task<string> AuthenticateUser(LoginDTO loginDto)
    {
        var user = await _userCredentialRepository.GetCredByEmailAsync(loginDto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return null;
        }

        var roles = new List<string> { user.Role };

        var token = _tokenRepository.CreateJWTToken(new IdentityUser { Email = user.Email }, roles);

        return token;
    }
}