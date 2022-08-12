using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace NewLake.Api.Infrastructure.Services
{
    public abstract class MessageServiceBase
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;
        protected readonly QueueSettings _queueSettings;

        protected MessageServiceBase(IOptions<QueueSettings> options)
        {            
            _queueSettings = options.Value;

            _factory = new ConnectionFactory()
            {
                HostName = _queueSettings.HostName
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ConfirmSelect();
        }
    }
}
