using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using WeatherForecastAPI.Converters;
using WeatherForecastAPI.Mapper;
using WeatherForecastAPI.Middleware;
using WeatherForecastAPI.Model;
using WeatherForecastAPI.OpenWeatherAPIClient;
using WeatherForecastAPI.Options;
using WeatherForecastAPI.Persistence;
using WeatherForecastAPI.Repository;
using WeatherForecastAPI.Service;
using WeatherForecastAPI.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
}); 
builder.Services.AddEndpointsApiExplorer();

// Configure swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Weather Forecast API",
        Version = "v1",
        Description = "This API offers endpoints to display available cities, " +
        "average temperatures for chosen cities within a specified time range (up to 5 days in advance), " +
        "and the ability to sort cities by their average temperatures. The application is " +
        "implemented using .NET C# and ASP.NET Core.",
        Contact = new OpenApiContact() { Name = "Todor Belic", Email = "belictodor@gmail.com" },

    });
    options.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("10/10/2023 14:00")
    });
    options.ExampleFilters();
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

// Configure dependency injection
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<IGenericRepository<City, int>, GenericRepository<City, int>>();
builder.Services.AddScoped<IGenericRepository<WeatherForecast, int>, GenericRepository<WeatherForecast, int>>();
builder.Services.AddScoped<DbContext, WeatherForecastDbContext>();
builder.Services.AddTransient<ExceptionMiddleware>();

//Configure background service
builder.Services.AddHostedService<WeatherDataSyncService>();

// Configure auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure Database
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
builder.Services.AddSingleton<AuditingInterceptor>();
builder.Services.AddDbContext<WeatherForecastDbContext>((sp, options) =>
{
    var auditingInterceptor = sp.GetService<AuditingInterceptor>();
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")).AddInterceptors(auditingInterceptor);
}
);

// City Options
builder.Services.AddOptions<CityOptions>().BindConfiguration(nameof(CityOptions));

// Weather Data Sync Options
builder.Services.AddOptions<WeatherDataSyncOptions>().BindConfiguration(nameof(WeatherDataSyncOptions));

// Configure Logger
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Forecast API v1");
    });
}

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseRouting();

app.MapControllers();

app.Run();