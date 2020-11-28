namespace Chat.Core.Interfaces
{
    public interface IUserBotQueueProducer
    {
        // Insert message(stock) to be consumed in (User -> Bot) Queue
        void SearchStock(string stockCode);
    }
}