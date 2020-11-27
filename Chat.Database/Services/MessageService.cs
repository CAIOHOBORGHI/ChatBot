using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Core.Interfaces;
using Chat.Core.Models;


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
            message.Id = (new Guid()).ToString();
            _context.Messages.Add(message);
            _context.SaveChanges();
            return message;
        }

        public List<Message> GetLastMessages(int count = 50)
        {
            List<Message> messages = _context.Messages?.OrderByDescending(o => o.SentAt).ToList();
            return messages;
        }
    }
}