using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repositories.Interface;
using Dapper;
using System.Data;

namespace AppointmentManagement.Repositories.Repository
{
    public class UserCredentialRepository : IUserCredentialRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserCredentialRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Read
        public async Task<UserCredential> GetCredByEmailAsync(string email)
        {
            var query = "SELECT * FROM UserCredentials WHERE Email = @Email";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserCredential>(query, new { Email = email });
        }

        // Create
        public async Task<bool> AddUserCredentialAsync(UserCredential userCredential)
        {
            if (userCredential != null)
            {
                var query = "INSERT INTO UserCredentials (Email, PasswordHash, Role, DoctorId, PatientId) VALUES (@Email, @PasswordHash, @Role, @DoctorId, @PatientId)";
                var result = await _dbConnection.ExecuteAsync(query, userCredential);
                return result > 0;
            }
            return false;
        }
    }
}