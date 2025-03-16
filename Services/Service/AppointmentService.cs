using System;
using System.Numerics;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
using AppointmentManagement.Repositories.Repository;
using AppointmentManagement.Repository;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;

namespace AppointmentManagement.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;

        public AppointmentService(ITimeSlotRepository timeSlotRepository, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository, IPatientRepository patientRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<AvailableTimeSlotResponseDTO>> GetAvailableTimeSlots(DateOnly date)
        {
            var timeSlots = await _timeSlotRepository.GetAvailableTimeSlotsByDate(date);

            var response = new List<AvailableTimeSlotResponseDTO>();

            foreach (var timeSlot in timeSlots)
            {
                var doctor = await _doctorRepository.GetDoctorByIdAsync(timeSlot.DoctorId);
                response.Add(new AvailableTimeSlotResponseDTO
                {
                    DoctorId = timeSlot.DoctorId,
                    DoctorName = doctor.Name,
                    Date = timeSlot.Date,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime
                });
            }

            return response;
        }

        public async Task<IEnumerable<BookedTimeSlotResponseDTO>> GetBookedTimeSlots(BookedTimeSlotsDTO bookedSlotsDTO)
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(bookedSlotsDTO.DoctorEmail);
            var timeSlots = await _timeSlotRepository.GetBookedTimeSlotsByDateAndDrId(bookedSlotsDTO.Date, doctor.DoctorId);

            var response = new List<BookedTimeSlotResponseDTO>();

            foreach (var timeSlot in timeSlots)
            {
                response.Add(new BookedTimeSlotResponseDTO
                {
                    Date = timeSlot.Date,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime
                });
            }

            return response;
        }

        public async Task<BookAppointmentResponseDTO> BookAppointment(BookAppointmentDTO bookAppointmentDTO)
        {
            var slotStatus = await _timeSlotRepository.GetAvailableTimeSlotsByDateTimeAndDrId(bookAppointmentDTO.Date, bookAppointmentDTO.StartTime, bookAppointmentDTO.DoctorId);
            if (slotStatus.IsAvailable == false)
            {
                return null;
            }

            var patient = await _patientRepository.GetPatientByEmailAsync(bookAppointmentDTO.PatientEmail);

            if (patient == null)
            {
                return null;
            }

            var appointment = new Appointment
            {
                PatientId = patient.PatientId,
                Date = bookAppointmentDTO.Date,
                DoctorId = bookAppointmentDTO.DoctorId,
                TimeSlot = bookAppointmentDTO.StartTime,
                Status = "Booked",
            };
            var added = await _appointmentRepository.AddAppointmentAsync(appointment);

            if (added == false)
            {
                return null;
            }

            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(bookAppointmentDTO.Date, bookAppointmentDTO.StartTime, bookAppointmentDTO.DoctorId, false);

            var doctor = await _doctorRepository.GetDoctorByIdAsync(bookAppointmentDTO.DoctorId);
            var bookingDetails = new BookAppointmentResponseDTO
            {
                AppointmentId = appointment.AppointmentId,
                StartTime = bookAppointmentDTO.StartTime,
                DoctorName = doctor.Name,
                Date = bookAppointmentDTO.Date
            };

            return bookingDetails;
        }

        public async Task<BookAppointmentResponseDTO> UpdateAppointment(UpdateAppointmentDTO updateAppointmentDTO)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(updateAppointmentDTO.AppointmentId);
            if (appointment == null) { return null; }

            var newAppointmentStatus = await _timeSlotRepository.GetAvailableTimeSlotsByDateTimeAndDrId(updateAppointmentDTO.Date, updateAppointmentDTO.StartTime, updateAppointmentDTO.DoctorId);
            if (newAppointmentStatus.IsAvailable == false || newAppointmentStatus == null) { return null; }

            // Update the time slot availability
            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(appointment.Date, appointment.TimeSlot, appointment.DoctorId, true);

            // Update the appointment details
            appointment.DoctorId = updateAppointmentDTO.DoctorId;
            appointment.TimeSlot = updateAppointmentDTO.StartTime;

            // Save the updated appointment
            await _appointmentRepository.UpdateAppointmentAsync(appointment);

            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(appointment.Date, appointment.TimeSlot, appointment.DoctorId, false);

            // Fetch the updated doctor details
            var doctor = await _doctorRepository.GetDoctorByIdAsync(updateAppointmentDTO.DoctorId);
            var bookingDetails = new BookAppointmentResponseDTO
            {
                AppointmentId = appointment.AppointmentId,
                StartTime = appointment.TimeSlot,
                DoctorName = doctor.Name,
                Date = appointment.Date
            };

            return bookingDetails;
        }

        public async Task<CancelAppointmentResponseDTO> CancelAppointmentAsync(CancelAppointmentDTO cancelAppointmentDTO)
        {
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(cancelAppointmentDTO.AppointmentId);
            var patient = await _patientRepository.GetPatientByEmailAsync(cancelAppointmentDTO.PatientEmail);

            if (appointment == null || patient == null)
            {
                return new CancelAppointmentResponseDTO
                {
                    Success = false,
                    Message = "Appointment or Patient Not Found."
                };
            }

            if (patient.PatientId != appointment.PatientId)
            {
                return new CancelAppointmentResponseDTO
                {
                    Success = false,
                    Message = "Patient does not match the appointment."
                };
            }

            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(appointment.Date, appointment.TimeSlot, appointment.DoctorId, true);

            appointment.Status = "Canceled";
            await _appointmentRepository.UpdateAppointmentAsync(appointment);

            return new CancelAppointmentResponseDTO
            {
                Success = true,
                Message = "Appointment cancelled successfully."
            };
        }
    }
}
