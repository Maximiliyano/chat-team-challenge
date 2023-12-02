using ChatTeamChallenge.Contracts.Common;
using ChatTeamChallenge.Contracts.Message;
using ChatTeamChallenge.Domain.Apartments;

namespace ChatTeamChallenge.Domain.Reviews;

public interface IMessageRepository
{
    Task InsertAsync(Message message);
    Task<Message?> ReadByIdAsync(int id);
    Task RemoveAsync(Message message);
    Task<PagedList<Message>> ReadAllAsync(SearchMessageRequest? searchMessageDto, int page = 1, int pageSize = 10);
    Task UpdateAsync(Message message);
}