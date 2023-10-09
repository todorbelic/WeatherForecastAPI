using Newtonsoft.Json;
using RestSharp;
using WeatherForecastAPI.ACL;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Options;

namespace WeatherForecastAPI.OpenWeatherAPIClient
{
    public static class CityCoordinatesFetcher
    {
        public static CityCoordinatesDTO? GetCityCoordinates(CityOptions cityOptions, City city)
        {
            string geocodingApiUrl = cityOptions.GeocodingApiUrl
                                       .Replace("{city name}", city.Name)
                                       .Replace("{country code}", city.CountryCode)
                                       .Replace("{limit}", "1")
                                       .Replace("{API key}", cityOptions.ApiKey);

            CityCoordinatesDTO coordinatesResponse;
            try
            {
                var client = new RestClient(geocodingApiUrl);
                var response = client.Get(new RestRequest());
                if (response.IsSuccessful)
                    coordinatesResponse = JsonConvert.DeserializeObject<List<CityCoordinatesDTO>>(response.Content).First();
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return coordinatesResponse;
        }
    }
}
