using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _messageController { get; set; }
        public MessageController(IMessageService messageController)
        {
            _messageController = messageController;
        }

        // GET api/message
        [HttpGet]
        public ActionResult<IEnumerable<Message>> Get()
        {
            return _messageController.GetMessages();
            //return new string[] { "value1", "value2" };
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
    }
}
