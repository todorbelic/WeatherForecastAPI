using AutoMapper;
using WeatherForecastAPI.ACL;
using WeatherForecastAPI.DTO;
using WeatherForecastAPI.Model;

namespace WeatherForecastAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityResponseDTO>().ReverseMap();
            CreateMap<WeatherForecast, WeatherForecastDataDTO>().ReverseMap()
                                                                .ForMember(weatherData => weatherData.Temperature,
                                                                weatherDataDto => weatherDataDto.MapFrom(src => src.Main.Temp));
        }
    }
}
