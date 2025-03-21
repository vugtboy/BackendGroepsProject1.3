using Microsoft.AspNetCore.Mvc;

namespace GroepsProject1._3Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoController : ControllerBase
    {

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
