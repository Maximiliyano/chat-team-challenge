using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Core.Events.Chat;

public sealed class ChatMemberCreatedDomainEvent : IDomainEvent
{
    internal ChatMemberCreatedDomainEvent(ChatMember chatMember) => ChatMember = chatMember;

    public ChatMember ChatMember { get; }
}