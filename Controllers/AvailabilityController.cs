using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository _context;

        public AvailabilityController(IAvailabilityRepository context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Availability>> AddAvailability([FromBody] Availability availability)
        {
            if (availability == null)
            {
                return BadRequest("Appointment data is missing.");
            }
            await _context.AddAvailability(availability);
            return Ok(availability);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Availability>>> GetAllAvailabilities()
        {
            var availability = await _context.GetAllAvailabilityAsync();
            return Ok(availability);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAvailabilityById(int id)
        {
            var availability = await _context.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return NotFound();
            }
            return Ok(availability);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAvailability(int id, [FromBody] Availability availability)
        {
            if (id != availability.DoctorId)
            {
                return BadRequest();
            }
            await _context.UpdateAvailabilityAsync(availability);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            var availability = await _context.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            await _context.DeleteAvailabilityAsync(id);
            return NoContent();
        }
    }
}

