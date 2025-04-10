using AppointmentManagement.Models.DTO;
using AppointmentManagement.Services;
using AppointmentManagement.Services.Interface;
using AppointmentManagement.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Controllers.User_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        private readonly IConsultationService _consultationService;
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IAvailabilityService availabilityService, IConsultationService consultationService, IAppointmentService appointmentService, ILogger<DoctorController> logger)
        {
            _availabilityService = availabilityService;
            _consultationService = consultationService;
            _appointmentService = appointmentService;
            _logger = logger;
        }



        [HttpPost("Add-Availability")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddAvailability([FromBody] AvailabilityDTO availabilityDTO)
        {
            try
            {
                var response = await _availabilityService.AddAvailabilityAsync(availabilityDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding availability.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost("Booked-Slots")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetBookedTimeSlots([FromBody] BookedTimeSlotsDTO bookedSlotsDTO)
        {
            try
            {
                var response = await _availabilityService.GetBookedTimeSlots(bookedSlotsDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving booked time slots.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpDelete("Remove-Slot")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> RemoveTimeSlot([FromBody] RemoveTimeSlotDTO removeTimeSlotDTO)
        {
            try
            {
                var response = await _availabilityService.RemoveTimeSlotAsync(removeTimeSlotDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing the time slot.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost("Add-Prescriptons-And-Notes")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddPatientsPrescriptionAndNotes([FromBody] ConsultationDTO consultationDTO)
        {
            try
            {
                var response = await _consultationService.AddPatientsPrescriptionAndNotes(consultationDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding consultation.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
