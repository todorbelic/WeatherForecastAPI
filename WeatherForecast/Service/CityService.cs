using AutoMapper;
using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.UnitOfWork;

namespace WeatherForecastAPI.Service
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<CityService> logger;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CityService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        }

        public List<CityResponseDTO> GetAllCities()
        {
            logger.LogInformation("Fetching all cities.");
            List<City> allCities = unitOfWork.CityRepository.GetAll().ToList();
            logger.LogInformation("All cities fetched successfully.");
            return (mapper.Map<List<CityResponseDTO>>(allCities));
        }

        public async Task<City> GetCityById(int id)
        {
            logger.LogInformation("Fetching city by ID: {CityId}", id);
            return await unitOfWork.CityRepository.GetByIdAsync(id);
        }
    }
}
