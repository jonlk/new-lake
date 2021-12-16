using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace NewLake.Core.Services.Messaging
{
    public class MessageService<TMessage> :
        MessageServiceBase,
        IMessageService<TMessage>
    {
        private readonly ILogger<MessageService<TMessage>> _logger;
        private string _queueName;

        public MessageService(ILogger<MessageService<TMessage>> logger)
        {
            _logger = logger;
            _queueName = "local-task-queue";

            _channel.BasicAcks += (sender, ea) =>
            {
                _logger.LogInformation($"Message Tag: {ea.DeliveryTag} successfully queued at {DateTime.Now}");
            };

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Publish(TMessage message)
        {
            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            var properties = _channel.CreateBasicProperties();

            properties.Persistent = true;

            _logger.LogInformation($"Dispatching message: {message} with Tag: { _channel.NextPublishSeqNo}");

            _channel.BasicPublish(exchange: $"",
                                      routingKey: _queueName,
                                      basicProperties: properties,
                                      body: body);
        }
    }
}
