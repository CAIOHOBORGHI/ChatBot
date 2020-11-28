using System;
using Chat.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Chat.Core.Entities
{
    public class ChatMessage
    {
        public ChatMessage() { }
        public ChatMessage(Message dbMessage)
        {
            this.SentAt = dbMessage.SentAt;
            this.Text = dbMessage.Text;
            this.UserName = dbMessage.Writter.UserName;
            this.UserID = dbMessage.Writter.Id;
        }

        public DateTime SentAt { get; set; } = DateTime.Now;

        public string Text { get; set; }

        public string UserName { get; set; }

        public string UserID { get; set; }
    }
}