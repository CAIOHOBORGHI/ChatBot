using System.Collections.Generic;
using Chat.Core;
using Chat.Core.Entities;
using Chat.Core.Interfaces;
using Chat.Core.Models;
using Chat.Core.Utils;

namespace Chat.Web.Services
{
    public class CommandService : ICommandService
    {
        private CommandInfos _commandInfos = new CommandInfos();

        private List<string> _mockedCommandList = new List<string>() { "/stock" };

        public string GetCommandError(string text)
        {
            if (!text.Contains("="))
                return "Error! I guess you forgot to insert '='";

            string[] splitter = text.Split("=");
            string command = splitter[0];
            string param = splitter[1];
            if (string.IsNullOrWhiteSpace(command.Replace("/", "")))
                return Constants.ERROR_NULL_COMMAND;

            if (!_mockedCommandList.Contains(command))
                return $"'{command}' " + Constants.ERROR_COMMAND_NOT_FOUND;

            if (string.IsNullOrWhiteSpace(param))
                return Constants.ERROR_NULL_PARAMETER;

            return null;
        }

        public CommandInfos GetCommandInfos(string text)
        {
            string error = GetCommandError(text);
            if (error == null)
            {
                string[] splitter = text.Split("=");
                string command = splitter[0];
                if (!_mockedCommandList.Contains(command))
                    return null;

                string parameter = splitter[1];
                _commandInfos.Command = command;
                _commandInfos.Parameter = parameter;
            }

            _commandInfos.Error = error;
            return _commandInfos;
        }

        public bool IsCommand(string text)
        {
            return text.StartsWith("/");
        }
    }
}