using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Chat.Core.Interfaces;
using Chat.Core.Services;
using Chat.Core.Utils;

namespace Chat.Worker.RabbitMQ
{
    public class UserBotQueueConsumer : ConsumerService, IUserBotQueueConsumer
    {
        private HttpClient _client;
        private string _url = "https://stooq.com/q/l/?s=#stock&f=sd2t2ohlcv&h&e=csv";
        private IBotUsersQueueProducer _producer;

        public UserBotQueueConsumer(string rabbitConnection, IBotUsersQueueProducer producer) : base(rabbitConnection)
        {
            _client = new HttpClient();
            _producer = producer;
        }

        private string GetStockMessage(string code)
        {
            try
            {
                string response = _client.GetStringAsync(_url.Replace("#stock", code)).Result;

                /* Gets second line of csv */
                string[] lines = response.Split('\n');
                string secondLine = lines[1];

                /* Get properties */
                List<string> properties = secondLine.Split(",").ToList();
                string stockName = properties.First();
                properties.Reverse();
                string closePrice = properties[1];
                if (closePrice == "N/D")
                    throw new Exception("Not found!");
                return $"{stockName} quote is ${closePrice} per share";
            }
            catch (Exception ex)
            {
                return $"Error trying to get stock \"{code}\": " + ex.Message;
            }
        }

        public void WaitForStockCode()
        {
            base.Consume<string>(Constants.USER_BOT_QUEUE, (code) =>
            {
                string message = GetStockMessage(code);
                /* Produces message */
                _producer.SendToUsers(message);
            });
        }
    }
}