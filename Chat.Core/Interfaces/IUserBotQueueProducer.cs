namespace Chat.Core.Interfaces
{
    public interface IUserBotQueueProducer
    {
        // Insert stock code to be searched int (User -> Bot) Queue
        void SearchStock(string code);
    }
}