using Chat.Core.Entities;

namespace Chat.Core.Interfaces
{
    public interface ICommandService
    {
        string GetCommandError(string text);
        CommandInfos GetCommandInfos(string text);
        bool IsCommand(string text);
    }
}