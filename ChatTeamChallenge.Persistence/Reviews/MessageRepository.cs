using ChatTeamChallenge.Application.Core.Abstractions.Data;
using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Apartments;
using ChatTeamChallenge.Domain.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ChatTeamChallenge.Persistence.Reviews;

public sealed class MessageRepository : RepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(IDbContext context) : base(context)
    {
    }

    public async Task<PagedList<Message>> ReadAllAsync(SearchMessageRequest? searchMessageDto, int page, int pageSize)
    {
        var messageResponsesQuery = DbContext.Set<Message>()
            .AsNoTracking()
            .AsQueryable();

        if (searchMessageDto is not null)
        {
            if (searchMessageDto.SenderId.HasValue)
            {
                messageResponsesQuery = messageResponsesQuery.Where(m => m.SenderId == searchMessageDto.SenderId);
            }

            if (searchMessageDto.ReceiverId.HasValue)
            {
                messageResponsesQuery = messageResponsesQuery.Where(m => m.ReceiverId == searchMessageDto.ReceiverId);
            }
            
            if (searchMessageDto.ChatId.HasValue)
            {
                messageResponsesQuery = messageResponsesQuery.Where(m => m.ChatId == searchMessageDto.ChatId);
            }

            if (searchMessageDto.IsRead.HasValue)
            {
                messageResponsesQuery = messageResponsesQuery.Where(m => m.IsRead == searchMessageDto.IsRead);
            }
            
            if (searchMessageDto.Date.HasValue)
            {
                messageResponsesQuery = messageResponsesQuery.Where(m => m.CreatedAt.Date == searchMessageDto.Date.Value.Date);
            }
        }
        
        var messages = await PagedList<Message>.CreateAsync(messageResponsesQuery, page, pageSize);
        return messages;
    }

    public async Task UpdateAsync(Message message) =>
        await DbContext.Set<Message>()
            .Where(m => m.Id == message.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.UpdatedAt, message.UpdatedAt)
                .SetProperty(m => m.Body, message.Body));
}