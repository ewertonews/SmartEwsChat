using EwsChat.Data.Models;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class ChatRoomRepositoryUnitTests
    {
        //TODO: Write tests
        private Mock<IChatRoomRepository> chatRoomRepository;

        [SetUp]
        public void Setup()
        {
            chatRoomRepository = new Mock<IChatRoomRepository>();
        }

        [Test]
        public void GetChatRoomsShouldReturnAllRooms()
        {
            Assert.Pass();
        }

        
    }
}