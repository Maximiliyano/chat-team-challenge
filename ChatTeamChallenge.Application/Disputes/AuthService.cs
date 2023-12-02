using ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Create;
using ChatTeamChallenge.Application.Requests.RefreshTokens.Commands.Remove;
using ChatTeamChallenge.Contracts.Authentication;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Contracts.Token;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Helpers;
using ChatTeamChallenge.Domain.Interfaces;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Responses;
using ChatTeamChallenge.Domain.Reviews;
using MediatR;

namespace ChatTeamChallenge.Application.Disputes;

public sealed class AuthService : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IJwtFactory _jwtFactory;
    private readonly IMediator _mediator;

    public AuthService(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IUserService userService,
        IJwtFactory jwtFactory,
        IMediator mediator)
    {
        _mediator = mediator;
        _userService = userService;
        _jwtFactory = jwtFactory;
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
    }
    
    public async Task<Result<AuthUserResponse>> Authorize(LoginRequest credentials)
    {
        var userEntity = await _userRepository.ReadByEmailAsync(credentials.Email);

        if (userEntity is null)
            return Result.Failure<AuthUserResponse>(DomainErrors.User.NotFound(credentials.Email));

        if (!BCrypt.Net.BCrypt.Verify(credentials.Password, userEntity.Password))
            return Result.Failure<AuthUserResponse>(DomainErrors.Authentication.InvalidEmailOrPassword);

        var token = (await GenerateAccessToken(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.Roles)).Value;
        var user = UserModel.Create(
            userEntity.Id, userEntity.Username, userEntity.Email, userEntity.City,
            userEntity.IsRemote, userEntity.Roles, userEntity.CreatedAt, userEntity.UpdatedAt,
            userEntity.Description,
            userEntity.InstagramLink,
            userEntity.DiscordLink,
            userEntity.TelegramLink,
            userEntity.SpotifyLink);

        return Result.Success(GlobalBuilderHelper.BuildAuthUserDto(user, token));
    }

    public async Task<Result<AuthUserResponse>> Register(RegisterRequest registerUser)
    {
        var resultUserModel = await _userRepository.ReadByEmailAsync(registerUser.Email);

        if (resultUserModel is not null)
            return Result.Failure<AuthUserResponse>(DomainErrors.Authentication.IsEmailNotUnique);
        
        var createdUserResult = await _userService.CreateAsync(registerUser);
        
        if (createdUserResult.IsFailure)
            return Result.Failure<AuthUserResponse>(createdUserResult.Error);

        var createdUser = createdUserResult.Value;
        
        // Generation token
        var tokenResult = await GenerateAccessToken(createdUser.Id, createdUser.Username, createdUser.Email, createdUser.Roles);
        
        return tokenResult.IsFailure ? 
            Result.Failure<AuthUserResponse>(tokenResult.Error) : 
            Result.Success(GlobalBuilderHelper.BuildAuthUserDto(createdUser, tokenResult.Value));
    }

    public async Task<Result<AccessTokenResponse>> GenerateAccessToken(int userId, string userName, string email, CreativeRoles roles)
    {
        var token = _jwtFactory.GenerateRefreshToken();

        var createCommand = new CreateRefreshTokenCommand(token, userId);
        await _mediator.Send(createCommand);

        var accessToken = await _jwtFactory.GenerateAccessToken(userId, userName, email, roles);
        return Result.Success(GlobalBuilderHelper.BuildAccessTokenDto(accessToken, token));
    }

    public async Task<Result<AccessTokenResponse>> RefreshToken(RefreshTokenRequest request)
    {
        var userIdResult = _jwtFactory.GetUserIdFromToken(request.AccessToken);
        
        if (userIdResult.IsFailure)
            return Result.Failure<AccessTokenResponse>(DomainErrors.AccessToken.InvalidToken);
        
        var userId = userIdResult.Value;
        
        var userEntity = await _userRepository.ReadByIdAsync(userId);
        
        if (userEntity is null)
            return Result.Failure<AccessTokenResponse>(DomainErrors.User.NotFound(userId));

        var rToken = await _refreshTokenRepository.ReadByUserTokenAsync(request.RefreshToken, userId);

        if (rToken is null)
            return Result.Failure<AccessTokenResponse>(DomainErrors.RefreshToken.InvalidToken);

        if (!rToken.IsActive)
            return Result.Failure<AccessTokenResponse>(DomainErrors.RefreshToken.Expired);

        var jwtToken = await _jwtFactory.GenerateAccessToken(userEntity.Id, userEntity.Username, userEntity.Email, userEntity.Roles);
        var refreshToken = _jwtFactory.GenerateRefreshToken();
        
        var removeCommand = new RemoveRefreshTokenCommand(rToken);
        await _mediator.Send(removeCommand);
        
        var insertCommand = new CreateRefreshTokenCommand(refreshToken, userEntity.Id);
        await _mediator.Send(insertCommand);

        return Result.Success(GlobalBuilderHelper.BuildAccessTokenDto(jwtToken, refreshToken));
    }

    public async Task<Result> RevokeRefreshToken(string refreshToken, int userId)
    {
        var rToken = await _refreshTokenRepository.ReadByUserTokenAsync(refreshToken, userId);

        if (rToken is null)
            return Result.Failure(DomainErrors.RefreshToken.InvalidToken);
        
        var removeCommand = new RemoveRefreshTokenCommand(rToken);
        var result = await _mediator.Send(removeCommand);
        
        return Result.Success(result);
    }
}