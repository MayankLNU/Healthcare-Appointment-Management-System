using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repository
{
    public interface IAppointmentRepository
    {
        Task AddAppointmentAsync(Appointment appointment);
        Task<Appointment> GetAppointmentByIdAsync(int appointmentId);
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int appointmentId);
    }
}
