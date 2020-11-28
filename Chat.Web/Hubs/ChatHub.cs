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
        private IUserService _userService;
        private IUserBotQueueProducer _userBotQueueProducer;
        public ChatHub(ICommandService commandService, IMessageService messageService, IUserService userService, IUserBotQueueProducer userBotQueueProducer)
        {
            _commandService = commandService;
            _messageService = messageService;
            _userService = userService;
            _userBotQueueProducer = userBotQueueProducer;
        }

        /// <summary>
        /// Method to broadcast message to all connected clients
        /// </summary>
        /// <param name="chatMessage">Message object with text and Writter</param>
        public async void SendAll(ChatMessage chatMessage)
        {

            if (_commandService.IsCommand(chatMessage.Text))
            {
                CommandInfos infos = _commandService.GetCommandInfos(chatMessage.Text);

                /* Im broadcasting user command, but not saving it to database */
                await Broadcast(chatMessage);

                if (infos.Error != null)
                {
                    await Broadcast(AdminMessage(infos.Error));
                }
                else
                {
                    _userBotQueueProducer.SearchStock(infos.Parameter);
                }
            }
            else
            {
                /* If is not a command, save to database and broadcast */
                string userId = chatMessage.UserID;
                ChatUser chatUser = _userService.GetUser(userId);
                Message message = new Message(chatMessage.Text, chatUser);
                _messageService.AddMessage(message);
                chatMessage.SentAt = message.SentAt;
                await Broadcast(chatMessage);
            }
        }

        private ChatMessage AdminMessage(string text)
        {
            return new ChatMessage
            {
                Text = text,
                UserName = "Administrator",
            };
        }

        private async Task Broadcast(ChatMessage chatMessage)
        {
            await Clients.All.SendAsync("receive", chatMessage);
        }
    }
}