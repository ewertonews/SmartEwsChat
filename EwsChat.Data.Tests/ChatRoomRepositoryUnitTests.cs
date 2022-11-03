using Moq;
using NUnit.Framework;

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