using EwsChat.Data.Contracts;
using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatUserRespository : IRepositoryBase<ChatUser>
    {
        Task AddUserAsync(ChatUser user);

        Task RemoveUserAsync(string userId);

        Task<IEnumerable<ChatUser>> GetAllUsersAsync();

        Task<IEnumerable<ChatUser>> GetUsersOfRoomAsync(int roomId);

        Task<ChatUser> GetUserByIdAsync(string userId);

        Task<ChatUser> UpdateUserAsync(ChatUser updatedUser);
    }
}
