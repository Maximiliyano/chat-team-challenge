using Bogus;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Extensions;

public static class ModelBuilderExtension
{
    private const int EntityCount = 10;
    
    public static void Configure(this ModelBuilder modelBuilder) // TODO rework to configurations
    {
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("Tokens");
            
            builder.Ignore(t => t.IsActive);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("Users");
        });
        
        modelBuilder.Entity<Message>(builder =>
        {
            builder.ToTable("Messages");

            builder
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Chat>(builder =>
        {
            builder.ToTable("Chats");
        });

        modelBuilder.Entity<ChatMember>(builder =>
        {
            builder.ToTable("ChatMembers");

            builder.HasKey(c => c.Id);
            
            builder
                .HasOne(c => c.Chat)
                .WithMany(c => c.Members)
                .HasForeignKey(c => c.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Members)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var chats = GenerateRandomChats();
        var users = GenerateRandomUsers();
        var chatMembers = GenerateRandomChatMembers(users, chats);
        var messages = GenerateRandomMessages(chats, users.ToList());
        
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Message>().HasData(messages);
        modelBuilder.Entity<Chat>().HasData(chats);
        modelBuilder.Entity<ChatMember>().HasData(chatMembers);
    }

    private static IEnumerable<ChatMember> GenerateRandomChatMembers(IReadOnlyCollection<User> users, IReadOnlyCollection<Chat> chats)
    {
        var chatMemberId = 1;
        
        var testChatMembers = new Faker<ChatMember>()
            .CustomInstantiator(f => ChatMember.Create(
                f.PickRandom<User>(users).Id, 
                f.PickRandom<Chat>(chats).Id,
                DateTime.UtcNow,
                chatMemberId++));

        var generatedChatMembers = testChatMembers.Generate(EntityCount);
        return generatedChatMembers;
    }


    private static IReadOnlyCollection<Chat> GenerateRandomChats()
    {
        var chatId = 1;

        var testChats = new Faker<Chat>() // TODO use create method
            .CustomInstantiator(f => new Chat
            {
                Id = chatId++,
                Topic = f.Random.Words(),
                IsPublic = f.Random.Bool(),
                CreatedAt = DateTime.UtcNow
            });
        
        var generatedChats = testChats.Generate(EntityCount);
        return generatedChats;
    }
    
    private static IEnumerable<Message> GenerateRandomMessages(IEnumerable<Chat> chats, IList<User> users)
    {
        var messageId = 1;

        var testMessages = new Faker<Message>() // TODO use create method
            .CustomInstantiator(f => new Message
            {
                Id = messageId++,
                SenderId = f.PickRandom(users).Id,
                SenderUserName = f.PickRandom(users).Username,
                ReceiverId = f.PickRandom(users).Id,
                ChatId = f.PickRandom(chats).Id,
                IsRead = f.Random.Bool(),
                Body = f.Random.Words(10),
                CreatedAt = DateTime.UtcNow
            });

        var generatedMessages = testMessages.Generate(EntityCount);
        return generatedMessages;
    }

    private static IReadOnlyCollection<User> GenerateRandomUsers()
    {
        var userId = 1;

        var testUsersFake = new Faker<User>() // TODO use create method
            .CustomInstantiator(f => new User
            {
                Id = userId++,
                City = f.Address.City(),
                Email = f.Internet.Email(),
                Username = f.Internet.UserName(),
                Password = BCrypt.Net.BCrypt.HashPassword(f.Internet.Password(16)),
                CreatedAt = DateTime.UtcNow,
                Roles = f.PickRandom<CreativeRoles>(),
                IsRemote = f.Random.Bool(),
                Description = f.Random.Words(10),
                InstagramLink = f.Person.Website,
                DiscordLink = f.Person.Website,
                TelegramLink = f.Person.Website,
                SpotifyLink = f.Person.Website
            });

        var generatedUsers = testUsersFake.Generate(EntityCount);
        return generatedUsers;
    }
}