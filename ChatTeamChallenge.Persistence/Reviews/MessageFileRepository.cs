using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class MessageFileRepository : RepositoryBase<MessageFile>, IMessageFileRepository
{
    public MessageFileRepository(IDbContext context) : base(context)
    {
    }

    public async Task<PagedList<MessageFile>> ReadAllAsync(int page, int pageSize)
    {
        var query = DbContext.Set<MessageFile>()
            .AsNoTracking()
            .AsQueryable();
        
        return await PagedList<MessageFile>.CreateAsync(query, page, pageSize);
    }
}