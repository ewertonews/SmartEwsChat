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


        public async Task AddUserAsync(ChatUser user)
        {
            //check what exception does EF throws when sending a user with an existing nickname
            await Task.Run(() => {
                Create(user);
            });            
        }


        public async Task<IEnumerable<ChatUser>> GetUsersOfRoomAsync(int roomId)
        {
            //check what exception does EF throw when sending room that does not exist
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

        public async Task<ChatUser> UpdateUserAsync(ChatUser updatedUser)
        {
            //check what exception does EF throw when sending user that does not exist
            return await Task.Run(() =>
            {
                Update(updatedUser);
                return updatedUser;
            });
        }

        public async Task RemoveUserAsync(string userId)
        {
            var userToRemove = await GetUserByIdAsync(userId);

            if (userToRemove == null)
            {
                throw new UserNotFoundException("There is no user registered with given user id.");
            }
            Delete(userToRemove);
        }
        
    }
}
