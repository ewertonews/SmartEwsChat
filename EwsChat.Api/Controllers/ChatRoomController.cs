using EwsChat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EwsChat.Api.Controllers
{
    [ApiController]
    [Route("api/ewschat/rooms")]
    [Produces("application/json")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ChatRoomController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets all Chat Rooms in the EwsChat database.
        /// </summary>
        /// <returns>A list of Chat Room objects</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var chatRooms = await _repositoryFactory.ChatRoom.GetChatRoomsAsync();
            return new OkObjectResult(chatRooms);
        }


        /// <summary>
        /// Gets all messages and participants from a Chat Room.
        /// </summary>
        /// <param name="chatRoomId"></param>
        /// <returns>A Chat Room object</returns>
        [Authorize]
        [HttpGet("{chatRoomId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]

        public async Task<IActionResult> Get(int chatRoomId)
        {
            var chatRoom = await _repositoryFactory.ChatRoom.GetChatRoomByIdAsync(chatRoomId);
            return new OkObjectResult(chatRoom);
        }

        //TODO: Post room
        //TODO: Delete room
    }
}
