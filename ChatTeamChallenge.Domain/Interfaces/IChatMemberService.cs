using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IChatMemberService
{
    Task<Result<int>> AddUserToChatAsync(ChatMemberRequest chatMemberRequest);
    Task<Result<ChatMemberModel>> GetUserFromChatAsync(int userId, int chatId);
    Task<Result<PagedList<ChatMemberModel>>> GetAll(int page, int pageSize, int? chatId, int? userId);
    Task<Result> RemoveUserToChatAsync(ChatMemberRequest chatMemberRequest);
}