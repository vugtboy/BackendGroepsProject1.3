using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
>>>>>>> Stashed changes
using GroepsProject1._3Backend.WebApi.Services;
using Microsoft.AspNetCore.Mvc;       
using System;                      
using System.Collections.Generic;    
using System.Threading.Tasks;         
>>>>>>> Stashed changes
namespace GroepsProject1._3Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentRepository _repository;
        private readonly IAuthenticationService _authentication;
        public AppointmentController(IAppointmentRepository repository, IAuthenticationService authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [HttpGet("GetAllAppointments")]
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            //todo userId ophalen
            string userId = null;
=======
        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            Guid userId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
>>>>>>> Stashed changes
=======
        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            Guid userId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
>>>>>>> Stashed changes
            return await _repository.GetAllAppointmentsAsync(userId);
        }

        [HttpPost("CreateAppointment")]
        public async Task<ActionResult> CreateAppointmentAsync(Appointment appointment)
        {
            appointment.UserId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
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
