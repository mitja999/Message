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

        /// <summary>
        /// Get All Messages
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PagedList<Message>> GetAllMessages(int page, int pageSize)
        {

            return _messageController.GetMessages(page, pageSize);

        }

        /// <summary>
        /// Get Message
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MessageExtended> GetMessageExtended(int id)
        {
            return _messageController.GetMessageById(id);
        }

        /// <summary>
        /// Create Message
        /// </summary>
        /// <param name="messageExtended"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Message>> CreateMessageAsync([FromBody] MessageExtended messageExtended)
        {
            return await _messageController.CreateMessageAsync(messageExtended);
        }

        /// <summary>
        /// Update Message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Message>> UpdateMessage(int id, [FromBody]  Message message)
        {
            return await _messageController.UpdateMessageAsync(message);

        }

        /// <summary>
        /// Delete Message
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<Message>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void DeleteMessage(int id)
        {
            _messageController.DeleteMessageAsync(id);
        }


        //TODO: put in new controller , dummy implementation
        /// <summary>
        /// Authenticate WebUser
        /// </summary>
        /// <param name="id"></param>
        /// <param name="webuser"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WebUser))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
