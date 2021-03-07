using System;
using System.Collections.Generic;
using System.Text;

namespace EwsChat.Data.Models
{
    public class ChatUser
    {
        public string ChatUserId { get; set; }
        public string NickName { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public int ChatRoomId { get; set; }
    }
}
