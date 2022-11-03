using EwsChat.Data.Contracts;
using EwsChat.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatUserRespository : IRepositoryBase<ChatUser>
    {
        void AddUser(ChatUser user);

        void RemoveUser(string userId);

        Task<IEnumerable<ChatUser>> GetAllUsersAsync();

        Task<IEnumerable<ChatUser>> GetUsersOfRoomAsync(int roomId);

        Task<ChatUser> GetUserByIdAsync(string userId);

        void UpdateUser(ChatUser updatedUser);
    }
}
