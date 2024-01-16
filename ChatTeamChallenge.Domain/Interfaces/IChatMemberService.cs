using ChatTeamChallenge.Contracts.ChatMember;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Enums;
using ChatTeamChallenge.Domain.Core.Primities.Result;
using ChatTeamChallenge.Domain.Models;

namespace ChatTeamChallenge.Domain.Interfaces;

public interface IChatMemberService
{
    Task<Result<int>> AddUserToChatAsync(ChatMemberRequest chatMemberRequest);
    Task<Result> AddToRequiredChatsAsync(int userId, CreativeRoles roles, bool globalChat = false);
    Task<Result<ChatMemberModel>> GetUserFromChatAsync(int userId, int chatId);
    Task<Result<PagedList<ChatMemberModel>>> GetAllAsync(int page, int pageSize, int? chatId, int? userId);
    Task<Result> RemoveUserToChatAsync(ChatMemberRequest chatMemberRequest);
    Task<Result> RemoveRangeAsync(IEnumerable<ChatMemberRequest> chatMemberRequests);
}