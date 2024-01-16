using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class ChatMemberRepository : RepositoryBase<ChatMember>, IChatMemberRepository
{
    public ChatMemberRepository(IDbContext context) : base(context)
    {
    }

    public async Task<ChatMember?> GetDetailsAsync(int userId, int chatId)
    {
        return await DbContext.Set<ChatMember>()
            .Include(c => c.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ChatId == chatId);
    }
    
    public async Task<PagedList<ChatMember>> GetAllAsync(int page, int pageSize, int? userId, int? chatId)
    {
        var queries = DbContext.Set<ChatMember>()
            .Include(c => c.User)
            .AsNoTracking()
            .AsQueryable();
        
        if (userId.HasValue)
        {
            queries = queries.Where(c => c.UserId == userId);
        }

        if (chatId.HasValue)
        {
            queries = queries.Where(c => c.ChatId == chatId);
        }

        var pagedListChats = await PagedList<ChatMember>.CreateAsync(queries, page, pageSize);
        return pagedListChats;
    }

    public async Task RemoveRange(IEnumerable<ChatMember> chatMembers)
    {
        foreach (var chatMember in chatMembers)
        {
            await RemoveAsync(chatMember);
        }
    }
}