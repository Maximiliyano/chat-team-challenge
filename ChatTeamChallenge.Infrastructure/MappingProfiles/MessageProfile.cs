using AutoMapper;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Create;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Remove;
using ChatTeamChallenge.Application.Requests.Messages.Commands.Update;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Infrastructure.MappingProfiles;

public sealed class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageModel>();
        
        CreateMap<MessageModel, Message>();

        CreateMap<CreateMessageCommand, Message>();
        CreateMap<Message, CreateMessageCommand>();
        
        CreateMap<UpdateMessageCommand, Message>();
        CreateMap<MessageModel, UpdateMessageCommand>();
        
        CreateMap<Message, RemoveMessageCommand>();
        CreateMap<RemoveMessageCommand, Message>();

        CreateMap<PagedList<Message>, PagedList<MessageModel>>();
    }
}