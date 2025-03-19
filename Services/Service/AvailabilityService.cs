using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repositories.Interface;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppointmentManagement.Services.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ITimeSlotRepository _timeSlotRepository;

        public AvailabilityService(IAvailabilityRepository availabilityRepository, IDoctorRepository doctorRepository, ITimeSlotRepository timeSlotRepository)
        {
            _availabilityRepository = availabilityRepository;
            _doctorRepository = doctorRepository;
            _timeSlotRepository = timeSlotRepository;
        }



        public async Task<AvailabilityResponseDTO> AddAvailabilityAsync(AvailabilityDTO availabilityDTO)
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(availabilityDTO.Email);

            if (doctor == null)
            {
                return new AvailabilityResponseDTO { Success = false, Message = "Doctor not found. Please check Email of doctor."};
            }

            // Create an Availability instance from the DTO
            var availability = new Availability
            {
                DoctorId = doctor.DoctorId,
                Date = availabilityDTO.Date
            };

            // Generate time slots
            var startTime = availabilityDTO.StartTime;
            var endTime = availabilityDTO.EndTime;

            TimeOnly currentTime = startTime;
            while (currentTime < endTime)
            {
                TimeOnly slotEndTime = currentTime.AddMinutes(30);
                if (slotEndTime > endTime)
                {
                    break;
                }
                availability.TimeSlots.Add(new TimeSlot
                {
                    StartTime = currentTime,
                    EndTime = slotEndTime,
                    DoctorId = availability.DoctorId,
                    Date = availability.Date
                });
                currentTime = slotEndTime;
            }

            // Save availability and time slots to the database
            await _availabilityRepository.UpdateAvailabilityAsync(availability);

            // Save changes to the database
            return new AvailabilityResponseDTO { Success = true, Message = "Added Time Slots Successfully." };
        }



        public async Task<RemoveTimeSlotResponseDTO> RemoveTimeSlotAsync(RemoveTimeSlotDTO removeTimeSlotDTO)
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(removeTimeSlotDTO.DoctorEmail);

            if (doctor == null)
            {
                return new RemoveTimeSlotResponseDTO { Success = false, Message = "Doctor not found. Please check Email of doctor." };
            }

            var timeSlot = await _timeSlotRepository.GetAvailableTimeSlotsByDateTimeAndDrId(removeTimeSlotDTO.Date, removeTimeSlotDTO.StartTime, doctor.DoctorId);

            if (timeSlot == null)
            {
                return new RemoveTimeSlotResponseDTO { Success = false, Message = "This time slot does not exist." };
            }
            else if (!timeSlot.IsAvailable)
            {
                return new RemoveTimeSlotResponseDTO { Success = false, Message = "This time slot is already booked by a patient." };
            }

            await _timeSlotRepository.DeleteTimeSlotAsync(timeSlot.TimeSlotId);
            return new RemoveTimeSlotResponseDTO { Success = true, Message = "Time slot removed successfully." };
        }



        public async Task<IEnumerable<AvailableTimeSlotResponseDTO>> GetAvailableTimeSlots(DateOnly date)
        {
            var timeSlots = await _timeSlotRepository.GetAvailableTimeSlotsByDate(date);

            if (timeSlots == null || !timeSlots.Any())
            {
                return new List<AvailableTimeSlotResponseDTO>
                {
                    new AvailableTimeSlotResponseDTO { Message = "No available time slots found for the given date." }
                };
            }

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
                    EndTime = timeSlot.EndTime,
                    Message = "Time Slot Available"
                });
            }

            return response;
        }



        public async Task<IEnumerable<BookedTimeSlotResponseDTO>> GetBookedTimeSlots(BookedTimeSlotsDTO bookedSlotsDTO)
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(bookedSlotsDTO.DoctorEmail);
            if (doctor == null)
            {
                return new List<BookedTimeSlotResponseDTO>
                {
                    new BookedTimeSlotResponseDTO { Message = "Doctor not found." }
                };
            }

            var timeSlots = await _timeSlotRepository.GetBookedTimeSlotsByDateAndDrId(bookedSlotsDTO.Date, doctor.DoctorId);
            if (timeSlots == null || !timeSlots.Any())
            {
                return new List<BookedTimeSlotResponseDTO>
                {
                    new BookedTimeSlotResponseDTO { Message = "No booked time slots found for the given date." }
                };
            }

            var response = new List<BookedTimeSlotResponseDTO>();

            foreach (var timeSlot in timeSlots)
            {
                response.Add(new BookedTimeSlotResponseDTO
                {
                    Date = timeSlot.Date,
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.EndTime,
                    Message = "Time Slot Booked"
                });
            }

            return response;
        }
    }
}