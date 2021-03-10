using EwsChat.Data.Models;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class ChatRoomRepositoryUnitTests
    {
        private Mock<IChatRoomRepository> chatRoomRepository;

        [SetUp]
        public void Setup()
        {
            chatRoomRepository = new Mock<IChatRoomRepository>();
        }

        [Test]
        public void GetChatRoomsShouldReturnAllRooms()
        {
            var result = chatRoomRepository.Setup(cr => cr.FindAll()).Returns(SeedData.ChatRooms().AsQueryable());
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EquivalentTo(SeedData.ChatRooms()));
        }

        [Test]
        public void GetChatRoomByIdShouldReturnRoomWithGivenId()
        {
            int roomId = 1001;

            var returnedChatRoom = chatRoomRepository.Setup(cr => cr.FindByCondition(r => r.ChatRoomId.Equals(roomId)))
                .Returns(SeedData.ChatRooms().Where(r => r.ChatRoomId.Equals(roomId)).AsQueryable());

            Assert.That(returnedChatRoom.ChatRoomId, Is.EqualTo(roomId));
        }

        [Test]
        public void GetChatRoomByIdWithInvalidIdShouldReturnNull()
        {
            int roomId = 1201;

            ChatRoom returnedChatRoom = chatRoomRepository.GetChatRoomByIdAsync(roomId).Result;

            Assert.That(returnedChatRoom, Is.Null);
        }
    }
}