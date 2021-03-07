using EwsChat.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwsChat.Data
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options) : base(options)
        { }

        public DbSet<ChatUser> Users { get; set; }
        public DbSet<ChatRoom> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatUser>()
              .HasIndex(u => u.NickName)
              .IsUnique();

            modelBuilder.Entity<ChatRoom>().HasData(
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
                });

            string welcomeText = "Let's have some nice talks, guys!";
            modelBuilder.Entity<Message>().HasData(
                new Message()
                {
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1001,
                    ToUserName = "Everyone"
                },
                new Message()
                {
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1002,
                    ToUserName = "Everyone"
                },
                new Message()
                {
                    CreatedAt = DateTime.Now,
                    Text = welcomeText,
                    ChatRoomId = 1003,
                    ToUserName = "Everyone"
                });
        }
    }
}
