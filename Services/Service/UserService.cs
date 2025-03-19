using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;
using Azure;
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



    public async Task<UserNewAccountResponse> RegisterUser(UserDTO userDTO)
    {
        if (userDTO == null)
        {
            return new UserNewAccountResponse { Success = false, Message = "User details cannot be null." };
        }

        if (userDTO.Role != "Doctor" && userDTO.Role != "Patient")
        {
            return new UserNewAccountResponse { Success = false, Message = "Invalid role specified." };
        }

        if (userDTO.Role == "Doctor")
        {
            var check = await _doctorRepository.GetDoctorByEmailAsync(userDTO.Email);
            if (check != null)
            {
                return new UserNewAccountResponse { Success = false, Message = "User already exists." };
            }

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
            var check = await _patientRepository.GetPatientByEmailAsync(userDTO.Email);
            if (check != null)
            {
                return new UserNewAccountResponse { Success = false, Message = "User already exists." };
            }

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
        return new UserNewAccountResponse { Success = true, Message = "User registered successfully. Please Login!!" };
    }



    public async Task<AuthenticateUserResponse> AuthenticateUser(LoginDTO loginDto)
    {
        var user = await _userCredentialRepository.GetCredByEmailAsync(loginDto.Email);

        if (user == null) {
            return new AuthenticateUserResponse { Message = "User not found." };
        }
        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return new AuthenticateUserResponse { Message = "Invalid Credentials. Please check email and password." };
        }

        var roles = new List<string> { user.Role };

        var token = _tokenRepository.CreateJWTToken(new IdentityUser { Email = user.Email }, roles);

        if (user.Role == "Doctor")
        {
            return new AuthenticateUserResponse
            {
                UserId = user.DoctorId,
                JwtToken = token,
                Message = "Welcome Doctor."
            };
        }
        else if (user.Role == "Patient")
        {
            return new AuthenticateUserResponse
            {
                UserId = user.PatientId,
                JwtToken = token,
                Message = "Login Successful."
            };
        }

        throw new InvalidOperationException("Invalid user role.");
    }
}