using System;
using Chat.Core.Interfaces;
using Chat.Web.Services;
using Xunit;

namespace Chat.Tests
{
    public class CommandServiceTests
    {
        private ICommandService _commandService = new CommandService();

        [Fact]
        public void IsCommand_ValidCommand_ShouldReturnTrue()
        {
            string completeCommand = "/anyCommand";
            bool isCommand = _commandService.IsCommand(complete);
            Assert.Equal(isCommand, true);
        }

        [Fact]
        public void IsCommand_NotACommand_ShouldReturnFalse()
        {
            string notACommand = "Simple message";
            bool isCommand = _commandService.IsCommand(complete);
            Assert.Equal(isCommand, false);
        }

        [Fact]
        public void GetCommandError_NullParameter_ShouldReturnNullParameterMessage()
        {
            string text = "/stock=";
            string errorMessage = _commandService.GetCommandError(text);
            Asset.Equal(errorMessage, Constants.ERROR_NULL_PARAMETER);
        }

        [Fact]
        public void GetCommandError_NullCommand_ShouldReturnNullCommandMessage()
        {
            string text = "/";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Equal(errorMessage, Constants.ERROR_NULL_COMMAND);
        }

        [Fact]
        public void GetCommandError_CommandNotFound_ShouldReturnCommandNotFound()
        {
            string text = "/notRegisteredCommand=parameter";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Contains(Constants.ERROR_COMMAND_NOT_FOUND, errorMessage);
        }

        [Fact]
        public void GetCommandError_ValidCommand_ShouldReturnNull()
        {
            string text = "/stock=AAPL.us";
            string errorMessage = _commandService.GetCommandError(text);
            Assert.Null(errorMessage);
        }
    }
}
