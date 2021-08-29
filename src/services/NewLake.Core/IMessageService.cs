using System;
namespace NewLake.Core
{
    public interface IMessageService<TMessage>
    {
        void Publish(TMessage message);
    }
}
