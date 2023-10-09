namespace WeatherForecastAPI.ACL
{
    public class WeatherForecastResponseDTO
    {
        public List<WeatherForecastDataDTO> List { get; set; }
        public CityDTO City { get; set; }
    }
}
