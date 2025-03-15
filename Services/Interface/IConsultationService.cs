using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IConsultationService
    {
        Task<bool> AddPatientsPrescriptionAndNotes(ConsultationDTO consultationDTO);
        Task<ConsultationDTO> ReadPrescriptionsAndNotes(PatientConsultationDTO patientConsultationDTO);
    }
}