using CsvHelper;
using CsvHelper.Configuration;
using LayerArchitecture.Web.Model;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace LayerArchitecture.Web.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
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

        [HttpGet]
        [Produces(MediaTypeNames.Text.Csv)]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        public IActionResult ExportToCsv()
        {
            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8
            };

            using MemoryStream memoryStream = new MemoryStream();
            
            using (var streamWriter = new StreamWriter(memoryStream, leaveOpen: true))
            {
                using(var csvWriter = new CsvWriter(streamWriter, csvConfiguration))
                {
                    csvWriter.WriteRecords(new List<Weather>
                    {
                        new Weather { City = "Lyon", Meteorology = "Sunny" },
                        new Weather { City = "Angers", Meteorology = "Cloud" }
                    });
                }
            }

            return File(memoryStream.ToArray(), "text/csv", "weather.csv");
        }

        [HttpPost("import")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult ImportFromCsv(IFormFile file)
        {
           if (file == null || file.Length == 0)
           {
               return BadRequest("No file uploaded");
           }

            // Read the CSV file 
            using StreamReader streamReader = new StreamReader(file.OpenReadStream());

            using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                Encoding = Encoding.UTF8
            }))
            {
                List<Weather> records = csvReader.GetRecords<Weather>().ToList();
                
                return Ok($"Imported {records.Count} records");
            }
        }
    }
}
