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
        private readonly ILogger<WeatherController> _logger;

        //constructor injection
        public WeatherController(WeatherService weatherService, ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the current weather for the specified latitude and longitude.
        /// </summary>
        /// <param name="latitude">Latitude between -90 and 90.</param>
        /// <param name="longitude">Longitude between -180 and 180.</param>
        [HttpGet("current")]
        public async Task<ActionResult<WeatherResponse>> GetWeatherFromOpenMeteo(double? latitude, double? longitude)
        {
            //Logging Incoming Request
            _logger.LogInformation("Weather Request Received. Latitude: {Latitude}, Longitude: {Longitude}", latitude, longitude);

            //Check if latitude and longitude are provided
            if (latitude is null || longitude is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Missing parameters.",
                    Message = "Both latitude and longitude are required."
                });
            }

            //Check if latitude and longitude are within valid ranges
            if (latitude < -90 || latitude > 90)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Invalid latitude.",
                    Message = "Latitude must be between -90 and 90."
                });
            }

            if (longitude < -180 || longitude > 180)
            {
                return BadRequest(new ErrorResponse
                {
                    Error = "Invalid longitude.",
                    Message = "Longitude must be between -180 and 180."
                });
            }

            try
            {
                return await _weatherService.GetWeatherFromOpenMeteo(latitude.Value, longitude.Value);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogWarning(ex, "External weather API request failed.");

                // Return 502 when the external weather API fails
                return StatusCode(502, ex.Message);
            }
        }
    }
}
