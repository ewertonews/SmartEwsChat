using EwsChat.Data.Contracts;
using EwsChat.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        Task AddMessageAsync(Message message);
        Task<IEnumerable<Message>> GetAllMessagesAsync();
        Task<IEnumerable<Message>> GetAllMessagesFromRoomAsync(int roomId);
        Task<IEnumerable<Message>> GetLatestMessagesFromRoomAsync(int roomId, DateTime lastUpdate);
    }
}