using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.Users.Commands.Update;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<int>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateUserCommandHandler(
        IUserRepository messageRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _userRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<Result<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request); // TODO create user
        
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}