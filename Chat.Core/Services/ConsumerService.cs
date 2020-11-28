using System;
using System.Text.Json;
using Chat.Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Chat.Core.Services
{
    public class ConsumerService : IConsumerService
    {
        private IConnectionFactory _connectionFactory;
        public ConsumerService(string connectionString)
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri(connectionString) };
        }

        public void Consume<T>(string queue, Action<T> execute)
        {
            using IConnection connection = _connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                byte[] body = e.Body.ToArray();
                T queueObject = JsonSerializer.Deserialize<T>(body);
                execute(queueObject);
            };
            Console.ReadLine();
        }
    }
}