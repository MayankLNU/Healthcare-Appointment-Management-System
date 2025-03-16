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

        public DoctorController(IAvailabilityService availabilityService, IConsultationService consultationService, IAppointmentService appointmentService)
        {
            _availabilityService = availabilityService;
            _consultationService = consultationService;
            _appointmentService = appointmentService;
        }

        [HttpPost("Add-Timeslots")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddTimeSlots([FromBody] AvailabilityDTO availabilityDTO)
        {
            if (availabilityDTO == null)
            {
                return BadRequest("Data is missing.");
            }

            var result = await _availabilityService.AddAvailabilityAsync(availabilityDTO);

            if (result)
            {
                return Ok("Time slots added successfully.");
            }
        return BadRequest(new { Message = "Invalid Data! Please provide valid details." });
        }

        [HttpPost("Booked-Slots")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetBookedSlots([FromBody] BookedTimeSlotsDTO bookedSlotsDTO)
        {
            var bookedSlots = await _appointmentService.GetBookedTimeSlots(bookedSlotsDTO);
            return Ok(bookedSlots);
        }

        [HttpDelete("Remove-Slot")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> RemoveSlots([FromBody] RemoveTimeSlotDTO removeTimeSlotDTO)
        {
            var removed = await _availabilityService.RemoveTimeSlotAsync(removeTimeSlotDTO);
            if (removed) { return Ok("Slot Removed Successfully"); }
            return BadRequest(new { Message = "Slot already booked or Invalid details!" } );
        }

        [HttpPost("Add-Prescriptons-And-Notes")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddPatientsPrescriptionsAndNotes([FromBody] ConsultationDTO consultationDTO)
        {
            if (consultationDTO == null)
            {
                return BadRequest(new { Message = "Invalid Data! Please provide valid details." });
            }

            var result = await _consultationService.AddPatientsPrescriptionAndNotes(consultationDTO);

            if (result == false)
            {
                return BadRequest(new { Message = "Something went wrong while adding data! Data not added" });
            }

            return Ok(new { Message = "Prescriptions and Notes added successfully." });
        }
    }
}
