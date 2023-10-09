using Newtonsoft.Json;
using RestSharp;
using WeatherForecastAPI.ACL;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Options;

namespace WeatherForecastAPI.OpenWeatherAPIClient
{
    public static class WeatherDataFetcher
    {
        public static WeatherForecastResponseDTO? GetWeatherData(WeatherDataSyncOptions weatherDataSyncOptions, City city)
        {
            string openWeatherApiUrl = weatherDataSyncOptions.OpenWeatherApiUrl
                                           .Replace("{longitude}", city.Longitude.ToString())
                                           .Replace("{latitude}", city.Latitude.ToString())
                                           .Replace("{API key}", weatherDataSyncOptions.ApiKey);

            WeatherForecastResponseDTO weatherDataResponse;
            try
            {
                var client = new RestClient(openWeatherApiUrl);
                var response = client.Get(new RestRequest());
                if (response.IsSuccessful)
                    weatherDataResponse = JsonConvert.DeserializeObject<WeatherForecastResponseDTO>(response.Content);
                else
                    return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return weatherDataResponse;
        }
    }
}
