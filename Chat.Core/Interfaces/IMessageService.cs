using System.Collections.Generic;
using Chat.Core.Models;

namespace Chat.Core.Interfaces
{
    public interface IMessageService
    {
        Message AddMessage(Message message);

        List<Message> GetLastMessages(int count = 50);
    }
}