using Chat.Core.Models;

namespace Chat.Core.Interfaces
{
    public interface ICommandService
    {
        ValidCommand GetCommandInfos(string text);
        bool IsValidSyntaxCommand(string text);
    }
}