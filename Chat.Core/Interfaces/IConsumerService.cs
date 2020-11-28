using System;

namespace Chat.Core.Interfaces
{
    public interface IConsumerService : IDisposable
    {
        void Consume<T>(string queue, Action<T> execute);
    }
}