namespace WeatherForecastAPI.Exceptions
{
    public class CityNotFoundException : Exception
    {
        public CityNotFoundException(int id) : base($"City with {id} id is not found") { }
    }
}
