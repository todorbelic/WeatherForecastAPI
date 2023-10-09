namespace WeatherForecastAPI.DTO
{
    public class CityResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ZipCode { get; set; }

        public string CountryCode { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }
    }
}
