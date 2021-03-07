using EwsChat.Data.Contracts;
using EwsChat.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IChatRoomRepository : IRepositoryBase<ChatRoom>
    {
        Task<IEnumerable<ChatRoom>> GetChatRoomsAsync();
        Task<ChatRoom> GetChatRoomByIdAsync(int id);
    }
}