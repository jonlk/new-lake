using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NewLake.Queue.Listener
{
    public class QueueListenerService : BackgroundService
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        private readonly ILogger<QueueListenerService> _logger;

        public QueueListenerService(ILogger<QueueListenerService> logger)
        {
            _logger = logger;
            _queueName = "local-task-queue";
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            _factory = new ConnectionFactory()
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            _channel.BasicQos(0, 1, false);

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(_channel);

            _logger.LogInformation($"Started listening on Queue: {_queueName}");

            consumer.Received += async (bc, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                try
                {
                    _logger.LogInformation($"Receiving message: {message}");

                    await Task.Delay(5000, stoppingToken); // simulate an async  process

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception e)
                {
                    _logger.LogError(default, e, e.Message);
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

            await Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _connection.Close();
            _logger.LogInformation($"Stopped listening on Queue: {_queueName}");
            await base.StopAsync(cancellationToken);
        }
    }
}