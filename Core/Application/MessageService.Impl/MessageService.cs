using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MessageService.Impl
{
    public class MessageService : IMessageService
    {

        private readonly IMessageDbContext _messageDbContext;
        public MessageService(IMessageDbContext messageDbContext)
        {
            _messageDbContext = messageDbContext;
        }

        public List<Message> GetMessages()
        {
            List<Message> messages = new List<Message>();
            foreach(var message in _messageDbContext.Messages)
            {
                messages.Add(message);
            }
            return messages;
        }

    }

}
