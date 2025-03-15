using AppointmentManagement.Models.DTO;
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

        public DoctorController(IAvailabilityService availabilityService, IConsultationService consultationService)
        {
            _availabilityService = availabilityService;
            _consultationService = consultationService;
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
            else
            {
                return BadRequest(new { Message = "Invalid Data! Please provide valid details." });
            }
        }

        [HttpPost("Add-PrescriptonsAndNotes")]
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
