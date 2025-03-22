using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GroepsProject1._3Backend.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GroepsProject1._3Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly IInfoRepository _repository;
        private readonly IAuthenticationService _authentication;
        public InfoController(IInfoRepository repository, IAuthenticationService authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        [HttpGet("GetInfo")]
        public async Task<Info> GetInfo()
        {
            Guid userId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
            return await _repository.GetInfoAsync(userId);
        }

        [HttpPost("CreateInfo")]
        public async Task<ActionResult> CreateInfo(Info info)
        {
            info.UserId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
            await _repository.CreateInfoAsync(info);

            var createdInfo = await _repository.GetInfoAsync(info.UserId);

            //bad request als info niet bestaat
            if (createdInfo == null)
            {
                return BadRequest("Ja, mislukt, de info is niet goed aangemaakt :P");
            }
            //Goed response teruggeven als het gelukt is
            else
            {
                return CreatedAtAction(nameof(GetInfo), new { appointmentId = info.UserId }, info);
            }
        }

        [HttpPut("SaveInfo")]
        public async Task<ActionResult> SaveInfo(Info info)
        {
            info.UserId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
            await _repository.UpdateInfoAsync(info);
            return Created();
        }

        [HttpDelete("DeleteInfo")]
        public async Task<ActionResult> DeleteInfo()
        {
            Guid userId = new Guid(_authentication.GetCurrentAuthenticatedUserId());
            await _repository.DeleteInfoAsync(userId);
            return NoContent();
        }
    }
}
