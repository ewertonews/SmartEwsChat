using EwsChat.Data.Exceptions;
using EwsChat.Data.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace EwsChat.Data.Tests
{
    public class MessageRepositoryUnitTests
    {
        private IMessageRepository _messageRepository;
        private IChatRoomRepository _chatRoomRepository;

        [SetUp]
        public void Setup()
        {
            //would be a mock if using actual DB
            _chatRoomRepository = new ChatRoomRepository();
            _messageRepository = new MessageRepository(_chatRoomRepository);
        }

        [Test]
        public void AddMessageShouldAddMessageSuccessfully()
        {
            var message = new Message
            {
                TargetRoomId = 1001,
                MessageId = Guid.NewGuid().ToString(),
                Text = "Hello",
                CreatedAt = DateTime.UtcNow
            };

            _messageRepository.AddMessageAsync(message);

            var messagesFromRoom = _messageRepository.GetAllMessagesFromRoomAsync(1001).Result;

            Assert.That(messagesFromRoom.Contains(message));
        }

        [Test]
        public void AddMessageWithoutIdShouldCreateIdAndAddMessageSuccessfully()
        {
            var message = new Message
            {
                TargetRoomId = 1001,
                Text = "Hello",
                CreatedAt = DateTime.UtcNow
            };

            _messageRepository.AddMessageAsync(message);

            var messageFromRoom = _messageRepository.GetAllMessagesFromRoomAsync(1001).Result;

            Assert.That(messageFromRoom, Is.Not.Null);
            Assert.That(messageFromRoom.FirstOrDefault().MessageId, Is.Not.Null);
        }

        [Test]
        public void AddMessageShouldThrowNullArgumentExceptoin()
        {
            Message message = null;
            Assert.That(() => _messageRepository.AddMessageAsync(message), Throws.ArgumentNullException);
        }


        [Test]
        public void AddMessageShouldThrowInvalidMessageExceptoin()
        {
            int roomId = 1002;
            string emptyMessage = string.Empty;
            var message = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = roomId,
                Text = emptyMessage
            };

            Assert.That(() => _messageRepository.AddMessageAsync(message), Throws.TypeOf<InvalidMessageException>());
        }
        [Test]
        public void AddMessageShouldThrowRoomNotFoundException()
        {
            int idOfNonExistentRoom = 1004;
            var message = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = idOfNonExistentRoom,
                Text = "'sup guys!"
            };

            Assert.That(() => _messageRepository.AddMessageAsync(message), Throws.TypeOf<RoomNotFoundException>());
        }

        [Test]
        public void GetAllMessagesShouldReturnAllMessagesInRepository()
        {
            AddMessagesToRoom(1001);

            var allMessages = _messageRepository.GetAllMessagesAsync().Result;

            Assert.That(allMessages.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetAllMessagesFromRoomShouldReturnOnlyMessagesWithGivenRoomId()
        {
            var punkRockRoomId = 1001;
            AddMessagesToRoom(punkRockRoomId);

            var heavyMetalRoomId = 1002;
            AddMessagesToRoom(heavyMetalRoomId);

            var messagesFromHeavyMetalRoom = _messageRepository.GetAllMessagesFromRoomAsync(heavyMetalRoomId).Result;

            Assert.That(messagesFromHeavyMetalRoom.Any(m => m.TargetRoomId != heavyMetalRoomId), Is.False);
        }       

        [Test]
        public void GetAllMessagesFromRoomShouldThrowRoomNotFoundExceptionForNonExistentRoom()
        {
            var unknownRoomId = 8888;

            Assert.That(() => _messageRepository.GetAllMessagesFromRoomAsync(unknownRoomId), Throws.TypeOf<RoomNotFoundException>());
        }

        [Test]
        public void GetLatestMessagesFromRoomShouldReturMessagesFromGivenDatetime()
        {
            var lastUpdate = DateTime.UtcNow.AddMinutes(1);
            var roomId = 1002;

            AddMessagesToRoom(roomId);

            var messagesFromDate = _messageRepository.GetLatestMessagesFromRoomAsync(roomId, lastUpdate).Result;

            Assert.That(messagesFromDate.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetLatestMessagesFromRoomShouldReturnEmptyIEnumerable()
        {
            var lastUpdate = DateTime.UtcNow.AddMinutes(3);
            var roomId = 1002;

            AddMessagesToRoom(roomId);

            var messagesFromDate = _messageRepository.GetLatestMessagesFromRoomAsync(roomId, lastUpdate).Result;

            Assert.That(messagesFromDate, Is.Empty);
        }

        private void AddMessagesToRoom(int roomId)
        {
            var message1 = new Message()
            {
                CreatedAt = DateTime.UtcNow,
                TargetRoomId = roomId,
                Text = "'sup guys!"
            };
            var message2 = new Message()
            {
                CreatedAt = DateTime.UtcNow.AddMinutes(1),
                TargetRoomId = roomId,
                Text = "Heeyy!"
            };
            var message3 = new Message()
            {
                CreatedAt = DateTime.UtcNow.AddMinutes(2),
                TargetRoomId = roomId,
                Text = "What are you guys up to?"
            };

            _messageRepository.AddMessageAsync(message1).Wait();
            _messageRepository.AddMessageAsync(message2).Wait();
            _messageRepository.AddMessageAsync(message3).Wait();
        }
    }
}
