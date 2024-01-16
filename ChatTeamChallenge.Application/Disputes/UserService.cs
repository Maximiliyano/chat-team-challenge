using AutoMapper;
using ChatTeamChallenge.Application.Requests.Users.Commands.ChangeRole;
using ChatTeamChallenge.Application.Requests.Users.Commands.Create;
using ChatTeamChallenge.Application.Requests.Users.Commands.Delete;
using ChatTeamChallenge.Application.Requests.Users.Commands.Update;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Contracts.User;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;
using MediatR;

namespace ChatTeamChallenge.Application.Disputes;

public sealed class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        IMediator mediator,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _mediator = mediator;
        _userRepository = userRepository;
    }

    public async Task<Result<UserModel>> CreateAsync(RegisterRequest registerRequest)
    {
        var createUserCommand = new CreateUserCommand(registerRequest);
        var result = await _mediator.Send(createUserCommand);

        if (result.IsFailure)
            return Result.Failure<UserModel>(result.Error);
            
        var createdUserResponse = await ReadByEmailAsync(registerRequest.Email);
        return createdUserResponse.IsFailure ? 
            Result.Failure<UserModel>(createdUserResponse.Error) : 
            Result.Success(createdUserResponse.Value);
    }

    public async Task<Result<PagedList<UserModel>>> ReadAllAsync(int page, int pageSize)
    {
        if (page < 1 || pageSize < 1)
            return Result.Failure<PagedList<UserModel>>(DomainErrors.Message.ZeroPageCount);
        
        var userRecords = await _userRepository.ReadAllAsync(page, pageSize);
        var results = _mapper.Map<PagedList<UserModel>>(userRecords);
        return Result.Success(results);
    }

    public async Task<Result<UserModel>> ReadByIdAsync(int userId)
    {
        var entity = await _userRepository.ReadByIdAsync(userId);
        return entity is null
            ? Result.Failure<UserModel>(DomainErrors.User.NotFound(userId)) 
            : Result.Success(_mapper.Map<UserModel>(entity));
    }

    public async Task<Result<UserModel>> ReadByEmailAsync(string email)
    { 
        var entity = await _userRepository.ReadByEmailAsync(email);
        return entity is null 
            ? Result.Failure<UserModel>(DomainErrors.User.NotFound(email)) 
            : Result.Success(_mapper.Map<UserModel>(entity));
    }

    public async Task<bool> IsUserExistAsync(int userId)
    {
        return await _userRepository.IsUserExistAsync(userId);
    }

    public async Task<Result> ChangePasswordAsync(int userId, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
        {
            return Result.Failure(DomainErrors.Message.EmptyPassword);
        }
        
        var userRecord = await _userRepository.ReadByIdAsync(userId);

        if (userRecord is null)
            return Result.Failure(DomainErrors.User.NotFound(userId));
        
        userRecord.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

        var updateCommand = _mapper.Map<UpdateUserCommand>(userRecord);
        
        var result = await _mediator.Send(updateCommand);
        return result;
    }

    public async Task<Result> ChangeRoleAsync(int userId, CreativeRoles roles)
    {
        var changeRoleUserCommand = new ChangeRoleUserCommand(userId, roles);
        var result = await _mediator.Send(changeRoleUserCommand);
        
        var updateCommand = _mapper.Map<UpdateUserCommand>(result.Value); // TODO new update yser command
        return await _mediator.Send(updateCommand);
    }

    public async Task<Result> UpdateAsync(UpdateUserRequest updatedUserDto)
    {
        var userResult = await ReadByIdAsync(updatedUserDto.Id);

        if (userResult.IsFailure)
            return Result.Failure<UserModel>(userResult.Error);
        
        var user = userResult.Value;
        
        if(user.Username == updatedUserDto.Username)
            return Result.Failure<int>(DomainErrors.User.IsUsernameNotUnique);
        
        if (user.Roles != updatedUserDto.Roles)
        {
            await ChangeRoleAsync(user.Id, updatedUserDto.Roles);
        }
        
        user.IsRemote = updatedUserDto.IsRemote;
        user.Username = updatedUserDto.Username;
        user.City = updatedUserDto.City;
        user.Roles = updatedUserDto.Roles;
        user.Description = updatedUserDto.Description?.Trim();
        user.InstagramLink = updatedUserDto.InstagramLink;
        user.DiscordLink = updatedUserDto.DiscordLink;
        user.TelegramLink = updatedUserDto.TelegramLink;
        user.SpotifyLink = updatedUserDto.SpotifyLink;
        
        var updateCommand = _mapper.Map<UpdateUserCommand>(user);
        var result = await _mediator.Send(updateCommand);
        return result;
    }

    public async Task<Result> DeleteAsync(int userId)
    {
        var user = await _userRepository.ReadByIdAsync(userId);

        if (user is null)
            return Result.Failure(DomainErrors.User.NotFound(userId));
        
        var deleteCommand = new RemoveUserCommand(user);
        var result = await _mediator.Send(deleteCommand);
        return result;
    }
}