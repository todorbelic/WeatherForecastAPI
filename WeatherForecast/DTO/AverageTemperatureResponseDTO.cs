using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.DTO
{
    public class AverageTemperatureResponseDTO
    {
        public decimal AverageTemperature { get; set; }
        public string CityName { get; set; }

        public AverageTemperatureResponseDTO(decimal temperature, string cityName)
        {
            AverageTemperature = temperature;
            CityName = cityName;
        }
    }
}
