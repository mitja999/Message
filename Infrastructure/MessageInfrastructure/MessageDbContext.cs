using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace MessageInfrastructure
{

    public class MessageDbContext :  DbContext, IMessageDbContext
    {
        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {

        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<MessageUser> MessageUser { get; set; }
    }
}
