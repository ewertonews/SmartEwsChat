using EwsChat.Api.Controllers;
using EwsChat.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace EwsChat.Api.Tests
{
    public class ChatRoomControllerUnitTests
    {
        Mock<IRepositoryFactory> repositoryFactoryMock;
        private ChatRoomController chatRoomController;

        [SetUp]
        public void Setup()
        {
            repositoryFactoryMock = new Mock<IRepositoryFactory>();
            chatRoomController = new ChatRoomController(repositoryFactoryMock.Object);
        }

        [Test]
        public void GetShouldReturnOkAndAllRoomsInTheRepository()
        {
            repositoryFactoryMock.Setup(mr => mr.ChatRoom.GetChatRoomsAsync()).Returns(Task.FromResult(SeedData.ChatRooms()));

            var result = chatRoomController.Get().Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.Not.Empty);
        }

        [Test]
        public void GetShouldReturnOkAndARoomWithTheGivenId()
        {
            int chatRoomId = 1001;
            var chatRoom = SeedData.ChatRooms().First();
            repositoryFactoryMock.Setup(mr => mr.ChatRoom.GetChatRoomByIdAsync(chatRoomId)).Returns(Task.FromResult(chatRoom));

            var result = chatRoomController.Get(chatRoomId).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.EqualTo(chatRoom));
        }
    }
}
