namespace Chat.Core.Interfaces
{
    public interface IUserBotQueueConsumer
    {
        // Insert response message into (Bot -> User) Queue
        void SendMessage();

        void WaitForStockCode();
    }
}