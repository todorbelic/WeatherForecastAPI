using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Options
{
    public class WeatherDataSyncOptions
    {
        public string OpenWeatherApiUrl { get; init; }
        public string ApiKey { get; init; }
        public int DaysForecasted { get; init; }
        public int SyncFrequencyHours { get; init; }
    }
}
