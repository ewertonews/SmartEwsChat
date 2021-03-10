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

            modelBuilder.Entity<ChatRoom>().HasData(SeedData.ChatRooms());
            modelBuilder.Entity<Message>().HasData(SeedData.Messages());
        }
    }
}
