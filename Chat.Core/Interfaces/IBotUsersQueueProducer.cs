namespace Chat.Core.Interfaces
{
    // Bot send message to Users
    public interface IBotUsersQueueProducer
    {
        // Insert message into (Bot -> Users) Queue
        void SendToUsers(string message);
    }
}