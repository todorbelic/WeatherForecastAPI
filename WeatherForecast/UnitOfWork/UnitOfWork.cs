using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.Repository;

namespace WeatherForecastAPI.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        public IGenericRepository<City, int> CityRepository { get; set; }
        public IGenericRepository<WeatherForecast, int> WeatherForecastRepository { get; set; }

        public UnitOfWork(DbContext dbContext, IGenericRepository<City, int> cityRepository,
                                               IGenericRepository<WeatherForecast, int> weatherForecastRepository)
        {
            _dbContext = dbContext;
            CityRepository = cityRepository;
            WeatherForecastRepository = weatherForecastRepository;
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
