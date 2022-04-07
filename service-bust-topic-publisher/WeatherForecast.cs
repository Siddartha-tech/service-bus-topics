namespace service_bust_topic_publisher;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public class WeatherForecastAdded
{
    public Guid Id { get; set; }
    public DateTime CeatedDateTime { get; set; }
    public DateTime ForDate { get; set; }
}
