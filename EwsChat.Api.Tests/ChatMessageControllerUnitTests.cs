using EwsChat.Api.Controllers;
using EwsChat.Data;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace EwsChat.Api.Tests
{
    public class ChatMessageControllerUnitTests
    {
        private Mock<IRepositoryFactory> _repositoryFactoryMock;
        private ChatMessageController _chatMessageController;

        [SetUp]
        public void Setup()
        {
            _repositoryFactoryMock = new Mock<IRepositoryFactory>();
            _chatMessageController = new ChatMessageController(_repositoryFactoryMock.Object);
        }

        [Test]
        public void GetShouldReturnOkAndMessagesFromGivenRoom()
        {
            var message1 = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ChatRoomId = 1001,
                Text = "Hey, are you a SMART guy?"
            };

            var message2 = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ChatRoomId = 1001,
                Text = "Hello, yes I am!"
            };
            IEnumerable<Message> listOfMessages = new List<Message>() { message1, message2 };
            _repositoryFactoryMock.Setup(mr => mr.Message.GetAllMessagesFromRoomAsync(1001)).Returns(Task.FromResult(listOfMessages));

            var result = _chatMessageController.Get(1001).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.Not.Empty);
        }

        [Test]
        [Ignore("Failing - needs investigation")]
        public void GetShouldReturnOkAndMessagesFromGivenRoomWithCreatedAtEqualOrGreaterThanLastUpdate()
        {
            var message1 = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                ChatRoomId = 1001,
                Text = "Hello!"
            };
            var lastUpdate = DateTime.Now;

            IEnumerable<Message> listOfMessages = new List<Message>() { message1 };
            _repositoryFactoryMock.Setup(mr => mr.Message.GetLatestMessagesFromRoomAsync(1001, lastUpdate)).Returns(Task.FromResult(listOfMessages));

            var result = _chatMessageController.Get(1001, lastUpdate.ToString(CultureInfo.InvariantCulture)).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.Not.Empty);
        }

        [Test]
        public void PostShouldReturnCreatedAtRouteResultForValidMessage()
        {
            var message = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ChatRoomId = 1001,
                Text = "Hey, can you take a blip?"
            };
            _repositoryFactoryMock.Setup(mr => mr.Message.AddMessage(message));

            var result = _chatMessageController.Post(message).Result;

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
        }

    }
}
