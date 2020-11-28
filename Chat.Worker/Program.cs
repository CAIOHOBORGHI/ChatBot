using System;

namespace Chat.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Implement appSettings.json
            string rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");

            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                Console.WriteLine("Rabbit`s connection string is required!");
                return;
            }

            BotUsersQueueProducer producer = new BotUsersQueueProducer(rabbitConnection);
            UserBotQueueConsumer consumer = new UserBotQueueConsumer(rabbitConnection, producer);
            consumer.WaitForStockCode();
        }
    }
}
