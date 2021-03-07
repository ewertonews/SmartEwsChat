using EwsChat.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace EwsChat.Api.Controllers
{
    [Route("api/ewschat/rooms")]
    [ApiController]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomController(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get()
        {
            var chatRooms = await _chatRoomRepository.GetChatRoomsAsync();
            return new OkObjectResult(chatRooms);
        }

        [HttpGet("{chatRoomId}")]
        //[Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<IActionResult> Get(int chatRoomId)
        {
            var chatRoom = await _chatRoomRepository.GetChatRoomByIdAsync(chatRoomId);
            return new OkObjectResult(chatRoom);
        }

        //TODO: Post room
        //TODO: Delete room
    }
}
