using System;
using System.Linq;
using System.Net.Http;
using Chat.Core.Interfaces;
using Chat.Core;
using Chat.Web.Models;
using Microsoft.Extensions.Configuration;
using Chat.Core.Services;
using Chat.Core.Utils;

namespace Chat.Web.RabbitMQ
{
    public class BotUsersQueueConsumer : ConsumerService, IBotUsersQueueConsumer
    {
        private HttpClient _client = new HttpClient();
        private IBotUsersQueueProducer _botUsersQueueProducer;

        // Setting const for challenge purposes, could come from Config file or database
        private const string STOCK_INFOS_URL = "https://stooq.com/q/l/?s=#code&f=sd2t2ohlcv&h&e=csv";
        public BotUsersQueueConsumer(IConfiguration configuration, IBotUsersQueueProducer botUsersQueueProducer) :
            base(Helper.GetConnection(configuration, "RabbitConnectionString"))
        {
            _botUsersQueueProducer = botUsersQueueProducer;
        }

        /// <summary>
        /// Search for stock info in stooq
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Stock with Name and Close or null</returns>
        private Stock GetStock(string code)
        {
            Stock stock = null;
            try
            {
                string response = _client.GetStringAsync(STOCK_INFOS_URL.Replace("#code", code)).Result;

                /* If other properties were needed, I could use a .csv parser */

                /* Gets second line of csv */
                string[] lines = response.Split('\n');
                string secondLine = lines[1];

                /* Get properties */
                string[] properties = secondLine.Split(",");
                string stockName = properties.First();
                properties.Reverse();
                string closePrice = properties[1];

                /* Instanciates new Stock with parsed infos */
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
            base.Consume<string>
            (
                Constants.BOT_USERS_QUEUE,
                code =>
                {
                    Stock stock = GetStock(code);
                    string message =
                        stock == null ?
                            $"Error trying to get stock {code}!" :
                            $"{stock.Name} is ${stock.Close} per share";
                    _botUsersQueueProducer.SendToUsers(message);
                }
            );
        }
    }
}