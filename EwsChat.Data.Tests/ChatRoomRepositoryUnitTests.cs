using EwsChat.Data.Models;
using NUnit.Framework;

namespace EwsChat.Data.Tests
{
    public class ChatRoomRepositoryUnitTests
    {
        private IChatRoomRepository ChatRoomRepository;

        [SetUp]
        public void Setup()
        {
            ChatRoomRepository = new ChatRoomRepository();
        }

        [Test]
        public void GetChatRoomsShouldReturnAllRooms()
        {
            var result = ChatRoomRepository.GetChatRoomsAsync().Result;

            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void GetChatRoomByIdShouldReturnRoomWithGivenId()
        {
            int roomId = 1001;

            ChatRoom returnedChatRoom = ChatRoomRepository.GetChatRoomByIdAsync(roomId).Result;

            Assert.That(returnedChatRoom.ChatRoomId, Is.EqualTo(roomId));
        }

        [Test]
        public void GetChatRoomByIdWithInvalidIdShouldReturnNull()
        {
            int roomId = 1201;

            ChatRoom returnedChatRoom = ChatRoomRepository.GetChatRoomByIdAsync(roomId).Result;

            Assert.That(returnedChatRoom, Is.Null);
        }
    }
}