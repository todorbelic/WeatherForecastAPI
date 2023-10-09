using WeatherForecastAPI.Model;
using WeatherForecastAPI.Repository;

namespace WeatherForecastAPI.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IGenericRepository<City, int> CityRepository { get; }
        public IGenericRepository<WeatherForecast, int> WeatherForecastRepository { get; }
        Task CompleteAsync();
    } 
}
