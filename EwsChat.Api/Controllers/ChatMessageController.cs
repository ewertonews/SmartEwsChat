using EwsChat.Data;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EwsChat.Api.Controllers
{   
    [Authorize]
    [ApiController]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    [Produces("application/json")]
    [Route("api/ewschat/messages/{roomId:int}")]
    public class ChatMessageController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ChatMessageController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets all messages from the Chat Room with the given ID.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [HttpGet("", Name = "roomById")]
        public async Task<IActionResult> Get(int roomId)
        {
            var messages = await _repositoryFactory.Message.GetAllMessagesFromRoomAsync(roomId);
            return new OkObjectResult(messages);
        }

        /// <summary>
        /// Gets the  messages from a room that were created from the lastUpdated date onwards.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="lastUpdated"></param>
        /// <returns></returns>
        [ProducesResponseType(200)]
        [HttpGet("{lastUpdated}")]       
        public async Task<IActionResult> Get(int roomId, string lastUpdated)
        {
            DateTime dateLastUpdated = DateTime.Parse(lastUpdated);
            var messages = await _repositoryFactory.Message.GetLatestMessagesFromRoomAsync(roomId, dateLastUpdated);
            return new OkObjectResult(messages);
        }


        /// <summary>
        /// Send a message to the a Chat Room.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post(Message message)
        {
            _repositoryFactory.Message.AddMessage(message);
            await _repositoryFactory.SaveAsync();
            return CreatedAtRoute("roomById", new { roomId = message.ChatRoomId }, message);
        }

    }
}
