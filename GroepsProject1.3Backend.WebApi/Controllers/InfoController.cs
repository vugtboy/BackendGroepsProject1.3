using Microsoft.AspNetCore.Mvc;

namespace GroepsProject1._3Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IInfoRepository _repository;

        public InfoController(IInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAllInfo")]
        IEnumerable<Info> GetAllInfo()
        {
            return null;
        }
    }
}
