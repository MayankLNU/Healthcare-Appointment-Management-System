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

        public async Task<bool> AddPatientsPrescriptionAndNotes(ConsultationDTO consultationDTO)
        {
            var consultation = new Consultation
            {
                Notes = consultationDTO.Notes,
                Prescription = consultationDTO.Prescription,
                AppointmentId = consultationDTO.AppointmentId
            };

            var result = await _consultationRepository.AddConsultationAsync(consultation);

            if (result)
            {
                var appointment = await _appointmentRepository.GetAppointmentByIdAsync(consultation.AppointmentId);
                if (appointment != null)
                {
                    appointment.Status = "Completed";
                    await _appointmentRepository.UpdateAppointmentAsync(appointment);
                }
                else
                {
                    throw new InvalidOperationException("AppointmentId does not exist.");
                }
            }

            return result;
        }

        public async Task<ConsultationDTO> ReadPrescriptionsAndNotes(PatientConsultationDTO patientConsultationDTO)
        {
            var consultation = await _consultationRepository.GetConsultationByAppointmentIdAsync(patientConsultationDTO.AppointmentId);

            if (consultation == null)
            {
                return null;
            }

            var foundConsultation = new ConsultationDTO
            {
                AppointmentId = patientConsultationDTO.AppointmentId,
                Notes = consultation.Notes,
                Prescription = consultation.Prescription
            };

            return foundConsultation;
        }
    }
}