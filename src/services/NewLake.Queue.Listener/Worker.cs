public class QueueListenerService : BackgroundService
{
    private ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    private readonly ILogger<QueueListenerService> _logger;
    private readonly QueueSettings _queueSettings;

    public QueueListenerService(
        IOptions<QueueSettings> options,
        ILogger<QueueListenerService> logger)
    {
        _queueSettings = options.Value;
        _logger = logger;
    }

    public async override Task StartAsync(CancellationToken cancellationToken)
    {
        _factory = new ConnectionFactory()
        {
            HostName = _queueSettings.HostName,
            DispatchConsumersAsync = true
        };

        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _queueSettings.QueueName,
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

        _logger.LogInformation($"Started listening on Queue: {_queueSettings.QueueName}");

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

        _channel.BasicConsume(queue: _queueSettings.QueueName,
                                autoAck: false,
                                consumer: consumer);

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _connection.Close();
        _logger.LogInformation($"Stopped listening on Queue: {_queueSettings.QueueName}");
        await base.StopAsync(cancellationToken);
    }
}
