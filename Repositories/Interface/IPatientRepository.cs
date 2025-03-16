using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<bool> AddPatientAsync(Patient user);
        Task<Patient> GetPatientByIdAsync(int userId);
        Task<Patient> GetPatientByEmailAsync(string email);
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<bool> UpdatePatientAsync(Patient user);
        Task DeletePatientAsync(int userId);
    }
}