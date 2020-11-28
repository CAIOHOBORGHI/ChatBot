using System;

namespace Chat.Core.Interfaces
{
    public interface IConsumerService
    {
        void Consume<T>(string queue, Action<T> execute);
    }
}