using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Create;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserEmailUnique = await _userRepository.IsEmailUniqueAsync(request.Email);

        if (!isUserEmailUnique)
            return Result.Failure<int>(DomainErrors.User.IsEmailNotUnique);

        var isUsernameUnique = await _userRepository.IsUsernameUniqueAsync(request.Username);

        if (!isUsernameUnique)
            return Result.Failure<int>(DomainErrors.User.IsUsernameNotUnique);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = User.Create(
            request.Username, 
            request.Email, 
            hashedPassword, 
            request.City, 
            request.IsRemote, 
            request.Roles,
            request.Description,
            request.InstagramLink,
            request.DiscordLink,
            request.TelegramLink,
            request.SpotifyLink);
        
        await _userRepository.InsertAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}