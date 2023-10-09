using AutoMapper;
using Microsoft.Extensions.Options;
using WeatherForecastAPI.ACL;
using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Exceptions;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.OpenWeatherAPIClient;
using WeatherForecastAPI.Options;
using WeatherForecastAPI.UnitOfWork;

namespace WeatherForecastAPI.Service
{
    public class WeatherForecastService : IWeatherForecastService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICityService cityService;
        private readonly WeatherDataSyncOptions weatherDataSyncOptions;
        private readonly ILogger<WeatherForecastService> logger;

        public WeatherForecastService(IUnitOfWork unitOfWork, IOptions<WeatherDataSyncOptions> weatherDataSyncOptions, 
                                      IMapper mapper, ICityService cityService, ILogger<WeatherForecastService> logger)
        {
            this.unitOfWork = unitOfWork;
            this.weatherDataSyncOptions = weatherDataSyncOptions.Value;
            this.cityService = cityService;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task SyncWeatherDataAndSave()
        {
            InvalidateOlderWeatherForecasts();
            List<City> availableCities = unitOfWork.CityRepository.GetAll().ToList();
            logger.LogInformation("Starting weather data synchronization and save process.");
            foreach (var city in availableCities)
            {
                WeatherForecastResponseDTO? weatherDataResponse = WeatherDataFetcher.GetWeatherData(weatherDataSyncOptions, city);
                if(weatherDataResponse == null)
                {
                    continue;
                }
                foreach (var weatherData in weatherDataResponse.List)
                {
                    var weatherForecast = mapper.Map<WeatherForecast>(weatherData);
                    weatherForecast.City = city;
                    weatherForecast.UtcOffset = weatherDataResponse.City.Timezone;
                    weatherForecast.UpToDate = true;
                    await unitOfWork.WeatherForecastRepository.Add(weatherForecast);
                }
            }
            await unitOfWork.CompleteAsync();
            logger.LogInformation("Weather data synchronization and save process completed successfully.");
        }

        public async Task<List<AverageTemperatureResponseDTO>> GetAverageTemperaturesByCities(GetAverageTemperaturesRequestDTO avgTemperaturesRequest)
        {
            logger.LogInformation("Fetching average temperatures by cities.");
            if (avgTemperaturesRequest.CityIds != null)
            {
                foreach (var cityId in avgTemperaturesRequest.CityIds)
                {
                    if (await cityService.GetCityById(cityId) == null)
                    {
                        logger.LogWarning($"City with id: {cityId} not found");
                        throw new CityNotFoundException(cityId);
                    }
                }     
            }

            DateTime timeStampNow = DateTime.Now;
            if(avgTemperaturesRequest.StartTime < timeStampNow 
               || avgTemperaturesRequest.EndTime < timeStampNow)
            {
                logger.LogWarning("Invalid date(s)");
                throw new InvalidDateException("Date can't be in past!");
            }

            DateTime timeStampMaxForecasted = timeStampNow.AddDays(weatherDataSyncOptions.DaysForecasted);
            if (avgTemperaturesRequest.StartTime > timeStampMaxForecasted 
               || avgTemperaturesRequest.EndTime > timeStampMaxForecasted)
            {
                logger.LogWarning("Date(s) exceed the maximum forecastable date.");
                throw new InvalidDateException($"Data is forecasted only {weatherDataSyncOptions.DaysForecasted} days in advance!");
            }

            var query = from weatherForecast in unitOfWork.WeatherForecastRepository.GetAll()
                        join city in unitOfWork.CityRepository.GetAll() on weatherForecast.City.Id equals city.Id

                        where weatherForecast.ForecastTime.AddSeconds(weatherForecast.UtcOffset + weatherDataSyncOptions.SyncFrequencyHours * 3600) > avgTemperaturesRequest.StartTime
                            && weatherForecast.ForecastTime.AddSeconds(weatherForecast.UtcOffset) <= avgTemperaturesRequest.EndTime
                            && weatherForecast.UpToDate == true
                            && (avgTemperaturesRequest.CityIds.Contains(city.Id) || avgTemperaturesRequest.CityIds == null)

                        group weatherForecast by weatherForecast.City.Name into g
                        orderby avgTemperaturesRequest.Ascending ? g.Average(weatherForecast => weatherForecast.Temperature) : -g.Average(weatherForecast => weatherForecast.Temperature)
                        select new AverageTemperatureResponseDTO(g.Average(weatherForecast => weatherForecast.Temperature), g.Key);

            logger.LogInformation("Average temperatures by cities fetched successfully.");
            return query.ToList();
        }

        private void InvalidateOlderWeatherForecasts()
        {
            logger.LogInformation("Invalidating older weather forecasts.");

            List<WeatherForecast> weatherForecasts = unitOfWork.WeatherForecastRepository.GetAll().ToList();
            foreach (var weatherForecast in weatherForecasts)
            {
                if (weatherForecast.UpToDate)
                {
                    weatherForecast.UpToDate = false;
                    unitOfWork.WeatherForecastRepository.Update(weatherForecast);
                }
            }
            logger.LogInformation("Older weather forecasts invalidated successfully.");
        }
    }
}
