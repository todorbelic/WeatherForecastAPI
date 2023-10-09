using WeatherForecastAPI.DTO;

namespace WeatherForecastAPI.Service
{
    public interface IWeatherForecastService
    {
        Task SyncWeatherDataAndSave();
        Task<List<AverageTemperatureResponseDTO>> GetAverageTemperaturesByCities(GetAverageTemperaturesRequestDTO avgTemperaturesRequest);
    }
}
