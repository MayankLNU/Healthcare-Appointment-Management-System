using System;
using System.Numerics;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
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



        public async Task<BookAppointmentResponseDTO> BookAppointment(BookAppointmentDTO bookAppointmentDTO)
        {
            if (bookAppointmentDTO == null)
            {
                return new BookAppointmentResponseDTO { Message = "Please enter valid details." };
            }

            var slotStatus = await _timeSlotRepository.GetAvailableTimeSlotsByDateTimeAndDrId(bookAppointmentDTO.Date, bookAppointmentDTO.StartTime, bookAppointmentDTO.DoctorId);
            if (slotStatus == null)
            {
                return new BookAppointmentResponseDTO { Message = "Invalid slot details." };
            }
            else if (slotStatus.IsAvailable == false)
            {
                return new BookAppointmentResponseDTO { Message = "Slot already booked by someone else." };
            }

            var patient = await _patientRepository.GetPatientByEmailAsync(bookAppointmentDTO.PatientEmail);
            if (patient == null)
            {
                return new BookAppointmentResponseDTO { Message = "Invalid patient details." };
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
                throw new Exception("Something went wrong while booking slot.");
            }

            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(bookAppointmentDTO.Date, bookAppointmentDTO.StartTime, bookAppointmentDTO.DoctorId, false);

            var doctor = await _doctorRepository.GetDoctorByIdAsync(bookAppointmentDTO.DoctorId);
            var bookingDetails = new BookAppointmentResponseDTO
            {
                AppointmentId = appointment.AppointmentId,
                StartTime = bookAppointmentDTO.StartTime,
                DoctorName = doctor.Name,
                Date = bookAppointmentDTO.Date,
                Message = "Slot Booked Successfully."
            };

            return bookingDetails;
        }



        public async Task<BookAppointmentResponseDTO> UpdateAppointment(UpdateAppointmentDTO updateAppointmentDTO)
        {
            if (updateAppointmentDTO == null)
            {
                return new BookAppointmentResponseDTO { Message = "Please enter valid details." };
            }

            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(updateAppointmentDTO.AppointmentId);
            if (appointment == null)
            {
                return new BookAppointmentResponseDTO { Message = "Invalid appointment id! No slot was booked on this id." };
            }

            var newAppointmentStatus = await _timeSlotRepository.GetAvailableTimeSlotsByDateTimeAndDrId(updateAppointmentDTO.Date, updateAppointmentDTO.StartTime, updateAppointmentDTO.DoctorId);
            if (newAppointmentStatus == null)
            {
                return new BookAppointmentResponseDTO { Message = "This slot does not exist." };
            }
            else if (newAppointmentStatus.IsAvailable == false)
            {
                return new BookAppointmentResponseDTO { Message = "Slot already booked by someone else." };
            }

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
                Date = appointment.Date,
                Message = "Slot Booked Successfully."
            };

            return bookingDetails;
        }



        public async Task<CancelAppointmentResponseDTO> CancelAppointmentAsync(CancelAppointmentDTO cancelAppointmentDTO)
        {
            if (cancelAppointmentDTO == null)
            {
                return new CancelAppointmentResponseDTO { Success = false, Message = "Please enter valid details" };
            }

            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(cancelAppointmentDTO.AppointmentId);
            var patient = await _patientRepository.GetPatientByEmailAsync(cancelAppointmentDTO.PatientEmail);

            if (appointment == null)
            {
                return new CancelAppointmentResponseDTO { Success = false, Message = "This appointment does not exists" };
            }

            if (appointment.Status == "Cancelled")
            {
                return new CancelAppointmentResponseDTO { Success = false, Message = "This appointment is already cancelled" };
            }

            if (patient == null)
            {
                return new CancelAppointmentResponseDTO { Success = false, Message = "Patient Not Found" };
            }

            if (patient.PatientId != appointment.PatientId)
            {
                return new CancelAppointmentResponseDTO { Success = false, Message = "Patient does not match the appointment" };
            }

            await _timeSlotRepository.UpdateTimeSlotAvailabilityAsync(appointment.Date, appointment.TimeSlot, appointment.DoctorId, true);

            appointment.Status = "Cancelled";
            await _appointmentRepository.UpdateAppointmentAsync(appointment);

            return new CancelAppointmentResponseDTO
            {
                Success = true,
                Message = "Appointment cancelled successfully."
            };
        }
    }
}
