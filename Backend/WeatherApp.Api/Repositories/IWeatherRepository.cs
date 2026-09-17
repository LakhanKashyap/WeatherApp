using WeatherApp.Api.Models;

namespace WeatherApp.Api.Repositories
{
    public interface IWeatherRepository
    {
        Task AddWeatherRecordAsync(WeatherRecord weatherRecord);

        Task<List<WeatherRecord>> GetWeatherRecordsAsync();
    }
}
