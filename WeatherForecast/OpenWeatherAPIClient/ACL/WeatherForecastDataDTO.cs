using Newtonsoft.Json;
using WeatherForecastAPI.Converters;

namespace WeatherForecastAPI.ACL
{
    public class WeatherForecastDataDTO
    {
        public long dt;
        [JsonConverter(typeof(UnixTimestampConverter))] // Apply the custom converter
        public DateTime ForecastTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(dt).UtcDateTime;
            }
            set
            {
                dt = new DateTimeOffset(value).ToUnixTimeSeconds();
            }
        }
        public WeatherForecastTemperatureDTO Main { get; set; }
    }
}
