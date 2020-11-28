using Chat.Core;
using Chat.Core.Interfaces;
using Chat.Core.Services;
using Chat.Core.Utils;
using Microsoft.Extensions.Configuration;


namespace Chat.Web.RabbitMQ
{
    public class UserBotQueueProducer : ProducerService, IUserBotQueueProducer
    {
        // Try to get connection string from envinronment variable(docker) or appSettings
        public UserBotQueueProducer(IConfiguration configuration) :
            base(Helper.GetConnection(configuration, "RabbitConnectionString"))
        {

        }

        public void SearchStock(string stockCode)
        {
            base.Produce<string>(Constants.USER_BOT_QUEUE, stockCode);
        }
    }
}