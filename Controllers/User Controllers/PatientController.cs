using AppointmentManagement.Models.DTO;
using AppointmentManagement.Services;
using AppointmentManagement.Services.Interface;
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
        public PatientController(IConsultationService consultationService, IAppointmentService appointmentService)
        {
            _consultationService = consultationService;
            _appointmentService = appointmentService;
        }

        [HttpGet("Available-Slots")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetAvailableSlots([FromQuery] DateOnly date)
        {
            var availableSlots = await _appointmentService.GetAvailableTimeSlots(date);
            return Ok(availableSlots);
        }

        [HttpPost("Book-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentDTO bookAppointmentDTO)
        {
            if (bookAppointmentDTO == null)
            {
                return BadRequest(new { Message = "Invalid Details! Please provide valid details." });
            }

            var result = await _appointmentService.BookAppointment(bookAppointmentDTO);

            if (result == null)
            {
                return BadRequest(new { Message = "Something went wrong. Appointment Not Booked. Maybe Slot Not Available." });
            }

            return Ok(result);
        }

        [HttpPost("Update-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentDTO updateAppointmentDTO)
        {
            if (updateAppointmentDTO == null)
            {
                return BadRequest(new { Message = "Invalid Details! Please provide valid details." });
            }

            var result = await _appointmentService.UpdateAppointment(updateAppointmentDTO);
            return Ok(result);
        }

        [HttpPost("Cancel-Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentDTO cancelAppointmentDTO)
        {
            if (cancelAppointmentDTO == null)
            {
                return BadRequest(new { Message = "Invalid Details! Please provide valid details." });
            }

            var result = await _appointmentService.CancelAppointmentAsync(cancelAppointmentDTO);

            return Ok(result);
        }

        [HttpPost("Read-PrescriptionsAndNotes")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> ReadPrescriptionsAndNotes([FromBody] PatientConsultationDTO patientConsultationDTO)
        {
            var result = await _consultationService.ReadPrescriptionsAndNotes(patientConsultationDTO);

            if (result == null)
            {
                return BadRequest(new { Message = "No Prescriptions or Notes found." });
            }

            return Ok(result);
        }
    }
}
