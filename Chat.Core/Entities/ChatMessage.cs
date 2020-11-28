using Microsoft.AspNetCore.Identity;

namespace Chat.Core.Entities
{
    public class ChatMessage
    {
        public string Text { get; set; }

        public IdentityUser Writter { get; set; }
    }
}