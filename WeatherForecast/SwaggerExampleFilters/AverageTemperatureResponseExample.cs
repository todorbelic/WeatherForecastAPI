using Swashbuckle.AspNetCore.Filters;
using WeatherForecastAPI.DTO;

namespace WeatherForecastAPI.SwaggerExampleFilters
{
    public class AverageTemperatureResponseExample : IExamplesProvider<List<AverageTemperatureResponseDTO>>
    {
        public List<AverageTemperatureResponseDTO> GetExamples()
        {
            List<AverageTemperatureResponseDTO> examples = new List<AverageTemperatureResponseDTO>
            {
                new AverageTemperatureResponseDTO(17.32m, "Beograd"),
                new AverageTemperatureResponseDTO(15.12m, "Zrenjanin")
            };
            return examples;
        }
    }
}
