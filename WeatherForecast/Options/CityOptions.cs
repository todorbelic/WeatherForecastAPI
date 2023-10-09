using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Options
{
    public class CityOptions
    {
        public string GeocodingApiUrl { get; init; }
        public string ApiKey { get; init; }
        public List<City> AvailableCities { get; init; }
    }
}
