using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Api.DTOs;
using WeatherApp.Api.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        //constructor injection
        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current")]
        public async Task<ActionResult<WeatherResponse>> GetWeatherFromOpenMeteo(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                return BadRequest("Invalid latitude. Latitude must be between -90 and 90.");
            }

            if (longitude < -180 || longitude > 180)
            {
                return BadRequest("Invalid longitude. Longitude must be between -180 and 180.");
            }

            try
            {
                return await _weatherService.GetWeatherFromOpenMeteo(latitude, longitude);
            }
            catch (HttpRequestException ex)
            {
                // Return 502 when the external weather API fails
                return StatusCode(502, ex.Message);
            }
        }
    }
}
