using EwsChat.Api.Controllers;
using EwsChat.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Api.Tests
{
    public class ChatRoomControllerUnitTests
    {
        Mock<IRepositoryFactory> _repositoryFactoryMock;
        private ChatRoomController _chatRoomController;

        [SetUp]
        public void Setup()
        {
            _repositoryFactoryMock = new Mock<IRepositoryFactory>();
            _chatRoomController = new ChatRoomController(_repositoryFactoryMock.Object);
        }

        [Test]
        public void GetShouldReturnOkAndAllRoomsInTheRepository()
        {
            _repositoryFactoryMock.Setup(mr => mr.ChatRoom.GetChatRoomsAsync()).Returns(Task.FromResult(SeedData.ChatRooms()));

            var result = _chatRoomController.Get().Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.Not.Empty);
        }

        [Test]
        public void GetShouldReturnOkAndARoomWithTheGivenId()
        {
            var chatRoomId = 1001;
            var chatRoom = SeedData.ChatRooms().First();
            _repositoryFactoryMock.Setup(mr => mr.ChatRoom.GetChatRoomByIdAsync(chatRoomId)).Returns(Task.FromResult(chatRoom));

            var result = _chatRoomController.Get(chatRoomId).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo(chatRoom));
        }
    }
}
