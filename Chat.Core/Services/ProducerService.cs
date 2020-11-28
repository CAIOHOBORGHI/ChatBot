using System;
using System.Text;
using System.Text.Json;
using Chat.Core.Interfaces;
using RabbitMQ.Client;

namespace Chat.Core.Services
{
    public class ProducerService : IProducerService
    {
        private IConnectionFactory _connectionFactory;
        public ProducerService(string connectionString)
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri(connectionString) };
        }

        public void Produce<T>(string queue, T message)
        {
            using IConnection connection = _connectionFactory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue,
               durable: true,
               exclusive: false,
               autoDelete: false,
               arguments: null
           );
            byte[] body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            channel.BasicPublish("", queue, null, body);
        }
    }
}