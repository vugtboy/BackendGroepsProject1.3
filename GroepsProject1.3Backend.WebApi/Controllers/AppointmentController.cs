using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace GroepsProject1._3Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentRepository _repository;

        public AppointmentController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetAllAppointments")]
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            //todo userId ophalen
            string userId = null;
            return await _repository.GetAllAppointmentsAsync(userId);
        }

        [HttpPost("CreateAppointment")]
        public async Task<ActionResult> CreateAppointmentAsync(Appointment appointment)
        {
            await _repository.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetAppointmentAsync), new { id = appointment.Id });
        }

        //todo actionresult van maken
        [HttpPut("SaveAppointment")]
        public async Task SaveAppointmentAsync(Appointment appointment)
        {
            _repository.UpdateAppointmentAsync(appointment);
        }

        //todo actionresult van maken
        [HttpDelete("DeleteAllAppointment")]
        public async Task DeleteAppointmentAsync(string appointmentId)
        {
            _repository.DeleteAppointmentAsync(appointmentId);
        }

        [HttpGet("GetAppointment")]
        public async Task<Appointment> GetAppointmentAsync(string appointmentId)
        {
            return await _repository.GetAppointmentAsync(appointmentId);
        }
    }
}
