using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class ChatUserRespository : RepositoryBase<ChatUser>, IChatUserRespository
    {
        public ChatUserRespository(ChatContext context) : base(context) 
        {

        }


        public void AddUser(ChatUser user)
        {
            //TODO: check what exception does EF throws when sending a user with an existing nickname
            Create(user);
        }


        public async Task<IEnumerable<ChatUser>> GetUsersOfRoomAsync(int roomId)
        {
            //TODO: check what exception does EF throw when sending room that does not exist
            var chatUser = await FindByCondition(user => user.ChatRoomId.Equals(roomId)).ToListAsync();
            return chatUser;
        }


        public async Task<IEnumerable<ChatUser>> GetAllUsersAsync()
        {
            return await FindAll().ToListAsync();
        }



        public async Task<ChatUser> GetUserByIdAsync(string userId)
        {
            var searchedUser = await FindByCondition(user => user.ChatUserId == userId).FirstOrDefaultAsync();

            if(searchedUser == null)
            {
                throw new UserNotFoundException("There is no user registered with given user id");
            }

            return searchedUser;
        }

        public void UpdateUser(ChatUser updatedUser)
        {
            //TODO: check what exception does EF throw when sending user that does not exist
            Update(updatedUser);
        }

        public void RemoveUser(string userId)
        {
            var userToRemove = GetUserByIdAsync(userId).Result;
            if (userToRemove == null)
            {
                throw new UserNotFoundException("There is no user registered with given user id.");
            }
            Delete(userToRemove);
        }        
    }
}
