using Chat.Core.Entities;

namespace Chat.Core.Interfaces
{
    public interface ICommandService
    {
        CommandInfos GetCommandInfos(string text);
        bool IsCommand(string text);
    }
}