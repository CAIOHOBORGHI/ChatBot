using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Database.Models
{
    public class ChatUser : IdentityUser
    {
        public virtual ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
