using EwsChat.Api.Controllers;
using EwsChat.Data;
using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EwsChat.Api.Tests
{
    //TODO: Rewrite the failing tests. See here
    //https://docs.microsoft.com/en-us/aspnet/core/test/middleware?view=aspnetcore-3.1
    public class ChatMessageControllerUnitTests
    {
        private Mock<IMessageRepository> messageRepositoryMock;
        private ChatMessageController chatMessageController;

        [SetUp]
        public void Setup()
        {
            messageRepositoryMock = new Mock<IMessageRepository>();
            chatMessageController = new ChatMessageController(messageRepositoryMock.Object);
        }

        [Test]
        public void GetShouldReturnOkAndMessagesFromGivenRoom()
        {
            var message1 = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = 1001,
                Text = "Hey, can you take a blip?"
            };

            var message2 = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = 1001,
                Text = "Hey, don't know what a blip is :/"
            };
            IEnumerable<Message> listOfMessages = new List<Message>() { message1, message2 };
            messageRepositoryMock.Setup(mr => mr.GetAllMessagesFromRoomAsync(1001)).Returns(Task.FromResult(listOfMessages));

            var result = chatMessageController.Get(1001).Result;

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            Assert.That(((OkObjectResult)result).Value, Is.Not.Empty);
        }

        [Test]
        public void GetShouldReturnBadRequestForNonExistentRoom()
        {
            string exMessage = "Message";
            messageRepositoryMock.Setup(mr => mr.GetAllMessagesFromRoomAsync(1001)).Throws(new RoomNotFoundException(exMessage));

            var result = chatMessageController.Get(1001).Result;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo(exMessage));
        }

        [Test]
        public void PostShouldReturnOkResultForValidMessage()
        {
            var message = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = 1001,
                Text = "Hey, can you take a blip?"
            };
            messageRepositoryMock.Setup(mr => mr.AddMessageAsync(message));

            var result = chatMessageController.Post(message).Result;

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        [TestCase(typeof(ArgumentNullException))]
        [TestCase(typeof(InvalidMessageException))]
        public void PostShouldReturnBadRequestWhenExceptionIsCaught(Type exceptionType)
        {
            var message = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = 1001,
                Text = "Hey, can you take a blip?"
            };
            var exception = (Exception)Activator.CreateInstance(exceptionType, string.Empty);
            messageRepositoryMock.Setup(mr => mr.AddMessageAsync(message)).Throws(exception);

            var result = chatMessageController.Post(message).Result;

            Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
        }

    }
}
