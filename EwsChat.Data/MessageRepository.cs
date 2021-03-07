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
            //await ValidadeRoom(roomId);
            return await FindByCondition(m => m.ChatRoomId.Equals(roomId)).OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetLatestMessagesFromRoomAsync(int roomId, DateTime lastUpdate)
        {
            //await ValidadeRoom(roomId);
            return await FindByCondition(m => m.ChatRoomId.Equals(roomId) && m.CreatedAt >= lastUpdate).OrderBy(m => m.CreatedAt).ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            await Task.Run(() =>
            {
                Create(message);
            });
        }

        //private async Task ValidateMessage(Message message)
        //{
        //    if (message == null || message.ChatRoomId == 0)
        //    {
        //        throw new ArgumentNullException("The message is null or there is not target room id");
        //    }

        //    await ValidadeRoom(message.ChatRoomId);

        //    if (string.IsNullOrEmpty(message.Text))
        //    {
        //        throw new InvalidMessageException("Message text cannot be empty.");
        //    }
        //}

        //private async Task ValidadeRoom(int roomId)
        //{
        //    var targetRoom = await _chatRoomRepository.GetChatRoomByIdAsync(roomId);
        //    if (targetRoom == null)
        //    {
        //        throw new RoomNotFoundException("There is no chat room with the given TargetRoomId.");
        //    }
        //}       
    }
}
