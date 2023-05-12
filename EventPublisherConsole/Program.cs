using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace EventPublisherConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile($"appsettings.json", true, true)
               .AddJsonFile($"appsettings.Development.json", true, true);

            var config = builder.Build();

            var _eventHubProducerClient = new EventHubProducerClient(config["eventHubConnectionString"], eventHubName: "source-hub");

            for (int i = 1; i <= 10; i++)
            {
                var test = new Message() { CaseId = i, ProductId = "Product" + i };

                // send using event hub producer client
                var testEvent = new Event()
                {
                    Source = "Testing",
                    Data = test
                };

                // Create a batch of events 
                var eventData = new EventData(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(testEvent)));
                try
                {
                    await _eventHubProducerClient.SendAsync(new List<EventData> { eventData }, CancellationToken.None);
                    Console.WriteLine($"Sent public event {i} to Event Hub successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send public events to Event Hub." + ex);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}