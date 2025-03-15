using AppointmentManagement.Models.Domain;
using AppointmentManagement.Models.DTO;
using AppointmentManagement.Repository.Interface;
using AppointmentManagement.Services.Interface;

namespace AppointmentManagement.Services.Service
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AvailabilityService(IAvailabilityRepository availabilityRepository, IDoctorRepository doctorRepository)
        {
            _availabilityRepository = availabilityRepository;
            _doctorRepository = doctorRepository;
        }

        public async Task<bool> AddAvailabilityAsync(AvailabilityDTO availabilityDTO)
        {
            var doctor = await _doctorRepository.GetDoctorByEmailAsync(availabilityDTO.Email);
            var startTime = availabilityDTO.StartTime;
            var endTime = availabilityDTO.EndTime;

            if (doctor == null)
            {
                return false;
            }

            // Create an Availability instance from the DTO
            var availability = new Availability
            {
                DoctorId = doctor.DoctorId,
                Date = availabilityDTO.Date
            };

            // Generate time slots
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

            // availability and time slots to the database
            await _availabilityRepository.UpdateAvailabilityAsync(availability);

            // Save changes to the database
            return true;
        }
    }
}