using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;       
using System;                      
using System.Collections.Generic;    
using System.Threading.Tasks;         
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
        public async Task<IEnumerable<Appointment>> GetAllAppointments(Guid userId)
        {
            //TODO: userId ophalen

            return await _repository.GetAllAppointmentsAsync(userId);
        }

        [HttpPost("CreateAppointment")]
        public async Task<ActionResult> CreateAppointment(Appointment appointment)
        {
            await _repository.CreateAppointmentAsync(appointment);

            var createdAppointment = await _repository.GetAppointmentAsync(appointment.Id);
            // Als de afspraak niet is aangemaakt een foutmelding geven
            if (createdAppointment == null)
            {
                return BadRequest("Ja, mislukt, de afspraak is niet goed aangemaakt, pipo :P");
            }
            //Goed response teruggeven als het gelukt is
            else
            {
                return CreatedAtAction(nameof(GetAppointment), new { appointmentId = appointment.Id }, appointment);
            }           
        }

        [HttpPut("SaveAppointment")]
        public async Task<ActionResult> SaveAppointment(Appointment appointment)
        {            
            await _repository.UpdateAppointmentAsync(appointment);
            return Created();
        }

        [HttpDelete("DeleteAppointment")]
        public async Task<ActionResult> DeleteAppointment(Guid appointmentId)
        {
            await _repository.DeleteAppointmentAsync(appointmentId);
            return NoContent();
        }

        [HttpGet("GetAppointment")]
        public async Task<Appointment?> GetAppointment(Guid appointmentId)
        {
            return await _repository.GetAppointmentAsync(appointmentId);
        }
    }
}
