using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Service;

namespace WeatherForecastAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        public CityController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        /// <summary>
        /// Returns list of available cities for weather forecast
        /// </summary>
        /// <response code="200">Request successful, list of cities returned</response>
        ///
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("getCities")]
        public ActionResult<List<CityResponseDTO>> GetCities()
        {
            return Ok(cityService.GetAllCities());
        }
    }
}
