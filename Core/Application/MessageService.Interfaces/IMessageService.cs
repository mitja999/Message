using MessageManagement.Interfaces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace MessageService.Interfaces
{
    public interface IMessageService
    {
        List<Message> GetMessages();
    }
}
