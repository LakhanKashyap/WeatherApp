namespace WeatherApp.Api.Models
{
    public class WeatherRecord
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Temperature { get; set; }

        public int Humidity { get; set; }

        public double WindSpeed { get; set; }

        public int WeatherCode { get; set; }

        public DateTime RecordedAt { get; set; }
    }
}
