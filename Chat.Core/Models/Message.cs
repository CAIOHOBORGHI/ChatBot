using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chat.Core.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; }

        public DateTime SentAt { get; set; } = new DateTime();

        [Required]
        public string Text { get; set; }

        public string UserId { get; set; }

        public virtual ChatUser Writter { get; set; }
    }
}
