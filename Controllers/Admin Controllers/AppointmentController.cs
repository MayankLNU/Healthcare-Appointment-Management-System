//using AppointmentManagement.Models.Domain;
//using AppointmentManagement.Repository.Interface;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace AppointmentManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize]
//    public class AppointmentController : ControllerBase
//    {
//        private readonly IAppointmentRepository _context;

//        public AppointmentController(IAppointmentRepository context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Appointment>> AddAppointment([FromBody] Appointment appointment)
//        {
//            if (appointment == null)
//            {
//                return BadRequest("Appointment data is missing.");
//            }
//            await _context.AddAppointmentAsync(appointment);
//            return Ok(appointment);
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Appointment>>> GetAllAppointments()
//        {
//            var user = await _context.GetAllAppointmentsAsync();
//            return Ok(user);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
//        {
//            var user = await _context.GetAppointmentByIdAsync(id);
//            if (user == null)
//            {
//                return NotFound();
//            }
//            return Ok(user);
//        }

//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
//        {
//            if (id != appointment.AppointmentId)
//            {
//                return BadRequest();
//            }
//            await _context.UpdateAppointmentAsync(appointment);
//            return NoContent();
//        }

//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteAppointment(int id)
//        {
//            var appointment = await _context.GetAppointmentByIdAsync(id);
//            if (appointment == null)
//            {
//                return NotFound();
//            }

//            await _context.DeleteAppointmentAsync(id);
//            return NoContent();
//        }
//    }
//}
