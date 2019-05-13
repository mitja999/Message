using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using MessageService.Interfaces.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _messageController { get; set; }
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageService messageController, ILogger<MessageController> logger)
        {
            _messageController = messageController;
            _logger = logger;
        }

        // GET api/message?page=0&pageSize=10
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult <PagedList<Message>> Get(int page, int pageSize)
        {

            var messages = Execute<PagedList<Message>>((response) =>
            {

                var res = _messageController.GetMessages(page, pageSize);
                response.Items = res.Items;
                response.Page = res.Page;
                response.PageSize = res.PageSize;
                response.TotalCount = res.TotalCount;

            }, "Get", page, pageSize);
            return messages;

        }

        // GET api/message/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/message
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/message/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/message/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        private T Execute<T>(Action<T> action, string methodName, params object[] args) where T : new()
        {
            _logger.LogInformation("Entering {0}({1})", methodName, string.Join(", ", args));

            T response = new T();

            action(response);

            _logger.LogInformation("Exiting {0} with {1}", methodName, response);

            return response;
        }
    }
}
