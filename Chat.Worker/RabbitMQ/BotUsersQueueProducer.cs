using Chat.Core.Entities;
using Chat.Core.Interfaces;
using Chat.Core.Utils;
using Chat.Core.Services;

namespace Chat.Worker.RabbitMQ
{
    public class BotUsersQueueProducer : ProducerService, IBotUsersQueueProducer
    {
        public BotUsersQueueProducer(string rabbitConnectionString) : base(rabbitConnectionString)
        {

        }

        public void SendToUsers(string message)
        {
            ChatMessage chatMessage = new ChatMessage
            {
                Text = message,
                UserName = "Bot"
            };
            base.Produce<ChatMessage>(Constants.BOT_USERS_QUEUE, chatMessage);
        }
    }
}