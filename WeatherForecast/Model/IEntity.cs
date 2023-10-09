namespace WeatherForecastAPI.Model
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
