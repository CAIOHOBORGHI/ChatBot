using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Chat.Web.RabbitMQ
{
    public class RabbitService
    {
        private string _connectionString;
        private IConnectionFactory _connectionFactory;
        public RabbitService(IConfiguration configuration)
        {
            /* 
                Tries to get connection from envinronment variables(docker) or appSettings if you`re not using docker
             */
            _connectionString = configuration["RabbitConnectionString"] ?? configuration.GetConnectionString("RabbitConnectionString");
            _connectionFactory = new ConnectionFactory { Uri = new Uri(_connectionString) };
        }

        public void Consume<T>(string queue, Action<T> execute)
        {
            using IConnection connection =  _connectionFactory.CreateConnection();
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
