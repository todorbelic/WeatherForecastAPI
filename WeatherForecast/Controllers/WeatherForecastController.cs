using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Service;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService weatherForecastService;
        public WeatherForecastController(IWeatherForecastService weatherForecastService)
        {
            this.weatherForecastService = weatherForecastService;
        }

        /// <summary>
        /// Returns average temperatures by requested cities for a requested date range,
        /// if cities are not provided, average temperature will be calculated for all available cities.
        /// By default, temperatures are sorted in descending order in Celsius unit.
        /// Average temperatures are calculated for city local time.
        /// </summary>
        /// <response code="200">Request successful, list of average temperatures by requested cities are returned</response>
        /// <response code="404">City couldn't be found for a requested id</response>
        /// <response code="400">Invalid inputs</response>
        ///
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("getAverageTemperatures")]
        public async Task<ActionResult<List<AverageTemperatureResponseDTO>>> GetAverageTemperatures([FromQuery] GetAverageTemperaturesRequestDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await weatherForecastService.GetAverageTemperaturesByCities(dto));
        }
    }
}
