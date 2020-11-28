using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Core.Interfaces;
using Chat.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Database.Services
{
    public class MessageService : IMessageService
    {
        private ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Message AddMessage(Message message)
        {
            message.Id = (Guid.NewGuid()).ToString();
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public List<Message> GetLastMessages(int count = 50)
        {
            List<Message> messages = _context
                                        .Messages?
                                        .Include(i => i.Writter)
                                        .OrderBy(o => o.SentAt)
                                        .Take(count)
                                        .ToList();
            return messages;
        }
    }
}