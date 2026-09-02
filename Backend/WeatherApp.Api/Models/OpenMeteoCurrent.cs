using System.Text.Json.Serialization;   

namespace WeatherApp.Api.Models
{
    public class OpenMeteoCurrent
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature2m { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int RelativeHumidity2m { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double Windspeed10m { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }

    }
}
