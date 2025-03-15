using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentManagement.Repository.Interface
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(Doctor user);
        Task<Doctor> GetDoctorByIdAsync(int userId);
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task UpdateDoctorAsync(Doctor user);
        Task DeleteDoctorAsync(int userId);
        Task<Doctor> GetDoctorByEmailAsync(string email);
    }
}