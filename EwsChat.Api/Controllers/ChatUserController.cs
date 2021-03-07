using EwsChat.Data;
using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EwsChat.Web.Controllers
{
    [Route("api/ewschat/users")]
    [ApiController]
    public class ChatUserController : ControllerBase
    {
        private readonly IChatUserRespository _chatUserRepository;

        public ChatUserController(IChatUserRespository chatUserRepository)
        {
            _chatUserRepository = chatUserRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allUsers = await _chatUserRepository.GetAllUsersAsync();
            return Ok(allUsers);
        }

        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> Get(int roomId)
        {
            //TODO:catch RoomNotFoundException;
            var usersOfRoom = await _chatUserRepository.GetUsersOfRoomAsync(roomId);
            return new OkObjectResult(usersOfRoom);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _chatUserRepository.GetUserByIdAsync(userId);
            return new OkObjectResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChatUser user)
        {
            await _chatUserRepository.AddUserAsync(user);
            return Created(user.ChatUserId, user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(ChatUser user)
        {
            await _chatUserRepository.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            await _chatUserRepository.RemoveUserAsync(userId);
            return NoContent();
        }
    }
}
