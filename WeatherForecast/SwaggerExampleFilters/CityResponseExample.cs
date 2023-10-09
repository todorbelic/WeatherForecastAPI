using Swashbuckle.AspNetCore.Filters;
using WeatherForecastAPI.DTO;

namespace WeatherForecastAPI.SwaggerExampleFilters
{
    public class CityResponseExample : IExamplesProvider<List<CityResponseDTO>>
    {
        public List<CityResponseDTO> GetExamples()
        {
            List<CityResponseDTO> examples = new List<CityResponseDTO>
        {
            new CityResponseDTO
            {
                Id = 1,
                Name = "Beograd",
                ZipCode = "21000",
                CountryCode = "RS",
                Longitude = 20.4567m,
                Latitude = 44.7890m
            },
            new CityResponseDTO
            {
                Id = 2,
                Name = "Zrenjanin",
                ZipCode = "23000",
                CountryCode = "RS",
                Longitude = 20.3889m,
                Latitude = 45.3812m
            }
        };
            return examples;
        }
    }
}
