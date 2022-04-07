using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
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

        [FunctionName("ServiceBusTopicTrigger1")]
        public void Run1([ServiceBusTrigger("weather-forcast-added", "update-report", Connection = "servicebuspractisenamespace_SERVICEBUS")] ServiceBusReceivedMessage mySbMsg)
        {
            //* Add action with rules which will maintain separate service Bus subscription queue for that *//
            // az servicebus topic subscription rule create --resource-group azure-practise-rg --namespace-name service-bus-practise-namespace --topic-name weather-forcast-added --subscription-name update-report --name TestName --action-sql-expression "set CustomProp='AAA'" --filter-sql-expression "Month='December'"
            // az servicebus topic subscription rule create --resource-group azure-practise-rg --namespace-name service-bus-practise-namespace --topic-name weather-forcast-added --subscription-name update-report --name TestName1 --action-sql-expression "set CustomProp='BBB'" --filter-sql-expression "Month like 'Ma%'"
            Console.WriteLine($"C# ServiceBus topic trigger 1 function processed message update-report: {JsonSerializer.Serialize(mySbMsg)}");
            _logger.LogInformation($"C# ServiceBus topic trigger 1 function processed message update-report: {JsonSerializer.Serialize(mySbMsg)}");
        }
    }
}
