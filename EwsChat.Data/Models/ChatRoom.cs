using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EwsChat.Data.Models
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }

        [Required]
        [StringLength(20)] 
        public string Name { get; set; }
        public HashSet<ChatUser> Participants { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
