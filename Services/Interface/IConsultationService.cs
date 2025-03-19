using AppointmentManagement.Models.DTO;

namespace AppointmentManagement.Services.Interface
{
    public interface IConsultationService
    {
        Task<ConsultationResponseDTO> AddPatientsPrescriptionAndNotes(ConsultationDTO consultationDTO);
        Task<PatientConsultationResponseDTO> ReadPrescriptionsAndNotes(PatientConsultationDTO patientConsultationDTO);
    }
}