using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class ChatRoomRepository : RepositoryBase<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatContext chatContext) : base(chatContext)
        {
        }

        public async Task<ChatRoom> GetChatRoomByIdAsync(int id)
        {
            var chatRoom = await FindByCondition(room => room.ChatRoomId == id)
                .Include(room => room.Participants)
                .Include(room => room.Messages)
                .FirstOrDefaultAsync();

            if (chatRoom == null)
            {
                throw new RoomNotFoundException("There is no Chat Room with the given ID.");
            }
            return chatRoom;
        }

        public async Task<IEnumerable<ChatRoom>> GetChatRoomsAsync()
        {
            return await FindAll().ToListAsync();
        }
    }
}
