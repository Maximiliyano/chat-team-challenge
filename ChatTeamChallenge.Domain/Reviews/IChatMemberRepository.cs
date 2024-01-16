using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IChatMemberRepository
{
    Task InsertAsync(ChatMember chatMember);
    Task<ChatMember?> GetDetailsAsync(int userId, int chatId);
    Task<PagedList<ChatMember>> GetAllAsync(int page = 1, int pageSize = 10, int? userId = null, int? chatId = null);
    Task RemoveAsync(ChatMember chatMember);
    Task RemoveRange(IEnumerable<ChatMember> chatMembers);
}