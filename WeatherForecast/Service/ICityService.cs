using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Service
{
    public interface ICityService
    {
        List<CityResponseDTO> GetAllCities();
        Task<City> GetCityById(int id);
    }
}
