namespace WeatherApp.Api.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetWeather()
        {
            return "Weather Service is working";
        }

        public async Task<string> GetWeatherFromOpenMeteo()
        {
            var url = "https://api.open-meteo.com/v1/forecast?latitude=28.6139&longitude=77.2090&current=temperature_2m,relative_humidity_2m,wind_speed_10m,weather_code";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return $"Open-Meteo request failed. Status Code: {(int)response.StatusCode}";
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
