using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using MessageService.Interfaces.Exceptions;
using MessageService.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageService.Impl
{
    public class MessageService : IMessageService
    {

        private readonly IMessageDbContext _messageDbContext;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IMessageDbContext messageDbContext, ILogger<MessageService> logger)
        {
            _logger = logger;
            _messageDbContext = messageDbContext;

            //load data and add to cache
            _messageDbContext.Messages.Load();
            _messageDbContext.Users.Load();
            _messageDbContext.MessageUser.Load();
        }

        /// <summary>
        /// Get messages by page and pagesize
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<Message> GetMessages(int page, int pageSize)
        {

            var result = Execute<PagedList<Message>>((messages) =>
            {
                messages.TotalCount = 100;
                messages.Items = _messageDbContext.Messages.Local.Skip((page - 1) * pageSize).Take(pageSize);
                messages.Page = page;
                messages.PageSize = pageSize;
                messages.TotalCount = _messageDbContext.Messages.Local.Count;
            }, "GetMessages", page, pageSize);
            return result;
        }

        private T Execute<T>(Action<T> action, string methodName, params object[] args) where T : new()
        {
            _logger.LogDebug("Entering {0}({1})", methodName, string.Join(", ", args));

            T response = new T();

            try
            {
                action(response);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            _logger.LogDebug("Exiting {0} with {1}", methodName, response);

            return response;
        }
    }

}
