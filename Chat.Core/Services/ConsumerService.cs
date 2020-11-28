using System;
using System.Text.Json;
using Chat.Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace Chat.Core.Services
{
    public class ConsumerService : IConsumerService, IDisposable
    {
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public ConsumerService(string connectionString)
        {
            _connectionFactory = new ConnectionFactory
            {
                Uri = new Uri(connectionString)
            };
        }

        public void Consume<T>(string queue, Action<T> execute)
        {
            Console.WriteLine("Starting connection...");
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            Console.WriteLine("Connection started!");
            _channel.QueueDeclare(queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            Console.WriteLine("Queue declared!");
            EventingBasicConsumer consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) =>
            {
                byte[] body = e.Body.ToArray();
                T queueObject = JsonSerializer.Deserialize<T>(body);
                Console.WriteLine("Message received -> " + queueObject.ToString());
                execute(queueObject);
            };

            consumer.Registered += OnConsumerRegistered;
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCanceled;
            _channel.BasicConsume(queue, true, consumer);
            Console.WriteLine("Waiting for message...");
            //Console.ReadLine();
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }

        private void OnConsumerCanceled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }

    }
}