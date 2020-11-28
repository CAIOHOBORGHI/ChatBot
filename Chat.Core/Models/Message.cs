using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Core.Models
{
    public class Message
    {
        public Message() { }
        public Message(string text, ChatUser writter)
        {
            this.Text = text;
            this.Writter = writter;
        }

        [Key]
        public string Id { get; set; }

        public DateTime SentAt { get; set; } = DateTime.Now;

        [Required]
        public string Text { get; set; }

        public string UserId { get; set; }

        public virtual ChatUser Writter { get; set; }
    }
}
