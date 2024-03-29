﻿using System;

namespace EwsChat.Data.Models
{
    public class Message
    {
        public string MessageId { get; set; }
        public int ChatRoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string ToUserName { get; set; }
    }
}
