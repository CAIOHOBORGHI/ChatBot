using Microsoft.AspNetCore.Identity;

namespace Chat.Core.Entities
{
    public class ChatMessage
    {
        public string Text { get; set; }

        public string UserID { get; set; }
    }
}