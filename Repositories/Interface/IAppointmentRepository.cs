using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repository.Interface
{
    public interface IAppointmentRepository
    {
        Task<bool> AddAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<bool> UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
    }
}
