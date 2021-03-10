using EwsChat.Data.Models;
using NUnit.Framework;
using System;
using EwsChat.Data.Exceptions;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class ChatUserRepositoryTests
    {
        private IChatUserRespository chatUserRespository;

        [SetUp]
        public void Setup()
        {
            chatUserRespository = new ChatUserRespository();
        }

        [Test]
        public void AddUserShouldAddUserSuccessfully()
        {
            var user = new ChatUser()
            {
                NickName = "TobiasMetal",
                ChatUserId = Guid.NewGuid().ToString()
            };

            chatUserRespository.AddUser(user).Wait();
            var userFromRecord = chatUserRespository.GetUserByIdAsync(user.ChatUserId).Result;

            Assert.That(userFromRecord, Is.Not.Null);
        }

        [Test]
        public void AddUserShouldThrowUserAlreadyExistsException()
        {
            var user = new ChatUser()
            {
                NickName = "TobiasMetal",
                ChatUserId = Guid.NewGuid().ToString(),
                ChatRoomId = 1002
            };

            chatUserRespository.AddUser(user).Wait();

            Assert.That(() => chatUserRespository.AddUser(user), Throws.TypeOf<UserAlreadyExistsException>());
        }

        [Test]
        public void GetUserByIdShouldReturnUserWithGivenId()
        {
            AddTwoUsers();
            string idUser = Guid.NewGuid().ToString();
            var heavyMeatUser = new ChatUser()
            {
                NickName = "HaavyMeat",
                ChatUserId = idUser,
            };
            chatUserRespository.AddUser(heavyMeatUser).Wait();

            var searchedUser = chatUserRespository.GetUserByIdAsync(idUser).Result;

            Assert.That(searchedUser, Is.Not.Null);
            Assert.That(searchedUser.ChatUserId, Is.EqualTo(idUser));
        }

        [Test]
        public void GetUserByIdShouldThrowNonExistentUserException()
        {
            string idUser = Guid.NewGuid().ToString();

            Assert.That(() => chatUserRespository.GetUserByIdAsync(idUser), Throws.TypeOf<UserNotFoundException>());
        }

        [Test]
        public void GetAllUsersShuldReturnAllUsersFromRepository()
        {
            AddTwoUsers();

            var allUsers = chatUserRespository.GetAllUsersAsync().Result;

            Assert.That(allUsers, Is.Not.Empty);
            Assert.That(allUsers.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetUsersOfRoomShouldReturnAllUsertsOfAgivenRoom()
        {
            AddTwoUsers();
            int punkRockRoomId = 1001;
            var anotherUser = new ChatUser()
            {
                ChatUserId = Guid.NewGuid().ToString(),
                NickName = "emily182",
                ChatRoomId = punkRockRoomId
            };
            chatUserRespository.AddUser(anotherUser).Wait();

            var usersOfRoom = chatUserRespository.GetUsersOfRoomAsync(punkRockRoomId).Result;
            
            Assert.That(usersOfRoom, Is.Not.Empty);
            Assert.That(usersOfRoom.Count(), Is.EqualTo(2));            
        }

        [Test]
        public void UpdateUserShouldUpdateGivenUserSuccefully()
        {
            var userId = Guid.NewGuid().ToString();
            int punkRockRoom = 1001;
            var user = new ChatUser()
            {
                ChatUserId = userId,
                NickName = "emily182",
                ChatRoomId = 0
            };
            chatUserRespository.AddUser(user).Wait();

            user.ChatRoomId = punkRockRoom;

            var updatedUser = chatUserRespository.UpdateUser(user).Result;

            Assert.That(updatedUser.ChatRoomId, Is.EqualTo(punkRockRoom));
        }

        [Test]
        public void RemoveUserShouldRemoveGivenUserSuccessfully()
        {
            ChatUser userToRemove = AddTwoUsersAndReturnAnExtraUser();
            chatUserRespository.AddUser(userToRemove).Wait();

            chatUserRespository.RemoveUser(userToRemove.ChatUserId).Wait();
            var updatedRepository = chatUserRespository.GetAllUsersAsync().Result;


            Assert.That(updatedRepository, Does.Not.Contain(userToRemove));
        }
        

        [Test]
        public void RemoveUserShouldThrowNonExistentUserException()
        {
            var userThatWasntAdded = AddTwoUsersAndReturnAnExtraUser();

            Assert.That(() => chatUserRespository.RemoveUser(userThatWasntAdded.ChatUserId), Throws.TypeOf<UserNotFoundException>());
        }

        private ChatUser AddTwoUsersAndReturnAnExtraUser()
        {
            AddTwoUsers();
            var userToRemove = new ChatUser()
            {
                NickName = "BillyGrunge",
                ChatUserId = Guid.NewGuid().ToString()
            };
            return userToRemove;
        }

        private void AddTwoUsers()
        {
            var user1 = new ChatUser()
            {
                NickName = "TobiasMetal",
                ChatUserId = Guid.NewGuid().ToString(),
                ChatRoomId = 1002
            };

            var user2 = new ChatUser()
            {
                NickName = "PunkJack",
                ChatUserId = Guid.NewGuid().ToString(),
                ChatRoomId = 1001
            };

            chatUserRespository.AddUser(user1).Wait();
            chatUserRespository.AddUser(user2).Wait();
        }
    }
}
