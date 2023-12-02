using AutoMapper;
using ChatTeamChallenge.Application.Requests.Users.Commands.Create;
using ChatTeamChallenge.Application.Requests.Users.Commands.Delete;
using ChatTeamChallenge.Application.Requests.Users.Commands.Update;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Infrastructure.MappingProfiles;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>();
        CreateMap<UserModel, User>();
        CreateMap<RegisterRequest, User>();

        CreateMap<User, CreateUserCommand>();
        CreateMap<CreateUserCommand, User>();

        CreateMap<User, UpdateUserCommand>();
        CreateMap<UserModel, UpdateUserCommand>();
        CreateMap<UpdateUserCommand, User>();
        
        CreateMap<RemoveUserCommand, User>();
        CreateMap<User, RemoveUserCommand>();

        CreateMap<PagedList<User>, PagedList<UserModel>>();
    }
}