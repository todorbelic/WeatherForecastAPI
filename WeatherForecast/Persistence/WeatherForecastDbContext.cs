using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Options;
using WeatherForecastAPI.Persistence.DataSeed;

namespace WeatherForecastAPI.Persistence
{
    public class WeatherForecastDbContext : DbContext
    {
        private readonly CityOptions cityOptions;
        public WeatherForecastDbContext(DbContextOptions options, IOptions<CityOptions> cityOptions) : base(options) 
        {
            this.cityOptions = cityOptions.Value;
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected async override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.SeedCities(cityOptions);
            base.OnModelCreating(builder);
        }
    }
}
