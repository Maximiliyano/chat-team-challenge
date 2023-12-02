using AutoMapper;
using ChatTeamChallenge.Application.Requests.Chat.Commands.Create;
using ChatTeamChallenge.Application.Requests.Chat.Commands.Delete;
using ChatTeamChallenge.Application.Requests.Chat.Commands.Update;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Infrastructure.MappingProfiles;

public sealed class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<CreateChatCommand, Chat>();
        CreateMap<UpdateChatCommand, UpdateChatRequest>();
        CreateMap<DeleteChatCommand, Chat>();
        CreateMap<PagedList<Chat>, PagedList<ChatModel>>();
        CreateMap<Chat, ChatModel>();
    }
}