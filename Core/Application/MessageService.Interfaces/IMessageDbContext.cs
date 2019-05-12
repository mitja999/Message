using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using MessageManagement.Interfaces.Entities;

namespace MessageService.Interfaces
{
    public interface IMessageDbContext
    {
        DbSet<Message> Messages { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<MessageUser> MessageUser { get; set; }
    }
}
