using AutoMapper;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Infrastructure.MappingProfiles;

public sealed class ChatMemberProfile : Profile
{
    public ChatMemberProfile()
    {
        CreateMap<PagedList<ChatMember>, PagedList<ChatMemberModel>>();
        CreateMap<ChatMember, ChatMemberModel>();
    }
}