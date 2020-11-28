using System.Collections.Generic;
using Chat.Core;
using Chat.Core.Interfaces;
using Chat.Core.Models;

namespace Chat.Web.Services
{
    public class CommandService : ICommandService
    {
        // Mocked list with command and queue, easy to implement more commands
        private List<string> _mockCommands = new List<string>() { "/stock" };

        public ValidCommand GetCommandInfos(string text)
        {
            string[] splitter = text.Split("=");
            string command = splitter[0];
            if (!_mockCommands.Contains(command))
                return null;

            string parameter = splitter[1];

            ValidCommand validCommand = new ValidCommand
            {
                Command = command,
                Parameter = parameter,
            };
            return validCommand;
        }

        public bool IsValidSyntaxCommand(string text)
        {
            if (!text.StartsWith("/") || !text.Contains("="))
                return false;

            return true;
        }
    }
}