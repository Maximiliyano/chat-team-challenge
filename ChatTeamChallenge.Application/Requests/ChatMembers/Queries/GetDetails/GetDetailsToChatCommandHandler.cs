using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Messaging;
using ChatTeamChallenge.Domain.Core.Errors;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;

namespace ChatTeamChallenge.Application.Requests.ChatMembers.Queries.GetDetails;

public sealed class GetDetailsToChatCommandHandler : IQueryHandler<GetDetailsToChatCommand, Result<ChatMemberModel>>
{
    private readonly IChatMemberRepository _chatMemberRepository;
    private readonly IMapper _mapper;
    
    public GetDetailsToChatCommandHandler(IChatMemberRepository chatMemberRepository, IMapper mapper)
    {
        _chatMemberRepository = chatMemberRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<ChatMemberModel>> Handle(GetDetailsToChatCommand request, CancellationToken cancellationToken)
    {
        var result = await _chatMemberRepository.GetDetailsAsync(request.UserId, request.ChatId);

        if (result is null)
        {
            return Result.Failure<ChatMemberModel>(DomainErrors.ChatMember.NotFound);
        }

        var chatMemberModel = _mapper.Map<ChatMemberModel>(result);
        return Result.Success(chatMemberModel);
    }
}