using AutoMapper;
using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Chat;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Models;
using ChatTeamChallenge.Domain.Reviews;
using ChatTeamChallenge.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class ChatRepository : RepositoryBase<Chat>, IChatRepository
{
    private readonly IMapper _mapper;
    
    public ChatRepository(IDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task UpdateAsync(UpdateChatRequest updateChatRequest) =>
        await DbContext.Set<Chat>()
            .Where(c => c.Id == updateChatRequest.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Topic, updateChatRequest.Topic)
                .SetProperty(c => c.IsPublic, updateChatRequest.IsPublic)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

    public new async Task<Chat?> ReadByIdAsync(int id)
    {
        return await DbContext.Set<Chat>()
            .Include(c => c.Members)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Chat?> ReadByIdWithMessagesAsync(int id)
    {
        return await DbContext.Set<Chat>()
            .Include(c => c.Members)
            .Include(c => c.Messages)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Chat?> ReadByTopicAsync(string topic)
    {
        return await DbContext.Set<Chat>()
            .Include(c => c.Members)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Topic == topic);
    }

    public async Task<bool> IsTopicUniqueAsync(string topic) =>
        !await AnyAsync(new ChatWithTopicSpecification(topic));

    public Task<PagedList<ChatModel>> ReadByAllAsync(int page, int pageSize, DateTime? date, bool? isPrivate)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<ChatModel>> ReadByAllAsync(int page, int pageSize, DateTime? date, bool? isPublic, int? userId)
    {
        var queries = DbContext.Set<Chat>()
            .Include(c => c.Members)
            .AsNoTracking()
            .AsQueryable();

        if (userId.HasValue)
        {
            queries = queries.Where(c => c.Members!.Any(m => m.UserId == userId.Value));
        }
        
        if (date.HasValue)
        {
            queries = queries.Where(c => c.CreatedAt.Date == date.Value.Date);
        }

        if (isPublic.HasValue)
        {
            queries = queries.Where(c => c.IsPublic == isPublic.Value);
        }

        var pagedListChats = await PagedList<Chat>.CreateAsync(queries, page, pageSize);
        return _mapper.Map<PagedList<ChatModel>>(pagedListChats);
    }
}