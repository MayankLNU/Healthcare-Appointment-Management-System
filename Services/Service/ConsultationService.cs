using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;

namespace AppointmentManagement.Services.Service
{
    public class ConsultationService : IConsultationService
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public ConsultationService(IConsultationRepository consultationRepository, IAppointmentRepository appointmentRepository)
        {
            _consultationRepository = consultationRepository;
            _appointmentRepository = appointmentRepository;
        }



        public async Task<ConsultationResponseDTO> AddPatientsPrescriptionAndNotes(ConsultationDTO consultationDTO)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(consultationDTO.AppointmentId);
            if (appointment == null)
            {
                return new ConsultationResponseDTO { Success = false, Message = "Appointment not found with this id." };
            }

            if (appointment.Status == "Completed")
            {
                return new ConsultationResponseDTO { Success = false, Message = "Already Added!" };
            }

            var consultation = new Consultation
            {
                Notes = consultationDTO.Notes,
                Prescription = consultationDTO.Prescription,
                AppointmentId = consultationDTO.AppointmentId
            };

            var result = await _consultationRepository.AddConsultationAsync(consultation);

            if (!result)
            {
                throw new Exception("Failed to add consultation. Please try again.");
            }

            appointment.Status = "Completed";
            await _appointmentRepository.UpdateAppointmentAsync(appointment);

            return new ConsultationResponseDTO { Success = true, Message = "Prescription and notes added successfully." };
        }



        public async Task<PatientConsultationResponseDTO> ReadPrescriptionsAndNotes(int appointmentId)
        {
            var consultation = await _consultationRepository.GetConsultationByAppointmentIdAsync(appointmentId);

            if (consultation == null)
            {
                return new PatientConsultationResponseDTO { Success = false, Message = "No record found on this id." };
            }

            return new PatientConsultationResponseDTO
            {
                AppointmentId = appointmentId,
                Notes = consultation.Notes,
                Prescription = consultation.Prescription,
                Message = "Please take care of yourself",
                Success = true
            };
        }
    }
}