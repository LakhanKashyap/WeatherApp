using System.Text.Json.Serialization;

namespace WeatherApp.Api.Models
{
    public class OpenMeteoResponse
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        public OpenMeteoCurrent Current { get; set; }

    }
}
