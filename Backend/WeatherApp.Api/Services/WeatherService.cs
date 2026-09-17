using WeatherApp.Api.DTOs;
using WeatherApp.Api.Models;
using WeatherApp.Api.Repositories;

namespace WeatherApp.Api.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherService(HttpClient httpClient, IWeatherRepository weatherRepository)
        {
            _httpClient = httpClient;
            _weatherRepository = weatherRepository;
        }

        public string GetWeather()
        {
            return "Weather Service is working";
        }

        //Get Current Weather from Open-Meteo API and save it to the database
        public async Task<WeatherResponse> GetWeatherFromOpenMeteo(double latitude, double longitude)
        {
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException($"Open-Meteo request failed. Status Code: {(int)response.StatusCode}. Details: {errorMessage}");
            }

            // Deserialize Open-Meteo JSON into our C# model
            var weatherData = await response.Content.ReadFromJsonAsync<OpenMeteoResponse>();

            // Get the current weather section from the Open-Meteo response
            var currentWeather = weatherData.Current;

            // Extract the weather values we need from 'currentWeather'
            var temperature = currentWeather.Temperature2m;
            var humidity = currentWeather.RelativeHumidity2m;
            var windSpeed = currentWeather.Windspeed10m;
            var weatherCode = currentWeather.WeatherCode;

            //Creates a new C# object representing one database record.
            var weatherRecord = new WeatherRecord
            {
                Latitude = weatherData.Latitude,
                Longitude = weatherData.Longitude,
                Temperature = temperature,
                Humidity = humidity,
                WindSpeed = windSpeed,
                WeatherCode = weatherCode,
                RecordedAt = DateTime.UtcNow,
            };

            //Save the weather record to the database through the repository
            await _weatherRepository.AddWeatherRecordAsync(weatherRecord);

            // Map external API data into our own response DTO (convert Open-Meteo's data into the format our WeatherApp exposes)
            // This is Object Initialization syntax in C# to create a new WeatherResponse object and populate its properties
            var weatherResponse = new WeatherResponse
            {
                Latitude = weatherData.Latitude,
                Longitude = weatherData.Longitude,
                Temperature = temperature,
                Humidity = humidity,
                WindSpeed = windSpeed,
                WeatherCode = weatherCode
            };

            return weatherResponse;
        }

        // Get all weather records from the database
        public async Task<List<WeatherRecord>> GetWeatherRecordsAsync()
        {
            return await _weatherRepository.GetWeatherRecordsAsync();
        }
    }
}
