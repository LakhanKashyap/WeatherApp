using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherApp.Api.Models;

namespace WeatherApp.Api.Data
{
    public class WeatherRecordConfiguration : IEntityTypeConfiguration<WeatherRecord>
    {
        public void Configure(EntityTypeBuilder<WeatherRecord> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Latitude)
                   .HasPrecision(9, 6);

            builder.Property(w => w.Longitude)
                   .HasPrecision(9, 6);

            builder.Property(w => w.Temperature)
                   .HasPrecision(5, 2);

            builder.Property(w => w.WindSpeed)
                   .HasPrecision(5, 2);

            builder.Property(w => w.RecordedAt)
                   .IsRequired();
        }

    }
}
