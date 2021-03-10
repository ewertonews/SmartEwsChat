using EwsChat.Data.Models;
using System;
using System.Collections.Generic;

namespace EwsChat.Data
{
    public static class SeedData
    {
        public static IEnumerable<Message> Messages()
        {
            string welcomeText = "Let's have some nice talks, guys!";
            return new List<Message>()
            {
                new Message()
                {
                    MessageId = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1001,
                    ToUserName = "Everyone"
                },
                new Message()
                {
                    MessageId = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1002,
                    ToUserName = "Everyone"
                },
                new Message()
                {
                    MessageId = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1003,
                    ToUserName = "Everyone"
                }
            };
        }

        public static IEnumerable<ChatRoom> ChatRooms()
        {
            return new List<ChatRoom>()
            {
                new ChatRoom
                {
                    ChatRoomId = 1001,
                    Name = "Punk Rock",
                    Participants = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    ChatRoomId = 1002,
                    Name = "Heavy Metal",
                    Participants = new HashSet<ChatUser>()
                },
                new ChatRoom
                {
                    ChatRoomId = 1003,
                    Name = "Grunge",
                    Participants = new HashSet<ChatUser>()
                }
            };
        }
    }
}
