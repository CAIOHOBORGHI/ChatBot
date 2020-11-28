using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Chat.Worker.RabbitMQ;
using Microsoft.Extensions.Configuration;

namespace Chat.Worker
{
    class Program
    {
        static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");
            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                var builder =
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                IConfigurationRoot configuration = builder.Build();
                rabbitConnection = configuration.GetConnectionString("RabbitConnectionString");
            }

            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                Console.WriteLine("Rabbit`s connection string is required!");
                return;
            }

            BotUsersQueueProducer producer = new BotUsersQueueProducer(rabbitConnection);
            UserBotQueueConsumer consumer = new UserBotQueueConsumer(rabbitConnection, producer);
            consumer.WaitForStockCode();
            await Task.Delay(Timeout.Infinite, Cts.Token).ConfigureAwait(false);
        }
    }
}
