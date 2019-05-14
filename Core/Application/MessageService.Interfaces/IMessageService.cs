using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageService.Interfaces
{
    public interface IMessageService
    {
        PagedList<Message> GetMessages(int page, int pageSize);

        MessageExtended GetMessageById(int id);

        Task<Message> CreateMessageAsync(MessageExtended messageExtended);

        Task<Message> UpdateMessageAsync(Message message);

        void DeleteMessageAsync(int id);
    }
}
