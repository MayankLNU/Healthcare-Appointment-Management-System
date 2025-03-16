using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Interface
{
    public interface IDoctorRepository
    {
        Task<bool> AddDoctorAsync(Doctor user);
        Task<Doctor> GetDoctorByIdAsync(int userId);
        Task<Doctor> GetDoctorByEmailAsync(string email);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<bool> UpdateDoctorAsync(Doctor user);
        Task DeleteDoctorAsync(int userId);
    }
}