using System;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace NewLake.Core
{
    public class MessageService<TMessage> : IMessageService<TMessage>, IDisposable
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageService()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: "images",                
                type: ExchangeType.Topic,
                durable: true);
        }

        public void Publish(TMessage message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(jsonMessage);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "images",
                                      routingKey: $"images.a.convert.{message}",
                                      basicProperties: properties,
                                      body: body);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}
