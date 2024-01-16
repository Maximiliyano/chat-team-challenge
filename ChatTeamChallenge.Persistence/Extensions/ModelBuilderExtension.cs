using Bogus;
using ChatTeamChallenge.Contracts.Common.Constants;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Apartments;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Extensions;

public static class ModelBuilderExtension
{
    private const int EntityCount = 10;

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var chats = GenerateRandomChats();
        var users = GenerateRandomUsers();
        var chatMembers = GenerateRandomChatMembers(users);
        var messages = GenerateRandomMessages(users.ToList());
        
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Message>().HasData(messages);
        modelBuilder.Entity<Chat>().HasData(chats);
        modelBuilder.Entity<ChatMember>().HasData(chatMembers);
    }

    private static IEnumerable<ChatMember> GenerateRandomChatMembers(
        IReadOnlyCollection<User> users)
    {
        var chatMemberId = 1;
        
        var testChatMembers = new Faker<ChatMember>()
            .CustomInstantiator(f => ChatMember.Create(
                f.PickRandom<User>(users).Id, 
                EntityConstants.GeneralChatId,
                f.PickRandom<ChatMemberRoles>(),
                chatMemberId++));

        var generatedChatMembers = testChatMembers.Generate(EntityCount);
        return generatedChatMembers;
    }


    private static IEnumerable<Chat> GenerateRandomChats()
    {
        var chatId = EntityConstants.GeneralChatId + 1;
        var roles = Enum.GetValues<CreativeRoles>().Except(new[] { CreativeRoles.None });
        var generatedChats = new List<Chat>
        {
            Chat.Create(
                EntityConstants.GeneralChatName,
                EntityConstants.GeneralChatPrivacy,
                EntityConstants.GeneralChatId)
        };

        generatedChats
            .AddRange(roles
                .Select(role => 
                    Chat.Create(Enum.GetName(role)!, false, chatId++)));

        return generatedChats;
    }
    
    private static IEnumerable<Message> GenerateRandomMessages(IList<User> users)
    {
        var messageId = 1;

        var testMessages = new Faker<Message>()
            .CustomInstantiator(f => Message.Create(
                f.PickRandom(users).Id,
                1,
                f.PickRandom(users).Username,
                f.Random.Words(10),
                f.Random.Bool(),
                receiveId: f.PickRandom(users).Id,
                id: messageId++
            ));

        var generatedMessages = testMessages.Generate(EntityCount);
        return generatedMessages;
    }

    private static IReadOnlyCollection<User> GenerateRandomUsers()
    {
        var userId = 1;

        var testUsersFake = new Faker<User>()
            .CustomInstantiator(f => User.Create(
                f.Internet.UserName(),
                f.Internet.Email(),
                BCrypt.Net.BCrypt.HashPassword(f.Internet.Password(16)),
                f.Address.City(),
                f.Random.Bool(),
                f.PickRandom<CreativeRoles>(),
                f.Random.Words(10),
                f.Person.Website,
                f.Person.Website,
                f.Person.Website,
                f.Person.Website,
                id: userId++
            ));

        var generatedUsers = testUsersFake.Generate(EntityCount);
        return generatedUsers;
    }
}