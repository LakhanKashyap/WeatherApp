using Microsoft.EntityFrameworkCore;
using WeatherApp.Api.Data;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext _dbContext;

        //constructor
        public WeatherRepository(WeatherDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddWeatherRecordAsync(WeatherRecord weatherRecord)
        {
            _dbContext.WeatherRecords.Add(weatherRecord);
            await _dbContext.SaveChangesAsync();
        }

        //Gets all weather records from the database and returns them as a list of WeatherRecord objects.
        public async Task<List<WeatherRecord>> GetWeatherRecordsAsync()
        {
            return await _dbContext.WeatherRecords.AsNoTracking().ToListAsync();
        }
    }
    
}
