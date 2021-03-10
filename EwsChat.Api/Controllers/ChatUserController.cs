using EwsChat.Data;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EwsChat.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ewschat/users")]
    [Produces("application/json")]
    public class ChatUserController : ControllerBase
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ChatUserController(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        /// <summary>
        /// Gets all users from the EwsChat
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var allUsers = await _repositoryFactory.ChatUser.GetAllUsersAsync();
            return Ok(allUsers);
        }

        /// <summary>
        /// Get the users from a a Chat Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("room/{roomId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get(int roomId)
        {
            var usersOfRoom = await _repositoryFactory.ChatUser.GetUsersOfRoomAsync(roomId);
            return new OkObjectResult(usersOfRoom);
        }

        /// <summary>
        /// Get the details of a User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A User object.</returns>
        [ProducesResponseType(200)]
        [HttpGet("{userId}", Name = "UserById")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _repositoryFactory.ChatUser.GetUserByIdAsync(userId);
            return new OkObjectResult(user);
        }

        /// <summary>
        /// Creates a User in the EwsChat database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Post(ChatUser user)
        {
            _repositoryFactory.ChatUser.AddUser(user);
            await _repositoryFactory.SaveAsync();
            return CreatedAtRoute("UserById", new { userId = user.ChatUserId }, user);
        }

        /// <summary>
        /// Edits a User in the EwsChat (Change a room or the NickName).
        /// </summary>
        /// <param name="user"></param>
        /// <returns>No Content</returns>
        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Put(ChatUser user)
        {
            _repositoryFactory.ChatUser.UpdateUser(user);
            await _repositoryFactory.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Removes a User from the EwsChat database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>No Content</returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(string userId)
        {
            _repositoryFactory.ChatUser.RemoveUser(userId);
            await _repositoryFactory.SaveAsync();
            return NoContent();
        }
    }
}
