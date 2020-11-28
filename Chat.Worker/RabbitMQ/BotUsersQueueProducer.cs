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
            base.Produce<string>(Constants.BOT_USERS_QUEUE, message);
        }
    }
}