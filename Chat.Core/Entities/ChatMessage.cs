using System;
using Microsoft.AspNetCore.Identity;

namespace Chat.Core.Entities
{
    public class ChatMessage
    {
        public DateTime SentAt { get; set; } = DateTime.Now;

        public string Text { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }
    }
}