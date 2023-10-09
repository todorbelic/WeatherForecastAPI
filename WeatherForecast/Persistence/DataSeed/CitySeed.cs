using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.ACL;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.OpenWeatherAPIClient;
using WeatherForecastAPI.Options;

namespace WeatherForecastAPI.Persistence.DataSeed
{
    public static class CitySeed
    {
        public static void SeedCities(this ModelBuilder modelBuilder, CityOptions cityOptions)
        {

            int cityId = 1;
            foreach (var city in cityOptions.AvailableCities)
            {
                CityCoordinatesDTO? coordinatesResponse = CityCoordinatesFetcher.GetCityCoordinates(cityOptions, city);
                DateTime timeStamp = DateTime.UtcNow;
                modelBuilder.Entity<City>().HasData(
                    new City() {Id = cityId++, Name = city.Name, ZipCode = city.ZipCode, 
                                CountryCode = city.CountryCode, CreatedDate = timeStamp, 
                                ModifiedDate = timeStamp, Latitude = coordinatesResponse.Lat,
                                Longitude = coordinatesResponse.Lon}
                );
            }
        }
    }
}
