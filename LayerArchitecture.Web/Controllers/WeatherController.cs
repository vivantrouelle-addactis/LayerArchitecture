using Microsoft.AspNetCore.Mvc;

namespace LayerArchitecture.Web.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get()
        {
            return Ok("Sunny");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(void), 404)]
        public IActionResult Get(string id)
        {
            if (id == "Lyon")
            {
                return Ok("Sunny");
            }
            else if (id == "Angers")
            {
                return Ok("Cloud");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
