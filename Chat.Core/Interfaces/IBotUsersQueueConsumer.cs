namespace Chat.Core.Interfaces
{
    // API waits for message sent from bot
    public interface IBotUsersQueueConsumer
    {
        // Waits for bot response in (Bot -> Users) Queue
        // Then, broadcast it to users
        void WaitForBotResponse();
    }
}