namespace NewLake.Core.Services.Messaging
{
    public interface IMessageService<TMessage>
    {
        void Publish(TMessage message);
    }
}
