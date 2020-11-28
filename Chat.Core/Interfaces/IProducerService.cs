using System;

namespace Chat.Core.Interfaces
{
    public interface IProducerService
    {
        void Produce<T>(string queue, T message);
    }
}