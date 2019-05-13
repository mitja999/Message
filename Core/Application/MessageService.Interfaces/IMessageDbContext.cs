using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using MessageManagement.Interfaces.Entities;

namespace MessageService.Interfaces
{
    public interface IMessageDbContext
    {
        /// <summary>
        /// Message DB table
        /// </summary>
        DbSet<Message> Messages { get; set; }

        /// <summary>
        /// User DB table
        /// </summary>
        DbSet<User> Users { get; set; }

        /// <summary>
        /// MessageUser DB table
        /// </summary>
        DbSet<MessageUser> MessageUser { get; set; }
    }
}
