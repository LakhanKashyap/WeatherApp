using Microsoft.EntityFrameworkCore;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.Data
{
    public class WeatherDbContext : DbContext
    {
        //constructor that takes in DbContextOptions and passes it to the base class constructor
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {

        }

        //DbSets
        public DbSet<WeatherRecord> WeatherRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //we are not doing this anymore because in future we will add more configuration classes for other entities,
            //for that, we need to apply all the config classes manually, which is repetitive/time taking. T
            //herefore, we will use the 'ApplyConfigurationsFromAssembly' method to automatically apply all the configuration classes in the assembly.

            //modelBuilder.ApplyConfiguration(new WeatherRecordConfiguration()); //code that we are not using anymore

            //It means "EF Core, go through the assembly where WeatherDbContext is located, find all the entity configuration classes,
            //and automatically apply those configurations to the database model."
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WeatherDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
