namespace Chat.Core.Interfaces
{
    public interface ICommandService
    {
        bool IsValidCommand(string text);
    }
}