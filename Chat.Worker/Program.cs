using System;
using Chat.Worker.RabbitMQ;

namespace Chat.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: Implement appSettings.json
            // string rabbitConnection = Environment.GetEnvironmentVariable("RabbitConnectionString");
            string rabbitConnection = "amqp://jobsity:Jobsity2020@localhost:5672";
            if (string.IsNullOrWhiteSpace(rabbitConnection))
            {
                Console.WriteLine("Rabbit`s connection string is required!");
                return;
            }

            Console.WriteLine($"Using rabbit connection: {rabbitConnection}");

            BotUsersQueueProducer producer = new BotUsersQueueProducer(rabbitConnection);
            UserBotQueueConsumer consumer = new UserBotQueueConsumer(rabbitConnection, producer);
            consumer.WaitForStockCode();
        }
    }
}
