using System;
using System.Linq;
using System.Net.Http;
using Chat.Core.Interfaces;
using Chat.Core;
using Chat.Web.Models;
using Microsoft.Extensions.Configuration;

namespace Chat.Web.RabbitMQ
{
    public class BotUsersQueueConsumer : RabbitService, IBotUsersQueueConsumer
    {
        private HttpClient _client = new HttpClient();
        private IBotUsersQueueProducer _botUsersQueueProducer;

        // Setting const for challenge purposes, could come from Config file or database
        private const string STOCK_INFOS_URL = "https://stooq.com/q/l/?s=#code&f=sd2t2ohlcv&h&e=csv";
        public BotUsersQueueConsumer(IConfiguration configuration, IBotUsersQueueProducer botUsersQueueProducer) : base(configuration)
        {
            _botUsersQueueProducer = botUsersQueueProducer;
        }

        private Stock GetStock(string code)
        {
            Stock stock = null;
            try
            {
                string response = _client.GetStringAsync(STOCK_INFOS_URL.Replace("#code", code)).Result;

                /* Gets second line of csv */
                string[] lines = response.Split('\n');
                string secondLine = lines[1];

                /* Get properties */
                string[] properties = secondLine.Split(",");
                string stockName = properties.First();
                properties.Reverse();
                string closePrice = properties[1];
                stock = new Stock(stockName, double.Parse(closePrice));
            }
            catch (Exception)
            {
                // TODO: Implement logging
                // "Error trying to get stock!";
            }
            return stock;
        }

        public void WaitForBotResponse()
        {
            base.Consume<string>(Constants.BOT_USERS_QUEUE, 
                          (code)   => 
                          {
                            Stock stock = GetStock(code);
            string message = stock == null ?
              $"Error trying to get stock {code}!" :
              $"{stock.Name} is ${stock.Close.ToString()} per share";
            _botUsersQueueProducer.SendToUsers(message);

        });
        }
    }                           
}