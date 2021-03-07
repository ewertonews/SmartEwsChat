using EwsChat.Data;
using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwsChat.Api.Controllers
{
    [Route("api/ewschat/messages")]
    [ApiController]
    public class ChatMessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public ChatMessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> Get(int roomId)
        {
            var messages = await _messageRepository.GetAllMessagesFromRoomAsync(roomId);
            return new OkObjectResult(messages);
        }

        [HttpGet("{roomId}/{lastUpdated}")]
        public async Task<IActionResult> Get(int roomId, string lastUpdated)
        {
            DateTime dateLasteUpdated = DateTime.Parse(lastUpdated);
            var messages = await _messageRepository.GetLatestMessagesFromRoomAsync(roomId, dateLasteUpdated);
            return new OkObjectResult(messages);
        }


        [HttpPost]
        public  async Task<IActionResult> Post(Message message)
        {
            await _messageRepository.AddMessageAsync(message);
            return Ok();
        }

    }
}
