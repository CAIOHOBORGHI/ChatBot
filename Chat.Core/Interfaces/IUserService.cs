using System;
using Chat.Core.Models;

namespace Chat.Core.Interfaces
{
    public interface IUserService
    {
        ChatUser GetUser(string id);
    }
}
