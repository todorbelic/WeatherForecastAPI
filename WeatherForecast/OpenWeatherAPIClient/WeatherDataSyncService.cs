using Microsoft.Extensions.Options;
using WeatherForecastAPI.Options;
using WeatherForecastAPI.Service;

namespace WeatherForecastAPI.OpenWeatherAPIClient
{
    public class WeatherDataSyncService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<WeatherDataSyncService> logger;
        private readonly WeatherDataSyncOptions weatherDataSyncOptions;

        public WeatherDataSyncService(IServiceProvider serviceProvider, ILogger<WeatherDataSyncService> logger, 
                                      IOptions<WeatherDataSyncOptions> weatherDataSyncOptions)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.weatherDataSyncOptions = weatherDataSyncOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var scopedService = scope.ServiceProvider.GetRequiredService<IWeatherForecastService>();
                        await scopedService.SyncWeatherDataAndSave();
                    }
                    logger.LogInformation("Weather data synchronization and save process completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during weather data synchronization.");
                }
                await Task.Delay(TimeSpan.FromHours(weatherDataSyncOptions.SyncFrequencyHours), stoppingToken);
            }
        }
    }
}
