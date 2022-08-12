namespace NewLake.Api.Infrastructure.Services
{
    public interface IMessageService<TMessage>
    {
        void Publish(TMessage message);
    }
}
