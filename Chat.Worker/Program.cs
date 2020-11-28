using System;
using System.Threading;
using System.Threading.Tasks;
using Chat.Worker.RabbitMQ;

namespace Chat.Worker
{
    class Program
    {
        static readonly CancellationTokenSource Cts = new CancellationTokenSource();
        static async Task Main(string[] args)
        {
            //TODO: Implement appSettings.json
            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");

            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                Console.WriteLine("Rabbit`s connection string is required!");
                return;
            }

            Console.WriteLine($"Using rabbit connection: {rabbitConnection}");

            BotUsersQueueProducer producer = new BotUsersQueueProducer(rabbitConnection);
            UserBotQueueConsumer consumer = new UserBotQueueConsumer(rabbitConnection, producer);
            consumer.WaitForStockCode();
            await Task.Delay(Timeout.Infinite, Cts.Token).ConfigureAwait(false);
        }
    }
}
