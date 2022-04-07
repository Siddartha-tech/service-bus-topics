using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace service_bust_topic_publisher.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost]
    public async Task Post(WeatherForecast data)
    {
        WeatherForecastAdded message = new WeatherForecastAdded()
        {
            Id = Guid.NewGuid(),
            CeatedDateTime = DateTime.UtcNow,
            ForDate = data.Date
        };

        string connectionString = "Endpoint=sb://service-bus-practise-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mZYoWDW+dM/04AI9N9O8gKQ2FFroZckX6lImmrNktBw=";
        ServiceBusClient client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender("weather-forcast-added");
        string body = JsonSerializer.Serialize(message);
        ServiceBusMessage sbMessage = new ServiceBusMessage(body);
        sbMessage.ApplicationProperties.Add("Month", data.Date.ToString("MMMM"));//Added custom property to message
        await sender.SendMessageAsync(sbMessage);
    }
}
