using Chat.Core.Entities;
using Chat.Core.Interfaces;
using Chat.Core.Models;
using Chat.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IMessageService _messageService;

        public ChatController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize]
        public IActionResult Index()
        {
            /* Returns 50 last messages! */
            List<ChatMessage> chatMessages = new List<ChatMessage>();
            List<Message> dbMessages = _messageService.GetLastMessages();
            if(dbMessages != null && dbMessages.Count > 0)
            {
                chatMessages = dbMessages.Select(s => new ChatMessage(s)).ToList();
            }
            return View(chatMessages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
