using AppointmentManagement.Models.Domain;

namespace AppointmentManagement.Repositories.Interface
{
    public interface IConsultationRepository
    {
        Task<bool> AddConsultationAsync(Consultation consultation);
        Task<Consultation> GetConsultationByAppointmentIdAsync(int appointmentId);
    }
}