using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {

        public MessageRepository(ChatContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> GetAllMessagesAsync()
        {
            return await FindAll().OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetAllMessagesFromRoomAsync(int roomId)
        {
            return await FindByCondition(m => m.ChatRoomId.Equals(roomId)).OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesFromRoomAsync(int roomId, DateTime lastUpdate)
        {
            return await FindByCondition(m => m.ChatRoomId.Equals(roomId) && m.CreatedAt >= lastUpdate)
                .OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public void AddMessage(Message message)
        {
            Create(message);
        }              
    }
}
