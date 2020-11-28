using System;
using System.Collections.Generic;
using System.Linq;
using Chat.Core.Interfaces;
using Chat.Core.Models;

namespace Chat.Database.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ChatUser GetUser(string id)
        {
            ChatUser user = _context.ChatUsers.FirstOrDefault(f => f.Id == id);
            return user;
        }
    }
}