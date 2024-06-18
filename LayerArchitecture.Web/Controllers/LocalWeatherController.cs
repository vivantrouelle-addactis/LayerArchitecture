using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LayerArchitecture.Web.Controllers
{
    [Route("api/weather/{id}")]
    [ApiController]
    public class LocalWeatherController : ControllerBase
    {
        [HttpGet("temperature")]
        public IActionResult GetTemperature(string id)
        {
            if (id == "Lyon")
            {
                return Ok("30°C");
            }
            else if (id == "Angers")
            {
                return Ok("25°C");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("rainfall")]
        public IActionResult GetRainFall(string id)
        {
            if (id == "Lyon")
            {
                return Ok("0mm");
            }
            else if (id == "Angers")
            {
                return Ok("30mm");
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
