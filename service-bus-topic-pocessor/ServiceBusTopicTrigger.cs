using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ServiceBusTopicProcessor.Function
{
    public class ServiceBusTopicTrigger
    {
        private readonly ILogger<ServiceBusTopicTrigger> _logger;

        public ServiceBusTopicTrigger(ILogger<ServiceBusTopicTrigger> log)
        {
            _logger = log;
        }

        [FunctionName("ServiceBusTopicTrigger")]
        public void Run([ServiceBusTrigger("weather-forcast-added", "send-email", Connection = "servicebuspractisenamespace_SERVICEBUS")] string mySbMsg)
        {
            Console.WriteLine($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
