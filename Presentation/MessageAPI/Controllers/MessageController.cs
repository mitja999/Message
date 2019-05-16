using System.Threading.Tasks;
using MessageManagement.Interfaces.Entities;
using MessageService.Interfaces;
using MessageService.Interfaces.Models;
using Microsoft.AspNetCore.Authorization;
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
        public ActionResult<PagedList<Message>> FindMessages(int page, int pageSize)
        {

            return _messageController.GetMessages(page, pageSize);

        }

        // GET api/message/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<MessageExtended> GetMessageExtended(int id)
        {
            return _messageController.GetMessageById(id);
        }

        // POST api/message
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessageAsync([FromBody] MessageExtended messageExtended)
        {
            return await _messageController.CreateMessageAsync(messageExtended);
        }

        // PUT api/message/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Message>> UpdateMessage(int id, [FromBody]  Message message)
        {
            return await _messageController.UpdateMessageAsync(message);

        }

        // DELETE api/message/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public void DeleteMessage(int id)
        {
            _messageController.DeleteMessageAsync(id);
        }


        //TODO: put in new controller , dummy implementation
        // Post api/message/5
        [HttpPost("Authenticate")]
        public ActionResult<WebUser> Authenticate(int id, [FromBody]  WebUser webuser)
        {
            if (webuser.Username == "admin")
            {
                webuser.Token= Extensions.JwtTokenGenerator.Generate(webuser.Username, true, "azurewebsites", "dasd-dasd-213dsae-321");
            }
            else
            {
                webuser.Token = Extensions.JwtTokenGenerator.Generate(webuser.Username, true, "azurewebsites", "dasd-dasd-213dsae-321");
            }
            return webuser;
        }

    }
}
