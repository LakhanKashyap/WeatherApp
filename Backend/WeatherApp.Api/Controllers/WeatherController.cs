using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("open-meteo")]
        public async Task<string> GetWeatherFromOpenMeteo()
        {
            return await _weatherService.GetWeatherFromOpenMeteo();
        }
    }
}
