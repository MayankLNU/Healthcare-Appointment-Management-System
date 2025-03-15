//using AppointmentManagement.Models.Domain;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AppointmentManagement.Repository.Interface;
//using AppointmentManagement.Services.Service;
//using AppointmentManagement.Models.DTO;

//namespace AppointmentManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class AvailabilityController : ControllerBase
//    {
//        private readonly AvailabilityService _availabilityService;
//        private readonly IAvailabilityRepository _context;

//        public AvailabilityController(IAvailabilityRepository context, AvailabilityService availabilityService)
//        {
//            _context = context;
//            _availabilityService = availabilityService;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Availability>> AddAvailability([FromBody] AvailabilityDTO availabilityDTO)
//        {
//            if (availabilityDTO == null)
//            {
//                return BadRequest("Appointment data is missing.");
//            }

//            var result = await _availabilityService.AddAvailabilityAsync(availabilityDTO);
//            if (result)
//            {
//                return Ok(availabilityDTO);
//            }
//            else
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding availability.");
//            }
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Availability>>> GetAllAvailabilities()
//        {
//            var availability = await _context.GetAllAvailabilityAsync();
//            return Ok(availability);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Availability>> GetAvailabilityById(int id)
//        {
//            var availability = await _context.GetAvailabilityByIdAsync(id);
//            if (availability == null)
//            {
//                return NotFound();
//            }
//            return Ok(availability);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] Availability availability)
//        {
//            if (id != availability.DoctorId)
//            {
//                return BadRequest();
//            }
//            await _context.UpdateAvailabilityAsync(availability);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAvailability(int id)
//        {
//            var availability = await _context.GetAvailabilityByIdAsync(id);
//            if (availability == null)
//            {
//                return NotFound();
//            }

//            await _context.DeleteAvailabilityAsync(id);
//            return NoContent();
//        }
//    }
//}