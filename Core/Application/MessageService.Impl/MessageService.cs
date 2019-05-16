using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using MessageService.Interfaces.Exceptions;
using MessageService.Interfaces.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Get Message By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageExtended GetMessageById(int id)
        {

            var result = Execute<MessageExtended>((message) =>
            {
                Message mes = _messageDbContext.Messages.Find(id);
                if (mes == null)
                {
                    mes = _messageDbContext.Messages.Single(s => s.Id == id);
                }
                if (mes == null)
                {
                    throw new NotFoundException();
                }
                message.Id = mes.Id;
                message.Name = mes.Name;
                message.State = mes.State;
                message.Template = mes.Template;
                message.Type = mes.Type;
                var mu = _messageDbContext.MessageUser.Where(m => m.MessageId == mes.Id).Select(m => m.UserId).ToList();
                //var users = _messageDbContext.Users.Where(u => mu.Contains(u.Id)).ToList();
                message.UserIds = mu;

            }, "GetMessageById", id);
            return result;
        }

        /// <summary>
        /// Create Message Async
        /// </summary>
        /// <param name="messageExtended"></param>
        /// <returns></returns>
        public async Task<Message> CreateMessageAsync(MessageExtended messageExtended)
        {
            var result = await ExecuteAsync<Message>(async (message) =>
            {
                var mes = _messageDbContext.Messages.Add(new Message
                {
                    Name = messageExtended.Name,
                    State = messageExtended.State,
                    Template = messageExtended.Template,
                    Type = messageExtended.Type
                });

                var users = messageExtended.UserIds.Select(
                    u => new MessageUser() { MessageId = mes.Entity.Id, UserId = u }
                ).ToArray();

                _messageDbContext.MessageUser.AddRange(users);
                await _messageDbContext.SaveChangesAsync(CancellationToken.None);
            }, "GetMessageById", messageExtended);
            return result;
        }

        /// <summary>
        /// Update Message Async
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<Message> UpdateMessageAsync(Message message)
        {
            var result = await ExecuteAsync<Message>(async (mes) =>
            {
                if (_messageDbContext.Messages.Single(s => s.Id == message.Id) != null)
            {

                _messageDbContext.Messages.Update(message);
                await _messageDbContext.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                throw new NotFoundException();
            }

            }, "UpdateMessageAsync", message);
            return result;
        }

        /// <summary>
        /// Delete Message Async
        /// </summary>
        /// <param name="id"></param>
        public async void DeleteMessageAsync(int id)
        {
            await ExecuteAsync<Message>(async (message) =>
            {
                if (_messageDbContext.Messages.Single(s => s.Id == id) != null)
                {
                    var mu = _messageDbContext.MessageUser.Where(m => m.MessageId == id).ToList();
                    _messageDbContext.MessageUser.RemoveRange(mu);
                    await _messageDbContext.SaveChangesAsync(CancellationToken.None);
                }
                else
                {
                    throw new NotFoundException();
                }
            }, "DeleteMessageAsync",id);
        }


        private async Task<T> ExecuteAsync<T>(Func<T, Task> action, string methodName, params object[] args) where T : new()
        {
            _logger.LogDebug("Entering {0}({1})", methodName, string.Join(", ", args));

           T response = new T();

            try
            {
                await action(response);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message, ex);
            }

            _logger.LogDebug("Exiting {0} with {1}", methodName, response);

            return response;
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
