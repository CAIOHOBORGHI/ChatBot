using System.Text.Json;
using System.Threading.Tasks;
using Chat.Core;
using Chat.Core.Entities;
using Chat.Core.Interfaces;
using Chat.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Web.Hubs
{
    public class ChatHub : Hub
    {
        private ICommandService _commandService;
        private IMessageService _messageService;
        private IUserBotQueueProducer _userBotQueueProducer;
        public ChatHub(ICommandService commandService, IMessageService messageService, IUserBotQueueProducer userBotQueueProducer)
        {
            _commandService = commandService;
            _messageService = messageService;
            _userBotQueueProducer = userBotQueueProducer;
        }

        /// <summary>
        /// Method to broadcast message to all connected clients
        /// </summary>
        /// <param name="chatMessage">Message object with text and Writter</param>
        public async void SendAll(ChatMessage chatMessage)
        {
            if (_commandService.IsValidSyntaxCommand(chatMessage.Text))
            {
                /* If command is valid, get infos */
                ValidCommand command = _commandService.GetCommandInfos(chatMessage.Text);
                if (command == null)
                {
                    /* In case command doesnt exists, sends feedback */
                    await Clients.All.SendAsync("receive", "Sorry, command not found!");
                }
                else
                {
                    _userBotQueueProducer.SearchStock(command.Parameter);
                }
            }
            else
            {
                /* If is not a valid command syntax, save to database and broadcast */
                ChatUser chatUser = (ChatUser)chatMessage.Writter;
                Message message = new Message(chatMessage.Text, chatUser);
                _messageService.AddMessage(message);
                await Clients.All.SendAsync("receive", chatMessage);

            }
        }
    }
}