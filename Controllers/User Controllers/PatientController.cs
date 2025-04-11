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
    public class PatientController : ControllerBase
    {
        private readonly IConsultationService _consultationService;
        private readonly IAppointmentService _appointmentService;
        private readonly IAvailabilityService _availabilityService;
        private readonly ILogger<PatientController> _logger;
        public PatientController(IConsultationService consultationService, IAppointmentService appointmentService, IAvailabilityService availabilityService, ILogger<PatientController> logger)
        {
            _consultationService = consultationService;
            _appointmentService = appointmentService;
            _availabilityService = availabilityService;
            _logger = logger;
        }



        [HttpGet("Available-Slots/{date}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAvailableTimeSlots(DateOnly date)
        {
            try
            {
                var response = await _availabilityService.GetAvailableTimeSlots(date);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving time slots.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost("Book-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDTO bookAppointmentDTO)
        {
            try
            {
                var response = await _appointmentService.BookAppointment(bookAppointmentDTO);
                if (response.Message == "Slot Booked Successfully.")
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while booking appointment.");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPut("Update-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDTO updateAppointmentDTO)
        {
            try
            {
                var response = await _appointmentService.UpdateAppointment(updateAppointmentDTO);
                if (response.Message == "Slot Booked Successfully.")
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating appointment.");
                return StatusCode(500, "Internal server error. Please contact admin.");
            }
        }



        [HttpPost("Cancel-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentDTO cancelAppointmentDTO)
        {
            try
            {
                var response = await _appointmentService.CancelAppointmentAsync(cancelAppointmentDTO);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while cancelling appointment.");
                return StatusCode(500, "Internal server error. Please contact admin.");
            }
        }



        [HttpGet("Read-Prescriptions-And-Notes/{appointmentId}")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> ReadPrescriptionsAndNotes(int appointmentId)
        {
            try
            {
                var response = await _consultationService.ReadPrescriptionsAndNotes(appointmentId);
                if (response.Success)
                {
                    return Ok(response);
                }
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while reading consultation.");
                return StatusCode(500, "Internal server error. Please contact admin.");
            }
        }
    }
}
