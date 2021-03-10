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
        private Mock<IRepositoryFactory> repositoryFactoryMock;
        private ChatMessageController chatMessageController;

        [SetUp]
        public void Setup()
        {
            repositoryFactoryMock = new Mock<IRepositoryFactory>();
            chatMessageController = new ChatMessageController(repositoryFactoryMock.Object);
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
            repositoryFactoryMock.Setup(mr => mr.Message.GetAllMessagesFromRoomAsync(1001)).Returns(Task.FromResult(listOfMessages));

            var result = chatMessageController.Get(1001).Result;

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
            repositoryFactoryMock.Setup(mr => mr.Message.GetLatestMessagesFromRoomAsync(1001, lastUpdate)).Returns(Task.FromResult(listOfMessages));

            var result = chatMessageController.Get(1001, lastUpdate.ToString()).Result;

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
            repositoryFactoryMock.Setup(mr => mr.Message.AddMessage(message));

            var result = chatMessageController.Post(message).Result;

            Assert.That(result, Is.TypeOf<CreatedAtRouteResult>());
        }        

    }
}
