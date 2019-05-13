using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MessageService.Interfaces
{
    public interface IMessageService
    {
        PagedList<Message> GetMessages(int page, int pageSize);
    }
}
