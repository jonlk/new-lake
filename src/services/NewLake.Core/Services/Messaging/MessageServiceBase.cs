using RabbitMQ.Client;

namespace NewLake.Core.Services.Messaging
{
    public abstract class MessageServiceBase
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        protected MessageServiceBase()
        {
            //TODO: Need to pass host in config options
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ConfirmSelect();          
        }
    }
}
